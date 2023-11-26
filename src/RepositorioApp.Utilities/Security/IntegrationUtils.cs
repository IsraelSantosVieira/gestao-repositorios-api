using System;
using System.Text.RegularExpressions;
namespace RepositorioApp.Utilities.Security
{
    public static class IntegrationUtils
    {
        public const int INTEGRATION_CODE_SIZE = 23;
        public const int UNIQUE_CODE_SIZE = 6;
        public const int FULL_CNPJ_SIZE = 14;
        public const int MATRIX_CNPJ_SIZE = 8;
        public const int VERSION_SIZE = 2;

        public static string CreateStoreIntegrationCode(string version, string cnpj, string accessCode)
        {
            var originCnpj = cnpj[..MATRIX_CNPJ_SIZE];
            var key = version + originCnpj + accessCode;
            return accessCode + "-" + GetVerificationDigit(key);
        }

        public static string CreateRandomCode()
        {
            var random = new Random();
            var code = random.Next(10 ^ UNIQUE_CODE_SIZE);
            return code.ToString("D6");
        }

        public static int GetVerificationDigit(string key)
        {
            var weight = 2;
            var product = 0;

            for (var i = key.Length - 1; i >= 0; i--)
            {
                var number = int.Parse(key[i].ToString());
                product += number * weight;

                if (weight == 9)
                {
                    weight = 2;
                }
                else
                {
                    weight++;
                }
            }

            var rest = product % 11;
            return rest is 0 or 1 ? 0 : 11 - rest;
        }

        public static string GetVersionFromClientId(string clientId)
        {
            return clientId[..VERSION_SIZE];
        }

        public static string GetCnpjFromClientId(string clientId)
        {
            return clientId.Substring(VERSION_SIZE, FULL_CNPJ_SIZE);
        }

        public static string GetCodeFromClientId(string clientId)
        {
            return clientId.Substring(VERSION_SIZE + FULL_CNPJ_SIZE, UNIQUE_CODE_SIZE);
        }

        public static string GetVerificationDigitFromClientId(string clientId)
        {
            return clientId.Substring(INTEGRATION_CODE_SIZE - 1, 1);
        }

        public static int GetServerVersion(string version)
        {
            return int.Parse(Regex.Replace(version, @"[^0-9]", ""));
        }

        public static bool CompareStoreCnpj(string headOffice, string filial)
        {
            var headOfficeCnpj = headOffice[..MATRIX_CNPJ_SIZE];
            var filialCnpj = filial[..MATRIX_CNPJ_SIZE];
            return headOfficeCnpj == filialCnpj;
        }
    }
}
