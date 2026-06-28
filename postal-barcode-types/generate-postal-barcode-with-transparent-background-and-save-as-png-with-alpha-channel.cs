using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Postnet barcode and saving it as a PNG with a transparent background.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Postnet barcode for a sample ZIP code and writes it to a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "postal.png";

        // Initialize a BarcodeGenerator for the Postnet symbology with a sample ZIP code.
        using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, "12345"))
        {
            // Set the background color to transparent so the PNG retains alpha channel.
            generator.Parameters.BackColor = Color.Transparent;

            // Optionally increase the image resolution for higher quality output.
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode as a PNG file, preserving transparency.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user that the barcode has been saved successfully.
        Console.WriteLine($"Postal barcode saved to {outputPath}");
    }
}