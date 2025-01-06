
using System.Net;
using Newtonsoft.Json;
using System.ComponentModel.Design.Serialization;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace PictureGenerator
{
    public partial class Form1 : Form
    {
        private PictureBox pictureBox;
        private Button button;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            pictureBox = new PictureBox();
            pictureBox.Size = new Size(500, 500); //asettaa kuvaboksin koon
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom; //mahdollistaa sen ett‰ kuva on annetun koon mukainen suurentamalla tai pienent‰m‰ll‰ kuvaa

            this.Controls.Add(pictureBox);

            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FetchNewImageDog();

        }
        private void FetchNewImageDog()
        {
            pictureBox.Update(); //Tyhjent‰‰ edellisen kuvan pois update-metodilla, koska Clear tai Empty‰ metodia ei ole
            string url = "http://shibe.online/api/shibes?count=1&urls=true&httpsUrls=true"; //Sivusto, mist‰ haetaan kuvat Shiba Inuista, eli API
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url); //tekee web-haun annetulle nettisivulle
            HttpWebResponse response = (HttpWebResponse)request.GetResponse(); //l‰hett‰‰ hhtp-pyyntˆj‰ ja vastaanottaa http
            Stream stream = response.GetResponseStream(); //data stream HHTP responsesta
            StreamReader reader = new StreamReader(stream);
            string json = reader.ReadToEnd();
            //parse the json to get image url
            var imageUrl = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(json).FirstOrDefault(); //k‰ytet‰‰n newtonsoftin kirjastoa JSON vastauksen j‰sent‰miseen listaksi
            //var imageurl tallentaa listan jossa on URL osoitteen kuva.
            pictureBox.ImageLocation = imageUrl;


            this.Controls.Add(pictureBox);
        }

        private void FetchNewImageFox()
        {
            pictureBox.Update();
            string url = "https://randomfox.ca/floof/";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string json = reader.ReadToEnd();
            JObject image = JObject.Parse(json); //JSON objekti image parsetetaan
            pictureBox.Load(image["image"].ToString());  //tekee JSON haun objektia nimelt‰ image



            this.Controls.Add(pictureBox);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            FetchNewImageFox();
        }
    }
}