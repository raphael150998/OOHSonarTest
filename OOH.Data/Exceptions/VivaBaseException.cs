using System;
using System.Collections.Generic;
using System.Text;

namespace OOH.Data.Exceptions
{
    public abstract class VivaBaseException : Exception
    {
        protected VivaBaseException(string relatedField, Type relatedObject, string message)
        {

        }

        public string RelatedField { get; set; }
        public Type RelatedObject { get; set; }

        public abstract HttpErrorType GetErrorType();
        public abstract int GetExceptionTypeCode();
        public abstract string GetExceptionTypeDescription();
    }

    public enum HttpErrorType
    {
        Client = 400,
        Unauthorize = 401,
        Forbidden = 403,
        NotFound = 404
    }
}
    