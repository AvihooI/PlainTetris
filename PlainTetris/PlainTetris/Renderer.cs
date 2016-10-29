using System.Drawing;
using System.Threading;

namespace PlainTetris
{
    internal class Renderer
    {
        private int _lastFps;
        private int _currentFps;
        private Timer _fpsTimer;
        private readonly Font _fpsFont;
        private readonly SolidBrush _fpsBrush;
        private TetrisRenderBlock[,] _tetrisRenderBlocks;
        private int _blockSize;
        private Font _gameOverFont;
        private Brush _gameOverBrush;

        public Renderer(int blockSize)
        {
            _fpsTimer = new Timer(FpsCallback, null, 0, 1000);
            _fpsFont = new Font(FontFamily.GenericMonospace, 16);
            _fpsBrush = new SolidBrush(Color.Blue);

            _gameOverFont = new Font(FontFamily.GenericSerif, 42);
            _gameOverBrush = new SolidBrush(Color.Chocolate);

            _blockSize = blockSize;
        }

        private void FpsCallback(object state)
        {
            _lastFps = _currentFps;
            _currentFps = 0;
        }

        public void Draw(Graphics gfx, TetrisRenderBlock[,] tetrisRenderBlocks, bool gameOver)
        {
            gfx.Clear(Color.Black);
            //Draw tetris blocks
            for (var x = 0; x < 12; x++)
            {
                for (var y = 0; y < 16; y++)
                {
                    if (!tetrisRenderBlocks[x, y].Active) continue;

                    var drawColor = tetrisRenderBlocks[x, y].Color;
                    var drawRect = new Rectangle(x*_blockSize+2,y*_blockSize+2,38,38);
                    gfx.FillRectangle(new SolidBrush(drawColor), drawRect);
                    gfx.DrawRectangle(new Pen(Dim(drawColor),2), drawRect);
                }
            }
            

            //Draw Game Over indicator if required, otherwise draw score
            if (gameOver)
            {
                var layoutRectangle = new Rectangle(170, 200, 300, 300);
                gfx.DrawString("Game Over!", _gameOverFont, _gameOverBrush, layoutRectangle);
            }

            //Draw FPS overlay
            gfx.DrawString(_lastFps.ToString(), _fpsFont, _fpsBrush, 0, 0);
            _currentFps++;
        }

        private static Color Dim(Color color)
        {
            return Color.FromArgb(color.A,color.R/2,color.G/2,color.B/2);
        }
    }
}