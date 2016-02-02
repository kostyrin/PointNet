using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PointNet.Data.Repositories;
using PointNet.Core.Common;
using PointNet.CommandProcessor.Command;
using PointNet.Model;
using PointNet.Model.Commands;

namespace PointNet.Domain.Handlers
{
    public class CanChangePassword : IValidationHandler<ChangePasswordCommand>
    {
        private readonly IUserRepository userRepository;

        public CanChangePassword(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public IEnumerable<ValidationResult> Validate(ChangePasswordCommand command)
        {
            User user = userRepository.GetById(command.UserId);
            var encoded = Md5Encrypt.Md5EncryptPassword(command.OldPassword);

            if (!user.PasswordHash.Equals(encoded))
            {
                yield return new ValidationResult("OldPassword", Resources.Password);
            }
        }
    }
}
