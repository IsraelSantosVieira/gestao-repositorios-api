using System.ComponentModel;
namespace RepositorioApp.Domain.Enums
{
    public enum EParameterType : short
    {
        [Description("Texto")] Text = 1,
        [Description("Inteiro")] Integer = 2,
        [Description("Decimal")] Decimal = 3,
        [Description("Lógico")] Boolean = 4
    }
}
