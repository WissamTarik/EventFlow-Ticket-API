using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Application.Common
{
    public record Result
    {
        public Result(bool isSuccess, IReadOnlyList<Error> errors)
        {
            IsSuccess = isSuccess;
            Errors = errors;
        }
        public Result(bool isSuccess, Error error)
        {
            IsSuccess = isSuccess;
            Errors = [error];
        }

        public bool IsSuccess { get; set; }
        public IReadOnlyList<Error> Errors { get; set; }

        public static Result Ok() => new(true, Error.None());
        public static Result Failure(IReadOnlyList<Error> errors) => new(false, errors);
        public static Result Failure(Error error) => new(false, error);
    }
    public record Result<T> : Result
    {
        public T Data { get; set; }
        public Result(IReadOnlyList<Error> errors) : base(false, errors)
        {
            Data = default!;
        }
        public Result( Error error) : base(false, error)
        {
            Data = default!;
        }
        public Result(T Value) : base(true, [])
        {
            Data = Value;
        }
        public static Result<T> Ok(T Data ) => new(Data);
        public new static Result<T> Failure(IReadOnlyList<Error> errors) => new(errors);
        public new static Result<T> Failure(Error error) => new(error);
    }



}
