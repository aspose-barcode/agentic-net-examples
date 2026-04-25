using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Data to encode in the QR code
        string codeText = "https://example.com";

        // Local file path for the thumbnail image
        string localPath = "qr_thumbnail.jpg";

        // Create and configure the QR code generator
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Use interpolation auto‑size mode and set image dimensions
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Point = 200f;
            generator.Parameters.ImageHeight.Point = 200f;

            // QR specific settings
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Visual appearance
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Generate the image into a memory stream as JPEG
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Jpeg);
                ms.Position = 0;

                // Save locally
                File.WriteAllBytes(localPath, ms.ToArray());

                // Real cloud upload (commented out – SDK not available in the runner)
                // Example for Azure Blob Storage:
                // var blobClient = new BlobClient(connectionString, containerName, blobName);
                // blobClient.Upload(ms);
                // Example for AWS S3:
                // var s3Client = new AmazonS3Client(accessKey, secretKey, RegionEndpoint.USEast1);
                // var putRequest = new PutObjectRequest
                // {
                //     BucketName = bucketName,
                //     Key = objectKey,
                //     InputStream = ms,
                //     ContentType = "image/jpeg"
                // };
                // s3Client.PutObjectAsync(putRequest).Wait();
            }
        }

        Console.WriteLine($"QR code thumbnail saved to '{localPath}'.");
    }
}