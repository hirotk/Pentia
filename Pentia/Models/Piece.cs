using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentia.Utilities;

namespace Pentia.Models {
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

        Leng,
        Random
    }

    public class Piece : IMovable, IRotatable {
        private static readonly NPoint[][] PC_SHPS = {
            new NPoint[] {new NPoint(0,0), new NPoint(-1,0), new NPoint(1,0), new NPoint(-2,0), new NPoint(2,0)}, // I
            new NPoint[] {new NPoint(0,0), new NPoint(-1,0), new NPoint(1,0), new NPoint(2,0), new NPoint(2,-1)}, // L
            new NPoint[] {new NPoint(0,0), new NPoint(-1,0), new NPoint(1,0), new NPoint(-2,0), new NPoint(-2,-1)}, // J
            new NPoint[] {new NPoint(0,0), new NPoint(-1,0), new NPoint(1,0), new NPoint(0,-1), new NPoint(2,0)} // T
        };
        public const int PC_SIZE = 5;

        public string Status { get; set; }

//        private Field Field;
        public Field Field { get; set; }

        private NPoint pos;
        public int X { 
            get { return pos.x; }
            set { pos.x = value; }
        }
        public int Y { 
            get { return pos.y; }
            set { pos.y = value; }
        }

        public PcColor Color { get; private set; }
        public NPoint[] Shape { get; private set; }

        public Piece(Field field, int x, int y, PcColor color, PcType type) {
            this.Field = field;
            this.pos = new NPoint(x, y);
            this.Color = color;

            var PcShp = PC_SHPS[(int)type];
            this.Shape = new NPoint[PcShp.Length];
            PcShp.CopyTo(this.Shape, 0); 
        }

        private bool canMove(Direction direction) {
            int COLS = Field.COLS, ROWS = Field.ROWS;

            foreach (NPoint pt in Shape) {
                var tpt = pos + Mover.Move(pt, direction);

                if (Field[tpt.x, tpt.y] != PcColor.None) {
                    return  false;
                }
            }

            return true;
        }

        public bool Move(Direction direction) {
            Field.RemovePiece(this);
            bool moved = false;

            if (canMove(direction)) {

                // Just output status
/*                switch (direction) {
                    case Direction.Left:
                        this.Status = "Move the piece to the left\n";
                        break;
                    case Direction.Right:
                        this.Status = "Move the piece to the right\n";
                        break;
                    case Direction.Down:
                        this.Status = "Move the piece to the down\n";
                        break;
                }*/

                Mover.Move(ref pos, direction);

                moved = true;
            }

            Field.PutPiece(this);
            Field.Draw();
            return moved;
        }

        private bool canRotate(RtDirection direction) {
            int COLS = Field.COLS, ROWS = Field.ROWS;

            foreach (NPoint pt in Shape) {
                var tpt = this.pos + Rotator.Rotate(pt, direction);

                if (Field[tpt.x, tpt.y] != PcColor.None) {
                   return false;
                }
            }

            return true;
        }

        public bool Rotate(RtDirection direction) {
            Field.RemovePiece(this);
            bool rotated = false;

            if (canRotate(direction)) {

                // Just output status
/*                switch (direction) {
                    case RtDirection.Clockwise:
                        this.Status = "Rotate the piece to the clockwise\n";
                        break;
                    case RtDirection.CtrClockwise:
                        this.Status = "Rotate the piece to the counter clockwise\n";
                        break;
                }*/

                Rotator.Rotate(Shape, direction);

                rotated = true;
            }

            Field.PutPiece(this);
            Field.Draw(); 
            return rotated;
        }
    }
}
