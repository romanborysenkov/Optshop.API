using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System.Drawing;
using ShopManagement;
using System.Runtime.CompilerServices;
using System.IO;

namespace ShopManagemant
{
    public class Program
    {
       private static TelegramBotClient client;

        private const string token = "6423900666:AAHqwGibyyog-HoNRSADjkdPwv7-QRWWOhA";
        private const long MyId = 385113590;
        static void Main()
        {
            client = new TelegramBotClient(token);
            client.OnMessage += OnMessageReceiver;
            client.StartReceiving();
            Console.ReadLine();
            client.StopReceiving();
        }

        public static async void OnMessageReceiver(object sender, MessageEventArgs e)
        {
            var msg = e.Message;

            if (msg.Text == "/shape")
            {
                client.OnMessage -= OnMessageReceiver;
                client.OnMessage += OnShapingProduct;

            }
            // 385113590

        }
       static Product product = new Product();
       static int property = 1;

        public static async void OnShapingProduct(object sender, MessageEventArgs e)
        {
            switch (property)
            {
                case 1:
                    product.name = e.Message.Text;
                    property += 1;
                    break;
                case 2:
                    product.description = e.Message.Text;
                    property += 1;
                    break;
                case 3:
                    product.price = int.Parse(e.Message.Text);
                    property += 1;
                    break;
                case 4:
                    product.characters = e.Message.Text;
                    property += 1;
                    break;
                case 5:

                    product.color = e.Message.Text;
                  
                    property += 1;
                    break;
                case 6:
                    if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Photo)
                    {

                        SendProductAsync(e);
                       // PhotoPost(e);

                    }

                    property = 1;
                    client.OnMessage += OnMessageReceiver;
                    client.OnMessage -= OnShapingProduct;
                    break;

            }
        }

        public static async void SendProductAsync(MessageEventArgs e)
        {
            var file = await client.GetFileAsync(e.Message.Photo[e.Message.Photo.Count() - 1].FileId);

            using (var stream = new MemoryStream())
            {
                await client.DownloadFileAsync(file.FilePath, stream);
                stream.Position = 0;
                using (HttpClient client = new HttpClient())
                {
                    string fileName = GenerateRandomString(8);
                    fileName += ".JPG";
                    product.photoName = fileName;

                    var request = JsonConvert.SerializeObject(product);

                    var fin = new StringContent(request, Encoding.UTF8, "application/json");
                    var result = await client.PostAsync("http://localhost:5255/api/Products/", fin);


                    product.photoFile = new MultipartFormDataContent();
                    //  product.photoFile.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
                    product.photoFile.Add(new StreamContent(stream), "photo", fileName);

                    var response = await client.PostAsync("http://localhost:5255/api/Products/UploadFile", product.photoFile);

                    product = new Product();
                }
            }
        }

        static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();

            // Use LINQ to generate a random string of the specified length
            string randomString = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return randomString;
        }

        public static async void PhotoPost(MessageEventArgs e)
        {
            
               // var photo = e.Message.Photo[^1];
                var file = await client.GetFileAsync(e.Message.Photo[e.Message.Photo.Count() - 1].FileId);
 
                  using (var stream = new MemoryStream())
                  {
                      await client.DownloadFileAsync(file.FilePath, stream);
                      stream.Position = 0;


                          using (HttpClient client = new HttpClient())
                          {

                            
                        product.photoFile = new MultipartFormDataContent();
                        product.photoFile.Add(new StreamContent(stream), "photo", file.FilePath);
                    var fileName = stream.ToString();

                   
                        var response = await client.PostAsync("http://localhost:5255/api/Products/UploadFile", product.photoFile);


                    product = new Product();
                          }

                  }
            
        }
    }
}