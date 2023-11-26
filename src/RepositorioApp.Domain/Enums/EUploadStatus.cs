using System.ComponentModel;
namespace RepositorioApp.Domain.Enums
{
    public enum EUploadStatus : short
    {
        [Description("Processando")]
        Processing = 1,
        [Description("Completado")]
        Completed = 2,
        [Description("Erro")]
        Failed = 3
    }
}
