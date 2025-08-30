using System;

namespace PMApp.Data.Entities
{
    public abstract class Entity : IEntity
    {
        public string Id { get ; set; }
    }
}
