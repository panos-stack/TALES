using System.Data.SQLite;
using Newtonsoft.Json.Linq;
using NAudio.Wave;
using NAudio.Lame;
using System.Speech.Synthesis;
//using Windows.Media.SpeechSynthesis;

namespace TALES
{
    public partial class Form1 : Form
    {
        private List<Data> dataList = new List<Data>();
        private string defSourceLang = "en";
        // Contains controls measurements on load
        private List<Rectangle> origCtr = new List<Rectangle>();
        Rectangle orFormSize;
        bool play, mute, language;
        int volume;

        IWavePlayer waveOutDevice;
        AudioFileReader audioFileReader;
        AudioFileReader audioFileReader1;

        public Form1()
        {
            InitializeComponent();

            var sql = "SELECT * FROM 'Tales'";
            try
            {
                using (var con = new SQLiteConnection("Data Source=DATA/DataBase.db"))
                {
                    con.Open();
                    using (var com = new SQLiteCommand(sql, con))
                    {
                        using (var r = com.ExecuteReader())
                            while (r.Read())
                            {
                                dataList.Add(new Data(r.GetInt32(0), r.GetString(1), r.GetString(2), r.GetString(3)));
                            }
                        com.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error " + ex.Message);
            }
            //string t = await getTrans(defSourceLang, "el", data.Title);
        }

        private async Task<string> getTrans(string sourceLang, string targetLang, string text)
        {
            string url = "https://api.mymemory.translated.net/get";
            //string sourceLang = "en"; // Αγγλικά
            //string targetLang = "el"; // gr

            // Δημιουργία HTTP client
            using (HttpClient client = new HttpClient())
            {
                // Δημιουργία πλήρους URL με τα query parameters
                string requestUrl = $"{url}?q={Uri.EscapeDataString(text)}&langpair={sourceLang}|{targetLang}";

                try
                {
                    // Κλήση της API
                    HttpResponseMessage response = await client.GetAsync(requestUrl);

                    // Βεβαιώσου ότι η κλήση πέτυχε
                    response.EnsureSuccessStatusCode();

                    // Ανάγνωση του περιεχομένου της απάντησης
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Ανάλυση της JSON απάντησης και εξαγωγή της μετάφρασης
                    JObject jsonResponse = JObject.Parse(responseBody);
                    string translatedText = jsonResponse["responseData"]["translatedText"].ToString();

                    // Εμφάνιση μόνο της μετάφρασης
                    return translatedText;
                }
                catch (Exception e)
                {
                    // Διαχείριση σφαλμάτων
                    return $"Σφάλμα: {e.Message}";
                }
            }
        }

        private void playBtn_Click(object sender, EventArgs e)
        {
            if (!play)
            {   //pause
                waveOutDevice.Pause();
                playBtn.BackgroundImage = Properties.Resources.Pause_button;
            }
            else
            {   //play
                waveOutDevice.Play();
                playBtn.BackgroundImage = Properties.Resources.Play_button;
            }
            play = !play;
        }
        private void volumeBtn_Click(object sender, EventArgs e)
        {
            if (!mute)
            {   //mute
                waveOutDevice.Volume = 0;
                volumeBtn.BackgroundImage = Properties.Resources.Mute;
            }
            else
            {   //unmute
                waveOutDevice.Volume = 1;
                volumeBtn.BackgroundImage = Properties.Resources.Volume;
            }
            mute = !mute;
        }
        //επιλογή ελληνικά/αγγλικά
        private void gr_enBtn_Click(object sender, EventArgs e)
        {
            if (!language)
                label1.Text = "GR";
            else
            {
                label1.Text = "EN";
                //translate
            }
            language = !language;
        }


        private void Form1_Resize(object sender, EventArgs e)
        {
            for (int i = 0; i < Controls.Count; i++)
                resizeControl(origCtr[i], Controls[i]);
        }

        private void resizeControl(Rectangle r, Control c)
        {
            // Finds the form ratio at a given instance
            // Width and Height contain the form's measurements at the time the function is called
            float xRatio = (float)Width / (float)(orFormSize.Width);
            float yRatio = (float)Height / (float)(orFormSize.Height);

            c.Bounds = new Rectangle((int)(r.Location.X * xRatio), (int)(r.Location.Y * yRatio),
                                     (int)(r.Width * xRatio), (int)(r.Height * yRatio));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            play = false;
            mute = false;
            language = false;
            volume = 60;

            foreach (Data data in dataList)
            {
                Item item = new Item(data.Id, data.Title, data.Category, flowLayoutPanel1.Width);
                item.Click += new System.EventHandler(select_Click);
                flowLayoutPanel1.Controls.Add(item);
            }

            //                              \/ resize \/

            //Width, Height, Location.X, Location.Y, contain the form's measurements on load
            orFormSize = new Rectangle(Location.X, Location.Y, Width, Height);
            foreach (Control c in Controls)
                // Takes the bounds of each control and adds it to the list
                origCtr.Add(c.Bounds);
        }

        private void select_Click(dynamic sender, EventArgs e)
        {
            foreach (Item item in flowLayoutPanel1.Controls)
                if (item != sender)
                    if (item.s)
                        item.unSel();
            if (!sender.s)
            {
                sender.sel();
                playStory(sender.Id);
            }
        }

        private async void playStory(int id)
        {
            waveOutDevice?.Dispose();
            audioFileReader?.Dispose();
            using (SpeechSynthesizer synthesizer = new SpeechSynthesizer())
            {
                synthesizer.SetOutputToWaveFile("output.wav");
                if (language)
                {
                    foreach (var voice in synthesizer.GetInstalledVoices())
                    {
                        MessageBox.Show($" - {voice.VoiceInfo.Name}");
                    }
                    //synthesizer.SelectVoice("Microsoft Stephanos");
                    string text = dataList[id].History;
                    if (text.Length > 500)
                    {
                        int s = text.Length / 500;
                        int start = 0;
                        string txt = "";
                        for(int i = 0; i < s; i++)
                        {
                            int end = start + 499;
                            txt += await getTrans(defSourceLang, "el", text.Substring(start, end));
                            start = end;
                            Thread.Sleep(2000);
                        }
                        synthesizer.Speak(txt);
                    }
                    else
                    {
                        string txt = await getTrans(defSourceLang, "el", dataList[id].History);
                        Thread.Sleep(1000);
                        synthesizer.Speak(txt);
                    }

                }
                else
                    synthesizer.Speak(dataList[id].History);
            }

            waveOutDevice = new WaveOut();
            audioFileReader = new AudioFileReader("output.wav");
            waveOutDevice.Init(audioFileReader);

            waveOutDevice.Play();

            playBtn.BackgroundImage = Properties.Resources.Play_button;
            play = true;
        }
    }

    class Item : FlowLayoutPanel
    {
        public bool s = false;
        public int Id;
        private Label Sid;
        private Label c;

        public Item(int id, string title, string cat, int width)
        {
            Id = id;
            BackColor = Color.Transparent;
            Width = width - 19;
            FlowDirection = FlowDirection.TopDown;

            Sid = new Label()
            {
                AutoSize = true,
                Text = id + 1 + ") " + title,
                Font = new Font("French Script MT", 17),
                ForeColor = Color.White
            };

            c = new Label()
            {
                Text = cat,
                Font = new Font("French Script MT", 10),
                ForeColor = Color.White
            };
            Controls.Add(Sid);
            Controls.Add(c);
        }

        public void sel()
        {
            s = true;
            BackColor = Color.FromArgb(80, 255, 255, 255);
            Sid.BackColor = Color.Transparent;
            c.BackColor = Color.Transparent;
        }

        public void unSel()
        {
            s = false;
            c.SendToBack();
            BackColor = Color.Transparent;
            Sid.BackColor = Color.Transparent;
            c.BackColor = Color.Transparent;
        }
    }

    class Picture : PictureBox
    {
        public Picture(int id)
        {
            Width = 100;
            Height = 100;
        }
    }

    class Data
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string History { get; set; }

        public Data(int id, string category, string title, string history)
        {
            Id = id;
            Category = category;
            Title = title;
            History = history;
        }
    }
}