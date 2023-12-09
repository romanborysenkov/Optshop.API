using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
class Program
{
    const string token = "6423900666:AAHqwGibyyog-HoNRSADjkdPwv7-QRWWOhA";

    static TelegramBotClient botClient;
    static void Main()
    {
        botClient = new TelegramBotClient(token);

        botClient.OnMessage += async (sender, e) =>
        {
            BotOnMessageReceiver(sender, e);
        };

        botClient.StartReceiving();
        Console.ReadLine();
        botClient.StopReceiving();

    }

    private static void BotOnMessageReceiver(object sender, MessageEventArgs e)
    {
       
    }
}
