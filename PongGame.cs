using System;
using System.Drawing;
using System.Windows.Forms;

namespace PongGame {
    public partial class Pong : Form {
        private const int RACKET_LENGTH = 60;
        private const int RACKET_WIDTH = 10;
        private const int BALL_SIZE = 10;
        private int leftRacketY = 0;
        private int rightRacketY = 0;
        private int ballX;
        private int ballY;
        private int ballSpeedX = 5;
        private int ballSpeedY = 5;
        private int leftPoints = 0;
        private int rightPoints = 0;

        private bool moveLeftRacketUp = false;
        private bool moveLeftRacketDown = false;
        private bool moveRightRacketUp = false;
        private bool moveRightRacketDown = false;

        public Pong() {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 20;
            timer.Tick += new EventHandler(OnTick);
            timer.Start();
            this.KeyDown += new KeyEventHandler(OnKeyDown);
            this.KeyUp += new KeyEventHandler(OnKeyUp);
            this.BackColor = Color.Black;
            ballX = ClientSize.Width / 2 - BALL_SIZE / 2;
            ballY = ClientSize.Height / 2 - BALL_SIZE / 2;
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            DrawRackets(g);
            DrawBall(g);
            DrawScore(g);
        }

        private void OnTick(object sender, EventArgs e) {
            MoveBall();
            MoveRackets();
            CheckCollisions();
            Invalidate();
        }

        private void OnKeyDown(object sender, KeyEventArgs e) {
            switch (e.KeyCode) {
                case Keys.W:
                    moveLeftRacketUp = true;
                    break;
                case Keys.S:
                    moveLeftRacketDown = true;
                    break;
                case Keys.Up:
                    moveRightRacketUp = true;
                    break;
                case Keys.Down:
                    moveRightRacketDown = true;
                    break;
            }
        }

        private void OnKeyUp(object sender, KeyEventArgs e) {
            switch (e.KeyCode) {
                case Keys.W:
                    moveLeftRacketUp = false;
                    break;
                case Keys.S:
                    moveLeftRacketDown = false;
                    break;
                case Keys.Up:
                    moveRightRacketUp = false;
                    break;
                case Keys.Down:
                    moveRightRacketDown = false;
                    break;
            }
        }

        private void MoveRackets() {
            if (moveLeftRacketUp) {
                leftRacketY = Math.Max(0, leftRacketY - 10);
            }
            if (moveLeftRacketDown) {
                leftRacketY = Math.Min(ClientSize.Height - RACKET_LENGTH, leftRacketY + 10);
            }
            if (moveRightRacketUp) {
                rightRacketY = Math.Max(0, rightRacketY - 10);
            }
            if (moveRightRacketDown) {
                rightRacketY = Math.Min(ClientSize.Height - RACKET_LENGTH, rightRacketY + 10);
            }
        }

        private void DrawRackets(Graphics g) {
            g.FillRectangle(Brushes.White, 10, leftRacketY, RACKET_WIDTH, RACKET_LENGTH);
            g.FillRectangle(Brushes.White, ClientSize.Width - 20, rightRacketY, RACKET_WIDTH, RACKET_LENGTH);
        }

        private void DrawBall(Graphics g) {
            g.FillEllipse(Brushes.White, ballX, ballY, BALL_SIZE, BALL_SIZE);
        }

        private void DrawScore(Graphics g) {
            g.DrawString($"{leftPoints} | {rightPoints}", Font, Brushes.White, ClientSize.Width / 2 - 20, 10);
        }

        private void MoveBall() {
            ballX += ballSpeedX;
            ballY += ballSpeedY;
        }

        private void CheckCollisions() {
            if (ballY <= 0 || ballY >= ClientSize.Height - BALL_SIZE)
            {
                ballSpeedY = -ballSpeedY;
            }

            if (ballX <= 20 && ballY >= leftRacketY && ballY <= leftRacketY + RACKET_LENGTH ||
                ballX >= ClientSize.Width - 30 && ballY >= rightRacketY && ballY <= rightRacketY + RACKET_LENGTH)
            {
                ballSpeedX = -ballSpeedX;
            }

            if (ballX <= 0)
            {
                rightPoints++;
                ResetBall();
            }

            if (ballX >= ClientSize.Width - BALL_SIZE)
            {
                leftPoints++;
                ResetBall();
            }
        }

        private void ResetBall() {
            ballX = ClientSize.Width / 2 - BALL_SIZE / 2;
            ballY = ClientSize.Height / 2 - BALL_SIZE / 2;
            ballSpeedX = -ballSpeedX;
        }
    }
}
