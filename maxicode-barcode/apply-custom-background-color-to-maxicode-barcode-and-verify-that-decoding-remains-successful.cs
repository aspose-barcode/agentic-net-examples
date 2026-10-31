// Title: Apply custom background color to MaxiCode barcode and verify decoding
// Description: Demonstrates generating a MaxiCode barcode with a custom background color, saving it as PNG, and confirming that the barcode can still be decoded correctly.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to customize visual appearance of a barcode using the BarcodeGenerator.Parameters properties (BackColor, BarColor) and then use BarCodeReader to decode the image. Developers working with MaxiCode or other 2D symbologies often need to adjust colors for branding while ensuring readability, making this pattern useful for creating printable or on‑screen barcodes.
// Prompt: Apply a custom background color to a MaxiCode barcode and verify that decoding remains successful.
// Tags: maxicode, barcode generation, barcode recognition, background color, color customization, aspose.barcode, png, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a MaxiCode barcode with a custom background color, saves it to a temporary file,
/// decodes it to verify readability, and cleans up the temporary resources.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the barcode generation, decoding verification,
    /// and optional cleanup steps.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to store the generated barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "MaxiCodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "maxicode.png");

        // Generate a MaxiCode barcode with a custom yellow background and black bars
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "1234567890"))
        {
            generator.Parameters.BackColor = Color.Yellow; // Set custom background color
            generator.Parameters.Barcode.BarColor = Color.Black; // Set bar (foreground) color
            generator.Save(imagePath, BarCodeImageFormat.Png); // Save as PNG file
        }

        // Verify that the image file was successfully created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Decode the generated barcode to ensure the custom colors did not affect readability
        using (var reader = new BarCodeReader(imagePath, DecodeType.MaxiCode))
        {
            var results = reader.ReadBarCodes();
            bool success = results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText);
            Console.WriteLine("Decoding " + (success ? "succeeded" : "failed"));
            if (success)
            {
                Console.WriteLine("Decoded text: " + results[0].CodeText);
            }
        }

        // Clean up temporary files and folder (optional)
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore any errors that occur during cleanup
        }
    }
}