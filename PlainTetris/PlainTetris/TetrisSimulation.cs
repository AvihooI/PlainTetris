using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using Timer = System.Threading.Timer;

namespace PlainTetris
{
    internal class TetrisSimulation
    {
        //Configurations (for rotation)
        private List<uint[,]> _optionalConfigurations;
        private int _configurationIndex;

        //Controlled piece
        private uint[,] _controlledBlocks;
        private int _cLeft, _cTop;

        //Landed blocks
        private uint[,] _landedBlocks;

        //Timer
        private Timer _simulationTimer;
        private bool _pieceActive = false;
        private bool _simulationRuns;

        public bool GameOver { get; private set; }

        //Something changed since render
        public bool HasStateChangedSinceRender { get; private set; } = true;

        //Simulation

        private static Color Dim(Color color)
        {
            return Color.FromArgb(color.A, color.R / 2, color.G / 2, color.B / 2);
        }

        public TetrisRenderBlock[,] GetRenderBlocks()
        {
            HasStateChangedSinceRender = false;

            var result = new TetrisRenderBlock[12, 16];

            for (var i = 0; i < 12; i++)
            {
                for (var j = 0; j < 16; j++)
                {
                    if (_landedBlocks[i, j] != 0)
                    {
                        result[i, j].Color = Dim(NumberToColor(_landedBlocks[i,j]));
                        result[i, j].Active = true;
                    }
                    else
                    {
                        result[i, j].Active = false;
                    }
                }
            }

            if (!_pieceActive) return result;

            {
                for (var i = 0; i < 4; i++)
                {
                    for (var j = 0; j < 4; j++)
                    {
                        if (_controlledBlocks[i, j] == 0) continue;

                        result[i + _cLeft, j + _cTop].Color = NumberToColor(_controlledBlocks[i, j]);
                        result[i + _cLeft, j + _cTop].Active = true;
                    }
                }
            }

            return result;
        }

        private Color NumberToColor(uint landedBlock)
        {
            switch (landedBlock)
            {
                case 0:
                    return Color.Black;
                case 1:
                    return Color.Blue;
                case 2:
                    return Color.Green;
                case 3:
                    return Color.Red;
                case 4:
                    return Color.Yellow;
                case 5:
                    return Color.Purple;
                case 6:
                    return Color.Orange;
                case 7:
                    return Color.LightSlateGray;

                default:
                    throw  new ArgumentOutOfRangeException();
            }
        }

        public TetrisSimulation()
        {
            _configurationIndex = 0;
            _optionalConfigurations = new List<uint[,]>();
            _controlledBlocks = new uint[4, 4];
            _landedBlocks = new uint[12, 16];
            _pieceActive = false;
        }

        private CheckResult CheckConfiguration(uint[,] configuration, int cLeft, int cTop)
        {
            var result = CheckResult.Available;

            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    if (configuration[i, j] == 0) continue;

                    var x = i + cLeft;
                    var y = j + cTop;

                    if (x < 0)
                        result = CheckResult.OutOfBoundsLeft;
                    else if (x >= 12)
                        result = CheckResult.OutOfBoundsRight;
                    else if (y < 0 || y >= 16 || _landedBlocks[x, y] != 0)
                    {
                        return CheckResult.Blocked;
                    }
                }
            }

            return result;
        }

        private void LandPiece()
        {
            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    if (_controlledBlocks[i, j] != 0)
                    {
                        _landedBlocks[i + _cLeft, j + _cTop] = _controlledBlocks[i, j];
                    }
                }
            }

            _pieceActive = false;

            Reduce();
        }

        private void CreateNewPiece()
        {
            if (GameOver)
                return;

            int cTop;
            int cLeft;
            var createdPiece = PieceCreator.CreateRandomPiece(out cTop, out cLeft);

            _optionalConfigurations = createdPiece;
            _configurationIndex = 0;
            _cTop = cTop;
            _cLeft = cLeft;

            _controlledBlocks = _optionalConfigurations[0];

            switch (CheckConfiguration(_controlledBlocks, _cLeft, _cTop))
            {
                case CheckResult.Available:
                    break;
                case CheckResult.Blocked:
                    GameOver = true;
                    return;
                case CheckResult.OutOfBoundsLeft:
                    break;
                case CheckResult.OutOfBoundsRight:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _pieceActive = true;
            HasStateChangedSinceRender = true;
        }

        private void HandleFalling()
        {
            for (var y = 14; y >= 0; y--)
            {
                var lineIsClear = true;

                for (var x = 0; x < 12; x++)
                {
                    if (_landedBlocks[x, y] == 0) continue;

                    lineIsClear = false;
                    break;
                }

                if (!lineIsClear) continue;

                FallLine(y);
                break;
            }

        }

        private void Reduce()
        {
            for (var y = 14; y >= 0; y--)
            {
                var lineIsFull = true;

                for (var x = 0; x < 12; x++)
                {
                    if (_landedBlocks[x, y] != 0) continue;

                    lineIsFull = false;
                    break;
                }

                if (!lineIsFull) continue;

                ReduceLine(y);
                Reduce();
                break;
            }

            HasStateChangedSinceRender = true;
            HandleFalling();
        }
        private void FallLine(int lineY)
        {
            for (var x = 0; x < 12; x++)
            {
                for (var y = lineY - 1; y >= 0; y--)
                {
                    _landedBlocks[x, y + 1] = _landedBlocks[x, y];
                    _landedBlocks[x, y] = 0;
                }
            }

            HasStateChangedSinceRender = true;
        }
        private void ReduceLine(int lineY)
        {
            for (var x = 0; x < 12; x++)
            {
                _landedBlocks[x, lineY] = 0;
            }
        }

        public void SpawnTimer()
        {
            _simulationTimer = new Timer(SimulationCallback, null, 0, 500); ;
        }

        private void SimulationCallback(object state)
        {
            if (_simulationRuns)
            {
                PerformOneStep();
            }
        }

        public void SimulationInput(TetrisInput tetrisInput)
        {
            switch (tetrisInput)
            {
                case TetrisInput.Up:
                    Rotate();
                    HasStateChangedSinceRender = true;
                    break;
                case TetrisInput.Left:
                    Left();
                    break;
                case TetrisInput.Right:
                    Right();
                    break;
                case TetrisInput.Down:
                    DownPressed();
                    break;
                case TetrisInput.Space:
                    Space();
                    break;
                case TetrisInput.Enter:
                    Enter();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(tetrisInput), tetrisInput, null);
            }
        }

        //SpawnTimer/Pause
        private void Enter()
        {
            if (GameOver)
            {
                _configurationIndex = 0;
                _optionalConfigurations = new List<uint[,]>();
                _controlledBlocks = new uint[4, 4];
                _landedBlocks = new uint[12, 16];
                _pieceActive = false;
                GameOver = false;
            }

            _simulationRuns = (_simulationRuns != true);   

            if (_simulationRuns)
                SpawnTimer();

        }

        //Selected tetris piece goes down
        private void Space()
        {
            if (!_simulationRuns) return;

            while (PerformOneStep() == StepResult.StepMade);
        }

        //Timer
        private void DownPressed()
        {
            if (!_simulationRuns) return;

            var stepResult = PerformOneStep();

            switch (stepResult)
            {
                case StepResult.StepMade:
                    break;
                case StepResult.Landed:
                    _pieceActive = false;
                    break;
                case StepResult.GameOver:
                    LandPiece();
                    _simulationRuns = false;
                    GameOver = true;
                    break;
                case StepResult.PieceCreated:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Right()
        {
            if (!_simulationRuns) return;

            var checkResult = CheckConfiguration(_controlledBlocks, _cLeft + 1, _cTop);

            switch (checkResult)
            {
                case CheckResult.Available:
                    _cLeft++;
                    HasStateChangedSinceRender = true;
                    break;
                case CheckResult.Blocked:
                    break;
                case CheckResult.OutOfBoundsLeft:
                    break;
                case CheckResult.OutOfBoundsRight:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Left()
        {
            if (!_simulationRuns) return;

            var checkResult = CheckConfiguration(_controlledBlocks, _cLeft - 1, _cTop);

            switch (checkResult)
            {
                case CheckResult.Available:
                    _cLeft--;
                    HasStateChangedSinceRender = true;
                    break;
                case CheckResult.Blocked:
                    break;
                case CheckResult.OutOfBoundsLeft:
                    break;
                case CheckResult.OutOfBoundsRight:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Rotate()
        {
            if (_optionalConfigurations.Count < 2)
                return;

            var nextConfigration = _optionalConfigurations[(_configurationIndex + 1)%_optionalConfigurations.Count];

            var checkResult = CheckConfiguration(nextConfigration, _cLeft, _cTop);

            switch (checkResult)
            {
                case CheckResult.Available:
                    _configurationIndex = (_configurationIndex + 1)%_optionalConfigurations.Count;
                    break;
                case CheckResult.Blocked:
                    break;
                case CheckResult.OutOfBoundsLeft:
                    AttemptMoveRight(1);
                    break;
                case CheckResult.OutOfBoundsRight:
                    AttemptMoveLeft(1);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _controlledBlocks = _optionalConfigurations[_configurationIndex];
        }

        private void AttemptMoveLeft(int amount)
        {
            var nextConfigration = _optionalConfigurations[(_configurationIndex + 1) % _optionalConfigurations.Count];

            var checkResult = CheckConfiguration(nextConfigration, _cLeft - amount, _cTop);

            switch (checkResult)
            {
                case CheckResult.Available:
                    _configurationIndex = (_configurationIndex + 1) % _optionalConfigurations.Count;
                    _cLeft -= amount;
                    break;
                case CheckResult.Blocked:
                    break;
                case CheckResult.OutOfBoundsLeft:
                    AttemptMoveLeft(amount + 1);
                    break;
                case CheckResult.OutOfBoundsRight:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _controlledBlocks = _optionalConfigurations[_configurationIndex];
        }

        private void AttemptMoveRight(int amount)
        {
            var nextConfigration = _optionalConfigurations[(_configurationIndex + 1) % _optionalConfigurations.Count];

            var checkResult = CheckConfiguration(nextConfigration, _cLeft + amount, _cTop);

            switch (checkResult)
            {
                case CheckResult.Available:
                    _configurationIndex = (_configurationIndex + 1) % _optionalConfigurations.Count;
                    _cLeft += amount;
                    break;
                case CheckResult.Blocked:
                    break;
                case CheckResult.OutOfBoundsLeft:
                    AttemptMoveRight(amount + 1);
                    break;
                case CheckResult.OutOfBoundsRight:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _controlledBlocks = _optionalConfigurations[_configurationIndex];
        }

        private StepResult PerformOneStep()
        {
            var result = StepResult.StepMade;

            if (_pieceActive == false)
            {
                CreateNewPiece();
                return StepResult.PieceCreated;
            }

            for (var j = 3; j >= 0; j--)
            {
                for (var i = 0; i < 4; i++)
                {
                    if (_controlledBlocks[i, j] == 0) continue;

                    var nextX = _cLeft + i;
                    var nextY = _cTop + j + 1;

                    if (nextY < 15 && _landedBlocks[nextX, nextY] == 0) continue;

                    result = _cTop == 0 ? StepResult.GameOver : StepResult.Landed;
                    LandPiece();
                    HasStateChangedSinceRender = true;
                    return result;
                }
            }

            _cTop++;
            HasStateChangedSinceRender = true;
            return result;
        }
    }

    internal enum StepResult
    {
        PieceCreated,
        StepMade,
        Landed,
        GameOver
    }

    internal enum CheckResult
    {
        Available,
        Blocked,
        OutOfBoundsLeft,
        OutOfBoundsRight
    }
}