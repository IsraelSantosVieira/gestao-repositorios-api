using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Newtonsoft.Json;
namespace BitCode.Correios.Consulta.Cep
{
    [XmlRoot(ElementName = "return")]
    public class ConsultaCepResposta
    {

        private string _cep;
        public ConsultaCepResposta()
        {
        }

        public ConsultaCepResposta(
            string endereco,
            string bairro,
            string cep,
            string cidade,
            string uf,
            string complement1 = null,
            string complement2 = null)
        {
            Endereco = endereco;
            Complemento = string.IsNullOrWhiteSpace(complement1)
                ? complement2
                : complement1;
            Bairro = bairro;
            Cep = cep;
            Cidade = cidade;
            Uf = uf;
        }

        [JsonProperty("logradouro")]
        [XmlElement("end")]
        public string Endereco { get; set; }

        [JsonProperty("complemento")]
        [XmlElement("complemento")]
        public string Complemento { get; set; }

        [XmlElement("complemento2")]
        public string Complemento2 { get; set; }

        [JsonProperty("bairro")]
        [XmlElement("bairro")]
        public string Bairro { get; set; }

        [JsonProperty("cep")]
        [XmlElement("cep")]
        public string Cep
        {
            set => _cep = value;
            get => Regex.Replace(_cep, @"[^0-9]", "");
        }

        [JsonProperty("localidade")]
        [XmlElement("cidade")]
        public string Cidade { get; set; }

        [JsonProperty("ibge")]
        public string CodIbgeCidade { get; set; }

        [JsonProperty("uf")]
        [XmlElement("uf")]
        public string Uf { get; set; }

        [JsonProperty("erro")]
        public bool Erro { get; set; }

        public string CepFormatado
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Cep))
                {
                    return string.Empty;
                }

                if (Cep.Trim().Length == 8)
                {
                    var value = Convert.ToUInt64(Cep);
                    return value.ToString(@"00000\-000");
                }

                return Cep;
            }
        }


        public string EnderecoCompleto
        {
            get
            {
                var value = new[]
                {
                    Endereco,
                    Complemento,
                    Bairro,
                    CepFormatado,
                    !string.IsNullOrWhiteSpace(Cidade) && !string.IsNullOrWhiteSpace(Uf) ? $"{Cidade}-{Uf}" :
                    !string.IsNullOrWhiteSpace(Cidade) ? Cidade : ""
                };

                return string.Join(", ", value.Where(x => !string.IsNullOrWhiteSpace(x)));
            }
        }
    }
}
