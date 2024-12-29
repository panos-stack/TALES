namespace TALES
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBox1 = new PictureBox();
            bwBtn = new Button();
            fwBtn = new Button();
            playBtn = new Button();
            volumeBtn = new Button();
            panel2 = new Panel();
            replayBtn = new Button();
            progressBar1 = new ProgressBar();
            label1 = new Label();
            gr_enBtn = new Button();
            flowLayoutPanel1 = new FlowLayoutPanel();
            panel1 = new Panel();
            bookPanel = new Panel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = Properties.Resources.Book_cover;
            pictureBox1.Location = new Point(475, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(400, 450);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // bwBtn
            // 
            bwBtn.BackColor = Color.BurlyWood;
            bwBtn.BackgroundImage = Properties.Resources.Backward_button;
            bwBtn.BackgroundImageLayout = ImageLayout.Stretch;
            bwBtn.Location = new Point(356, 14);
            bwBtn.Margin = new Padding(5);
            bwBtn.Name = "bwBtn";
            bwBtn.Size = new Size(50, 50);
            bwBtn.TabIndex = 8;
            bwBtn.UseVisualStyleBackColor = false;
            // 
            // fwBtn
            // 
            fwBtn.BackColor = Color.BurlyWood;
            fwBtn.BackgroundImage = Properties.Resources.Forward_button;
            fwBtn.BackgroundImageLayout = ImageLayout.Stretch;
            fwBtn.Location = new Point(532, 14);
            fwBtn.Margin = new Padding(5);
            fwBtn.Name = "fwBtn";
            fwBtn.Size = new Size(50, 50);
            fwBtn.TabIndex = 7;
            fwBtn.UseVisualStyleBackColor = false;
            // 
            // playBtn
            // 
            playBtn.BackColor = Color.BurlyWood;
            playBtn.BackgroundImage = Properties.Resources.Play_button;
            playBtn.BackgroundImageLayout = ImageLayout.Stretch;
            playBtn.Location = new Point(416, 14);
            playBtn.Margin = new Padding(5);
            playBtn.Name = "playBtn";
            playBtn.Size = new Size(50, 50);
            playBtn.TabIndex = 0;
            playBtn.UseVisualStyleBackColor = false;
            playBtn.Click += playBtn_Click;
            // 
            // volumeBtn
            // 
            volumeBtn.BackColor = Color.BurlyWood;
            volumeBtn.BackgroundImage = Properties.Resources.Volume;
            volumeBtn.BackgroundImageLayout = ImageLayout.Stretch;
            volumeBtn.Location = new Point(620, 14);
            volumeBtn.Margin = new Padding(5);
            volumeBtn.Name = "volumeBtn";
            volumeBtn.Size = new Size(50, 50);
            volumeBtn.TabIndex = 9;
            volumeBtn.UseVisualStyleBackColor = false;
            volumeBtn.Click += volumeBtn_Click;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Transparent;
            panel2.Controls.Add(replayBtn);
            panel2.Controls.Add(progressBar1);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(gr_enBtn);
            panel2.Controls.Add(volumeBtn);
            panel2.Controls.Add(playBtn);
            panel2.Controls.Add(fwBtn);
            panel2.Controls.Add(bwBtn);
            panel2.Location = new Point(205, 508);
            panel2.Margin = new Padding(0);
            panel2.Name = "panel2";
            panel2.Size = new Size(924, 125);
            panel2.TabIndex = 0;
            // 
            // replayBtn
            // 
            replayBtn.BackColor = Color.BurlyWood;
            replayBtn.BackgroundImage = Properties.Resources.Replay;
            replayBtn.BackgroundImageLayout = ImageLayout.Stretch;
            replayBtn.Location = new Point(474, 14);
            replayBtn.Name = "replayBtn";
            replayBtn.Size = new Size(50, 50);
            replayBtn.TabIndex = 10;
            replayBtn.UseVisualStyleBackColor = false;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(270, 72);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(400, 29);
            progressBar1.TabIndex = 8;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.BurlyWood;
            label1.Location = new Point(226, 22);
            label1.Name = "label1";
            label1.Size = new Size(38, 28);
            label1.TabIndex = 4;
            label1.Text = "GR";
            // 
            // gr_enBtn
            // 
            gr_enBtn.BackColor = Color.BurlyWood;
            gr_enBtn.BackgroundImage = Properties.Resources.GR_EN_icon;
            gr_enBtn.BackgroundImageLayout = ImageLayout.Stretch;
            gr_enBtn.Location = new Point(270, 14);
            gr_enBtn.Name = "gr_enBtn";
            gr_enBtn.Size = new Size(50, 50);
            gr_enBtn.TabIndex = 0;
            gr_enBtn.UseVisualStyleBackColor = false;
            gr_enBtn.Click += gr_enBtn_Click;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.BackColor = Color.Transparent;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(202, 633);
            flowLayoutPanel1.TabIndex = 3;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.Location = new Point(1132, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(202, 633);
            panel1.TabIndex = 5;
            // 
            // bookPanel
            // 
            bookPanel.BackColor = Color.Transparent;
            bookPanel.BackgroundImage = Properties.Resources.Cover;
            bookPanel.BackgroundImageLayout = ImageLayout.Stretch;
            bookPanel.Location = new Point(588, 103);
            bookPanel.Name = "bookPanel";
            bookPanel.Size = new Size(184, 262);
            bookPanel.TabIndex = 7;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            BackgroundImage = Properties.Resources.BG;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1332, 633);
            Controls.Add(bookPanel);
            Controls.Add(panel1);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(panel2);
            Controls.Add(pictureBox1);
            ImeMode = ImeMode.On;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += Form1_Load;
            Resize += Form1_Resize;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private PictureBox pictureBox1;
        private Button bwBtn;
        private Button fwBtn;
        private Button playBtn;
        private Button volumeBtn;
        private Panel panel2;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button gr_enBtn;
        private Label label1;
        private Panel panel1;
        private Panel bookPanel;
        private ProgressBar progressBar1;
        private Button replayBtn;
    }
}
