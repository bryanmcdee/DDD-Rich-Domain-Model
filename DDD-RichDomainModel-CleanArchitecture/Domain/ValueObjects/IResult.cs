using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ValueObjects
{
    public interface IResult
    {
        bool IsFailure { get; }
        bool IsSuccess { get; }
    }
}
