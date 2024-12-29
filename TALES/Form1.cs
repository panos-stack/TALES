namespace TALES
{
    public partial class Form1 : Form
    {
        bool play, volume, language;
        List<string> name = new List<string> { };
        public Form1()
        {
            InitializeComponent();
        }

        private void playBtn_Click(object sender, EventArgs e)
        {
            if (!play)
                playBtn.BackgroundImage = Properties.Resources.Pause_button;
            else
                playBtn.BackgroundImage = Properties.Resources.Play_button;
            play = !play;
        }
        private void volumeBtn_Click(object sender, EventArgs e)
        {
            if (!volume)
                volumeBtn.BackgroundImage = Properties.Resources.Mute;
            else
                volumeBtn.BackgroundImage = Properties.Resources.Volume;
            volume = !volume;
        }

        private void gr_enBtn_Click(object sender, EventArgs e)
        {
            if (!language)
                label1.Text = "EN";
            else
                label1.Text = "GR";
            language = !language;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            play = volume = language = false;
            foreach (string i in name)
            {
                flowLayoutPanel1.Controls.Add(new Item(2, i, "a"));
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {

        }
    }

    class Item : Panel
    {
        public Item(int id, string tit, string cat)
        {
            BackColor = Color.White;
            Width = 100;
            Height = 100;
            Label lb = new Label();
            lb.Text = tit;
            Controls.Add(lb);
        }
    }

    class Picture : PictureBox
    {
        public Picture(int id)
        {
            Width = 100;
            Height = 100;
            PictureBox pic = new PictureBox();
            Controls.Add(pic);
        }
    }
}
