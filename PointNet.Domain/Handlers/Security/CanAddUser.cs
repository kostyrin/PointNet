using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using PointNet.Model.Commands;
using PointNet.CommandProcessor.Command;
using PointNet.Data.Infrastructure;
using PointNet.Data.Repositories;
using PointNet.Core.Common;
using PointNet.Model;

namespace PointNet.Domain.Handlers
{
    public class CanAddUser : IValidationHandler<UserRegisterCommand>
    {
        private readonly IUserRepository userRepository;
        private readonly IMappingEngine _mapper;

        public CanAddUser(IMappingEngine mapper, IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            _mapper = mapper;
        }

        public IEnumerable<ValidationResult> Validate(UserRegisterCommand command)
        {
            User isUserExists = null;
            isUserExists = userRepository.Get(c => c.Email == command.Email);

            if (isUserExists != null)
            {
                yield return new ValidationResult("EMail", Resources.UserExists);
            }
        }
    }
}
