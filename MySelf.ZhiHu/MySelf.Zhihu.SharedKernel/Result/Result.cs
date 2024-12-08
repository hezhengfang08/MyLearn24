using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.SharedKernel.Result
{
    public class Result<T> : IResult
    {
        public T? Value { get; init; }

        public ResultStatus Status { get; protected set; } = ResultStatus.Ok;
        protected internal Result(T value)
        {
            Value = value;
        }

        protected internal Result(ResultStatus status)
        {
            Status = status;
        }
        public bool IsSuccess => Status == ResultStatus.Ok;
        public IEnumerable<string>? Errors { get; protected set; }
        public object? GetValue()
        {
            return Value;
        }
        /// <summary>
        /// 隐式转换
        /// </summary>
        /// <param name="result"></param>
        public static implicit operator Result<T>(Result result)
        {
            return new Result<T>(default(T))
            {
                Status = result.Status,
                Errors = result.Errors
            };
        }
    }

    public class Result : Result<Result>
    {
        protected internal Result(Result value) : base(value)
        {
        }

        protected internal Result(ResultStatus status) : base(status)
        {
        }

        public static Result From(IResult result)
        {
            return new Result(result.Status)
            {
                Errors = result.Errors
            };
        }

        public static Result Success()
        {
            return new Result(ResultStatus.Ok);
        }

        public static Result<T> Success<T>(T value)
        {
            return new Result<T>(value);
        }

        public static Result Failure()
        {
            return new Result(ResultStatus.Error);
        }

        public static Result Failure(IEnumerable<string>? errors)
        {
            return new Result(ResultStatus.Error)
            {
                Errors = errors
            };
        }

        public static Result NotFound()
        {
            return new Result(ResultStatus.NotFound);
        }

        public static Result Forbidden()
        {
            return new Result(ResultStatus.Forbidden);
        }

        public static Result Unauthorized()
        {
            return new Result(ResultStatus.Unauthorized);
        }

        public static Result Invalid()
        {
            return new Result(ResultStatus.Invalid);
        }

        public static Result Invalid(IEnumerable<string>? errors)
        {
            return new Result(ResultStatus.Invalid)
            {
                Errors = errors
            };
        }
    }
}
