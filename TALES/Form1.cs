using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Web;
using System.Text.Json;

//using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using NAudio;
using NAudio.Wave;

namespace TALES
{
    public partial class Form1 : Form
    {
        // Contains controls measurements on load
        private List<Rectangle> origCtr = new List<Rectangle>();
        Rectangle orFormSize;

        bool play, mute, language;
        int volume;
        List<string> name = new List<string> { };
        private static string path = "Data.json";
        IWavePlayer waveOutDevice;
        AudioFileReader audioFileReader;

        public Form1()
        {
            InitializeComponent();
            waveOutDevice = new WaveOut();
            //audioFileReader = new AudioFileReader("a.mp3");
            //waveOutDevice.Init(audioFileReader);
        }



        private void playBtn_Click(object sender, EventArgs e)
        {
            if (!play)
            {   //pause
                playBtn.BackgroundImage = Properties.Resources.Pause_button;
                waveOutDevice.Pause();
            }
            else
            {   //play
                playBtn.BackgroundImage = Properties.Resources.Play_button;
                waveOutDevice.Play();
            }
            play = !play;
        }
        private void volumeBtn_Click(object sender, EventArgs e)
        {   
            if (!mute)
            {   //mute
                volumeBtn.BackgroundImage = Properties.Resources.Mute;
                waveOutDevice.Volume = 0;
            }
            else
            {   //unmute
                volumeBtn.BackgroundImage = Properties.Resources.Volume;
                waveOutDevice.Volume = 1;
            }
            mute = !mute;
        }
        //επιλογή ελληνικά/αγγλικά
        private void gr_enBtn_Click(object sender, EventArgs e)
        {
            if (!language)
                label1.Text = "EN";
            else
            {
                label1.Text = "GR";
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
            List<jsonconvert> list = new List<jsonconvert>();


            //Width, Height, Location.X, Location.Y, contain the form's measurements on load
            orFormSize = new Rectangle(Location.X, Location.Y, Width, Height);

            foreach (Control c in Controls)
                // Takes the bounds of each control and adds it to the list
                origCtr.Add(c.Bounds);


            //txt to json
            var filePath = "Untitled2.txt";

            if (File.Exists(filePath))
            {
                string text = File.ReadAllText(filePath);
                string newText = text.Replace("\r", "").Replace("\n", "_");
                File.WriteAllText(filePath, newText);

                text = File.ReadAllText(filePath);
                newText = text.Replace(" ", "_").Replace("\"_", "\" ").Replace("_\"", " \"");//βγάζουμε τα κενά
                File.WriteAllText(filePath, newText);

                int id = 0;
                string cat = "";//category
                string tit = "";//title
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line = sr.ReadLine();

                    string[] words = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                    if (words.Length != 0)
                        for (int i = 0; i < words.Length; i += 2)
                            if (words[i].ToLower() == "\"category\"")
                                cat = words[i + 1].Replace("_", " ");
                            else if (words[i].ToLower() == "\"title\"")
                                tit = words[i + 1].Replace("_", " ");
                            else if (words[i].ToLower() == "\"history\"")
                                list.Add(new jsonconvert(id++, cat, tit, words[i + 1].Replace("_", " ")));
                }
                MessageBox.Show(id.ToString());
            }

            File.WriteAllText(path, JsonSerializer.Serialize(list));

            //json to database
            string file = File.ReadAllText(path);
            var js = JsonSerializer.Deserialize<List<jsonconvert>>(file);
            string connectionString = "Data source = DataBase.db";

            using (var connection = new SQLiteConnection(connectionString))
            {
                //δημιουργούμε το data base
                connection.Open();
                string tableS = @"CREATE TABLE Tales(
                    Id INTEGER NOT NULL, 
                    Category TEXT NOT NULL, 
                    Title TEXT NOT NULL, 
                    History TEXT NOT NULL)";
                using (var command = new SQLiteCommand(tableS, connection))
                {
                    //command.ExecuteNonQuery();
                }
                string insert = "INSERT INTO Tales (Id, Category, Title, History) VALUES (@Id, @Category, @Title, @History)";
                foreach (var item in js)
                {
                    using (var com = new SQLiteCommand(insert, connection))
                    {
                        //παίρνουν τιμές
                        com.Parameters.AddWithValue("@Id", item.Id);
                        com.Parameters.AddWithValue("@Category", item.Category);
                        com.Parameters.AddWithValue("@Title", item.Title);
                        com.Parameters.AddWithValue("@History", item.History);
                        com.ExecuteNonQuery();
                    }
                }
                Console.WriteLine("Ready");
                Console.ReadLine();
            }

            foreach (string i in name)
            {
                flowLayoutPanel1.Controls.Add(new Item(2, i, "a"));
            }
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

    class jsonconvert
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string History { get; set; }

        public jsonconvert(int id, string category, string title, string history)
        {
            Id = id;
            Category = category;
            Title = title;
            History = history;
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
