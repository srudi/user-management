namespace UserManagement.Infrastructure.Persistence.Exceptions
{
    public class DatabaseUpdateException : Exception
    {
        public DatabaseUpdateException() :
            base("An error occurred while updating the database")
        {
        }
    }
}
