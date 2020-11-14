using FluentValidation.Results;
using System;

namespace UserCQRS.Domain.Commands
{
    public abstract class Command
    {
        public Command()
        {
            Timestamp = DateTime.Now;
        }
        public DateTime Timestamp { get; }
        public ValidationResult ValidationResult { get; set; }

        public virtual bool IsValid()
        {            
            return ValidationResult.IsValid;
        }
    }
}
