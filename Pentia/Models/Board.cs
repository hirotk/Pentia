using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentia.Controllers;
using Pentia.Utilities;

namespace Pentia.Models {
    public class Board : IUpdatable {
        public string Status { get; set; }

        private Field field;
        private Piece piece;
        private Random rand;

        public int DeletedRowNum { get; private set; }

        public Board(Field field) {
            this.field = field;
#if DEBUG
            this.rand = new Random(0);
#else
            this.rand = new Random();
#endif
            this.piece = getNewPiece() ;
        }

        private Piece getNewPiece() {
            var type = rand.Next((int)PcType.Leng);
            return new Piece(field, field.COLS / 2, 0, (PcColor)(type + 1), (PcType)type);            
        }

        public void Update() {
//            this.Status += "Update the board\n";
            if (piece.Move(Direction.Down)) {
                this.DeletedRowNum = 0;
                return; 
            }

            int deletedRowNum;
            field.Update(out deletedRowNum);
            this.DeletedRowNum = deletedRowNum;

            if (piece.Y <= 0) {
                this.Status += "Game over";
                return; 
            }

            this.piece = getNewPiece();
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
