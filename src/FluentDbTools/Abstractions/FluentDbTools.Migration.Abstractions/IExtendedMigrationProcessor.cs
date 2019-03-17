﻿using System.Data;
using FluentMigrator.Expressions;

namespace FluentDbTools.Migration.Abstractions
{
    public interface IExtendedMigrationProcessor
    {
        void Process(CreateSchemaExpression expression);
        void Process(DeleteSchemaExpression expression);
        void Process(DeleteDataExpression expression);
        void Process(CreateConstraintExpression expression);
        void Process(CreateIndexExpression expression);
        void Process(CreateTableExpression expression);
        void Process(CreateSequenceExpression expression);
        void Process(DeleteTableExpression expression);

        IDbConnection GetMigrationDbConnection();
        void ProcessSql(string sql);

    }

    public interface IExtendedMigrationProcessor<T> : IExtendedMigrationProcessor
    {
    }
}