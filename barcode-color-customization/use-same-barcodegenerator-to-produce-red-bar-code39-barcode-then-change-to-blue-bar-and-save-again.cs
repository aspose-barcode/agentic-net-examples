using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating Code39 barcodes with different bar colors using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates two PNG barcode images: one with red bars and one with blue bars,
    /// and saves them to the current working directory.
    /// </summary>
    static void Main()
    {
        // Determine the directory where the executable is running.
        string outputDir = Directory.GetCurrentDirectory();

        // Build full file paths for the output images.
        string redPath = Path.Combine(outputDir, "code39_red.png");
        string bluePath = Path.Combine(outputDir, "code39_blue.png");

        // Create a barcode generator for Code39FullASCII with the data "123ABC".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, "123ABC"))
        {
            // Set bar color to red and save the image.
            generator.Parameters.Barcode.BarColor = Color.Red;
            generator.Save(redPath, BarCodeImageFormat.Png);

            // Change bar color to blue and save the second image.
            generator.Parameters.Barcode.BarColor = Color.Blue;
            generator.Save(bluePath, BarCodeImageFormat.Png);
        }

        // Output the locations of the generated files to the console.
        Console.WriteLine($"Red barcode saved to: {redPath}");
        Console.WriteLine($"Blue barcode saved to: {bluePath}");
    }
}