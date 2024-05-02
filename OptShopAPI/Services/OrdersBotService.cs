using Newtonsoft.Json;
using OptShopAPI.Models;
using System.Text;
using System.Text.RegularExpressions;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace OptShopAPI.Services
{
    public class OrdersBotService
    {
        static TelegramBotClient client;

        private const string token = "6548281536:AAE6qx2lrTZPudHi33E7-JfIyc0OfDJHqRo";
        private static long MyChatId = 385113590;

        public OrdersBotService()
        {
            client = new TelegramBotClient(token);
            client.OnMessage += SumariseDeliveryPrice;
            client.StartReceiving();
        }

        public void StopWork()
        {
            Console.ReadLine();
            client.StopReceiving();
        }

        static string paymentid = "";

        public async void SendingDataAboutCustomer(string message, string paymentId="")
        {
            paymentid = paymentId;
            try
            {
                client.OnMessage += SumariseDeliveryPrice;

               var messageId = await client.SendTextMessageAsync(MyChatId, message, Telegram.Bot.Types.Enums.ParseMode.Html);
            }
            catch
            {
                await client.SendTextMessageAsync(MyChatId, message);
            }
        }

       

         public async void SumariseDeliveryPrice(object sender, MessageEventArgs e)
         {
            var msg = e.Message;
            int deliveryPrice;
            if (int.TryParse(msg.Text, out deliveryPrice))
            {
                using (HttpClient client = new HttpClient())
                {
                    PaymentUpdate payment = new PaymentUpdate(deliveryPrice, paymentid);
                    var request = JsonConvert.SerializeObject(payment);

                    var fin = new StringContent(request, Encoding.UTF8, "application/json");
                    var response = await client.PutAsync($"http://optshopapi:5001/api/Payment/", fin);

                }


            }
         }
    }
}
