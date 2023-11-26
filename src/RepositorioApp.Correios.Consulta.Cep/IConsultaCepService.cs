using System.Threading.Tasks;
namespace BitCode.Correios.Consulta.Cep
{
    public interface IConsultaCepService
    {
        Task<ConsultaCepResposta> ConsultaCepAsync(string cep);
        ConsultaCepResposta ConsultaCep(string cep);
    }
}
