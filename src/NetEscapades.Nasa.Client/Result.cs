using System;
using System.Collections.Generic;
using System.Text;

namespace NetEscapades.Nasa
{
    public class Result<T> : IResult<T> where T : class
    {
        protected Result(bool isSuccess, string errorMessage)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }

        protected Result(bool isSuccess, T data)
        {
            IsSuccess = isSuccess;
            Data = data;
        }

        public bool IsSuccess { get; }
        public T Data { get; }
        public string ErrorMessage { get; }

        public static Result<T> Failure(string message) => new Result<T>(false, message);
        public static Result<T> Success(T data) => new Result<T>(true, data);
    }
}
