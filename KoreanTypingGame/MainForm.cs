using System;
using System.Drawing;
using System.Windows.Forms;

namespace KoreanTypingGame
{
    public class MainForm : Form
    {
        private GameSession gameSession;
        private Label lblCurrentWord;
        private TextBox txtInput;
        private Label lblScore;
        private Label lblTime;
        private Label lblStatus;
        private Button btnStart;
        private Timer gameTimer;

        public MainForm()
        {
            InitializeComponent();
            gameSession = new GameSession();
        }

        private void InitializeComponent()
        {
            this.lblCurrentWord = new Label();
            this.txtInput = new TextBox();
            this.lblScore = new Label();
            this.lblTime = new Label();
            this.lblStatus = new Label();
            this.btnStart = new Button();
            this.gameTimer = new Timer();

            this.SuspendLayout();

            //
            // lblCurrentWord
            //
            this.lblCurrentWord.Font = new Font("Malgun Gothic", 24F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(129)));
            this.lblCurrentWord.Location = new Point(50, 50);
            this.lblCurrentWord.Size = new Size(700, 60);
            this.lblCurrentWord.TextAlign = ContentAlignment.MiddleCenter;
            this.lblCurrentWord.Text = "준비됨";

            //
            // txtInput
            //
            this.txtInput.Font = new Font("Malgun Gothic", 18F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(129)));
            this.txtInput.Location = new Point(200, 150);
            this.txtInput.Size = new Size(400, 40);
            this.txtInput.KeyDown += new KeyEventHandler(this.TxtInput_KeyDown);

            //
            // btnStart
            //
            this.btnStart.Font = new Font("Malgun Gothic", 12F);
            this.btnStart.Location = new Point(350, 220);
            this.btnStart.Size = new Size(100, 40);
            this.btnStart.Text = "시작";
            this.btnStart.Click += new EventHandler(this.BtnStart_Click);

            //
            // lblScore
            //
            this.lblScore.Font = new Font("Malgun Gothic", 12F);
            this.lblScore.Location = new Point(50, 300);
            this.lblScore.Size = new Size(200, 30);
            this.lblScore.Text = "점수: 0";

            //
            // lblTime
            //
            this.lblTime.Font = new Font("Malgun Gothic", 12F);
            this.lblTime.Location = new Point(550, 300);
            this.lblTime.Size = new Size(200, 30);
            this.lblTime.Text = "시간: 00:00";
            this.lblTime.TextAlign = ContentAlignment.TopRight;

            //
            // lblStatus
            //
            this.lblStatus.Font = new Font("Malgun Gothic", 10F);
            this.lblStatus.ForeColor = Color.Gray;
            this.lblStatus.Location = new Point(200, 120);
            this.lblStatus.Size = new Size(400, 20);
            this.lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            this.lblStatus.Text = "";

            //
            // gameTimer
            //
            this.gameTimer.Interval = 1000;
            this.gameTimer.Tick += new EventHandler(this.GameTimer_Tick);

            //
            // MainForm
            //
            this.AutoScaleDimensions = new SizeF(7F, 12F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(800, 400);
            this.Controls.Add(this.lblCurrentWord);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblStatus);
            this.Text = "한글 타자 연습";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            gameSession.StartGame();
            UpdateUI();
            txtInput.Text = "";
            txtInput.Focus();
            gameTimer.Start();
            lblStatus.Text = "타자를 입력하고 엔터를 누르세요.";
        }

        private void TxtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Prevent ding sound
                string input = txtInput.Text;
                bool correct = gameSession.CheckInput(input);

                if (correct)
                {
                    lblStatus.ForeColor = Color.Green;
                    lblStatus.Text = "정답!";
                    txtInput.Text = ""; // Clear input on success
                }
                else
                {
                    lblStatus.ForeColor = Color.Red;
                    lblStatus.Text = "오답입니다. 다시 시도하거나 넘어가세요.";
                    // Optionally clear input or select all
                    txtInput.SelectAll();
                }

                UpdateUI();
            }
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            lblTime.Text = "시간: " + gameSession.GetElapsedTime().ToString(@"mm\:ss");
        }

        private void UpdateUI()
        {
            lblCurrentWord.Text = gameSession.CurrentTarget;
            lblScore.Text = $"점수: {gameSession.Score}";
        }
    }
}
