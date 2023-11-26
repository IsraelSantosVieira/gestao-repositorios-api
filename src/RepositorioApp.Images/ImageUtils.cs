using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
namespace RepositorioApp.Images
{
    public static class ImageUtils
    {
        // /// <summary>
        // /// Resizes an image for the purpose of generating a lower quality image.
        // /// On Linux and Mac libgdiplus should be installed:
        // /// Linux Debian based distros: $ sudo apt install libgdiplus
        // /// Mac: $ brew install mono-libgdiplus
        // /// </summary>
        // /// <param name="imageBuffer">Byte array of image that will be resized</param>
        // /// <param name="scaleFactor">Resize factor in %, eg: to get 30% of original image use 0.3D</param>
        // /// <returns>Resized image in byte array</returns>
        // public static byte[] ResizeImageFromBuffer(byte[] imageBuffer, double scaleFactor)
        // {
        //     using var ms = new MemoryStream(imageBuffer);
        //
        //     using var image = MediaTypeNames.Image.FromStream(ms);
        //
        //     var newWidth = (int)(image.Width * scaleFactor);
        //
        //     var newHeight = (int)(image.Height * scaleFactor);
        //
        //     using var thumbnailBitmap = new Bitmap(newWidth, newHeight);
        //
        //     using var thumbnailGraph = Graphics.FromImage(thumbnailBitmap);
        //
        //     thumbnailGraph.CompositingQuality = CompositingQuality.HighQuality;
        //
        //     thumbnailGraph.SmoothingMode = SmoothingMode.HighQuality;
        //
        //     thumbnailGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //
        //     var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
        //
        //     thumbnailGraph.DrawImage(image, imageRectangle);
        //
        //     using var stramResult = new MemoryStream();
        //
        //     thumbnailBitmap.Save(stramResult, image.RawFormat);
        //
        //     return stramResult.ToArray();
        // }
        //
        // /// <summary>
        // /// Compress an image for the purpose of generating a lower size image.
        // /// </summary>
        // /// <param name="imageBuffer">Byte array of image that will be resized</param>
        // /// <param name="compressionLevel">Compressein level, 1 to 100 allowed. Lower compression level values will generate lower size images.
        // ///  Recommended value on compression level is 30. Be aware with quality loss.
        // /// </param>
        // /// <param name="fileName">Image file name. Necessary to get the image mime type</param>
        // /// <returns>Resized image in byte array</returns>

        /// <summary>
        ///     Compress an png or jpeg image
        /// </summary>
        /// <param name="buffer">Byte array of image that will be compressed</param>
        /// <param name="level" cref="ECompressionLevel">Compresseion level to be applied</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async ValueTask<byte[]> Compress(byte[] buffer,
            ECompressionLevel level = ECompressionLevel.Best,
            CancellationToken cancellationToken = default)
        {
            var image = Image.Load<Rgba32>(buffer, out var format);

            var bufferResult = format.Name.ToLower() switch
            {
                "png" => await CompressPng(image, AdapterToPngCompressionLevel(level), cancellationToken),
                "jpg" or "jpeg" => await CompressJpg(image, level, cancellationToken),
                _ => throw new Exception("Image must be JPG or PNG")
            };

            return bufferResult.Length <= buffer.Length ? bufferResult : buffer;
        }

        private static PngCompressionLevel AdapterToPngCompressionLevel(ECompressionLevel level)
        {
            return level switch
            {
                ECompressionLevel.Best => PngCompressionLevel.BestCompression,
                ECompressionLevel.Low => PngCompressionLevel.BestSpeed,
                ECompressionLevel.Medium => PngCompressionLevel.Level5,
                _ => PngCompressionLevel.BestCompression
            };
        }

        private static async ValueTask<byte[]> CompressJpg(Image<Rgba32> image, ECompressionLevel compressionLevel, CancellationToken cancellationToken = default)
        {
            var jpgEncoder = new JpegEncoder
            {
                Quality = compressionLevel switch
                {
                    ECompressionLevel.Best => 50,
                    ECompressionLevel.Medium => 60,
                    ECompressionLevel.Low => 65,
                    _ => 50
                }
            };

            using var resultMemoryStream = new MemoryStream();
            await jpgEncoder.EncodeAsync(image, resultMemoryStream, cancellationToken);
            return resultMemoryStream.ToArray();
        }

        private static async ValueTask<byte[]> CompressPng(Image<Rgba32> image, PngCompressionLevel compressionLevel, CancellationToken cancellationToken = default)
        {
            var encoder = new PngEncoder
            {
                IgnoreMetadata = true,
                CompressionLevel = compressionLevel
            };
            using var memoryStream = new MemoryStream();
            await encoder.EncodeAsync(image, memoryStream, cancellationToken);
            return memoryStream.ToArray();
        }

        private static async ValueTask<byte[]> CompressPng(byte[] buffer, PngCompressionLevel compressionLevel, CancellationToken cancellationToken = default)
        {
            return await CompressPng(Image.Load<Rgba32>(buffer), compressionLevel, cancellationToken);
        }

        /// <summary>
        ///     Generate image with wather mark
        /// </summary>
        /// <param name="sourceImage">Byte array of image that will be applied water mark</param>
        /// <param name="waterMarkImage">Byte array of image water mark to be applied</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async ValueTask<byte[]> GenerateImageWithWatherMark(byte[] sourceImage, byte[] waterMarkImage, CancellationToken cancellationToken = default)
        {
            using var logo = Image.Load<Rgba32>(waterMarkImage);

            using var image = Image.Load<Rgba32>(sourceImage);

            var retangulo = image.Width > image.Height;
            var coff = retangulo
                ? image.Width * 0.1 / Math.Min(logo.Width, logo.Height)
                : image.Height * 0.1 / Math.Max(logo.Width, logo.Height);

            var size = new Size((int)(logo.Width * coff), (int)(logo.Height * coff));

            // Resize the logo
            logo.Mutate(i => i.Resize(size));

            //loop over the height of the image
            for (var y = 0; y < image.Height - size.Height / 3; y += size.Height)
            {
                //loop over the width of the image
                for (var x = 0; x <= image.Width - size.Width / 3; x += size.Width)
                {
                    //draw the logo on the image
                    var point = new Point(x, y);

                    image.Mutate(i => i.DrawImage(logo, point, 0.2f));
                }
            }

            using var memoryStream = new MemoryStream();
            var imageEncoder = image.GetConfiguration().ImageFormatsManager.FindEncoder(PngFormat.Instance);
            await image.SaveAsync(memoryStream, imageEncoder, cancellationToken);
            return memoryStream.ToArray();
        }

        /// <summary>
        ///     Generate thumbnail of an image
        /// </summary>
        /// <param name="buffer">Byte array of image to generate thumbnail image</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async ValueTask<byte[]> GenarateThumbnail(byte[] buffer, CancellationToken cancellationToken = default)
        {
            using var image = Image.Load<Rgba32>(buffer);

            var coff = image.Width > image.Height
                ? image.Width / (double)image.Height
                : image.Height / (double)image.Width;

            var size = image.Width > image.Height
                ? new Size(500, (int)(500 / coff))
                : new Size((int)(500 / coff), 500);

            // Resize the image
            image.Mutate(i => i.Resize(size));

            using var memoryStream = new MemoryStream();
            var imageEncoder = image.GetConfiguration().ImageFormatsManager.FindEncoder(PngFormat.Instance);
            await image.SaveAsync(memoryStream, imageEncoder, cancellationToken);
            return memoryStream.ToArray();
        }

        /// <summary>
        ///     Resizes an image for the purpose of generating a lower quality image.
        ///     On Linux and Mac libgdiplus should be installed:
        ///     Linux Debian based distros: $ sudo apt install libgdiplus
        ///     Mac: $ brew install mono-libgdiplus
        /// </summary>
        /// <param name="buffer">Byte array of image that will be resized</param>
        /// <param name="scaleFactor">Resize factor in %, eg: to get 30% of original image use 0.3D</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Resized image in byte array</returns>
        public static async ValueTask<byte[]> ResizeImageFromBuffer(byte[] buffer, double scaleFactor, CancellationToken cancellationToken = default)
        {
            using var image = Image.Load<Rgba32>(buffer);

            var newWidth = (int)(image.Width * scaleFactor);

            var newHeight = (int)(image.Height * scaleFactor);

            var size = new Size(newWidth, newHeight);

            // Resize the image
            image.Mutate(i => i.Resize(size));

            using var memoryStream = new MemoryStream();
            await image.SaveAsync(memoryStream, new JpegEncoder(), cancellationToken);
            return memoryStream.ToArray();
        }
    }
}
