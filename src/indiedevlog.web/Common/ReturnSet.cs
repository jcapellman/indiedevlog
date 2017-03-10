using System;

namespace indiedevlog.web.Common
{
    public class ReturnSet<T>
    {
        public T ObjectValue { get; set; }

        public string ErrorException { get; set; }

        public bool HasError => !string.IsNullOrEmpty(ErrorException) && ObjectValue != null;

        public ReturnSet(T objectValue)
        {
            ObjectValue = objectValue;
        }

        public ReturnSet(T objectValue, string exceptionMessage)
        {
            ObjectValue = objectValue;
            ErrorException = exceptionMessage;
        }

        public ReturnSet(T objectValue, Exception ex)
        {
            ObjectValue = objectValue;
            ErrorException = ex.Message;
        }
    }
}