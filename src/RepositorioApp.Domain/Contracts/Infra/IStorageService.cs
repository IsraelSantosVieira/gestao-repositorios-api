using System;
using System.Threading.Tasks;
using RepositorioApp.CrossCutting.Config;
using RepositorioApp.CrossCutting.Models;
namespace RepositorioApp.Domain.Contracts.Infra
{
    public interface IStorageService
    {
        AzureBlobConfig Config { get; }
        Task<string> UploadAsync(FileUpload fileInput, string fileName, bool isPublic = true);
        Task<string> UploadAsync(byte[] byteArray, string fileName, bool isPublic = true);
        Task<byte[]> DownloadAsync(string fileName, bool isPublic = true);
        Task<string> GetAuthUrlFileWithBlobSasAsync(Uri uri, DateTime expirationDate);
        Task<bool> RemoveByUrlAsync(string url);
        Task<bool> RemoveByKeyAsync(string key, bool isPublic = true);
        Task<string> StoreImageAsync(FileUpload image, Guid entityId, string pathType);
    }
}
