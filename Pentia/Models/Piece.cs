using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentia.Utilities;

namespace Pentia.Models {    
    public enum PcColor {
        None = 0,  // MintCream
        Red,
        Lime,
        Blue,
        Cyan,
        Magenta,
        Yellow,
        Maroon,
        Green,
        Navy,
        Teal,
        Purple,
        Olive,
        Salmon,
        Pink,
        Tomato,
        Orange,
        Violet,
        Indigo,
        SpringGreen,
        DeepSkyBlue,
        Chocolate,
        Brown,
        Wall,
        Leng
    }

    public enum PcType {
        I = 0, 
        L0, 
//        L1,
//        L2,
//        U,
        J0, 
//        J1,
//        J2,
        T0,
        T1,
//        T2,
//        X,
        O0,
        O1,
        S0,
//        S1,
//        S2,
//        S3,
        Z0,
//        Z1,
//        Z2,
//        Z3,
        Leng,
        Random
    }

    public class Piece : IMovable, IRotatable {
        private static readonly NPoint[][] PC_SHPS = {
            new NPoint[] {new NPoint(0,0), new NPoint(-1,0), new NPoint(1,0), new NPoint(-2,0), new NPoint(2,0)}, // I
            
            new NPoint[] {new NPoint(0,0), new NPoint(-1,0), new NPoint(1,0), new NPoint(2,0), new NPoint(2,-1)},  // L0
//            new NPoint[] {new NPoint(0,0), new NPoint(-1,0), new NPoint(1,0), new NPoint(-1,-1), new NPoint(-1,-2)}, // L1
//            new NPoint[] {new NPoint(0,0), new NPoint(-1,0), new NPoint(1,0), new NPoint(-1,-1), new NPoint(1,1)}, // L2

//            new NPoint[] {new NPoint(0,0), new NPoint(-1,0), new NPoint(1,0), new NPoint(-1,-1), new NPoint(1,-1)}, // U

            new NPoint[] {new NPoint(0,0), new NPoint(-1,0), new NPoint(1,0), new NPoint(-2,0), new NPoint(-2,-1)}, // J0
//            new NPoint[] {new NPoint(0,0), new NPoint(-1,0), new NPoint(1,0), new NPoint(1,-1),  new NPoint(1,-2)}, // J1
//            new NPoint[] {new NPoint(0,0), new NPoint(-1,0), new NPoint(1,0), new NPoint(1,-1),  new NPoint(-1,1)}, // J2

            new NPoint[] {new NPoint(0,0), new NPoint(-1,0), new NPoint(1,0), new NPoint(0,-1), new NPoint(2,0)}, // T0
            new NPoint[] {new NPoint(0,0), new NPoint(-1,0), new NPoint(1,0), new NPoint(0,-1), new NPoint(-2, 0)}, // T1
//            new NPoint[] {new NPoint(0,0), new NPoint(-1,0), new NPoint(1,0), new NPoint(0,-1), new NPoint(0, -2)}, // T2

//            new NPoint[] {new NPoint(0,0), new NPoint(-1,0), new NPoint(1,0), new NPoint(0,-1), new NPoint(0, 1)}, // X

            new NPoint[] {new NPoint(0,0), new NPoint(1,0), new NPoint(0,-1), new NPoint(1,-1), new NPoint(-1, -1)}, // O0
            new NPoint[] {new NPoint(0,0), new NPoint(1,0), new NPoint(0,-1), new NPoint(1,-1), new NPoint(-1, 0)}, // O1

            new NPoint[] {new NPoint(0,0), new NPoint(-1,0), new NPoint(0,-1), new NPoint(1,-1), new NPoint(2, -1)}, // S0
//            new NPoint[] {new NPoint(0,0), new NPoint(-1,0), new NPoint(0,-1), new NPoint(1,-1), new NPoint(1, -2)}, // S1 (W0)
//            new NPoint[] {new NPoint(0,0), new NPoint(-1,0), new NPoint(0,-1), new NPoint(1,-1), new NPoint(0, -2)}, // S2
//            new NPoint[] {new NPoint(0,0), new NPoint(-1,0), new NPoint(0,-1), new NPoint(1,-1), new NPoint(0, 1)}, // S3

            new NPoint[] {new NPoint(0,0), new NPoint(1,0), new NPoint(0,-1), new NPoint(-1,-1), new NPoint(-2, -1)}, // Z0
//            new NPoint[] {new NPoint(0,0), new NPoint(1,0), new NPoint(0,-1), new NPoint(-1,-1), new NPoint(-1, -2)}, // Z1 (W1)
//            new NPoint[] {new NPoint(0,0), new NPoint(1,0), new NPoint(0,-1), new NPoint(-1,-1), new NPoint(0, -2)}, // Z2
//            new NPoint[] {new NPoint(0,0), new NPoint(1,0), new NPoint(0,-1), new NPoint(-1,-1), new NPoint(0, 1)}, // Z3
        };
        private static readonly PcColor[] PC_CLRS =  {
            PcColor.Red,
            PcColor.Cyan,
            PcColor.Yellow,
            PcColor.Purple,
            PcColor.Orange,
            PcColor.Blue,
            PcColor.Green,
            PcColor.Lime,
            PcColor.Magenta
        };

        public const int PC_SIZE = 5;

        public string Status { get; set; }

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

        public Piece(Field field, int x, int y, PcType type, PcColor? color= null) {
            this.Field = field;
            this.pos = new NPoint(x, y);
            this.Color = color ?? PC_CLRS[(int)type];

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
