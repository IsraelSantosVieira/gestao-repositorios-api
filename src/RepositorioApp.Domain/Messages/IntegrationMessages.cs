namespace RepositorioApp.Domain.Messages
{
    public static class IntegrationMessages
    {
        public const string IdRequired = "O id de integração deve ser informado e deve conter 23 dígitos " +
                                         "sem caracteres especiais";

        public const string EmailRequired = "O endereço de email do cliente é obrigatório";

        public const string WrongVersion = "A versão informada na integração não coincide com a versão da API";

        public const string InvalidCnpj = "O CNPJ informado não é válido";

        public const string NotRecognizedSource = "Origem de integração desconhecida";

        public const string NotActiveIntegration = "A integração para esse revendedor não está ativa ou expirou";

        public const string NotRecognizedCode = "O código de integração não foi reconhecido";

        public const string NotRecognizedDigit = "O dígito verificador não coincide com o da integração";
    }
}
