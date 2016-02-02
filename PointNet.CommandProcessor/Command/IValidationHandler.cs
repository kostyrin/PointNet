using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PointNet.CommandProcessor;
using PointNet.Core.Common;

namespace PointNet.CommandProcessor.Command
{
    public interface IValidationHandler<in TCommand> where TCommand : ICommand
    {
        IEnumerable<ValidationResult>  Validate(TCommand command);
    }
}
