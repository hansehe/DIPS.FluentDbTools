﻿using System.Collections.Generic;
using System.Data;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Example.FluentDbTools.Database.Entities;
using FluentAssertions;
using FluentDbTools.Common.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Example.FluentDbTools.Database
{
    public static class DbExampleExecutor
    {
        public static async Task ExecuteDbExample(
            SupportedDatabaseTypes databaseType,
            bool useDbProviderFactory,
            Dictionary<string, string> overrideConfig = null)
        {
            var provider = DbExampleBuilder.BuildDbExample(
                databaseType,
                useDbProviderFactory,
                overrideConfig);

            var persons = CreatePersons(10).ToArray();
            
            using (var scope = provider.CreateScope())
            {
                var dbConnection = scope.ServiceProvider.GetService<IDbConnection>();
                var repository = scope.ServiceProvider.GetService<IPersonRepository>();

                foreach (var person in persons)
                {
                    await repository.InsertPerson(person, dbConnection);
                }
                
                (await repository.CountAlivePersons(dbConnection)).Should().BeInRange(10,20); // using range because of parallell test runs...
                (await repository.SelectPerson(persons.First().Id, dbConnection)).Id.Should().Be(persons.First().Id);

                var subsetPersons = persons.Take(persons.Length / 2).ToArray();
                var selectedPersons = (await repository.SelectPersons(subsetPersons.Select(x => x.Id).ToArray(), dbConnection)).Select(x => x.Id).ToArray();
                selectedPersons.Length.Should().Be(subsetPersons.Length);
                selectedPersons.Should().Contain(subsetPersons.Select(x => x.Id));

                persons.First().Alive = false;
                persons.First().Username = "New Name";
                await repository.UpdatePerson(persons.First(), dbConnection);
                (await repository.SelectPerson(persons.First().Id, dbConnection)).Id.Should().Be(persons.First().Id);

                await repository.DeletePerson(persons.First().Id, dbConnection);
                (await repository.SelectPersons(persons.Select(x => x.Id).ToArray(), dbConnection)).Should().NotContain(persons.First());
            }
            
        }

        private static IEnumerable<Person> CreatePersons(int nPersons)
        {
            var persons = new List<Person>();
            for (var i = 0; i < nPersons; i++)
            {
                var person = new Person {SequenceNumber = i + 1};
                persons.Add(person);
            }

            return persons;
        }
    }
}