// Title: Save Barcode as JPEG with Adjusted Resolution and Anti-Aliasing
// Description: Demonstrates generating a Code128 barcode and saving it as a JPEG image while adjusting resolution and anti-aliasing to influence file size and readability.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to configure barcode rendering parameters such as resolution and anti-aliasing before saving to common image formats like JPEG. Developers working with barcode creation often need to control output quality for web or mobile scenarios, and the BarCodeGenerator class together with its Parameters property provides the necessary settings.
// Prompt: Save a barcode as a JPEG with quality level set to 80 to balance size and readability.
// Tags: barcode, code128, jpeg, image, resolution, anti-aliasing, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a Code128 barcode and saves it as a JPEG image with adjusted rendering settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures rendering parameters,
    /// and writes the image to the current directory.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output JPEG file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.jpg");

        // Aspose.BarCode does not expose a direct JPEG quality property.
        // Adjust resolution and anti‑aliasing to indirectly affect file size and visual quality.
        try
        {
            // Initialize the barcode generator with Code128 symbology and sample data.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
            {
                // Set a lower DPI to reduce image size (default is 96 DPI).
                generator.Parameters.Resolution = 96f;

                // Disable anti‑aliasing to minimize compression artifacts.
                generator.Parameters.UseAntiAlias = false;

                // Save the generated barcode as a JPEG file.
                generator.Save(outputPath, BarCodeImageFormat.Jpeg);
            }

            // Inform the user that the barcode was saved successfully.
            Console.WriteLine($"Barcode saved to '{outputPath}'.");
            Console.WriteLine("Note: Aspose.BarCode does not support setting JPEG quality directly; default quality is used.");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during barcode generation.
            Console.WriteLine($"Error generating barcode: {ex.Message}");
        }
    }
}