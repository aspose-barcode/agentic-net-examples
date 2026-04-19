using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeExportDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Determine desired image format (png, jpeg, tiff). Default to png.
            string formatArg = args.Length > 0 ? args[0].ToLowerInvariant() : "png";

            BarCodeImageFormat imageFormat;
            string fileExtension;

            switch (formatArg)
            {
                case "png":
                    imageFormat = BarCodeImageFormat.Png;
                    fileExtension = "png";
                    break;
                case "jpeg":
                case "jpg":
                    imageFormat = BarCodeImageFormat.Jpeg;
                    fileExtension = "jpg";
                    break;
                case "tiff":
                case "tif":
                    imageFormat = BarCodeImageFormat.Tiff;
                    fileExtension = "tif";
                    break;
                default:
                    throw new ArgumentException($"Unsupported image format: {formatArg}. Supported formats are png, jpeg, tiff.");
            }

            // Prepare output file path
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), $"barcode.{fileExtension}");

            // Create a barcode generator for Code128 with sample text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Example: set some basic parameters (optional)
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Save the barcode image in the selected format
                generator.Save(outputPath, imageFormat);
            }

            Console.WriteLine($"Barcode saved to: {outputPath}");
        }
    }
}