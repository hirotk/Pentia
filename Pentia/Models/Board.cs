using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentia.Controllers;

namespace Pentia.Models {
    public class Board : IUpdatable {
        public string Status { get; set; }

        private Field field;
        private Piece piece;

        public Board(Field field) {
            this.field = field;
            this.piece = new Piece(field, field.COLS / 2, 1, PcColor.Blue, PcType.J);
        }

        public void Update() {
            this.Status += "Update the board\n";
//            this.field.Update();
            piece.Move(Direction.Down);
            this.Status += field.Status;
        }

        public void Reset() {
            this.Status = "Reset the board\n";
            this.field.Reset();
            this.Status += field.Status;
        }

        public bool MovePiece(Direction direction) {
            bool moved = piece.Move(direction);
            this.Status += piece.Status;
            return moved;
        }

        public bool RotatePiece(RtDirection direction) {
            bool rotated = piece.Rotate(direction);
            this.Status += piece.Status;
            return rotated;
        }

    }
}
