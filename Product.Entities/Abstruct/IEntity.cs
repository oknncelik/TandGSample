﻿namespace Product.Entities.Abstruct
{
    public interface IEntity
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool ActiveFlag { get; set; }
    }
}
