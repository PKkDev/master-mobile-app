namespace MasterMobile.Domain.Model.MailReader
{
    public enum AnalyzeResType
    {
        Error,
        Success
    }

    public class AnalyzeMessage
    {
        public string Message { get; set; }
        public AnalyzeResType Type { get; set; }

        public AnalyzeMessage(string message, AnalyzeResType type)
        {
            Message = message;
            Type = type;
        }
    }
}
