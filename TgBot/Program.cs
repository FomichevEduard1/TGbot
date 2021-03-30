using MihaZupan;
using System;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace El13Bot
{
    class Program
    {
        private static ITelegramBotClient botClient;
        static void Main(string[] args)
        {
            var proxy = new HttpToSocks5Proxy("184.181.217.210", 4115);
            botClient = new TelegramBotClient("1770753907:AAFCoB6ixSsV43TBo5Lg2Dc5Wls5KMR0ttM", proxy) { Timeout = TimeSpan.FromSeconds(5) };

            var me = botClient.GetMeAsync().Result;
            Console.WriteLine($"Bot id{me.Id}. Bot name: {me.FirstName}");
            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();

            Console.ReadKey();
        }

        private async static void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            var text = e?.Message?.Text;
            if (text == null)
                return;
            Console.WriteLine($"recived text message'{text}' in chat '{e.Message.Chat.Id}'");

            await botClient.SendTextMessageAsync(
                chatId: e.Message.Chat,
                text: $"You said '{text}'"
                ).ConfigureAwait(false);


        }
    }
}
