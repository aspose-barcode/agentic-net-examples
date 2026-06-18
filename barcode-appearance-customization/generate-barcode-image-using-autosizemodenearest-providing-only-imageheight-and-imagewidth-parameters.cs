using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Generates a barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output PNG file in the current directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // Initialize a BarcodeGenerator for Code128 symbology with the sample data "123456".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Configure the generator to automatically size the barcode to the nearest possible dimensions.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;

            // Set the desired image dimensions in points (1 point = 1/72 inch). These values are used by the Nearest mode.
            generator.Parameters.ImageWidth.Point = 200f;
            generator.Parameters.ImageHeight.Point = 100f;

            // Save the generated barcode image to the specified path.
            // The file format (PNG) is inferred from the file extension.
            generator.Save(outputPath);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}