using FluentDbTools.Example.Database;
using FluentDbTools.Migration.Contracts;
using FluentMigrator;

namespace FluentDbTools.Example.Migration.MigrationModels
{
    [Migration(1, "Migration Example")]
    public class AddPersonTable : MigrationModel
    {
        public override void Up()
        {
            Create.Table(Table.Person).InSchema(SchemaName)
                .WithColumn(Column.Id).AsGuid().PrimaryKey()
                .WithColumn(Column.SequenceNumber).AsInt32().NotNullable()
                .WithColumn(Column.Alive).AsBoolean().NotNullable()
                .WithColumn(Column.Username).AsString()
                .WithColumn(Column.Password).AsString()
                .WithTableSequence(this);
        }

        public override void Down()
        {
            Delete.Table(Table.Person).InSchema(SchemaName);
        }
    }
}