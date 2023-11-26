using System.Threading.Tasks;
using RepositorioApp.Images;
namespace RepositorioApp.CrossCutting.Utils
{
    public static class ImagesExtensions
    {
        public static async Task<byte[]> Compress(byte[] buffer)
        {
            var result = await ImageUtils.Compress(buffer, ECompressionLevel.Medium);
            return result.Length < buffer.Length
                ? result
                : buffer;
        }
    }
}
