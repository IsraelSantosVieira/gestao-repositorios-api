using System.Threading.Tasks;
using RepositorioApp.CrossCutting.Constants;
using RepositorioApp.Domain.AppServices.Integration.Commands;
using RepositorioApp.Domain.Messages;
using RepositorioApp.Domain.Results;
using RepositorioApp.Domain.ValueObjects;
using RepositorioApp.Utilities.Persistence;
using RepositorioApp.Utilities.Security;
namespace RepositorioApp.Domain.AppServices.Integration
{
    public class UseIntegrationAppService : BaseAppService, IUseIntegrationAppService
    {

        public UseIntegrationAppService(IUnitOfWork uow) : base(uow)
        {
        }

        public async Task<IntegrationResult> Validate(IntegrationDataCmd command)
        {
            var result = new IntegrationResult();

            var versionError = ValidateVersion(command.ClientId);
            if (versionError != string.Empty)
            {
                result.Error = versionError;
                result.Valid = false;
                return result;
            }

            var filialCnpjError = ValidateCnpj(command.ClientId);
            if (filialCnpjError != string.Empty)
            {
                result.Error = filialCnpjError;
                result.Valid = false;
                return result;
            }

            var validateCodeError = ValidateCode(command.ClientId, command.IntegrationCode);
            if (validateCodeError != string.Empty)
            {
                result.Error = validateCodeError;
                result.Valid = false;
                return result;
            }

            result.Error = string.Empty;
            result.Valid = true;
            return result;
        }

        private static string ValidateVersion(string clientId)
        {
            var clientVersion = int.Parse(IntegrationUtils.GetVersionFromClientId(clientId));
            var serverVersion = AppConstants.ApisConfiguration[AppConstants.PublicApiV1Key].Version;
            var serverVersionFormatted = IntegrationUtils.GetServerVersion(serverVersion);

            if (clientVersion <= 0 || clientVersion > serverVersionFormatted)
            {
                return IntegrationMessages.WrongVersion;
            }

            return string.Empty;
        }

        private static string ValidateCnpj(string clientId)
        {
            var filialCnpjNumber = IntegrationUtils.GetCnpjFromClientId(clientId);
            var filialCnpj = new Cnpj(filialCnpjNumber);
            return filialCnpj.IsValid ? string.Empty : IntegrationMessages.InvalidCnpj;
        }

        private static string ValidateCode(string clientId, string integrationCode)
        {
            var clientCode = IntegrationUtils.GetCodeFromClientId(clientId);
            var verificationDigit = IntegrationUtils.GetVerificationDigitFromClientId(clientId);

            var clientCodeWithDigit = integrationCode.Split("-");
            if (clientCodeWithDigit[0] != clientCode)
            {
                return IntegrationMessages.NotRecognizedCode;
            }

            return clientCodeWithDigit[1] != verificationDigit ? IntegrationMessages.NotRecognizedDigit : string.Empty;
        }
    }

    public interface IUseIntegrationAppService
    {
        Task<IntegrationResult> Validate(IntegrationDataCmd command);
    }
}
