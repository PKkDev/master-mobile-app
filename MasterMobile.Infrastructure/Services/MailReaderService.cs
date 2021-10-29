using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;

using AngleSharp.Dom;
using AngleSharp.Html.Parser;

using MySql.Data.MySqlClient;
using Dapper;
using Xamarin.Forms;
using MasterMobile.Domain.Model.MailReader;
using System.Threading.Tasks;

namespace MasterMobile.Infrastructure.Services
{
    public class MailReaderService
    {
        public MailReaderService() { }

        private class Payment
        {
            public int Coast { get; set; }
            public DateTime Day { get; set; }
            public TimeSpan Time { get; set; }

            public Payment(int coast, DateTime day, TimeSpan time)
            {
                Coast = coast;
                Day = day;
                Time = time;
            }
        }

        public async Task StartCheck(string email, string password)
        {
            var format = new NumberFormatInfo();
            format.NumberDecimalSeparator = ".";
            format.NumberGroupSeparator = ",";

            try
            {
                MessagingCenter.Send(new AnalyzeMessage("Start analyzis", AnalyzeResType.Success), "ResMailCheck");
                using (var client = new ImapClient())
                {

                    MessagingCenter.Send(new AnalyzeMessage("Connect to mail...", AnalyzeResType.Success), "ResMailCheck");
                    await client.ConnectAsync("imap.mail.ru", 993, true);
                    await client.AuthenticateAsync(email, password);

                    var paymentBox = client.GetFolder("5-ка чеки");
                    paymentBox.Open(FolderAccess.ReadWrite);

                    var query = SearchQuery.NotFlags(MessageFlags.Seen);
                    var mails = await paymentBox.SearchAsync(query);

                    MessagingCenter.Send(new AnalyzeMessage($"Total messages: {paymentBox.Count}", AnalyzeResType.Success), "ResMailCheck");
                    MessagingCenter.Send(new AnalyzeMessage($"Founded messages: {mails.Count}", AnalyzeResType.Success), "ResMailCheck");

                    var payments = new List<Payment>();
                    foreach (var mail in mails)
                    {
                        MessagingCenter.Send(new AnalyzeMessage($"Start parsing message {mail.Id}", AnalyzeResType.Success), "ResMailCheck");

                        var message = paymentBox.GetMessage(mail);
                        var htmlBody = message.HtmlBody;
                        DateTime date = message.Date.DateTime;

                        var parser = new HtmlParser();
                        var document = await parser.ParseDocumentAsync(htmlBody);

                        var listValue = new List<string>();
                        var selector = document.QuerySelectorAll("span");
                        foreach (IElement element in selector)
                        {
                            if (!string.IsNullOrEmpty(element.TextContent))
                                listValue.Add(element.TextContent);
                        }

                        var resIndex = listValue.FindIndex(x => x.Contains("Итог"));
                        if (resIndex != -1)
                        {
                            var paymentStr = listValue[resIndex + 1];
                            if (double.TryParse(paymentStr, NumberStyles.Float, format, out var paymentDb))
                            {
                                var payment = (int)paymentDb;
                                MessagingCenter.Send(new AnalyzeMessage($"Founded payment for {date} - {payment}", AnalyzeResType.Success), "ResMailCheck");
                                payments.Add(new Payment(payment, new DateTime(date.Year, date.Month, date.Day), date.TimeOfDay));
                            }
                        }

                        paymentBox.AddFlags(mail, MessageFlags.Seen, true);

                    }

                    client.Disconnect(true);

                    if (payments.Any())
                    {
                    }
                }
            }
            catch (Exception e)
            {
                MessagingCenter.Send(new AnalyzeMessage($"{e.Message}", AnalyzeResType.Error), "ResMailCheck");
            }
            finally
            {
                MessagingCenter.Send(new AnalyzeMessage("End analyzis", AnalyzeResType.Success), "ResMailCheck");
            }
        }
    }
}
