﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.SharedKernel.Result
{
    public interface IResult
    {
        IEnumerable<string>? Errors { get; }

        bool IsSuccess { get; }

        ResultStatus Status { get; }

        object? GetValue();
    }
}
