using Telegram.Bot.Polling;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

internal class Program
{
    private static void Main(string[] args)
    {
        var client = new TelegramBotClient("7183122119:AAEga-5mOVE2_V3sREOxoNDo_acI-vHrM-w");
        client.StartReceiving(UpdateHandler, Error); /*метод, который выводит бот*/
        Console.ReadLine();
    }
    private static async Task MessageHeandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        switch (update.Type)
        {
            case UpdateType.Message:
                {
                    var message = update.Message;
                    var user = message.From;
                    var chat = message.Chat;

                    switch (message.Type)
                    {
                        case MessageType.Text:
                            {
                                if (message.Text == null)
                                {
                                    return;

                                }
                                if (message.Text == "/start")
                                {
                                    //создание кнопок в строке
                                    InlineKeyboardMarkup inlineKeyboard = new(new[]
                                    {
                                        new []
                                        {
                                            InlineKeyboardButton.WithCallbackData(text: "Меню", callbackData: "Меню"),
                                        },
                                        });

                                    Message sentMessage = await botClient.SendTextMessageAsync(
                                        chatId: chat.Id,
                                        text: "Выбери",
                                        replyMarkup: inlineKeyboard,
                                        cancellationToken: cancellationToken);

                                    return;
                                }
                                return;
                            }
                    }
                    return;
                }
        }

    }
    private static async Task CallBack(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        //вывод в зависимости от выбранной кнопки
        if (update != null && update.CallbackQuery != null)
        {
            string answer = update.CallbackQuery.Data;
            switch (answer)
            {
                case "Меню":
                    InlineKeyboardMarkup inlineKeyboard = new(new[]
                     {
                     new []
                     {
                         InlineKeyboardButton.WithCallbackData(text: "Контакты", callbackData: "Контакты"),
                     },
                     new []
                     {
                         InlineKeyboardButton.WithCallbackData(text: "Местоположение", callbackData: "Местоположение"),
                     },
                         new []
                     {
                         InlineKeyboardButton.WithCallbackData(text: "Прайслист", callbackData: "Прайслист"),
                     },
                     });
                    await botClient.SendTextMessageAsync(
                                        chatId: update.CallbackQuery.Message.Chat.Id,
                                        text: "Сделай свой выбор",
                                        replyMarkup: inlineKeyboard,
                                        cancellationToken: cancellationToken);
                    break;

                case "Контакты":
                    await botClient.SendTextMessageAsync(
                    chatId: update.CallbackQuery.Message.Chat.Id,
                    text: "Телефон---+79659346523         Имя----Артемий         Фамилия---Мазокин" +
                    "Телефон---+79852642732         Имя---Эндрю         Фамилия---Шеченкоу",

                    cancellationToken: cancellationToken);
                    break;
                case "Местоположение":
                    await botClient.SendLocationAsync(
                    chatId: update.CallbackQuery.Message.Chat.Id,
                    latitude: 55.90440081233016f,
                    longitude: 37.58883592462701,
                    cancellationToken: cancellationToken);
                    break;
                case "Прайслист":
                    await botClient.SendTextMessageAsync(
    chatId: update.CallbackQuery.Message.Chat.Id,
    text: "Cadillac Escalade ESV, 2023, 6.2 л / 416 л.с. / бензин- 25 000 000 ₽" +
    "Kia K5, 2023, 2.0 л / 240 л.с. / бензин - 4 329 000 ₽" +
    "BMW X7 40d, 2023, 3.0 л / 340 л.с. / дизель- 21 950 000 ₽" +
    "Mercedes-Benz Mercedes-AMG SL 43, 381.00 л.с., бензин - 25 975 120₽",

    cancellationToken: cancellationToken);
                    break;
            }


            //InlineKeyboardMarkup inlineKeyboard = update.CallbackQuery.Message.ReplyMarkup!;
            //var inlines = inlineKeyboard.InlineKeyboard;
            //foreach (var item1 in inlines)
            //{
            //    foreach (var item2 in item1)
            //    {

            //       
            //    }
            //}
        }
    }

    private static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
    {
        throw new NotImplementedException();
    }
    private static async Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        await CallBack(botClient, update, cancellationToken);
        await MessageHeandler(botClient, update, cancellationToken);

    }
}