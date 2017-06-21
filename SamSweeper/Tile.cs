using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamSweeper {
    class Tile {

        private int x;
        private int y;
        private bool isBomb;
        private int numSurroundingBombs;

        public Tile(int x, int y) {
            this.x = x;
            this.y = y;
            this.isBomb = false;
            this.numSurroundingBombs = 0;
        }

        public int X {
            get {
                return this.x;
            }
        }

        public int Y {
            get {
                return this.y;
            }
        }

        public bool IsBomb {
            get {
                return this.isBomb;
            }
            set {
                this.isBomb = value;
            }
        }

        public int NumSurroundingBombs {
            get {
                return this.numSurroundingBombs;
            }
            set {
                this.numSurroundingBombs = value;
            }
        }
    }
}
