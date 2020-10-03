using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using System.Linq;
using System;
using Telegram.Bot.Types;

namespace slsr
{
    public class Sender
    {
        private const string Pause = "Pause";
        private const string Start = "Start";

        private TelegramBotClient Bot { get; set; } //todo add inmemorycache

        public async Task Send(string textMessage)
        {
            InlineKeyboardMarkup okButton = new InlineKeyboardMarkup(new InlineKeyboardButton[]
            {
                new InlineKeyboardButton() { Text = Pause, CallbackData = Pause },
                new InlineKeyboardButton() { Text = Start, CallbackData = Start }
            });
            
            await GetBot().SendTextMessageAsync(691533828, textMessage, replyMarkup: okButton);
            Console.WriteLine("Sended Text Message");
        }

        public async Task<bool> NeedPause()
        {
            string response = await GetResponse();

            if (response == Pause)
            {
                Console.WriteLine("Received PAUSE");
                return true;
            }
            return false;
        }

        public async Task<bool> NeedStart()
        {
            string response = await GetResponse();

            if (response == Start)
            {
                Console.WriteLine("Received START");
                return true;
            }
            return false;
        }

        private async Task<string> GetResponse()
        {
            Update[] updates = await GetBot().GetUpdatesAsync();
            return (await GetBot().GetUpdatesAsync()).Select(u => u?.CallbackQuery?.Data)?.LastOrDefault();
        }

        private TelegramBotClient GetBot()
        {
            if (Bot == null)
            {
                Bot = new TelegramBotClient("1358759246:AAE1U1lGR2Zq_sZqg0RpDzrzG1E3CSRxuik");
            }

            return Bot;
        }
    }
}
