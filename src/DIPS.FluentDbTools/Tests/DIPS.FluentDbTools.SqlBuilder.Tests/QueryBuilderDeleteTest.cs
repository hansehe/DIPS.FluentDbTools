﻿using DIPS.Extensions.FluentDbTools.SqlBuilder;
using DIPS.FluentDbTools.Common.Abstractions;
using DIPS.FluentDbTools.SqlBuilder.Abstractions;
using DIPS.FluentDbTools.SqlBuilder.Tests.TestEntities;
using DIPS.FluentDbTools.TestUtilities;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DIPS.FluentDbTools.SqlBuilder.Tests
{
    public class QueryBuilderDeleteTest
    {      
        [Theory]
        [InlineData(SupportedDatabaseTypes.Oracle, false, "DELETE FROM Entity WHERE Id = :IdParam AND Name = 'Arild'")]
        [InlineData(SupportedDatabaseTypes.Postgres, false, "DELETE FROM Entity WHERE Id = @IdParam AND Name = 'Arild'")]
        [InlineData(SupportedDatabaseTypes.Oracle, true, "DELETE FROM {0}.Entity WHERE Id = :IdParam AND Name = 'Arild'")]
        [InlineData(SupportedDatabaseTypes.Postgres, true, "DELETE FROM {0}.Entity WHERE Id = @IdParam AND Name = 'Arild'")]
        public void DeleteTest1(SupportedDatabaseTypes databaseTypes, bool useSchema, string expectedSql)
        {
            using (var scope = TestServiceProvider.GetDatabaseExampleServiceProvider(databaseTypes).CreateScope())
            {
                var dbConfig = scope.ServiceProvider.GetService<IDbConfig>();
                expectedSql = string.Format(expectedSql, dbConfig.Schema);
                
                var builder = dbConfig.CreateSqlBuilder();
                var delete = builder.Delete<Entity>();

                var sql = delete
                    .OnSchema(setSchemaNameIfExpressionIsEvaluatedToTrue: () => useSchema)
                    .Where(x => x.WP(item => item.Id, "IdParam"))
                    .Where(x => x.WV(item => item.Name, "Arild"))
                    .Build();

                sql.Should().Be(expectedSql);
            }
        }
    }
}