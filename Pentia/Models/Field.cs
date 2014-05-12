using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Pentia.Controllers;

namespace Pentia.Models {
    public class Field  : IUpdatable {
        public string Status { get; set; }

        public int COLS { get; private set; }
        public int ROWS { get; private set; }
        private int[,] cells;
        
        public Field(Canvas cvs, int rows, int cols) {
            this.COLS = cols;
            this.ROWS = rows;

            cells = new int[COLS, ROWS];
        }

        public void Update() {
            this.Status = "Update the field\n";        
        }

        public void Reset() {
            this.Status = "Reset the field\n";        
        }

    }
}
