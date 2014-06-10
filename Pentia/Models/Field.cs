using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using Pentia.Controllers;
using Pentia.Utilities;
using System.Windows.Media.Animation;

namespace Pentia.Models {
    public class Field  : IUpdatable {
        public int COLS { get; private set; }
        public int ROWS { get; private set; }
        private PcColor[,] cells;
        public PcColor this[int i, int j] {
            get { return cells[wallThickness + i, yOffset + j]; }
            set { cells[wallThickness + i, yOffset + j] = value; } 
        }
        private Canvas canvas;
        private Renderer renderer;

        private int yOffset;
        private int wallThickness;

        public int DelibleRowNum { get; private set; }

        private bool[] isDelibleRow;
        public bool GetIsDelibleRow(int i) {
            return isDelibleRow[yOffset + i];
        }
        private void setIsDelibleRow(int i, bool value) {
            isDelibleRow[yOffset + i] = value;
        }

        public Field(Canvas canvas, int cols, int rows, int yOffset = 0, int wallThickness = 0, int dispWallThickness = 0) {
            this.canvas = canvas;
            this.COLS = cols;
            this.ROWS = rows;
            this.yOffset = yOffset;
            this.wallThickness = wallThickness;

            cells = new PcColor[COLS + wallThickness * 2, yOffset + ROWS + wallThickness];
            if (0 < wallThickness) { setWalls(); }

            this.renderer = new Renderer(this, dispWallThickness);
            isDelibleRow = new bool[yOffset + ROWS];
        }

        private void setWalls() {
            // Set the side walls
            for (int j = 0; j < yOffset + ROWS + wallThickness; j++) {
                for (int i = 0; i < wallThickness; i++) {
                    cells[i, j] = PcColor.Wall; // Set the left wall  
                    cells[wallThickness + COLS + i, j] = PcColor.Wall; // Set the right wall  
                }
            }

            // Set the bottom
            for (int j = yOffset + ROWS; j < yOffset + ROWS + wallThickness; j++) {
                for (int i = 0; i < COLS + wallThickness * 2; i++) {
                    cells[i, j] = PcColor.Wall;
                }
            }
        }

        public void Update() {
            int tmp;
            Update(out tmp);
        }

        public void Update(out int deletedRowNum) {
            deletedRowNum = DeleteRows();
        }

        public void Reset() {
            for (int j = -yOffset; j < ROWS; j++) {
                for (int i = 0; i < COLS; i++) {
                    this[i, j] = PcColor.None;
                }
            }

            for (int j = 0; j < ROWS; j++) {
                setIsDelibleRow(j, false);
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

        public void Draw() {
            renderer.Draw(cells);
        }

        private void checkRows() {
            this.DelibleRowNum = 0;

            for (int j = ROWS - 1; j >= 0; j--) {
                setIsDelibleRow(j, true);
                for (int i = 0; i < COLS; i++) {
                    if (this[i, j] == PcColor.None) {
                        setIsDelibleRow(j, false);
                        break;
                    }
                }
                if (GetIsDelibleRow(j)) { DelibleRowNum++; }
            }
        }

        private void deleteRow(int row) {
            for (int j = row; j >= 0; j--) {
                for (int i = 0; i < COLS; i++) {
                    this[i, j] = this[i, j - 1];
                }
                setIsDelibleRow(j, GetIsDelibleRow(j - 1));
            }
            DelibleRowNum--;
        }

        public int DeleteRows() {
            int deletedRowNum = 0;
            checkRows();

            while (0 < DelibleRowNum) {
                for (int j = ROWS - 1; j >= 0; j--) {
                    if (GetIsDelibleRow(j)) {
                        renderer.FlashRow(j);
                    }
                }

                for (int j = ROWS - 1; j >= 0; j--) {
                    if (GetIsDelibleRow(j)) {
                        deleteRow(j);
                        deletedRowNum++;
                    }
                }
            }
            return deletedRowNum;
        }

        private class Renderer {
            private static RectangleGeometry[] rgColor;

            private Field field;
            private int cols, rows, yOffset, wallThickness;
            private Rectangle[,] rcCells;
            private Rectangle this[int i, int j] {
                get { return rcCells[wallThickness + i, j]; }
                set { rcCells[wallThickness + i, j] = value; }
            }
            private int cellWidth, cellHeight;

            public Renderer(Field field, int dispWallThickness = 0) {
                this.field = field;
                this.cols = field.COLS;
                this.rows = field.ROWS;
                this.yOffset = field.yOffset;
                this.wallThickness = (0 < field.wallThickness) ? dispWallThickness : 0;
                this.rcCells = new Rectangle[cols + wallThickness * 2, rows + wallThickness];

                this.cellWidth = (int)(field.canvas.Width / (cols + wallThickness * 2));
                this.cellHeight = (int)(field.canvas.Height / (rows + wallThickness));

                rgColor = new RectangleGeometry[(int)PcColor.Leng];
                for (int i = 0; i < rgColor.Length; i++) {
                    rgColor[i] = new RectangleGeometry(new Rect(i * cellWidth, 0, cellWidth, cellHeight));
                }

                for (int j = 0; j < rows + wallThickness; j++) {
                    for (int i = 0; i < cols + wallThickness * 2; i++) {
                        rcCells[i, j] = new Rectangle();
                        ImageLoader.LoadImage("Cells", rcCells[i, j]);
                        int c = (int)PcColor.None;
                        rcCells[i, j].Clip = rgColor[c];

                        Canvas.SetLeft(rcCells[i, j], (i - c) * cellWidth);
                        Canvas.SetTop(rcCells[i, j], j  * cellHeight);
                        field.canvas.Children.Add(rcCells[i, j]);
                    }
                }

                if (0 < wallThickness) { setWalls(); }
            }

            private void setWalls() {
                for (int j = 0; j < rows + wallThickness; j++) {
                    for (int i = 0; i < cols + wallThickness * 2; i++) {
                        int c = (int)field.cells[field.wallThickness - wallThickness + i, yOffset + j];
                        rcCells[i, j].Clip = rgColor[c];
                        Canvas.SetLeft(rcCells[i, j], (i - c) * cellWidth);
                    }
                }
            }

            public void Draw(PcColor[,] cells) { 
                for (int j = 0; j < rows; j++) {
                    for (int i = 0; i < cols; i++) {
                        int c = (int)field[i, j];
                        this[i, j].Clip = rgColor[c];
                        Canvas.SetLeft(this[i, j], (wallThickness + i - c) * cellWidth);
                    }
                }
            }

            private static DoubleAnimation fadeout = new DoubleAnimation(1.0, 0.5, TimeSpan.FromMilliseconds(200));
            private static DoubleAnimation fadein = new DoubleAnimation(0.5, 1.0, TimeSpan.FromMilliseconds(200));

            public void FlashRow(int row) {
                for (int i = 0; i < cols; i++) {
                    this[i, row].BeginAnimation(Rectangle.OpacityProperty, fadeout);
                    this[i, row].BeginAnimation(Rectangle.OpacityProperty, fadein);
                }
            }
        }
    }
}
