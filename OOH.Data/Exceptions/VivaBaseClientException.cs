using System;
using System.Collections.Generic;
using System.Text;

namespace OOH.Data.Exceptions
{
    public class VivaBaseClientException<T> : VivaBaseException where T : Enum
    {
        protected VivaBaseClientException(string relatedField, Type relatedObject, string message) : base(relatedField, relatedObject, message)
        {
        }

        public T ExceptionType { get; set; }

        protected static string GetMessage(T exceptionType, string relatedField, Type relatedObject)
        {
            return "";
        }

        public override HttpErrorType GetErrorType()
        {
            throw new NotImplementedException();
        }

        public override int GetExceptionTypeCode()
        {
            throw new NotImplementedException();
        }

        public override string GetExceptionTypeDescription()
        {
            throw new NotImplementedException();
        }
    }
}
