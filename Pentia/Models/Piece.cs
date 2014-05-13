using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pentia.Models {
    public enum Direction {
        Left, Right, Down
    }

    public enum RtDirection {
        Clockwise, CtrClockwise
    }

    public class Piece {
        public string Status { get; set; }

        public Piece() { }

        public bool Move(Direction direction) {
            switch (direction) { 
                case Direction.Left:
                    this.Status = "Move the piece to the left\n";
                    break;
                case Direction.Right:
                    this.Status = "Move the piece to the right\n";
                    break;
                case Direction.Down:
                    this.Status = "Move the piece to the down\n";
                    break;
            }

            return true;
        }

        public bool Rotate(RtDirection direction) {
            switch (direction) {
                case RtDirection.Clockwise:
                    this.Status = "Rotate the piece to the clockwise\n";
                    break;
                case RtDirection.CtrClockwise:
                    this.Status = "Rotate the piece to the counter clockwise\n";
                    break;
            }

            return true;
        }
    }
}
