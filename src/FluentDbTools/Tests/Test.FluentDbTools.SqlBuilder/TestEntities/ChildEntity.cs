﻿namespace FTest.FluentDbTools.SqlBuilderTestEntities
{
    public class ChildEntity
    {
        public int Id { get; set; }
        public int ChildChildEntityId { get; set; }

        public int EntityId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Relation { get; set; }
    }
}