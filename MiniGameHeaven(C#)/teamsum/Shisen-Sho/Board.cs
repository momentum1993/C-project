using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Shisen_Sho {
    public partial class Board : UserControl {
        private Graphics graphics, offscr_g;
        private Bitmap offscrBitmap;
        private int num_tiles_h;
        private int num_tiles_v;
        private Grid grid;
        private Point selectedPt;
        private Point clickedPt;
        private Point[] path;
        private Brush brushHighlight;
        private Pen pathPen;
        private Pen pathErasePen;

        private bool gameStarted = false;
        private int gameTime = 0;
        private bool cheated = false;

        private const int TILE_WIDTH = 40;
        private const int TILE_HEIGHT = 56;
        private const int TILE_MARGIN = 0;
        private const int BOARD_MARGIN = 20;

        private const float PATH_WIDTH = 6;
        private const int HIGHLIGHT_TRANSPARENCY = 130;

        public EventHandler gameWon;
        public EventHandler gameLost;
        public EventHandler cheatedChanged;

        public static Tile[] pieces = {
            new Tile("A"),
            new Tile("B"),
            new Tile("C"),
            new Tile("D"),
            new Tile("E"),
            new Tile("F"),
            new Tile("G"),
            new Tile("H"),
            new Tile("I"),
            new Tile("J"),
            new Tile("K"),
            new Tile("L"),
            new Tile("M"),
            new Tile("N"),
            new Tile("O"),
            new Tile("P"),
            new Tile("Q"),
            new Tile("R"),
            new Tile("S"),
            new Tile("T"),
            new Tile("U"),
            new Tile("V"),
            new Tile("W"),
            new Tile("X"),
            new Tile("Y"),
            new Tile("Z"),
            new Tile("1"),
            new Tile("2"),
            new Tile("3"),
            new Tile("4"),
            new Tile("5"),
            new Tile("6"),
            new Tile("7"),
            new Tile("8"),
            new Tile("9"),
            new Tile("0")
        };

        public Board() {
            InitializeComponent();
        }

        public event EventHandler OnGameWon {
            add {
                gameWon += value;
            }
            remove {
                gameWon -= value;
            }
        }

        public event EventHandler OnGameLost {
            add {
                gameLost += value;
            }
            remove {
                gameLost -= value;
            }
        }

        public event EventHandler OnCheatedChanged {
            add {
                cheatedChanged += value;
            }
            remove {
                cheatedChanged -= value;
            }
        }

        public void init(int num_tiles_v, int num_tiles_h) {
            setDimensions(num_tiles_v, num_tiles_h);
            grid.reset();
            brushHighlight = new SolidBrush(Color.FromArgb(HIGHLIGHT_TRANSPARENCY, Color.Black));
            pathPen = new Pen(Color.Black, PATH_WIDTH);
            pathErasePen = new Pen(Color.White, PATH_WIDTH);
            selectedPt = Point.Empty;

            this.BackColor = Color.White;
            paintBoard();
        }

        public void setDimensions(int num_tiles_v, int num_tiles_h) {
            if (num_tiles_v != this.num_tiles_v || num_tiles_h != this.num_tiles_h) {
                this.num_tiles_h = num_tiles_h;
                this.num_tiles_v = num_tiles_v;
                this.Width = BOARD_MARGIN * 2 + TILE_MARGIN + (TILE_WIDTH + TILE_MARGIN) * num_tiles_h;
                this.Height = BOARD_MARGIN * 2 + TILE_MARGIN + (TILE_HEIGHT + TILE_MARGIN) * num_tiles_v;
                grid = new Grid(num_tiles_v, num_tiles_h);
                graphics = this.CreateGraphics();
                graphics.Clear(this.BackColor);
                offscrBitmap = new Bitmap(this.Width, this.Height);
                offscr_g = Graphics.FromImage(offscrBitmap);
            }
        }

        public void reset() {
            grid.reset();
            gameStarted = false;
            tmrGame.Enabled = false;
            setCheated(false);
            selectedPt = clickedPt = Point.Empty;
            gameTime = 0;
            erasePath();
            paintBoard();
            copyBoard();
        }

        public bool isCheated() {
            return cheated;
        }

        private void setCheated(bool cheated) {
            this.cheated = cheated;
            cheatedChanged(this, EventArgs.Empty);
        }

        public bool isGameStarted() {
            return gameStarted;
        }

        public int getGameTime() {
            return gameTime;
        }

        public int incGameTime() {
            return ++gameTime;
        }

        private void paintBoard(Rectangle paintRect) {
            for (int i = 1; i <= num_tiles_v; i++) {
                for (int j = 1; j <= num_tiles_h; j++) {
                    drawTile(i, j);
                }
            }
        }

        private void paintBoard() {
            paintBoard(Rectangle.Empty);
        }

        private void copyBoard(Rectangle paintRect) {
            if (offscrBitmap != null) {
                if (paintRect != Rectangle.Empty) {
                    graphics.DrawImage(offscrBitmap, paintRect, paintRect, GraphicsUnit.Pixel);
                } else {
                    graphics.DrawImage(offscrBitmap, 0, 0);
                }
            }
        }

        private void copyBoard() {
            copyBoard(Rectangle.Empty);
        }

        //tile highlight rect
        private Rectangle getTileHRect(int y, int x) {
            return new Rectangle(BOARD_MARGIN + TILE_MARGIN / 2 + (x - 1) * (TILE_WIDTH + TILE_MARGIN),
                BOARD_MARGIN + TILE_MARGIN / 2 + (y - 1) * (TILE_HEIGHT + TILE_MARGIN),
                TILE_WIDTH + TILE_MARGIN,
                TILE_HEIGHT + TILE_MARGIN);
        }

        //tile paint location
        private Point getTileLoc(int y, int x) {
            return new Point(BOARD_MARGIN + TILE_MARGIN + (x - 1) * (TILE_WIDTH + TILE_MARGIN),
                BOARD_MARGIN + TILE_MARGIN + (y - 1) * (TILE_HEIGHT + TILE_MARGIN));
        }

        private Point getTileCenter(int y, int x) {
            Point tileLoc = getTileLoc(y, x);
            tileLoc.Y += TILE_HEIGHT / 2;
            tileLoc.X += TILE_WIDTH / 2;
            if (y <= 0 || x <= 0 || y > num_tiles_v || x > num_tiles_h) {
                if (y == 0)
                    tileLoc.Y = BOARD_MARGIN / 2;
                if (x == 0)
                    tileLoc.X = BOARD_MARGIN / 2;
                if (y == num_tiles_v + 1)
                    tileLoc.Y = BOARD_MARGIN + TILE_MARGIN + (TILE_MARGIN + TILE_HEIGHT) * num_tiles_v + BOARD_MARGIN / 2;
                if (x == num_tiles_h + 1)
                    tileLoc.X = BOARD_MARGIN + TILE_MARGIN + (TILE_MARGIN + TILE_WIDTH) * num_tiles_h + BOARD_MARGIN / 2;
            }
            return tileLoc;
        }

        private Point getTileCenter(Point pt) {
            return getTileCenter(pt.Y, pt.X);
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            copyBoard(e.ClipRectangle);
        }

        private void drawTile(int y, int x) {
            Point loc = getTileLoc(y, x);
            BoardCell cell = grid.getCell(y, x);
            Rectangle tileHRect = getTileHRect(y, x);
            eraseTile(y, x);
            offscr_g.DrawImage(pieces[cell.getType()].getImage(), loc);
            if (cell.isHighlighted()) {
                offscr_g.FillRectangle(brushHighlight, tileHRect);
            }
        }

        private void drawTile(Point pt) {
            drawTile(pt.Y, pt.X);
        }

        private void eraseTile(int y, int x) {
            Rectangle tileHRect = getTileHRect(y, x);
            offscr_g.FillRectangle(Brushes.White, tileHRect);
        }

        private void eraseTile(Point pt) {
            eraseTile(pt.Y, pt.X);
        }

        private void updateTiles(LinkedList<Point> liChanged) {
            foreach (Point pt in liChanged) {
                if (grid.isCellEmpty(pt))
                    eraseTile(pt);
                else
                    drawTile(pt);
            }
        }

        public bool drawHint() {
            setCheated(true);
            if (tmrPath.Enabled)
                return true;
            erasePath();
            Point[] pts;
            if ((pts = grid.getHint()) != null) {
                path = grid.getPath(pts[0], pts[1]);
                selectedPt = Point.Empty;
                LinkedList<Point> llChanged = grid.unHighlightAll();
                updateTiles(llChanged);
                grid.highlightCell(path[0]);
                grid.highlightCell(path[path.Length - 1]);
                drawPath();
                copyBoard();
                return true;
            }
            return false;
        }

        private void Board_MouseDown(object sender, MouseEventArgs e) {
            int tile_x = 1 + (e.X - BOARD_MARGIN - TILE_MARGIN) / (TILE_WIDTH + TILE_MARGIN);
            int tile_y = 1 + (e.Y - BOARD_MARGIN - TILE_MARGIN) / (TILE_HEIGHT + TILE_MARGIN);
            tile_x = tile_x > num_tiles_h ? -1 : tile_x;
            tile_y = tile_y > num_tiles_v ? -1 : tile_y;

            if (tile_x >= 0 && tile_y >= 0 && e.X >= BOARD_MARGIN && e.Y >= BOARD_MARGIN
                && !tmrPath.Enabled && !grid.isCellEmpty(tile_y, tile_x)) {
                clickedPt = new Point(tile_x, tile_y);
                erasePath();
                LinkedList<Point> llChanged = new LinkedList<Point>();
                if (e.Button == MouseButtons.Right) {
                    selectedPt = Point.Empty;
                    llChanged = grid.changeHighlightType(clickedPt);
                } else {
                    if (selectedPt != Point.Empty) {
                        if (selectedPt == clickedPt) {
                            grid.unHighlightCell(selectedPt);
                            drawTile(selectedPt);
                            selectedPt = Point.Empty;
                        } else if (grid.getCell(clickedPt).getType() == grid.getCell(selectedPt).getType()) {
                            path = grid.getPath(selectedPt, clickedPt);
                            if (path != null) {
                                grid.highlightCell(selectedPt);
                                grid.highlightCell(clickedPt);
                                drawPath();
                                tmrPath.Enabled = true;
                                tmrGame.Enabled = true;
                                gameStarted = true;
                            }
                        }
                    } else {
                        llChanged = grid.unHighlightAll();
                        llChanged.AddFirst(clickedPt);
                        grid.highlightCell(clickedPt);
                        selectedPt = clickedPt;
                    }
                }
                updateTiles(llChanged);
                copyBoard();
            }
        }

        private void drawPath() {
            drawTile(path[0]);
            drawTile(path[path.Length - 1]);
            drawPathLine(pathPen);
        }

        private void erasePath() {
            if (path != null) {
                drawPathLine(pathErasePen);
                drawTile(path[0]);
                drawTile(path[path.Length - 1]);
                path = null;
            }
        }

        private void drawPathLine(Pen pen) {
            Point center2 = getTileCenter(path[0]);
            Point center1;
            for (int i = 0; i < path.Length - 1; i++) {
                center1 = center2;
                center2 = getTileCenter(path[i + 1]);
                offscr_g.DrawLine(pen, center1, center2);
            }
        }

        private void drawGameWon() {
            string gameWon = "게임 종료";
            string yourTime = "걸린 시간: " + getGameTime();
            string youCheated = "(힌트를 썼기 때문에 기록되지 않습니다.)";
            Font gwFont = new Font("Verdana", 30, FontStyle.Bold);
            Font ytFont = new Font("Verdana", 25, FontStyle.Bold);
            Font ycFont = new Font("Verdana", 15, FontStyle.Bold);
            SizeF gwSize = graphics.MeasureString(gameWon, gwFont);
            SizeF ytSize = graphics.MeasureString(yourTime, ytFont);
            SizeF ycSize = graphics.MeasureString(youCheated, ycFont);
            PointF gwLoc =  new PointF((this.Width - gwSize.Width - (isCheated() ? ycSize.Width : 0)) / 2, this.Height / 2 - gwSize.Height);
            PointF ytLoc =  new PointF((this.Width - ytSize.Width) / 2, this.Height / 2);
            PointF ycLoc = new PointF(gwLoc.X + gwSize.Width , gwLoc.Y + ycSize.Height/2);

            offscr_g.DrawString(gameWon, gwFont, Brushes.Navy, gwLoc);
            offscr_g.DrawString(yourTime, ytFont, Brushes.Orange, ytLoc);
            if(isCheated())
                offscr_g.DrawString(youCheated, ycFont, Brushes.Navy, ycLoc);
            copyBoard();
        }

        private void Board_Load(object sender, EventArgs e)
        {

        }

        private void tmrPath_Tick(object sender, EventArgs e) {
            tmrPath.Enabled = false;
            erasePath();
            LinkedList<Point> llChanged = grid.clearPair(selectedPt, clickedPt);
            selectedPt = Point.Empty;
            clickedPt = Point.Empty;
            updateTiles(llChanged);
            copyBoard();
            if (grid.isGameWon()) {
                tmrGame.Enabled = false;
                gameStarted = false;
                drawGameWon();
                gameWon(this, EventArgs.Empty);
            } else if (grid.getHint() == null) {
                gameStarted = false;
                tmrGame.Enabled = false;
                gameLost(this, EventArgs.Empty);
            }
        }
    }
}
