using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
namespace RepositorioApp.Data.Imports
{
    public class UniversitiesSqlGenerator
    {
        public static string Sql()
        {
            var data = File.ReadAllText($"{AppContext.BaseDirectory}/Imports/universities-br.json");
            var universities = JsonSerializer.Deserialize<List<University>>(data);
            var sql = "INSERT INTO universities(id, name, acronym, uf) VALUES";

            foreach (var university in universities)
            {
                sql += $"('{Guid.NewGuid()}', '{university.Universidade}', '{university.Sigla}', '{university.Uf}'),";
            }

            sql += $"('{Guid.NewGuid()}', 'Outra', 'Outra', 'NA')";
            
            return sql + ";";
        }

        private class University
        {
            public string Universidade { get; set; }
            public string Sigla { get; set; }
            public string Uf { get; set; }
        }
    }
}
