using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pentia.Models {
    public struct NPoint {
        public int x, y;

        public NPoint(int x, int y) {
            this.x = x;
            this.y = y;
        }
    }

    public enum Direction {
        Left, Right, Down
    }

    public enum RtDirection {
        Clockwise, CtrClockwise
    }

    public class Piece {
        public string Status { get; set; }

        private NPoint pos;
        public int X { get { return pos.x; } }
        public int Y { get { return pos.y; } }

        private Field field;

        public Piece(int x, int y, Field field) {
            this.pos = new NPoint(x, y);
            this.field = field;
            field.PutPiece(this);
        }

        private bool canMove(Direction direction) {
            bool result = true;
            int dx = 0, dy = 0;

            switch (direction) {
                case Direction.Left: dx = -1; break;
                case Direction.Right: dx = 1; break;
                case Direction.Down: dy = 1; break;
            }

            int COLS = field.COLS, ROWS = field.ROWS;
            int tx = pos.x + dx, ty = pos.y + dy;

            if (tx < 0 || COLS <= tx || ty < 0 || ROWS <= ty) {
                result = false;
            }

            return result;
        }

        public bool Move(Direction direction) {
            field.RemovePiece(this);
            bool moved = false;

            if (canMove(direction)) {
                switch (direction) {
                    case Direction.Left:
                        this.Status = "Move the piece to the left\n";
                        this.pos.x -= 1;
                        break;
                    case Direction.Right:
                        this.Status = "Move the piece to the right\n";
                        this.pos.x += 1;
                        break;
                    case Direction.Down:
                        this.Status = "Move the piece to the down\n";
                        this.pos.y += 1;
                        break;
                }
                moved = true;
            }

            field.PutPiece(this);
            field.Update();
            return moved;
        }

        private bool canRotate(RtDirection direction) {
            // Todo: Check if you can rotate the piece
            return true; 
        }

        public bool Rotate(RtDirection direction) {
            field.RemovePiece(this);
            bool rotated = false;

            if (canRotate(direction)) {
                switch (direction) {
                    case RtDirection.Clockwise:
                        this.Status = "Rotate the piece to the clockwise\n";
                        // Todo: Rotate the piece to the clockwise
                        break;
                    case RtDirection.CtrClockwise:
                        this.Status = "Rotate the piece to the counter clockwise\n";
                        // Todo: Rotate the piece to the counter clockwise
                        break;
                }
                rotated = true;
            }

            field.PutPiece(this);
            field.Update(); 
            return rotated;
        }
    }
}
