using System;
using System.Linq;
using System.Text.RegularExpressions;
namespace RepositorioApp.Domain.ValueObjects
{
    public class Address
    {
        public string Street { get; set; }

        public string Number { get; set; }

        public string Complement { get; set; }

        public string Neighborhood { get; set; }
        public string ZipCode { get; set; }

        public string City { get; set; }

        public string Uf { get; set; }

        public string State { get; set; }

        public string ZipCodeFormatted
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ZipCode)) return string.Empty;

                return Convert.ToUInt64(Regex.Replace(ZipCode, @"[^0-9]", string.Empty))
                    .ToString(@"00000\-000");
            }
        }

        public string FullAddress
        {
            get
            {
                var value = new[]
                {
                    Street,
                    Complement,
                    Neighborhood,
                    ZipCodeFormatted,
                    !string.IsNullOrWhiteSpace(City) && !string.IsNullOrWhiteSpace(Uf) ? $"{City}-{Uf}" :
                    !string.IsNullOrWhiteSpace(City) ? City : ""
                };

                return string.Join(", ", value.Where(x => !string.IsNullOrWhiteSpace(x)));
            }
        }

        public string CompleteState
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Uf)) return string.Empty;

                switch (Uf)
                {
                    case "AC":
                        return "Acre";
                    case "AL":
                        return "Alagoas";
                    case "AM":
                        return "Amazonas";
                    case "AP":
                        return "Amapá";
                    case "BA":
                        return "Bahia";
                    case "CE":
                        return "Ceará";
                    case "DF":
                        return "Distrito Federal";
                    case "ES":
                        return "Espírito Santo";
                    case "GO":
                        return "Goiás";
                    case "MA":
                        return "Maranhão";
                    case "MG":
                        return "Minas Gerais";
                    case "MS":
                        return "Mato Grosso do Sul";
                    case "MT":
                        return "Mato Grosso";
                    case "PA":
                        return "Pará";
                    case "PB":
                        return "Paraíba";
                    case "PE":
                        return "Pernambuco";
                    case "PI":
                        return "Piauí";
                    case "PR":
                        return "Paraná";
                    case "RJ":
                        return "Rio de Janeiro";
                    case "RN":
                        return "Rio Grande do Norte";
                    case "RO":
                        return "Rondônia";
                    case "RR":
                        return "Roraima";
                    case "RS":
                        return "Rio Grande do Sul";
                    case "SC":
                        return "Santa Catarina";
                    case "SP":
                        return "São Paulo";
                    case "SE":
                        return "Sergipe";
                    case "TO":
                        return "Tocantins";
                    default:
                        return "";
                }
            }
        }
    }
}
