using System;
using System.Net;

namespace TesteSoft.Calculo.Common.Exceptions
{
    public class TesteSoftException : Exception
    {

        public TesteSoftException()
        {

        }

        public HttpStatusCode? Status { get; private set; }

        public TesteSoftException(string message, HttpStatusCode? status = null) : base(message)
        {
            Status = status;
        }

        public TesteSoftException(string message, Exception innerException, HttpStatusCode? status = null) : base(message, innerException)
        {
            Status = status;
        }

    }
}
