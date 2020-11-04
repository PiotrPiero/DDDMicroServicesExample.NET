using System;

namespace HomeBudget.Core.Impl
{
    public abstract class Entity : IEntity
    {

        public Entity()
        {

        }

        public Entity(DateTime created, DateTime modified, int authorId)
        {
            Created = created;
            Modified = modified;
            AuthorId = authorId;
            
        }
        public Entity(int id, DateTime created, DateTime modified, int authorId)
        {
            Id = id;
            Created = created;
            Modified = modified;
            AuthorId = authorId;
        }

        public int? Id { get; protected set; }
        public DateTime Created { get; protected set; }
        public DateTime Modified { get; protected set; }
        public int AuthorId { get; protected set; }
    }
}
