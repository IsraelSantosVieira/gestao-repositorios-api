using System.ComponentModel;
namespace RepositorioApp.Domain.Enums
{
    public enum EStatus : short
    {
        [Description("Ativo")]
        Ativo = 1,

        [Description("Inativo")]
        Inativo = 2
    }
}
