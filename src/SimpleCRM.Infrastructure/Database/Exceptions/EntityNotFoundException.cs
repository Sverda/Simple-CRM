namespace SimpleCRM.Infrastructure.Database.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(Type entityType, object id)
            : base($"Entity of type {entityType.Name} doesn't exist {id}")
        {
        }
    }
}
