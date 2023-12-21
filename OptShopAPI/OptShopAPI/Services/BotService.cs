using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using System.Net.Http;
using System.Threading.Tasks;
using OptShopAPI.Models;
using Newtonsoft.Json;
using System.Text;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace OptShopAPI.Services
{
    public class BotService
    {
        TelegramBotClient client;

        private const string token = "6423900666:AAHqwGibyyog-HoNRSADjkdPwv7-QRWWOhA";
        private const long MyId = 385113590;

        public BotService()
        {
            client = new TelegramBotClient(token);
            client.OnMessage += OnMessageReceiver;
            client.StartReceiving();
        }

        public void StopWork()
        {

           
            Console.ReadLine();
            client.StopReceiving();

        }



        public async void OnMessageReceiver(object sender, MessageEventArgs e)
        {
            var msg = e.Message;

            if(msg.Text == "/start")
            {
               // ChatId ids = msg.Chat.Id;
               await client.SendTextMessageAsync(e.Message.Chat.Id, "Для того, щоб створити товар, пиши /shape, і далі слідуй за тим, що скаже бот.");
            }

            if (msg.Text == "/shape")
            {
                client.OnMessage -= OnMessageReceiver;
                client.OnMessage += OnShapingProduct;
                await client.SendTextMessageAsync(e.Message.Chat.Id, "Введи ім'я товару: ");


            }
            if(msg.Text == "/delete_product")
            {
            client.OnMessage -= OnMessageReceiver;
            client.OnMessage += OnDeleteProduct;
            }
            // 385113590

        }

        Product product = new Product();
        int property = 1;
        List<Telegram.Bot.Types.File> photos = new List<Telegram.Bot.Types.File>();
        static int photosCount = 0;

        public async void OnShapingProduct(object sender, MessageEventArgs e)
        {
            
            switch (property)
            {

                case 1:
                    product.name = e.Message.Text;
                    await client.SendTextMessageAsync(e.Message.Chat.Id, "Введи опис до твого товару. Те, як він буде відформатований в тебе, так і на сайті буде відображатися ");
                    property += 1;
                    break;
                case 2:
                    product.description = e.Message.Text;
                    await client.SendTextMessageAsync(e.Message.Chat.Id, "Введи ціну товару: ");
                    property += 1;
                    break;
                case 3:
                    product.price = int.Parse(e.Message.Text);
                    await client.SendTextMessageAsync(e.Message.Chat.Id, "Введи характеристики товару: ");
                    property += 1;
                    break;
                case 4:
                    product.characters = e.Message.Text;
                    await client.SendTextMessageAsync(e.Message.Chat.Id, "Введи кольори товару в стилі HEX.\n\n Це типу ось так: #274543 \n Визначити код кольору можна, ввівши в гуглі color picker. \n\n І введи всі кольори через пробіл");
                    property += 1;
                    break;
                case 5:

                    product.color = e.Message.Text;
                    await client.SendTextMessageAsync(e.Message.Chat.Id, "Введи кількість зображень, які заллєш: ");
                    property += 1;
                    break;
                case 6:
                  
                     photosCount = int.Parse(e.Message.Text);

                    client.OnMessage += ReceivePhotos;
                    client.OnMessage -= OnShapingProduct;
                    await client.SendTextMessageAsync(e.Message.Chat.Id, "Відправ вказану кількість зображень І ВІДПРАВ ЇХ ОДНИМ ПОВІДОМЛЕННЯМ!!!!");



                    break;

            }     
        }

        public async void ReceivePhotos(object sender, MessageEventArgs e)
        {
           
            if (photosCount > 0)
            {
                photos.Add(await client.GetFileAsync(e.Message.Photo[e.Message.Photo.Count() - 1].FileId));
                photosCount--;
            }
             if (photosCount == 0)
            {
                client.OnMessage += OnMessageReceiver;
                client.OnMessage -= ReceivePhotos;
                property = 1;
                SendProductAsync();
              
            }
          await  client.SendTextMessageAsync(e.Message.Chat.Id, "Товар в базі даних, якщо хочеш додати ще один, напиши /shape");
        }

        public async void SendProductAsync()
        {                
                int i = 0;
                List<string> filesnames = new List<string>();
                     var stream = new List<MemoryStream>();
                    foreach(var photo in photos)
                        {
                     stream.Add(new MemoryStream());
                     await client.DownloadFileAsync(photo.FilePath, stream[i]);
                     stream[i].Position = 0;
                     i++;
                           
                     string fileName = GenerateRandomString(8);
                     fileName += ".JPG";
                     filesnames.Add(fileName);
                     product.photoName += fileName + "  ";
                            
                    }
            photos = new List<Telegram.Bot.Types.File>();

            using (HttpClient client = new HttpClient())
                {


                    var request = JsonConvert.SerializeObject(product);

                    var fin = new StringContent(request, Encoding.UTF8, "application/json");
                    var result = await client.PostAsync("http://localhost:5255/api/Products/", fin);

                    i = 0;
                foreach (var s in stream)
                {
                    product.photoFile = new MultipartFormDataContent();
                    product.photoFile.Add(new StreamContent(s), "photo", filesnames[i]);
                    i++;
                    var response = await client.PostAsync("http://localhost:5255/api/Products/UploadFile", product.photoFile);
                   await Task.Delay(1000);
                    product = new Product();
                }
            } 
        }
        private long MyChatId = 385113590;

        public  async void SendingDataAboutCustomer(string message)
        {
            await client.SendTextMessageAsync(MyChatId, message, Telegram.Bot.Types.Enums.ParseMode.Html);
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

       public void OnDeleteProduct(object sender, MessageEventArgs e){

        }

        }
    }


        

