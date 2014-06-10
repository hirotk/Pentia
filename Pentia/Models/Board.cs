using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentia.Controllers;
using Pentia.Utilities;

namespace Pentia.Models {
    public class Board : IUpdatable {
        public bool IsGameOver { get; private set; }

        private Field field;
        public Field Field { get { return field; } }
        private Field nextField;
        public Field NextField { get { return nextField; } }
        private Piece piece;
        public Piece Piece { get { return piece; } }

        private Queue<Piece> pieceQueue;
        private Random rand;

        public int DeletedRowNum { get; private set; }

        public Board(Field field, Field nextField) {
            this.IsGameOver = false;
            this.field = field;
            this.nextField = nextField;
            pieceQueue = new Queue<Piece>();

#if DEBUG
            this.rand = new Random(0);
#else
            this.rand = new Random();
#endif

            this.piece = createPiece(field, field.COLS / 2, 0);

            var nextPiece = createPiece(nextField, nextField.COLS / 2, nextField.ROWS / 2);
            pieceQueue.Enqueue(nextPiece);
            nextField.Draw();
        }

        private Piece createPiece(Field field, int x, int y, PcType type = PcType.Random) {
            if (type == PcType.Random) { type = (PcType)rand.Next((int)PcType.Leng); }
            var piece = new Piece(field, x, y, type);
            field.PutPiece(piece);
            return piece;
        }

        public void Update() {
            if (piece.Move(Direction.Down)) {
                this.DeletedRowNum = 0;
                return; 
            }

            int deletedRowNum;
            field.Update(out deletedRowNum);
            this.DeletedRowNum = deletedRowNum;

            if (piece.Y <= 0) {
                this.IsGameOver = true;
                return;
            }

            // Get the next piece
            var nextPiece = pieceQueue.Dequeue();
            nextField.RemovePiece(nextPiece);

            nextPiece.Field = this.field;
            nextPiece.X = this.field.COLS / 2;
            nextPiece.Y = 0;
            this.piece = nextPiece;

            nextPiece = createPiece(nextField, nextField.COLS / 2, nextField.ROWS / 2);
            pieceQueue.Enqueue(nextPiece);
            nextField.Draw();
        }

        public void Reset() {
            this.field.Reset();
            this.nextField.Reset();
        }

        public bool MovePiece(Direction direction) {
            bool moved = piece.Move(direction);
            return moved;
        }

        public bool RotatePiece(RtDirection direction) {
            bool rotated = piece.Rotate(direction);
            return rotated;
        }
    }
}
