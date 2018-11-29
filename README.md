# DIPS.FluentDbTools
FluentDbTools provides a fluent SQL abstraction layer for creating database connections, building sql queries and migrating your database.

Following databases are currently supported:
- Oracle
- Postgres

## Example
The `IDbConfig` interface is the only member you need to instantiate.
It provides a simple interface for providing which database type to use, and other necessary database settings.

### Create a Database Connection
```csharp
IDbConfig dbConfig = new DbConfig(..)
var dbConnection = dbConfig.CreateDbConnection();
```

### Build SQL Query Fluently
```csharp
IDbConfig dbConfig = new DbConfig(..)
var sql = dbConfig.CreateSqlBuilder().Select()
            .OnSchema()
            .Fields<Person>(x => x.F(item => item.Id))
            .Fields<Person>(x => x.F(item => item.SequenceNumber))
            .Fields<Person>(x => x.F(item => item.Username))
            .Fields<Person>(x => x.F(item => item.Password))
            .Where<Person>(x => x.WP(item => item.Id))
            .Build();
```

### Migrate the Database With Extended FluentMigrator
```csharp
[Migration(1, "Migration Example")]
public class AddPersonTable : MigrationModel
{
    public override void Up()
    {
        Create.Table(Table.Person).InSchema(SchemaName)
            .WithColumn(Column.Id).AsGuid().PrimaryKey()
            .WithColumn(Column.SequenceNumber).AsInt32().NotNullable()
            .WithColumn(Column.Username).AsString()
            .WithColumn(Column.Password).AsString()
            .WithTableSequence(this);
    }

    public override void Down()
    {
        Delete.Table(Table.Person).InSchema(SchemaName);
    }
}
```

### More Examples
Please have a look in the example folder: 
- [src/DIPS.FluentDbTools/Example](src/DIPS.FluentDbTools/Example)

## Get Started
1. Install [Docker](https://www.docker.com/)
2. Install [Python](https://www.python.org/) and [pip](https://pypi.org/project/pip/)
    - Windows:  https://matthewhorne.me/how-to-install-python-and-pip-on-windows-10/
    - Ubuntu: Python is installed by default
        - Install pip: sudo apt-get install python-pip
3. Install python dependencies:
    - pip install -r requirements.txt
4. See available commands:
    - python DockerBuild.py help

## Build & Run
1. Start domain development by deploying service dependencies:
    - python DockerBuild.py start-dev
2. Build solution as container images:
    - python DockerBuild.py build
3. Test solution in containers:
    - python DockerBuild.py test
4. Run solution in containers:
    - python DockerBuild.py run
5. Open solution and continue development:
    - [DIPS.DbTools](src/DIPS.DbTools)
6. Publish new nuget version:
    - Bump version in [CHANGELOG.md](CHANGELOG.md)
    - Expose your api key by exposing the environment variable `API_KEY`
        - Linux:
            - export API_KEY=<YOUR-API_KEY>
        - Powershell:
            - $env:API_KEY = "<YOUR-API_KEY>"
    - python DockerBuild.py publish
7. Stop development when you feel like it:
    - python DockerBuild.py stop-dev

## Additional Info

## Buildsystem
- [DockerBuildSystem](https://github.com/DIPSAS/DockerBuildSystem)
- [SwarmManagement](https://github.com/DIPSAS/SwarmManagement)
