using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using Pentia.Controllers;
using Pentia.Utilities;

namespace Pentia.Models {
    public class Field  : IUpdatable {
        public string Status { get; set; }

        public int COLS { get; private set; }
        public int ROWS { get; private set; }
        private PcColor[,] cells;
        public PcColor this[int i, int j] {  
            get { return cells[i, yOffset + j]; } 
            set { cells[i, yOffset + j] = value; } 
        }
        private Canvas canvas;
        private Renderer renderer;

        private int yOffset;

        public Field(Canvas canvas, int cols, int rows, int yOffset) {
            this.canvas = canvas;
            this.COLS = cols;
            this.ROWS = rows;
            this.yOffset = yOffset;
            this.renderer = new Renderer(this);

            cells = new PcColor[COLS, ROWS + yOffset];
        }

        public void Update() {
            this.Status = "Update the field\n";
            renderer.Draw(cells);
        }

        public void Reset() {
            this.Status = "Reset the field\n";
            for (int j = 0; j < cells.GetLength(1); j++) {
                for (int i = 0; i < cells.GetLength(0); i++) {
                    cells[i, j] = PcColor.None;
                }
            }
        }

        public void PutPiece(Piece piece) {
            foreach (NPoint pt in piece.Shape) {
                this[piece.X + pt.x, piece.Y + pt.y] = piece.Color;
            }
        }

        public void RemovePiece(Piece piece) {
            foreach (NPoint pt in piece.Shape) {
                this[piece.X + pt.x, piece.Y + pt.y] = PcColor.None;
            }
        }

        private class Renderer {
            private static readonly Brush[] PC_BRS =
                { Brushes.MintCream, Brushes.Red, Brushes.Lime, Brushes.Blue, Brushes.Cyan, Brushes.Magenta, Brushes.Yellow};

            private Canvas canvas;
            private int cols, rows, yOffset;
            private Rectangle[,] rcCells;
            
            public Renderer(Field field) {
                this.canvas = field.canvas;
                this.cols = field.COLS;
                this.rows = field.ROWS;
                this.yOffset = field.yOffset;

                this.rcCells = new Rectangle[cols, rows];

                int cellWidth = (int)(canvas.Width / cols);
                int cellHeight = (int)(canvas.Height / rows);

                for (int j = 0; j < rows; j++) {
                    for (int i = 0; i < cols; i++) {
                        rcCells[i, j] = new Rectangle();
                        rcCells[i, j].Width = cellWidth;
                        rcCells[i, j].Height = cellHeight;
                        rcCells[i, j].Fill = Brushes.MintCream;
                        Canvas.SetLeft(rcCells[i, j], i  * cellWidth);
                        Canvas.SetTop(rcCells[i, j], j  * cellHeight);
                        canvas.Children.Add(rcCells[i, j]);
                    }
                }
            }

            public void Draw(PcColor[,] cells) { 
                // Todo: Update rcCells based on the current cells

                // Test draw
/*                rcCells[0, 0].Fill = Brushes.Red;
                rcCells[cols - 1, 0].Fill = Brushes.Green;
                rcCells[0, rows - 1].Fill = Brushes.Blue;
                rcCells[cols - 1, rows - 1].Fill = Brushes.Cyan;*/

                for (int j = 0; j < rows; j++) {
                    for (int i = 0; i < cols; i++) {
                        int c = (int)cells[i, yOffset + j];
                        rcCells[i, j].Fill = PC_BRS[c];
                    }
                }
            }
        }
    }
}
