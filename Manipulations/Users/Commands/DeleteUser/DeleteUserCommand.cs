﻿using MediatR;

namespace Social_Network_API.Manipulations.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteUserCommand() { }

        public DeleteUserCommand(int id)
        {
            Id = id;
        }
    }
}
