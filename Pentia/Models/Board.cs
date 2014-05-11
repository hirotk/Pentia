using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentia.Controllers;

namespace Pentia.Models {
    public class Board : IUpdatable {
        public string Status { get; set; }

        public Board() { 
            
        }

        public void Update() {
            this.Status = "Update the board\n";
        }

        public void Reset() {
            this.Status = "Reset the board\n";
        }

    }
}
