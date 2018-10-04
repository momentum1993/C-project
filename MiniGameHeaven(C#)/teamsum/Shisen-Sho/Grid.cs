using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Reflection;
using System.Collections;

namespace Shisen_Sho {
    class Grid {
        private Random rand;

        private int numCells_h;
        private int numCells_v;
        private int numCells;
        private int numTiles;
        private int numCleared;
        private BoardCell[,] cells;

        public Grid(int num_tiles_v, int num_tiles_h) {
            if ((num_tiles_h * num_tiles_v) % 2 != 0) {
                throw new ArgumentException("Error");
            } else if (num_tiles_h < 4 || num_tiles_v < 4) {
                throw new ArgumentException("Error");
            }
            this.numCells_h = num_tiles_h + 2;
            this.numCells_v = num_tiles_v + 2;
            this.numCells = numCells_h * numCells_v;
            this.numTiles = num_tiles_h * num_tiles_v;
            this.cells = new BoardCell[numCells_v, numCells_h];
            this.rand = new Random();
        }

        public void reset() {
            numCleared = 0;
            initTiles();

            shuffleTiles((int)(Properties.Settings.Default.difficulty * numTiles));
            makeSolvable();
        }

        private void initTiles() {
            int type = 0;
            bool gen = true;
            for (int i = 0; i < numCells_v; i++) {
                for (int j = 0; j < numCells_h; j++) {
                    if (i == 0 || i == numCells_v - 1 || j == 0 || j == numCells_h - 1) {
                        cells[i, j] = BoardCell.bcEmpty;
                    } else {
                        if (gen)
                            type = rand.Next(Board.pieces.Length);
                        cells[i, j] = new BoardCell(type);
                        gen = !gen;
                    }
                }
            }
        }

        private void shuffleTiles(int times) {
            while (times-- > 0) {
                Point p1 = new Point(rand.Next(1, numCells_h - 1), rand.Next(1, numCells_v - 1));
                Point p2 = new Point(rand.Next(1, numCells_h - 1), rand.Next(1, numCells_v - 1));
                swap(p1, p2);
            }
        }

        private void swap(Point p1, Point p2, BoardCell[,] cells) {
            BoardCell tmp = cells[p1.Y, p1.X];
            cells[p1.Y, p1.X] = cells[p2.Y, p2.X];
            cells[p2.Y, p2.X] = tmp;
        }

        private void swap(Point p1, Point p2) {
            swap(p1, p2, cells);
        }

        private void swap(int cell1, int cell2) {
            swap(cellToPoint(cell1), cellToPoint(cell2), cells);
        }

        private void swap(int cell1, int cell2, BoardCell[,] cells) {
            swap(cellToPoint(cell1), cellToPoint(cell2), cells);
        }

        public bool isCellEmpty(Point pt) {
            return cells[pt.Y, pt.X].getType() == BoardCell.CELL_EMPTY;
        }

        public bool isCellEmpty(int y, int x) {
            return cells[y, x].getType() == BoardCell.CELL_EMPTY;
        }

        public bool isCellEmpty(int i) {
            return isCellEmpty(cellToPoint(i));
        }

        public BoardCell getCell(int i) {
            return cells[i / numCells_h, i % numCells_h];
        }

        public BoardCell getCell(int y, int x) {
            return cells[y, x];
        }

        public BoardCell getCell(Point pt) {
            return cells[pt.Y, pt.X];
        }

        public LinkedList<Point> clearPair(Point pt1, Point pt2, LinkedList<Point> llChanged) {
            Point higherPt = pt1.Y < pt2.Y ? pt1 : pt2;
            Point lowerPt = pt1.Y < pt2.Y ? pt2 : pt1;
            clearCell(higherPt, llChanged);
            return clearCell(lowerPt, llChanged);
        }

        public LinkedList<Point> clearPair(Point pt1, Point pt2) {
            return clearPair(pt1, pt2, new LinkedList<Point>());
        }

        public LinkedList<Point> clearPair(int cell1, int cell2, LinkedList<Point> llChanged) {
            return clearPair(cellToPoint(cell1), cellToPoint(cell2), llChanged);
        }

        private LinkedList<Point> clearCell(Point pt, LinkedList<Point> llChanged) {
            if(llChanged != null)
                llChanged.AddFirst(pt);

            cells[pt.Y, pt.X].clear();
            
            numCleared++;
            return llChanged;
        }

        private LinkedList<Point> clearCell(Point pt) {
            return clearCell(pt, new LinkedList<Point>());
        }

        //private LinkedList<Point> clearCell(int cell) {
        //    return clearCell(cellToPoint(cell));
        //}

        public void unHighlightCell(int y, int x) {
            cells[y, x].setHighlighted(false);
        }

        public void unHighlightCell(Point pt) {
            cells[pt.Y, pt.X].setHighlighted(false);
        }

        public void highlightCell(int y, int x) {
            cells[y, x].setHighlighted(true);
        }

        public void highlightCell(Point pt) {
            cells[pt.Y, pt.X].setHighlighted(true);
        }

        public LinkedList<Point> unHighlightAll() {
            LinkedList<Point> llChanged = new LinkedList<Point>();
            for (int i = 1; i < numCells_v - 1; i++) {
                for (int j = 1; j < numCells_h - 1; j++) {
                    if (cells[i, j].isHighlighted()) {
                        llChanged.AddFirst(new Point(j, i));
                    }
                    unHighlightCell(i, j);
                }
            }
            return llChanged;
        }

        public LinkedList<Point> changeHighlightType(int type) {
            LinkedList<Point> llChanged = new LinkedList<Point>();
            for (int i = 1; i < numCells_v - 1; i++) {
                for (int j = 1; j < numCells_h - 1; j++) {
                    if (cells[i, j].isHighlighted() && cells[i, j].getType() != type ||
                        !cells[i, j].isHighlighted() && cells[i, j].getType() == type) {
                        llChanged.AddFirst(new Point(j, i));
                    }
                    cells[i, j].setHighlighted(cells[i, j].getType() == type);
                }
            }
            return llChanged;
        }

        public LinkedList<Point> changeHighlightType(Point pt) {
            return changeHighlightType(cells[pt.Y, pt.X].getType());
        }

        private Point[] getPath1(Point p1, Point p2) {
            int i;
            if (p1.X == p2.X) {
                int minY = Math.Min(p1.Y, p2.Y);
                int maxY = Math.Max(p1.Y, p2.Y);
                for (i = minY + 1; i < maxY && isCellEmpty(i, p1.X); i++) ;
                return i == maxY ? new Point[] { p1, p2 } : null;
            } else if (p1.Y == p2.Y) {
                int minX = Math.Min(p1.X, p2.X);
                int maxX = Math.Max(p1.X, p2.X);
                for (i = minX + 1; i < maxX && isCellEmpty(p1.Y, i); i++) ;
                return i == maxX ? new Point[] { p1, p2 } : null;
            }
            return null;
        }

        private Point[] getPath2(Point p1, Point p2) {
            if (p1.X != p2.X && p1.Y != p2.Y) {
                Point sameY = new Point(p2.X, p1.Y);
                Point sameX = new Point(p1.X, p2.Y);
                if (isCellEmpty(sameY) && getPath1(p1, sameY) != null && getPath1(sameY, p2) != null)
                    return new Point[] { p1, sameY, p2 };
                else if (isCellEmpty(sameX) && getPath1(p1, sameX) != null && getPath1(sameX, p2) != null)
                    return new Point[] { p1, sameX, p2 };
            }
            return null;
        }

        private Point[] getPath3(Point p1, Point p2) {
            Point tmp = new Point(p1.X + 1, p1.Y);
            Point[] path2;
            while (tmp.X < numCells_h && isCellEmpty(tmp)) {
                if ((path2 = getPath2(tmp, p2)) != null)
                    return new Point[] { p1, tmp, path2[1], p2 };
                tmp.X++;
            }

            tmp = new Point(p1.X - 1, p1.Y);
            while (tmp.X >= 0 && isCellEmpty(tmp)) {
                if ((path2 = getPath2(tmp, p2)) != null)
                    return new Point[] { p1, tmp, path2[1], p2 };
                tmp.X--;
            }

            tmp = new Point(p1.X, p1.Y + 1);
            while (tmp.Y < numCells_v && isCellEmpty(tmp)) {
                if ((path2 = getPath2(tmp, p2)) != null)
                    return new Point[] { p1, tmp, path2[1], p2 };
                tmp.Y++;
            }

            tmp = new Point(p1.X, p1.Y - 1);
            while (tmp.Y >= 0 && isCellEmpty(tmp)) {
                if ((path2 = getPath2(tmp, p2)) != null)
                    return new Point[] { p1, tmp, path2[1], p2 };
                tmp.Y--;
            }
            return null;
        }

        private bool existsPath1(Point p1, Point p2) {
            int i;
            if (p1.X == p2.X) {
                int minY = Math.Min(p1.Y, p2.Y);
                int maxY = Math.Max(p1.Y, p2.Y);
                for (i = minY + 1; i < maxY && isCellEmpty(i, p1.X); i++) ;
                return i == maxY;
            } else if (p1.Y == p2.Y) {
                int minX = Math.Min(p1.X, p2.X);
                int maxX = Math.Max(p1.X, p2.X);
                for (i = minX + 1; i < maxX && isCellEmpty(p1.Y, i); i++) ;
                return i == maxX;
            }
            return false;
        }

        private bool existsPath2(Point p1, Point p2) {
            if (p1.X != p2.X && p1.Y != p2.Y) {
                Point sameY = new Point(p2.X, p1.Y);
                Point sameX = new Point(p1.X, p2.Y);
                return (isCellEmpty(sameY) && existsPath1(p1, sameY) && existsPath1(sameY, p2)) ||
                    (isCellEmpty(sameX) && existsPath1(p1, sameX) && existsPath1(sameX, p2));
            }
            return false;
        }

        private bool existsPath3(Point p1, Point p2) {
            Point tmp = new Point(p1.X + 1, p1.Y);
            while (tmp.X < numCells_h && isCellEmpty(tmp)) {
                if (existsPath2(tmp, p2))
                    return true;
                tmp.X++;
            }

            tmp = new Point(p1.X - 1, p1.Y);
            while (tmp.X >= 0 && isCellEmpty(tmp)) {
                if (existsPath2(tmp, p2))
                    return true;
                tmp.X--;
            }

            tmp = new Point(p1.X, p1.Y + 1);
            while (tmp.Y < numCells_v && isCellEmpty(tmp)) {
                if (existsPath2(tmp, p2))
                    return true;
                tmp.Y++;
            }

            tmp = new Point(p1.X, p1.Y - 1);
            while (tmp.Y >= 0 && isCellEmpty(tmp)) {
                if (existsPath2(tmp, p2))
                    return true;
                tmp.Y--;
            }
            return false;
        }

        private bool existsPath(Point p1, Point p2) {
            if (p1 == p2)
                return false;
            return existsPath1(p1, p2) || existsPath2(p1, p2) || existsPath3(p1, p2);
        }

        private bool existsPath(int cell1, int cell2) {
            return existsPath(cellToPoint(cell1), cellToPoint(cell2));
        }

        public Point[] getPath(int cell1, int cell2) {
            return getPath(cellToPoint(cell1), cellToPoint(cell2));
        }

        private Point cellToPoint(int cell) {
            return new Point(cell % numCells_h, cell / numCells_h);
        }

        public Point[] getPath(Point p1, Point p2) {
            if (p1 == p2)
                return null;
            Point[] path = getPath1(p1, p2);
            path = path == null ? getPath2(p1, p2) : path;
            path = path == null ? getPath3(p1, p2) : path;
            return path;
        }

        public Point[] getHint() {
            int iVal;
            for (int i = 0; i < numCells; i++) {
                iVal = getCell(i).getType();
                if (iVal != BoardCell.CELL_EMPTY) {
                    for (int j = i + 1; j < numCells; j++) {
                        if (getCell(j).getType() == iVal && existsPath(i, j)) {
                            return new Point[] { cellToPoint(i), cellToPoint(j) };
                        }
                    }
                }
            }
            return null;
        }
        
        private void makeSolvable() {
            Point[] hint;
            BoardCell[,] cellsCopy = new BoardCell[numCells_v, numCells_h];
            for (int i = 0; i < numCells_v; i++) {
                for (int j = 0; j < numCells_h; j++) {
                    cellsCopy[i, j] = new BoardCell(cells[i, j].getType());
                }
            }

            while (numCleared < numTiles) {
                //ÈùÆ® ¾ò±â
                while ((hint = getHint()) != null) {
                    clearPair(hint[0], hint[1], null);
                }
                if (numCleared == numTiles)
                    break;

                int cell1 = -1, cell2 = -1, cell3 = -1;
                int cell1Val = -1, iVal;
                for (int i = 0; i < numCells; i++) {
                    iVal = getCell(i).getType();
                    if (iVal != BoardCell.CELL_EMPTY) {
                        if (cell1 == -1) {
                            cell1 = i;
                            cell1Val = iVal;
                        } else if (iVal == cell1Val) {
                            cell2 = i;
                        } else if (getPath(cell1, i) != null) {
                            cell3 = i;
                        }
                        if (cell1 > -1 && cell2 > -1 && cell3 > -1)
                            break;
                    }
                }
                swap(cell2, cell3);
                swap(cell2, cell3, cellsCopy);
                clearPair(cell1, cell3, null);
            }
            cells = cellsCopy;
            numCleared = 0;
        }

        public bool isGameWon() {
            return numCleared == numTiles;
        }
    }
}
