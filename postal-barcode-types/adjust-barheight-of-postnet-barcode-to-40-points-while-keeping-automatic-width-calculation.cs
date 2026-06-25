using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a POSTNET barcode and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a POSTNET barcode for a sample ZIP code
    /// and writes the resulting image to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "postnet.png";

        // Create a BarcodeGenerator for POSTNET encoding with a sample ZIP code ("12345").
        // The generator implements IDisposable, so we use a using block to ensure proper cleanup.
        using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, "12345"))
        {
            // Set the height of the barcode bars to 40 points.
            // (1 point = 1/72 inch, so this results in a bar height of 40/72 inches.)
            generator.Parameters.Barcode.BarHeight.Point = 40f;

            // AutoSizeMode remains at its default value (None), which keeps the automatic width calculation.
            // No additional configuration is required for this example.

            // Save the generated barcode image to the specified PNG file.
            generator.Save(outputPath);
        }

        // Inform the user that the barcode has been saved successfully.
        Console.WriteLine($"Postnet barcode saved to: {outputPath}");
    }
}