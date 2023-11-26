using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using RepositorioApp.CrossCutting.Config;
using RepositorioApp.CrossCutting.Models;
using RepositorioApp.CrossCutting.Utils;
using RepositorioApp.Domain.Contracts.Infra;
using RepositorioApp.Extensions;
namespace RepositorioApp.Infra.Services
{
    public class StorageService : IStorageService
    {
        private readonly AzureBlobConfig _config;
        public StorageService(AzureBlobConfig config)
        {
            _config = config;
            Config = config;
        }
        public AzureBlobConfig Config { get; }
        public async Task<string> UploadAsync(FileUpload logo, string fileName, bool isPublic = true)
        {
            return await UploadAsync(logo.Buffer, fileName, isPublic);
        }
        public async Task<string> UploadAsync(byte[] byteArray, string fileName, bool isPublic = true)
        {
            new FileExtensionContentTypeProvider().TryGetContentType(fileName, out var contentType);
            if (!CloudStorageAccount.TryParse(_config.ConnectionString, out var account)) return null;
            var client = account.CreateCloudBlobClient();
            var container = client.GetContainerReference(isPublic ? _config.PublicContainer : _config.PrivateContainer);
            if (!await container.ExistsAsync()) return null;
            var blob = container.GetBlockBlobReference(fileName);
            using (var ms = new MemoryStream(byteArray))
            {
                blob.Properties.ContentType = contentType ?? fileName.HandleContentType();
                await blob.UploadFromStreamAsync(new MemoryStream(byteArray));
                return blob.Uri.ToString();
            }
        }


        public async Task<byte[]> DownloadAsync(string fileName, bool isPublic = true)
        {
            if (!CloudStorageAccount.TryParse(_config.ConnectionString, out var account)) return null;
            var client = account.CreateCloudBlobClient();
            var container = client.GetContainerReference(isPublic ? _config.PublicContainer : _config.PrivateContainer);
            if (!await container.ExistsAsync()) return null;
            var blob = container.GetBlockBlobReference(UrlToFileName(fileName));
            using (var ms = new MemoryStream())
            {
                await blob.DownloadToStreamAsync(ms);
                return ms.ToArray();
            }
        }

        public async Task<string> GetAuthUrlFileWithBlobSasAsync(Uri uri, DateTime expirationDate)
        {
            var accountName = CloudStorageAccount.Parse(_config.ConnectionString);
            var cloudBlockBlob = new CloudBlockBlob(uri, new StorageCredentials(accountName.Credentials.AccountName, _config.Key));
            var sharedAccessBlobPolicy = new SharedAccessBlobPolicy
            {
                SharedAccessExpiryTime = expirationDate,
                Permissions = SharedAccessBlobPermissions.Read
            };
            var sharedAcessSignature = cloudBlockBlob.GetSharedAccessSignature(sharedAccessBlobPolicy);
            return await Task.FromResult(cloudBlockBlob.Uri + sharedAcessSignature);
        }

        public async Task<bool> RemoveByKeyAsync(string key, bool isPublic = true)
        {
            if (!CloudStorageAccount.TryParse(_config.ConnectionString, out var account)) return false;
            var client = account.CreateCloudBlobClient();
            var container = client.GetContainerReference(isPublic ? _config.PublicContainer : _config.PrivateContainer);
            var blob = container.GetBlockBlobReference(UrlToFileName(key));
            await blob.DeleteAsync();
            return true;

        }
        public async Task<bool> RemoveByUrlAsync(string url)
        {
            if (!CloudStorageAccount.TryParse(_config.ConnectionString, out var account)) return false;
            var blob = new CloudBlockBlob(new Uri(url), account.Credentials);
            await blob.DeleteAsync();
            return true;

        }

        public async Task<string> StoreImageAsync(FileUpload image, Guid entityId, string pathType)
        {
            return await UploadAsync(
                image.FileName.GetExtension().Equals(".pdf")
                    ? image.Buffer
                    : ImagesExtensions.Compress(image.Buffer).Result,
                $"{pathType}/{entityId}{image.ContentType.GetExtensionFromMimeType()}");
        }

        private static string UrlToFileName(string url)
        {
            if (!url.StartsWith("http"))
                return url;

            var fileName = string.Empty;
            var uri = new Uri(url);

            var count = 0;

            foreach (var segment in uri.Segments)
            {
                if (count > 1)
                    fileName += segment;

                count++;
            }
            return fileName;

        }
    }
}
