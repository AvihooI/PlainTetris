using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlainTetris
{
    internal static class Program
    {
        static TetrisRenderBlock[,] _tetrisRenderBlocks = new TetrisRenderBlock[12,16];
        private static void Main()
        {
            var renderer = new Renderer(40);

            var run = true;
            var mainForm = new CanvasForm();

            mainForm.Show();
            mainForm.Closed += (s, o) =>
            {
                run = false;
            };

            var context = BufferedGraphicsManager.Current;
            context.MaximumBuffer = new Size(mainForm.Width, mainForm.Height);

            var tetrisSimulation = new TetrisSimulation(12,16);

            mainForm.KeyDown += (s, k) =>
            {
                if (k.KeyCode == Keys.Escape)
                {
                    run = false;
                }
                if (k.KeyCode == Keys.Up)
                {
                    tetrisSimulation.SimulationInput(TetrisInput.Up);
                }

                if (k.KeyCode == Keys.Space)
                {
                    tetrisSimulation.SimulationInput(TetrisInput.Space);
                }

                if (k.KeyCode == Keys.Left)
                {
                    tetrisSimulation.SimulationInput(TetrisInput.Left);
                }

                if (k.KeyCode == Keys.Right)
                {
                    tetrisSimulation.SimulationInput(TetrisInput.Right);
                }

                if (k.KeyCode == Keys.Down)
                {
                    tetrisSimulation.SimulationInput(TetrisInput.Down);
                }

                if (k.KeyCode == Keys.Enter)
                {
                    tetrisSimulation.SimulationInput(TetrisInput.Enter);
                }
            };

            var renderBlocks = tetrisSimulation.GetRenderBlocks();
            var renderNextPieceRenderBlocks = tetrisSimulation.GetNextPieceRenderBlocks();
            while (run)
            {
                
                using (var gfx = mainForm.CreateGraphics())
                {
                    var bgfx = context.Allocate(gfx,new Rectangle(0,0,mainForm.Width,mainForm.Height));
                    if (tetrisSimulation.HasStateChangedSinceRender)
                    {
                        renderBlocks = tetrisSimulation.GetRenderBlocks();
                    }
                    if (tetrisSimulation.HasNextPieceStateChangedSinceRender)
                    {
                        renderNextPieceRenderBlocks = tetrisSimulation.GetNextPieceRenderBlocks();
                    }
                    renderer.Draw(bgfx.Graphics, renderBlocks, new Rectangle(0,0,480,640), renderNextPieceRenderBlocks, new Rectangle(480,0,100,100),  tetrisSimulation.GameOver);
                    bgfx.Render();
                    bgfx.Graphics.Dispose();
                }
                Application.DoEvents();
            }

        }

    }
}
