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

        public Board(Field field) {
            this.field = field;
        }

        public void Update() {
            this.Status = "Update the board\n";
            this.field.Update();
            this.Status += field.Status;
        }

        public void Reset() {
            this.Status = "Reset the board\n";
            this.field.Reset();
            this.Status += field.Status;
        }

    }
}
