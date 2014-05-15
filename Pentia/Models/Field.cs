using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Pentia.Controllers;
using System.Windows.Shapes;
using System.Windows.Media;

namespace Pentia.Models {
    public class Field  : IUpdatable {
        public string Status { get; set; }

        public int COLS { get; private set; }
        public int ROWS { get; private set; }
        private int[,] cells;
        private Canvas canvas;
        private Renderer renderer;

        public Field(Canvas canvas, int cols, int rows) {
            this.canvas = canvas;
            this.COLS = cols;
            this.ROWS = rows;
            this.renderer = new Renderer(this);

            cells = new int[COLS, ROWS];
        }

        public void Update() {
            this.Status = "Update the field\n";
            renderer.Draw(cells);
        }

        public void Reset() {
            this.Status = "Reset the field\n";        
        }

        public void PutPiece(Piece piece) {
            cells[piece.X, piece.Y] = 1;
        }

        public void RemovePiece(Piece piece) {
            cells[piece.X, piece.Y] = 0;
        }

        private class Renderer {
            private Canvas canvas;
            private int cols, rows;
            private Rectangle[,] rcCells;
            
            public Renderer(Field field) {
                this.canvas = field.canvas;
                this.cols = field.COLS;
                this.rows = field.ROWS;
                
                this.rcCells = new Rectangle[cols, rows];

                int cellWidth = (int)(canvas.Width / cols);
                int cellHeight = (int)(canvas.Height / rows);

                for (int j = 0; j < rows; j++) {
                    for (int i = 0; i < cols; i++) {
                        rcCells[i, j] = new Rectangle();
                        rcCells[i, j].Width = cellWidth;
                        rcCells[i, j].Height = cellHeight;
                        rcCells[i, j].Fill = Brushes.LightYellow;
                        Canvas.SetLeft(rcCells[i, j], i  * cellWidth);
                        Canvas.SetTop(rcCells[i, j], j  * cellHeight);
                        canvas.Children.Add(rcCells[i, j]);
                    }
                }
            }

            public void Draw(int[,] cells) { 
                // Todo: Update rcCells based on the current cells

                // Test draw
/*                rcCells[0, 0].Fill = Brushes.Red;
                rcCells[cols - 1, 0].Fill = Brushes.Green;
                rcCells[0, rows - 1].Fill = Brushes.Blue;
                rcCells[cols - 1, rows - 1].Fill = Brushes.Cyan;*/

                for (int j = 0; j < rows; j++) {
                    for (int i = 0; i < cols; i++) {
                        if (0 < cells[i, j]) {
                            rcCells[i, j].Fill = Brushes.Magenta;
                        } else {
                            rcCells[i, j].Fill = Brushes.LightYellow;                        
                        }
                    }
                }
            }
        }
    }
}
