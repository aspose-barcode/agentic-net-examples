using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        const string outputPath = "barcode.png";

        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the resolution to 250 DPI
            generator.Parameters.Resolution = 250f;

            // Save the barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Evaluate the generated file size
        if (File.Exists(outputPath))
        {
            var fileInfo = new FileInfo(outputPath);
            Console.WriteLine($"Generated barcode file size: {fileInfo.Length} bytes");
        }
        else
        {
            Console.WriteLine("Barcode image was not created.");
        }
    }
}