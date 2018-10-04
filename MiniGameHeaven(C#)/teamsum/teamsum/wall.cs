using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

public enum GAME_STATE
{
    READY,         //게임 준비
    PLAYING,            //게임 진행 중
    GAME_OVER,          //게임 오버
};

public enum OBJECT_TYPE
{
    NONE = -1,
    PLAYER = 0,
    BALL = 1,
    BRICK = 2,
    BRICK_ROCK = 3,
    BRICK_BOMB = 4,
    BRICK_ROTATE = 5,
    BRICK_TURN = 6,
};

public enum SPEED
{
    PLAYER = 20,
    BALL = 5,
};

namespace teamsum
{
    public partial class wall : Form
    {
        static int POOL_SIZE = 600;
        GameObject[] object_pool = new GameObject[POOL_SIZE];
        GameObject player_obj;
        GameObject ball_obj;
        GameObject[][] brick_obj;
        GraphicsUnit units = GraphicsUnit.World;
        RectangleF[][] brickRectF;
        RectangleF ballRectF;
        RectangleF barRectF;
        RectangleF CompareRectF;
        GAME_STATE STATE;
        Matrix matrix = new Matrix();
        int point = 0;
        int playerwidth = 100;
        bool changed = false;
        

        Random r = new Random();

        int i = 0;
        int j = 0;

        Form1 main = null;
        public wall()
        {
            InitializeComponent();
            
        }
        public wall(Form1 main)//벽돌깨기 생성
        {
            InitializeComponent();
            this.main = main;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(10, 10);
            label1.Visible = true;
            label2.Visible = true;
            this.Show();
            STATE = GAME_STATE.READY;
        }

        private void wall_FormClosed(object sender, FormClosedEventArgs e)
        {
            main.Visible = true;
        }

        private void wall_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Transform = matrix;

            if (STATE == GAME_STATE.READY)
            {
                brick_obj = new GameObject[15][];
                brickRectF = new RectangleF[15][];
                ballRectF = new RectangleF();
                barRectF = new RectangleF();

                for (i = 0; i < 15; i++)
                {
                    brick_obj[i] = new GameObject[20];
                    brickRectF[i] = new RectangleF[20];
                }
                createBrick(0, 15);
            }

            if (STATE == GAME_STATE.PLAYING)
            {
                Label_Point.Visible = true;
                Label_Point.Text = "Point : " + point;
                createPlayer();
                g.DrawImage(player_obj.bitmap, player_obj.x, player_obj.y);
                createBall();
                if (point > 0 && point % 50 == 0 && player_obj.width > 50)
                {
                    playerwidth -= 10;
                }
                
                if (point > 0 && point % 50 == 0 && Timer_Ball_Move.Interval > 5 && Timer_Brick_Move.Interval > 5000)
                {
                    Timer_Ball_Move.Interval -= 1;
                    Timer_Brick_Move.Interval -= 1000;
                }
                ballRectF = ball_obj.bitmap.GetBounds(ref units);
                g.DrawImage(ball_obj.bitmap, ball_obj.x, ball_obj.y);
                for (i = 0; i < 15; i++)
                {
                    for (j = 0; j < 20; j++)
                    {
                        if (brick_obj[i][j].bitmap != null)
                        {
                            g.DrawImage(brick_obj[i][j].bitmap, brick_obj[i][j].x, brick_obj[i][j].y);
                        }
                    }
                }
            }
        }
        private void createPlayer()
        {
            if (player_obj != null)
            {
                return;
            }

            player_obj = new GameObject();

            if (player_obj != null)
            {
                player_obj.type = OBJECT_TYPE.PLAYER;
                player_obj.x = ClientSize.Width / 2.0f;
                player_obj.y = ClientSize.Height - 50.0f;
                player_obj.vx = (float)SPEED.PLAYER;
                player_obj.vy = -(float)SPEED.PLAYER;
                player_obj.width = playerwidth;
                player_obj.height = 10;
                player_obj.bitmap = new Bitmap(Image.FromFile("bar.png"), player_obj.width, player_obj.height);
            }
        }

        private void createBall()
        {
            if (ball_obj != null)
            {
                return;
            }

            ball_obj = new GameObject();

            if (ball_obj != null)
            {
                ball_obj.type = OBJECT_TYPE.BALL;
                ball_obj.x = ClientSize.Width / 2.0f;
                ball_obj.y = ClientSize.Height - 200.0f;
                ball_obj.vx = (float)SPEED.BALL;
                ball_obj.vy = -(float)SPEED.BALL;
                ball_obj.width = 20;
                ball_obj.height = 20;
                ball_obj.bitmap = new Bitmap(Image.FromFile("ball.png"), ball_obj.width, ball_obj.height);
            }
        }

        private void createBrick(int top, int bottom)
        {
            int i = 0;
            int j = 0;
            for (i = top; i < bottom; i++)
            {
                for (j = 0; j < 20; j++)
                {
                    if (brick_obj[i][j] != null)
                    {
                        return;
                    }
                    brick_obj[i][j] = new GameObject();

                    if (brick_obj[i][j] != null)
                    {
                        brick_obj[i][j].x = j * ClientSize.Width / 20.0f;
                        brick_obj[i][j].y = (-10 + i) * ClientSize.Width / 20.0f;
                        brick_obj[i][j].vx = 0;
                        brick_obj[i][j].vy = 0;
                        brick_obj[i][j].width = ClientSize.Width / 20;
                        brick_obj[i][j].height = ClientSize.Width / 20;
                        if (r.Next() % 30 == 1)
                        {
                            brick_obj[i][j].bitmap = new Bitmap(Image.FromFile("brick_Bomb.png"), brick_obj[i][j].width, brick_obj[i][j].height);
                            brick_obj[i][j].type = OBJECT_TYPE.BRICK_BOMB;
                        }
                        else if (r.Next() % 15 == 1)
                        {
                            brick_obj[i][j].type = OBJECT_TYPE.BRICK_ROCK;
                            brick_obj[i][j].bitmap = new Bitmap(Image.FromFile("brick_Rock.png"), brick_obj[i][j].width, brick_obj[i][j].height);
                        }
                        else if (r.Next() % 40 == 1)
                        {
                            brick_obj[i][j].type = OBJECT_TYPE.BRICK_TURN;
                            brick_obj[i][j].bitmap = new Bitmap(Image.FromFile("brick_Turn.png"), brick_obj[i][j].width, brick_obj[i][j].height);
                        }
                        else
                        {
                            brick_obj[i][j].bitmap = new Bitmap(Image.FromFile("brick.png"), brick_obj[i][j].width, brick_obj[i][j].height);
                            brick_obj[i][j].type = OBJECT_TYPE.BRICK;
                        }
                        brickRectF[i][j] = brick_obj[i][j].bitmap.GetBounds(ref units);
                    }
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (STATE == GAME_STATE.PLAYING)
            {
                if (e.KeyCode == Keys.Right && player_obj.x <= ClientSize.Width - 10 - player_obj.width)
                {
                    if (player_obj.vx < 0)
                        player_obj.vx = -player_obj.vx;
                    player_obj.x += player_obj.vx;
                    Invalidate();
                }
                else if (e.KeyCode == Keys.Left && player_obj.x >= 10)
                {
                    if (player_obj.vx > 0)
                        player_obj.vx = -player_obj.vx;
                    player_obj.x += player_obj.vx;
                    Invalidate();
                }
                if (r.Next() % 2 == 1)
                    BallMoving();
            }
            else if (STATE == GAME_STATE.READY)
            {
                if (e.KeyCode == Keys.Space)
                {
                    STATE = GAME_STATE.PLAYING;
                    label1.Visible = false;
                    label2.Visible = false;
                    Timer_Brick_Move.Start();
                    Timer_Ball_Move.Start();
                    
                }
            }
            else if (STATE == GAME_STATE.GAME_OVER)
            {
                if (e.KeyCode == Keys.Space)
                {
                    this.Close();
                }
            }
        }

        private void BallMoving()
        {
            if (ball_obj.vx > 0)
            {
                if (ball_obj.x > ClientSize.Width - 30) //오른쪽 벽 맞았을 때
                    ball_obj.vx = -ball_obj.vx;
            }
            else
            {
                if (ball_obj.x < 0) // 왼쪽 벽 맞았을 때
                    ball_obj.vx = -ball_obj.vx;
            }

            if (ball_obj.vy < 0)
            {
                if (ball_obj.y < 0)// 위의 벽을 맞았을 때
                    ball_obj.vy = -ball_obj.vy;
            }
            else
            {
                if (ball_obj.y > ClientSize.Height + 20) // 죽었을 경우!!!
                {
                    STATE = GAME_STATE.GAME_OVER;
                    game_over();
                }

            }
            CheckBrickCollision();
            ball_obj.x += ball_obj.vx;
            ball_obj.y += ball_obj.vy;
            Invalidate();
        }

        void CheckBrickCollision()
        {
            changed = false;
            if (ball_obj.y < brick_obj[14][0].y + brick_obj[14][0].height)
            {
                for (int i = 0; i < 15; i++)
                {
                    for (int j = 0; j < 20; j++)
                    {
                        if (ball_obj.y + ball_obj.height > brick_obj[i][j].y && ball_obj.y < brick_obj[i][j].y + brick_obj[i][j].height && ball_obj.x + ball_obj.width > brick_obj[i][j].x && ball_obj.x < brick_obj[i][j].x + brick_obj[i][j].width && changed == false)
                        {
                            CompareRectF = RectangleF.Intersect(ballRectF, brickRectF[i][j]);
                            if (!CompareRectF.IsEmpty && brick_obj[i][j] != null)
                            {
                                if ((ball_obj.x < brick_obj[i][j].x && ball_obj.x + ball_obj.width > brick_obj[i][j].x && brick_obj[i][j].y < ball_obj.y && brick_obj[i][j].y + brick_obj[i][j].width > ball_obj.y + ball_obj.width) ||
                                    (ball_obj.x + ball_obj.width > brick_obj[i][j].x + brick_obj[i][j].width && ball_obj.x > brick_obj[i][j].x && ball_obj.y < brick_obj[i][j].y && ball_obj.y + ball_obj.height < brick_obj[i][j].y + brick_obj[i][j].height))
                                {
                                    brickbreaked(i, j);
                                    ball_obj.vx = -ball_obj.vx;
                                }
                                else if ((ball_obj.y + ball_obj.height > brick_obj[i][j].y && ball_obj.y > brick_obj[i][j].y && ball_obj.x > brick_obj[i][j].x && brick_obj[i][j].x + brick_obj[i][j].width > ball_obj.x + ball_obj.width) ||
                                    (ball_obj.x > brick_obj[i][j].x && ball_obj.x + ball_obj.width < brick_obj[i][j].x + brick_obj[i][j].width && ball_obj.y + ball_obj.height > brick_obj[i][j].height + brick_obj[i][j].y && brick_obj[i][j].y > ball_obj.y))
                                {
                                    brickbreaked(i, j);
                                    ball_obj.vy = -ball_obj.vy;
                                }
                                else if (ball_obj.x < brick_obj[i][j].x && ball_obj.y + ball_obj.height > brick_obj[i][j].y)
                                {
                                    brickbreaked(i, j);
                                    if (ball_obj.x + ball_obj.width - brick_obj[i][j].x < ball_obj.y + ball_obj.height - brick_obj[i][j].y)
                                        ball_obj.vx = -ball_obj.vx;
                                    else if((ball_obj.x + ball_obj.width - brick_obj[i][j].x > ball_obj.y + ball_obj.height - brick_obj[i][j].y))
                                        ball_obj.vy = -ball_obj.vy;
                                    else
                                    {
                                        ball_obj.vx = -ball_obj.vx;
                                        ball_obj.vy = -ball_obj.vy;
                                    }
                                }
                                else if (ball_obj.x < brick_obj[i][j].x && ball_obj.y + ball_obj.height > brick_obj[i][j].y)
                                {
                                    brickbreaked(i, j);
                                    if (brick_obj[i][j].x + brick_obj[i][j].width - ball_obj.x < ball_obj.y + ball_obj.height - brick_obj[i][j].y)
                                        ball_obj.vx = -ball_obj.vx;
                                    else if ((brick_obj[i][j].x + brick_obj[i][j].width - ball_obj.x > ball_obj.y + ball_obj.height - brick_obj[i][j].y))
                                        ball_obj.vy = -ball_obj.vy;
                                    else
                                    {
                                        ball_obj.vx = -ball_obj.vx;
                                        ball_obj.vy = -ball_obj.vy;
                                    }
                                }
                                else if (ball_obj.x + ball_obj.width > brick_obj[i][j].x && ball_obj.y < brick_obj[i][j].y + brick_obj[i][j].height)
                                {
                                    brickbreaked(i, j);
                                    if (brick_obj[i][j].x + brick_obj[i][j].width - ball_obj.x < (brick_obj[i][j].height + brick_obj[i][j].y) - ball_obj.y)
                                        ball_obj.vx = -ball_obj.vx;
                                    else if(brick_obj[i][j].x + brick_obj[i][j].width - ball_obj.x > (brick_obj[i][j].height + brick_obj[i][j].y) - ball_obj.y)
                                        ball_obj.vy = -ball_obj.vy;
                                    else
                                    {
                                        ball_obj.vx = -ball_obj.vx;
                                        ball_obj.vy = -ball_obj.vy;
                                    }
                                }
                                else if (ball_obj.x + ball_obj.width > brick_obj[i][j].x && ball_obj.y < brick_obj[i][j].y + brick_obj[i][j].height)
                                {
                                    brickbreaked(i, j);
                                    if (ball_obj.y + ball_obj.height - brick_obj[i][j].y < ball_obj.vy / ball_obj.vx * (ball_obj.x - brick_obj[i][j].x))
                                        ball_obj.vx = -ball_obj.vx;
                                    else if(brick_obj[i][j].x + brick_obj[i][j].width - ball_obj.x > (brick_obj[i][j].height + brick_obj[i][j].y) - ball_obj.y)
                                        ball_obj.vy = -ball_obj.vy;
                                    else
                                    {
                                        ball_obj.vx = -ball_obj.vx;
                                        ball_obj.vy = -ball_obj.vy;
                                    }
                                }

                            }
                        }
                    }
                }
            }

            if (ball_obj.x >= player_obj.x && ball_obj.x + ball_obj.width <= player_obj.x + player_obj.width && ClientSize.Height - (ball_obj.y + ball_obj.height) < ClientSize.Height - player_obj.y && ClientSize.Height - (ball_obj.y + ball_obj.height) < ClientSize.Height - (player_obj.y + player_obj.height) && ball_obj.vy > 0)
            {
                CompareRectF = RectangleF.Intersect(ballRectF, barRectF);
                if (CompareRectF.IsEmpty)
                {
                    if (ball_obj.vy > 0 && ball_obj.x > player_obj.x && ball_obj.x + ball_obj.width < player_obj.x + player_obj.width && ball_obj.y + ball_obj.height > player_obj.height + player_obj.y && player_obj.y > ball_obj.y)
                    {
                        ball_obj.y -= ball_obj.vy;
                        ball_obj.vy = -ball_obj.vy;
                    }
                }
            }
        }


        private void Timer_Ball_Move_Tick(object sender, EventArgs e)
        {
            if (STATE == GAME_STATE.PLAYING)
            {
                BallMoving();
                if (CheckAllBreaked())
                {
                    for (i = 0; i < 15; i++)
                    {
                        brick_obj[i] = new GameObject[20];
                        brickRectF[i] = new RectangleF[20];
                    }
                    createBrick(0, 15);
                }
            }
        }

        private void Timer_Brick_Move_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < 15; i++)
                for (int j = 0; j < 20; j++)
                {
                    brick_obj[i][j].y += ClientSize.Width / 20;
                }
        }

        private void game_over()
        {
            Timer_Brick_Move.Stop();
            Timer_Ball_Move.Stop();
            label3.Visible = true;
            label5.Text = "Your Point : " + point;
            label5.Visible = true;
            label4.Visible = true;
        }

        private bool CheckAllBreaked()
        {
            for (i = 0; i < 15; i++)
                for (j = 0; j < 20; j++)
                {
                    if (brick_obj[i][j].bitmap != null || (brick_obj[i][j].type == OBJECT_TYPE.BRICK_ROCK && brick_obj[i][j].bitmap != null))
                        return false;
                }
            return true;
        }

        private void brickbreaked(int i, int j)
        {
            ball_obj.x -= ball_obj.vx;
            ball_obj.y -= ball_obj.vy;
            if (brick_obj[i][j].type == OBJECT_TYPE.BRICK_BOMB)
            {
                for (int k = 0; k < 15; k++)
                    if (brick_obj[k][j].bitmap != null)
                    {
                        brick_obj[k][j].bitmap = null;
                        brickRectF[k][j].Width = 0;
                        brickRectF[k][j].Height = 0;
                        point += 5;
                    }
                for (int l = 0; l < 20; l++)
                    if (brick_obj[i][l].bitmap != null)
                    {
                        brick_obj[i][l].bitmap = null;
                        brickRectF[i][l].Width = 0;
                        brickRectF[i][l].Height = 0;
                        point += 5;
                    }
            }
            else if (brick_obj[i][j].type == OBJECT_TYPE.BRICK_ROCK)
            {

            }
            else if (brick_obj[i][j].type == OBJECT_TYPE.BRICK_TURN)
            {
                brickRectF[i][j].Width = 0;
                brickRectF[i][j].Height = 0;
                brick_obj[i][j].bitmap = null;
                point += 5;
                rotatewindow();
            }
            else
            {
                brickRectF[i][j].Width = 0;
                brickRectF[i][j].Height = 0;
                brick_obj[i][j].bitmap = null;
                point += 5;
            }
            changed = true;
        }

        private void rotatewindow()
        {
            PointF middlepoint = new PointF(ClientSize.Width/2, ClientSize.Height/2);
            matrix.RotateAt(180, middlepoint);
            Invalidate();
        }
    }
}