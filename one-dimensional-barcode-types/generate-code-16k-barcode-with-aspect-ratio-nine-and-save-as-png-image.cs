using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a Code16K barcode and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Code16K barcode with sample data,
    /// configures its aspect ratio, saves it to a PNG file, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output PNG file in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "code16k.png");

        // Initialize a barcode generator for Code16K format with the sample text "1234567890".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, "1234567890"))
        {
            // Configure the barcode's aspect ratio (height divided by width) to 9.
            generator.Parameters.Barcode.Code16K.AspectRatio = 9f;

            // Render and save the barcode image to the specified file path in PNG format.
            generator.Save(outputPath);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Code16K barcode saved to: {outputPath}");
    }
}