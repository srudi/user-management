using System;

namespace UserManagement.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object id)
            : base($"\"{name}\" with Id : \"{id}\" was not found.")
        {
        }
    }
}
