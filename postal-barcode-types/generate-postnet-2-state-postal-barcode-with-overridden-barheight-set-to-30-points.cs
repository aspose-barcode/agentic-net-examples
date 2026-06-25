using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Postnet barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Postnet barcode for a sample ZIP code
    /// and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Sample ZIP code for Postnet (5‑digit)
        string zipCode = "12345";

        // Output file path for the generated barcode image
        string outputPath = "postnet.png";

        // Create a Postnet barcode generator with the specified code text
        using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, zipCode))
        {
            // Disable automatic sizing so that the custom BarHeight is applied
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Set the bar height to 30 points (manual sizing)
            generator.Parameters.Barcode.BarHeight.Point = 30f;

            // Save the generated barcode image to the specified file path
            generator.Save(outputPath);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Postnet barcode saved to {outputPath}");
    }
}