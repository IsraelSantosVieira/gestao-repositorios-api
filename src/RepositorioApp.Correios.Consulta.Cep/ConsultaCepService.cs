using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace BitCode.Correios.Consulta.Cep
{
    public class ConsultaCepService : IConsultaCepService
    {
        public const string ParametroCepInvalidoErro = "CEP não informado";

        public async Task<ConsultaCepResposta> ConsultaCepAsync(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep))
            {
                throw new ArgumentException(ParametroCepInvalidoErro);
            }

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri($"https://viacep.com.br/ws/{Regex.Replace(cep, @"[^0-9]", "")}/json"),
                Method = HttpMethod.Get
            };

            using var client = new HttpClient();

            var response = await client.SendAsync(request);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var resultdado = JsonConvert.DeserializeObject<ConsultaCepResposta>(responseContent);

            return resultdado?.Erro == false
                ? resultdado
                : null;
        }

        public ConsultaCepResposta ConsultaCep(string cep)
        {
            return ConsultaCepAsync(cep).GetAwaiter().GetResult();
        }

        // public ConsultaCepResposta ConsultaCepWsCorreios(string cep)
        // {
        //     if (string.IsNullOrWhiteSpace(cep))
        //     {
        //         throw new ArgumentException(ParametroCepInvalidoErro);
        //     }
        //
        //     try
        //     {
        //         var sb = new StringBuilder();
        //         sb.Append(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:cli=""http://cliente.bean.master.sigep.bsb.correios.com.br/"">");
        //         sb.Append("<soapenv:Header/>");
        //         sb.Append("<soapenv:Body>");
        //         sb.Append("<cli:consultaCEP>");
        //         sb.AppendFormat("<cep>{0}</cep>", cep);
        //         sb.Append("</cli:consultaCEP>");
        //         sb.Append("</soapenv:Body>");
        //         sb.Append("</soapenv:Envelope>");
        //
        //         var body = sb.ToString();
        //
        //         var client = new RestClient("https://apps.correios.com.br/SigepMasterJPA/AtendeClienteService/AtendeCliente");
        //         var request = new RestRequest(Method.POST);
        //         request.AddHeader("Content-Type", "text/xml");
        //         request.AddHeader("SOAPAction", "http://cliente.bean.master.sigep.bsb.correios.com.br/AtendeCliente/consultaCEP");
        //         request.AddParameter("text/xml", new StringContent(body, Encoding.UTF8, "text/xml"), ParameterType.RequestBody);
        //
        //         var response1 = client.Execute(request);
        //
        //         using var handler = new HttpClientHandler
        //         {
        //             AllowAutoRedirect = false,
        //             AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip,
        //             ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true,
        //             SslProtocols = SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12 | SslProtocols.Ssl3 | SslProtocols.Ssl2
        //         };
        //
        //         var request1 = new HttpRequestMessage
        //         {
        //             RequestUri = new Uri("https://apps.correios.com.br/SigepMasterJPA/AtendeClienteService/AtendeCliente"),
        //             Method = HttpMethod.Post,
        //             Content = new StringContent(body, Encoding.UTF8, "text/xml")
        //         };
        //
        //         using var client1 = new HttpClient(handler);
        //
        //         request1.Headers.Clear();
        //         client1.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));
        //         request1.Content.Headers.ContentType = new MediaTypeHeaderValue("text/xml");
        //         request1.Headers.Add("SOAPAction", "http://cliente.bean.master.sigep.bsb.correios.com.br/AtendeCliente/consultaCEP");
        //
        //         var response = client1.SendAsync(request1).GetAwaiter().GetResult();
        //
        //         var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        //
        //         if (!response.IsSuccessStatusCode)
        //         {
        //             return null;
        //         }
        //
        //         var soapResponse = XDocument.Parse(content);
        //         var xml = soapResponse.Descendants("return").FirstOrDefault()?.ToString();
        //
        //         if (xml == null)
        //         {
        //             return null;
        //         }
        //
        //         var result = Deserialize<ConsultaCepResposta>(xml);
        //
        //         return result;
        //     }
        //     catch (AggregateException e)
        //     {
        //         throw new Exception(e.GetCompleteRecursiveMessage(), e);
        //     }
        //     catch (Exception e)
        //     {
        //         throw new Exception(e.GetCompleteRecursiveMessage(), e);
        //     }
        // }

        // private static T Deserialize<T>(string xmlStr)
        // {
        //     var serializer = new XmlSerializer(typeof(T));
        //     using TextReader reader = new StringReader(xmlStr);
        //     var result = (T) serializer.Deserialize(reader);
        //     return result;
        // }

        // public ConsultaCepResposta ConsultaCep(string cep)
        // {
        //     if (string.IsNullOrWhiteSpace(cep))
        //     {
        //         throw new ArgumentException(ParametroCepInvalidoErro);
        //     }
        //
        //     try
        //     {
        //         var binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
        //
        //         var factory = new ChannelFactory<IAtendeCliente>(
        //             binding,
        //             new EndpointAddress(
        //                 "https://apps.correios.com.br/SigepMasterJPA/AtendeClienteService/AtendeCliente?wsdl"));
        //
        //         var serviceProxy = factory.CreateChannel();
        //
        //         var response = serviceProxy.consultaCEP(new consultaCEP(cep));
        //
        //         return new ConsultaCepResposta(
        //             response?.@return?.end,
        //             response?.@return?.bairro,
        //             response?.@return?.cep,
        //             response?.@return?.cidade,
        //             response?.@return?.uf,
        //             response?.@return?.complemento,
        //             response?.@return?.complemento2);
        //     }
        //     catch (Exception e)
        //     {
        //         throw new Exception(e.GetCompleteRecursiveMessage(), e);
        //     }
        // }
    }
}
