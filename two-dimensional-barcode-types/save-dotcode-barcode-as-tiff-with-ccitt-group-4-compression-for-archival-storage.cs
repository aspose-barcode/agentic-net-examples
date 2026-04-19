using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Define output file path
        string outputPath = "dotcode.tif";

        // Ensure the directory exists
        string directory = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Create a DotCode barcode generator with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, "SampleDotCode"))
        {
            // Set resolution suitable for archival quality
            generator.Parameters.Resolution = 300f;

            // Optionally, set image dimensions or other parameters here
            // generator.Parameters.ImageWidth.Point = 400f;
            // generator.Parameters.ImageHeight.Point = 400f;

            // Save the barcode as TIFF. Aspose.BarCode uses CCITT Group 4 compression for monochrome TIFF by default.
            generator.Save(outputPath, BarCodeImageFormat.Tiff);
        }

        Console.WriteLine($"DotCode barcode saved to {outputPath}");
    }
}