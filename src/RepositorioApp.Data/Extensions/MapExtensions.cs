using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RepositorioApp.Persistence.Ef;
namespace RepositorioApp.Data.Extensions
{
    internal static class MapExtensions
    {
        public static void MapId<T>(this EntityTypeBuilder<T> builder,
            Expression<Func<T, object>> exp)
            where T : class
        {
            builder.Property(exp)
                .HasColumnName("id")
                .HasColumnType("uuid")
                .ValueGeneratedNever()
                .IsRequired();
            builder.HasKey(exp);
        }

        public static PropertyBuilder<string> MapVarchar<T, TD>(this OwnedNavigationBuilder<T, TD> builder,
            Expression<Func<TD, string>> exp,
            string columnName,
            int columnSize,
            bool required)
            where T : class
            where TD : class
        {
            var pb = builder.Property(exp)
                .HasColumnName(columnName)
                .HasColumnType($"varchar({columnSize})");
            return required
                ? pb.IsRequired()
                : pb;
        }

        public static PropertyBuilder<DateTime> MapCreatedAt<T>(this EntityTypeBuilder<T> builder,
            Expression<Func<T, DateTime>> exp)
            where T : class
        {
            return builder.MapTimestamp(exp, "created_at");
        }

        public static PropertyBuilder<DateTime?> MapCriadoEm<T>(this EntityTypeBuilder<T> builder,
            Expression<Func<T, DateTime?>> exp)
            where T : class
        {
            return builder.MapTimestamp(exp, "created_at");
        }

        public static PropertyBuilder<Enum> MapEnumAsShort<T>(
            this EntityTypeBuilder<T> builder,
            Expression<Func<T, Enum>> exp,
            string columnName,
            bool isRequired)
            where T : class
        {
            var result = builder.Property(exp)
                .HasColumnName(columnName)
                .HasColumnType("smallint");

            return isRequired
                ? result.IsRequired()
                : result;
        }
    }
}
