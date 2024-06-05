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
using System.Globalization;
using Telegram.Bot.Requests.Abstractions;
using System.Text.RegularExpressions;
using Azure.AI.OpenAI;
using System.Text.Json.Serialization;
using HtmlAgilityPack;
using System.Linq;
using System.Net;

namespace OptShopAPI.Services
{
    public class BotService
    {
      static TelegramBotClient client;

        private const string token = "6423900666:AAHqwGibyyog-HoNRSADjkdPwv7-QRWWOhA";
       // private const long MyId = 385113590;
        private static long MyChatId = 385113590;

        OpenAIClient openai;

        private string apiKey = "sk-proj-qUDfgm6nJZYxLEHLKOChT3BlbkFJjn3kM6ah8v0dbSYqIyxI";

        public BotService()
        {
            openai = new OpenAIClient("sk-proj-qUDfgm6nJZYxLEHLKOChT3BlbkFJjn3kM6ah8v0dbSYqIyxI");
          
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

            using(var httpClient = new HttpClient())
            {
                var html = httpClient.GetStringAsync(msg.Text).Result;
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(html);


    // Name:
                var ProductNameElement = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='product-title-container']");
                product.name = ProductNameElement.InnerText.Trim();
                product.OriginalLink = msg.Text;

                Console.WriteLine("Product name: \n"+ product.name);


    // Attributes: 
                var ProductCharacteristics = htmlDocument.DocumentNode.Descendants("div").First(node => node.GetAttributeValue("class", "")
                .Contains("attribute-info"));

                foreach (var item in ProductCharacteristics.ChildNodes)
                {
                    if (item.Name == "div")
                    {
                        var list = item.ChildNodes;

                        foreach (var i in list)
                        {
                            var nameNode = i.SelectSingleNode(".//div[@class='left']");
                            var valueNode = i.SelectSingleNode(".//div[@class='right']");

                            if (nameNode != null && valueNode != null)
                            {
                                string attributeName = nameNode.InnerText.Trim();
                                string attributeValue = valueNode.InnerText.Trim();

                                product.characters += attributeName + " " + attributeValue + "<br>";

                                Console.WriteLine($"Attribute: {attributeName}, Value: {attributeValue}");
                            }
                        }
                    } 
                }

                // Photo:
                //  var Photos = htmlDocument.DocumentNode.Descendants("div").First(node => node.GetAttributeValue("class", "")
                // .Contains("detail-product-image-container")); 
              //  var deepNodes = htmlDocument.DocumentNode.SelectSingleNode("//div[@detail-module-name='module_productImage']//div[@class='detail-product-image-container']//div[@class='detail-product-image']");//div[@class='image-list']");//div[@class='image-list-slider']");

              //  PrintNodes(deepNodes);
              /*  httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                var requestBody = new
                {
                    model = "gpt-4",
                    messages = new[]
                    {
                         new {role = "user", content = $"Send me src of all images in image-list: {deepNodes.Descendants()}"}
                     }
                };

                var response = await httpClient.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestBody);
                if (response.IsSuccessStatusCode)
                {
                    var completionResponse = await response.Content.ReadFromJsonAsync<CompletionResponse>();

                    await client.SendTextMessageAsync(e.Message.Chat.Id, completionResponse?.Choices?[0].Message?.Content);
                }*/


                // var imageNodes = PhotoList.SelectNodes(".//img");
                /* if (imageNodes != null)
                 {
                     foreach (var node in imageNodes)
                     {
                         var src = node.GetAttributeValue("src", "");
                         Console.WriteLine(src);
                     }
                 }*/



                // Color:


                // Size:


                // Minimal count: 
                  WebClient wc = new WebClient();
                  byte[] data = wc.DownloadData(msg.Text);
                  string body = Encoding.ASCII.GetString(data);
                Console.WriteLine(body);
            /*
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                var requestBody = new
                {
                    model = "gpt-4",
                    messages = new[]
                    {
                         new {role = "user", content = $"Tell me all colors of this product and links to all images: {body}"}
                     }
                };

                var response = await httpClient.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestBody);
                if (response.IsSuccessStatusCode)
                {
                    var completionResponse = await response.Content.ReadFromJsonAsync<CompletionResponse>();

                    await client.SendTextMessageAsync(e.Message.Chat.Id, completionResponse?.Choices?[0].Message?.Content);
                }
                */

                /*
  var ProductCount = htmlDocument.DocumentNode.Descendants("div").First(node => node.GetAttributeValue("class", "")
                           .Contains("layout-overview"));

                foreach (var item in ProductCount.ChildNodes)
                {
                    if (item.Name == "div")
                    {

                        if (item.GetClasses().Contains("module_sku"))
                        {
                            var list = item.ChildNodes[0];

                            var colorNodes = list.SelectNodes("//div[@class='sku-info']");

                            var cN = colorNodes[0].SelectNodes("//div[@class='info-item']");

                            if (cN != null)
                            {
                                Console.WriteLine("Кольори товару:");
                                foreach (var colorNode in cN)
                                {
                                    Console.WriteLine(colorNode.InnerText.Trim());
                                }
                            }
                            else
                            {
                                Console.WriteLine("Кольори товару не знайдено.");
                            }



                        }
                    }
                }*/

                /*
                var divs = htmlDocument.DocumentNode.Descendants("div").First(node => node.GetAttributeValue("class", "")
                .Contains("layout-overview"));

                Console.WriteLine(divs.InnerHtml);
                */

                /* httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                 var requestBody = new
                 {
                     model = "gpt-4",
                     messages = new[]
                     {
                         new {role = "user", content = $"Tell me all characteristics of this product: {htmlDocument.Text}"}
                     }
                 };

                 var response = await httpClient.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestBody);
                 if (response.IsSuccessStatusCode)
                 {
                     var completionResponse = await response.Content.ReadFromJsonAsync<CompletionResponse>();

                     await client.SendTextMessageAsync(e.Message.Chat.Id, completionResponse?.Choices?[0].Message?.Content);
                 }*/
            }

            /*   if(msg.Text == "/start")
               {
                  // ChatId ids = msg.Chat.Id;
                  await client.SendTextMessageAsync(e.Message.Chat.Id, "Для того, щоб створити товар, пиши /shape, і далі слідуй за тим, що скаже бот.");
               }


               if (msg.Text == "/shape")
               {
                   client.OnMessage -= OnMessageReceiver;
                   client.OnMessage += OnShapingProduct;
                   await client.SendTextMessageAsync(e.Message.Chat.Id, "Введи оригінальний лінк на товар:");
               }*/

            // 385113590

        }

        static void PrintNodes(HtmlNode node, int level = 0)
        {
            
            string indent = new string(' ', level * 2);

          
                Console.WriteLine($"{indent}<{node.Name}>: {node.GetAttributeValue("src", "")} : {node.GetAttributeValue("class","")}");//InnerText.Trim()}");
           
                foreach (var childNode in node.ChildNodes)
                {
                    PrintNodes(childNode, level + 1);
                }
            
        }

        Product product = new Product();
        int property = 0;
        List<Telegram.Bot.Types.File> photos = new List<Telegram.Bot.Types.File>();
        static int photosCount = 0;

        public async void OnAnalyzeProduct(object sender, MessageEventArgs e)
        {
            await client.SendTextMessageAsync(e.Message.Chat.Id, "Введи посилання: ");

        }

        public async void OnShapingProduct(object sender, MessageEventArgs e)
        {
            
            switch (property)
            {
                case 0:
                    product.OriginalLink = e.Message.Text;
                    await client.SendTextMessageAsync(e.Message.Chat.Id, "Введи ім'я товару: ");
                    property += 1;
                    break;
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
                    if (e.Message.Text.Contains(".")) { product.price = double.Parse(e.Message.Text.Replace(".", ",")); }
                    else
                    {
                        product.price = double.Parse(e.Message.Text);
                    }
                    await client.SendTextMessageAsync(e.Message.Chat.Id, "Введи характеристики товару: ");
                    property += 1;
                    break;
                case 4:
                   product.characters = e.Message.Text.Replace("\n", "<br>");
                    await client.SendTextMessageAsync(e.Message.Chat.Id, "Введи кольори товару англійською через пробіл:");
                    property += 1;
                    break;
                case 5:

                    product.color = e.Message.Text.ToLower();
                    await client.SendTextMessageAsync(e.Message.Chat.Id, "Введи мінімальну кількість товару, яку може замовити користувач: ");
                    property += 1;
                    break;
                case 6:
                    product.minimalCount = int.Parse(e.Message.Text);
                    await client.SendTextMessageAsync(e.Message.Chat.Id, "Введи розміри товару через пробіл; якщо їх нема напиши 0:");
                    property += 1;
                    break;

                case 7:
                    if (e.Message.Text != "0")
                    {
                        product.size = e.Message.Text;
                    }
                    await client.SendTextMessageAsync(e.Message.Chat.Id, "Введи кількість зображень, які заллєш: ");
                    property += 1;
                    break;
                case 8:
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
                await client.SendTextMessageAsync(e.Message.Chat.Id, "Товар в базі даних, якщо хочеш додати ще один, напиши /shape");
            }
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
              //  product.characters = product.characters.Replace("\n", "<p>");
                    var request = JsonConvert.SerializeObject(product);

                    var fin = new StringContent(request, Encoding.UTF8, "application/json");
                var result = await client.PostAsync("http://optshopapi:5001/api/Products", fin);
                    i = 0;
                foreach (var s in stream)
                {
                    product.photoFile = new MultipartFormDataContent();
                    product.photoFile.Add(new StreamContent(s), "photo", filesnames[i]);
                    i++;
                    var response = await client.PostAsync("http://optshopapi:5001/api/Products/UploadFile", product.photoFile);

                    await Task.Delay(1000);
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
    }


    public class CompletionResponse
    {
        [JsonPropertyName("choices")]
        public Choice[] Choices { get; set; }
    }

    public class Choice
    {
        [JsonPropertyName("message")]
        public Message Message { get; set; }
    }

    public class Message
    {
        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }
    }

}