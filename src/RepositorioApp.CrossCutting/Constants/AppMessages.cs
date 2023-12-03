using RepositorioApp.Utilities.Json;
namespace RepositorioApp.CrossCutting.Constants
{
    public class AppMessages
    {
        public const string InternalServerError = "Erro interno no servidor";

        public const string NotFond = "Recurso não encontrado";

        public const string Unauthorized = "Não autorizado";

        public const string Forbidden = "Não encontrado";

        public const string UnprocessableEntity = "Payload não processado";

        public const string ErrorOnProcessEvent = "Erro de processamento do payload";

        public static readonly string ProblemSavindDataFriendly = "Ops, não foi possível processar sua requisição no momento. Por favor tente novamente mais tarde";

        public static readonly string InvalidCredentials = "E-mail e/ou senha inválido(s)";

        public static readonly string InvalidScope = "Usuário pertencente a outro escopo.";

        public static readonly string InvalidSession = "Sessão do usuário é inválida ou não possui conta";

        public static readonly string ErrorSavingS3File = "Erro ao salvar arquivo em storage";

        public static readonly string ErrorSavingImage = "Erro ao salvar arquivo imagem";

        public static string BuiltRequiredMessageError(string propertyName)
        {
            return $"{propertyName} é requerido";
        }

        public static string BuiltMaxLenghMessageError(string propertyName,
            int maxLength)
        {
            return $"{propertyName} deve conter no máximo {maxLength} caracteres";
        }

        public static string BuildResourceNotFoundError(string resourceName)
        {
            return $"{resourceName} não existe";
        }
        public static string BuildErrorOnProcessEvent(string payload)
        {
            return $"{ErrorOnProcessEvent}: {payload}";
        }

        public static string BuildErrorOnProcessEvent(string eventType, object payload, string error = null)
        {
            return $"{ErrorOnProcessEvent}: {eventType} -> {JsonUtils.Serialize(new { payload, error })}";
        }

        public static string BuildErrorOnProcessEvent(object payload, string error = null)
        {
            return $"{ErrorOnProcessEvent}: {JsonUtils.Serialize(new { payload, error })}";
        }
    }
}
