using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
namespace RepositorioApp.Data.Imports
{
    public class CityStateSqlGenerator
    {
        public static string Sql()
        {
            var data = File.ReadAllText($"{AppContext.BaseDirectory}/Imports/cities-states.json");
            var cityStates = JsonSerializer.Deserialize<List<CityState>>(data);
            var statesSql = "INSERT INTO states(id, initials, name) VALUES";
            var citiesSql = "INSERT INTO cities(id, state_id, name) VALUES";

            for (var i = 0; i < cityStates.Count; i++)
            {
                var stateId = Guid.NewGuid();
                statesSql += $"('{stateId}', '{cityStates[i].Sigla}', '{cityStates[i].Nome}')";
                if (i != cityStates.Count - 1) statesSql += ",";

                for (var j = 0; j < cityStates[i].Cidades.Count; j++)
                {
                    var cityId = Guid.NewGuid();
                    citiesSql += $"('{cityId}', '{stateId}', '{cityStates[i].Cidades[j].Replace("'", "''")}'),";
                }
            }
            citiesSql = citiesSql.Remove(citiesSql.Length - 1);
            citiesSql += ";";
            statesSql += ";";

            return statesSql + citiesSql;
        }

        private class CityState
        {
            public string Sigla { get; }
            public string Nome { get; }
            public IList<string> Cidades { get; }
        }
    }
}
