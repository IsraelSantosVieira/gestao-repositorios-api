namespace RepositorioApp.Domain.Messages
{
    public static class UserMessages
    {
        public const string NotExistsError = "Usuário não existe";

        public const string MaxSessionsOpenError = "Número máximo de sessão ativas atingido";

        public const string ChangeSelfStatusError = "Não é permitido alterar o próprio status";

        public const string AcceptTermRequired = "O termo de uso deve ser aceito";

        public const string SelfRemoveErro = "Não é permitido auto exclusão";

        public const string EmailIsUsed = "Este e-mail já está sendo usado";

        public static string EmailAlreadyUsedError(string email)
        {
            return $"Já existe um usuário com e-mail {email} cadastrado";
        }

        public static class _Authentication
        {
            public const string GrantTypeRequiredError = "Grant Type é obrigatório";
            public const string GrantTypeInvalidError = "Grant Type é inválido";
        }

        public static class _Email
        {
            public const string RequiredError = "E-mail é obrigatório";

            public const string MaxLengtheError = "E-mail deve conter no máximo 255 caracteres";

            public const string InvalidError = "E-mail inválido";
        }

        public static class _FirstName
        {
            public const string RequiredError = "Nome é obrigatório";

            public const string MaxLengthError = "Nome deve conter no máximo 255 caracteres";
        }

        public static class _LastName
        {
            public const string MaxLengthError = "Sobrenome deve conter no máximo 255 caracteres";
        }

        public static class _Phone
        {
            public const string MaxLengthError = "Telefone deve conter no máximo 11 caracteres";
            public const string RequiredError = "Telefone é obrigatório";
        }

        public static class _Password
        {
            public const string CurrentRequiredError = "Senha atual é obrigatória";

            public const string CurrentIncorrectError = "Senha autal incorreta";

            public const string RequiredError = "Senha é obrigatória";

            public const string NewRequridError = "Nova senha é obrigatória";

            public const string NewConfirmationRequiredError = "Confirmação da nova senha é obrigatória";

            public const string NotEqualsError = "Senhas não são iguais";
        }


        public static class _RefreshToken
        {
            public const string RequiredError = "E-mail é obrigatório";
            public const string ExpiredError = "Refresh Token inválido ou expirado";
        }


        public static class _ResetPassword
        {
            public const string CodeRequiredError = "Código é obrigatório";
            public const string CodeInvalidError = "Código inválido";
            public const string SuccessMessage = "Senha alterada com sucesso";
        }

        public static class _UserActivate
        {
            public const string CodeRequiredError = "Código é obrigatório";
            public const string CodeInvalidError = "Código inválido";
            public const string SuccessMessage = "Senha alterada com sucesso";
        }

        public static class _Avatar
        {
            public const string ImageRequiredError = "Arquivo da imagem é obrigatório";
            public const string BufferRequiredError = "O bufffer da imagem é obrigatório";
            public const string ContentTypeRequiredError = "O content type da imagem é obrigatório";
            public const string ContentTypeInvalidError = "Content Type do Banner deve ser do tipo imagem";
        }

        public static class _RecoverPassword
        {
            public const string GenericError = "Ocorreu um erro ao tentar reenviar o código de ativação";
            
            public static string MessageResult(string email)
            {
                return $"Se houver um registro em nossa base de dados, iremos enviar instruções de como resetar senha de acesso para o e-mail {email}";
            }
        }
        
        public static class _TwoFactor
        {
            public static class _Code
            {
                public const string RequiredError = "Código de autenticação é obrigatório";
                public const string InvalidError = "Código de autenticação é inválido";
            }
        }
    }
}
