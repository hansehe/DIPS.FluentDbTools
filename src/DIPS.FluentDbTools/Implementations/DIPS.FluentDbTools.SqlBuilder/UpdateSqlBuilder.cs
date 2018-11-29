﻿using System;
using System.Collections.Generic;
using System.Linq;
using DIPS.FluentDbTools.Common.Abstractions;
using DIPS.FluentDbTools.SqlBuilder.Abstractions;
using DIPS.FluentDbTools.SqlBuilder.Abstractions.Common;
using DIPS.FluentDbTools.SqlBuilder.Abstractions.Fields;
using DIPS.FluentDbTools.SqlBuilder.Common;

namespace DIPS.FluentDbTools.SqlBuilder
{
    internal class UpdateSqlBuilder<TClass> : IUpdateSqlBuilder<TClass>
    {
        private string UpdateTableWith;

        private readonly List<string> Wheres = new List<string>();
        private readonly UpdateFieldSelector<TClass> UpdateFieldSelector;
        private readonly IDbConfig DbConfig;
        private string SchemaName;

        public UpdateSqlBuilder(IDbConfig dbConfig)
        {
            DbConfig = dbConfig;
            DbType = dbConfig?.DbType ?? SupportedDatabaseTypes.Postgres;
            
            UpdateFieldSelector = new UpdateFieldSelector<TClass>(DbConfig);
        }

        public SupportedDatabaseTypes DbType { get; }

        public IUpdateSqlBuilder<TClass> Fields(Action<IFieldSetterSelector<TClass>> selector)
        {
            UpdateFieldSelector.OnSchema(SchemaName);
            selector(UpdateFieldSelector);

            UpdateTableWith = UpdateFieldSelector.Build();

            return this;
        }

        public IUpdateSqlBuilder<TClass> Where(Action<IWhereFieldSelector<TClass>> selector)
        {
            var whereSelector = new WhereFieldSelector<TClass>(DbConfig);
            selector(whereSelector);

            Wheres.AddRange(whereSelector.Build());

            return this;
        }

        public IUpdateSqlBuilder<TClass> WhereIf(Action<IWhereFieldSelector<TClass>> selector, Func<bool> statement)
        {
            if (statement.Invoke())
            {
                Where(selector);
            }
            return this;
        }

        public IUpdateSqlBuilder<TClass> OnSchema(string schemaName = null, Func<bool> setSchemaNameIfExpressionIsEvaluatedToTrue = null)
        {
            if (setSchemaNameIfExpressionIsEvaluatedToTrue?.Invoke() ?? true)
            {
                SchemaName = schemaName ?? DbConfig.Schema;
            }
            return this;
        }

        public ISqlFieldBuilder<TClass> AddFields(Action<IFieldSetterSelector<TClass>> selector)
        {
            return Fields(selector);
        }

        public ISqlFieldBuilder<TClass> AddDynamicFields(dynamic fields)
        {
            throw new NotImplementedException();
        }


        public string Build()
        {
            var sql = UpdateTableWith;

            if (Wheres.Count > 0)
            {
                var where = string.Join(" AND ", Wheres);
                sql = $"{sql} WHERE {@where}";
            }

            return sql;
        }
    }


}