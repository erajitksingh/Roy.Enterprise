namespace Roy.Enterprise.API.Helpers
{
    public class AppException : Exception
    {
        private int _errCode;
        public string err_msg { get; }
        public string ErrorCode { get { return "E" + _errCode.ToString().PadLeft(4, '0'); } }
        public AppException() : base() { }

        public AppException(string message) : base(message) { }

        public AppException(int errorCode, string message) : base(message)
        {
            _errCode = errorCode;
        }

        public AppException(int errorCode)
        {
            _errCode = errorCode;
            err_msg = "";
        }

        public override string Message => err_msg;
    }
}
