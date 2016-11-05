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
        private int _blockSize;
        private readonly Font _gameOverFont;
        private readonly Brush _gameOverBrush;

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

        public void Draw(Graphics gfx, TetrisRenderBlock[,] tetrisRenderBlocks, Rectangle mainRect, TetrisRenderBlock[,] nextPieceRenderBlocks, Rectangle nextPieceRect, bool gameOver)
        {
            //Clear the screen
            gfx.Clear(Color.Black);

            //Draw the main block space
            RenderBlocksAndFrame(gfx, tetrisRenderBlocks, mainRect);

            //Draw snapshot of next piece
            RenderBlocksAndFrame(gfx, nextPieceRenderBlocks, nextPieceRect);

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

        private static void RenderBlocksAndFrame(Graphics gfx, TetrisRenderBlock[,] tetrisRenderBlocks, Rectangle renderRect)
        {
            //Calculate dimensions
            var lengthX = tetrisRenderBlocks.GetLength(0);
            var lengthY = tetrisRenderBlocks.GetLength(1);

            //Calculate block size
            var blockSize = renderRect.Width / lengthX;

            //Draw tetris blocks
            for (var x = 0; x < lengthX; x++)
            {
                for (var y = 0; y < lengthY; y++)
                {
                    if (!tetrisRenderBlocks[x, y].Active) continue;

                    var drawColor = tetrisRenderBlocks[x, y].Color;
                    var drawRect = new Rectangle(renderRect.X + x * blockSize + 2, renderRect.Y + y * blockSize + 2, blockSize - 2, blockSize - 2);
                    gfx.FillRectangle(new SolidBrush(drawColor), drawRect);
                    gfx.DrawRectangle(new Pen(Dim(drawColor), 2), drawRect);
                }
            }

            //Draw frame
            gfx.DrawRectangle(new Pen(Color.Gray, 5), renderRect);
        }

        private static Color Dim(Color color)
        {
            return Color.FromArgb(color.A, color.R / 2, color.G / 2, color.B / 2);
        }
    }
}