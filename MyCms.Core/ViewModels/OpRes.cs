using System;
using System.Collections.Generic;

namespace MyCms.Core.ViewModels
{

    [Serializable]
    public class OpRes
    {
        public bool IsSuccess { get; protected set; }

        public OperationErrorResult Error { get; protected set; }

        protected List<OperationErrorResult> Errors { get; set; }

        public static OpRes BuildSuccess()
        {
            return new OpRes()
            {
                IsSuccess = true
            };
        }

        public static OpRes BuildError(OpRes error)
        {
            var errors = error.Errors;
            return new OpRes()
            {
                IsSuccess = false,
                Errors = errors,
                Error = new OperationErrorResult()
                {
                    Type = error.Error.Type,
                    Message = error.Error?.Message,
                    Exception = error.Error?.Exception,
                }
            };
        }

        public static OpRes BuildError(ErrorTypes type, string message)
        {
            return BuildError(type, message, null);
        }

        public static OpRes BuildError(string message)
        {
            return BuildError(ErrorTypes.Unknown, message, null);
        }

        public static OpRes BuildError(string message, Exception exception)
        {
            return BuildError(ErrorTypes.Unknown, message, exception);
        }

        public static OpRes BuildError(Exception exception)
        {
            return BuildError(ErrorTypes.Unknown, exception.Message, exception);
        }

        public static OpRes BuildError(ErrorTypes type, string message, Exception exception)
        {
            return new OpRes()
            {
                IsSuccess = false,
                Error = new OperationErrorResult
                {
                    Type = type,
                    Message = message,
                    Exception = exception
                },
            };
        }

    }

    [Serializable]
    public class OperationErrorResult
    {
        public string Message { get; set; }
        public ErrorTypes Type { get; set; }
        public Exception Exception { get; set; }
    }

    public class OPRes<T> : OpRes
    {
        public T Result { get; protected set; }

        public static OPRes<T> BuildSuccess(T value)
        {
            return new OPRes<T>()
            {
                IsSuccess = true,
                Result = value
            };
        }

        public new static OPRes<T> BuildError(OpRes error)
        {
            return new OPRes<T>()
            {
                IsSuccess = false,
                Error = new OperationErrorResult()
                {
                    Type = error.Error.Type,
                    Message = error.Error.Message,
                    Exception = error.Error.Exception,
                }
            };
        }

        public new static OPRes<T> BuildError(ErrorTypes type, string message)
        {
            return BuildError(type, message, null);
        }

        public new static OPRes<T> BuildError(string message)
        {
            return BuildError(ErrorTypes.Unknown, message, null);
        }

        public new static OPRes<T> BuildError(string message, Exception exception)
        {
            return BuildError(ErrorTypes.Unknown, message, exception);
        }

        public new static OPRes<T> BuildError(Exception exception)
        {
            return BuildError(ErrorTypes.Unknown, null, exception);
        }

        public new static OPRes<T> BuildError(ErrorTypes type, string message, Exception exception)
        {
            return new OPRes<T>()
            {
                IsSuccess = false,
                Error = new OperationErrorResult
                {
                    Type = type,
                    Message = message,
                    Exception = exception
                },
            };
        }


    }

    public enum ErrorTypes
    {
        Unknown = 0,
        NoContent = 1,
    }
}