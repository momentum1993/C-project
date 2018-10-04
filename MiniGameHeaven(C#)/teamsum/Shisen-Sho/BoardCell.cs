using System;
using System.Collections.Generic;
using System.Text;

namespace Shisen_Sho {
    class BoardCell {
        public static int CELL_EMPTY = -1;
        public static BoardCell bcEmpty = new BoardCell(CELL_EMPTY);
        private int type;
        private bool highlighted;

        public BoardCell(int type) {
            this.type = type;
        }

        public void clear() {
            type = CELL_EMPTY;
            setHighlighted(false);
        }

        public void setType(int type) {
            this.type = type;
        }

        public int getType() {
            return type;
        }

        public bool isHighlighted() {
            return highlighted;
        }

        public void setHighlighted(bool h) {
            this.highlighted = h;
        }
    }
}
