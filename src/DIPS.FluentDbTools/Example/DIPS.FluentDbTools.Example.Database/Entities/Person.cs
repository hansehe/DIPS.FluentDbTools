using System;

namespace DIPS.FluentDbTools.Example.Database.Entities
{
    public class Person
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int SequenceNumber { get; set; } = 0;
        public string Username { get; set; } = "Ola";
        public string Password { get; set; } = "Nordmann";
    }
}