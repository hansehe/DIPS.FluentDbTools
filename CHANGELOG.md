# Changelog FluentDbTools
All notable changes to this project will be documented in this file.
This project adheres to [Semantic Versioning](http://semver.org/).

<!-- the topmost header version must be set manually in the VERSION file -->
### Version 1.1.2 2019-03-15
 - Added skip flag to db provider registration.

### Version 1.1.1 2019-03-14
 - Added service collection extension feature.

### Version 1.1.0 2019-03-13
 - Removed direct db driver dependencies.
 - Included extension projects for oracle and postgres.

### Version 1.0.12 2019-03-12
 - Fixed bug in postgres description generator.

### Version 1.0.11 2019-03-06
 - Added features to specify table name.

### Version 1.0.10 2019-02-28
 - Added DbConfig to dbProvider solution.
 - Renamed DefaultDbConfig to MSDbConfig.
 - Added a default DbConfig to DbProvider (non MS specific).

### Version 1.0.9 2019-02-27
 - Updated default values for DbConfig.

### Version 1.0.8 2019-02-27
 - Default schema/schema password should be equal to user/user password.

### Version 1.0.7 2019-01-18
 - Added support for overriding functions in DefaultDbConfig.

### Version 1.0.6 2019-01-18
 - Added support for setting the connection string template.

### Version 1.0.5 2019-01-02
 - Added support for select with count.

### Version 1.0.4 2019-01-01
 - Added FluentDbProviderFactory extension.

### Version 1.0.3 2019-01-01
 - added SchemaPassword property to IDbConfig
 - Deleted old stuff from FluentDbTools.Migration.Abstractions 
 - Removed FluentDbTools.Database.Abstractions assembly

### Version 1.0.2 2018-11-31
 - Updated nuget metadata.

### Version 1.0.1 2018-11-31
 - Minor fix

### Version 1.0.0 2018-11-28
 - Initial release
