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

    public enum PcColor {
        None = 0,
        Red,
        Lime,
        Blue,
        Cyan,
        Magenta,
        Yellow,

        Wall,
        Leng
    }

    public enum PcType {
        I = 0, 
        L, 
        J, 
        T, 

        Leng
    }

    public class Piece {
        private static readonly NPoint[][] PC_SHPS = {
            new NPoint[] {new NPoint(0,0), new NPoint(-1,0), new NPoint(1,0), new NPoint(-2,0), new NPoint(2,0)}, // I
            new NPoint[] {new NPoint(0,0), new NPoint(-1,0), new NPoint(1,0), new NPoint(2,0), new NPoint(2,-1)}, // L
            new NPoint[] {new NPoint(0,0), new NPoint(-1,0), new NPoint(1,0), new NPoint(-2,0), new NPoint(-2,-1)}, // J
            new NPoint[] {new NPoint(0,0), new NPoint(-1,0), new NPoint(1,0), new NPoint(0,-1), new NPoint(2,0)} // T
        };

        public string Status { get; set; }

        private Field field;

        private NPoint pos;
        public int X { get { return pos.x; } }
        public int Y { get { return pos.y; } }

        public PcColor Color { get; private set; }
        public NPoint[] Shape { get; private set; }

        public Piece(Field field, int x, int y, PcColor color, PcType type) {
            this.field = field;
            
            this.pos = new NPoint(x, y);
            this.Color = color;
            this.Shape = PC_SHPS[(int)type];

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

            foreach (NPoint pt in Shape) {
                int tx = pos.x + pt.x + dx;
                int ty = pos.y + pt.y + dy;

                if (tx < 0 || COLS <= tx || ty < 0 || ROWS <= ty) {
                    result = false;
                }
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
