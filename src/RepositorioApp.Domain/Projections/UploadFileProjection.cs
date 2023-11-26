using System.Collections.Generic;
using System.Linq;
using RepositorioApp.Domain.Entities;
using RepositorioApp.Domain.ViewsModels;
using RepositorioApp.Extensions;
namespace RepositorioApp.Domain.Projections
{
    public static class UploadFileProjection
    {
        public static IQueryable<UploadFileVm> ToVm(this IQueryable<UploadFile> query)
        {
            return query.Select(entity => new UploadFileVm
            {
                Id = entity.Id,
                UploadStatus = entity.UploadStatus.Description(),
                CreatedAt = entity.CreatedAt
            });
        }

        public static IEnumerable<UploadFileVm> ToVm(this IEnumerable<UploadFile> query)
        {
            return query.AsQueryable().ToVm();
        }

        public static UploadFileVm ToVm(this UploadFile entity)
        {
            return new[]
            {
                entity
            }.AsQueryable().ToVm().FirstOrDefault();
        }
    }
}
