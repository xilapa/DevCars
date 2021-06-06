using System;

namespace DevCars.API.Entities 
{
    public abstract class BaseEntity 
    {
        public BaseEntity()
        {
            CreatedAt = DateTime.Now;
            Active = true;
        }

        public int Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool Active { get; private set; }

        public void Deactivate()
        {
            this.Active = false;
        }
        
    }
}