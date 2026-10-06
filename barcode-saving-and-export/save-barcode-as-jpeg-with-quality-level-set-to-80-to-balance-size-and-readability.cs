// Title: Save Code128 barcode as JPEG with adjusted quality settings
// Description: Demonstrates generating a Code128 barcode and saving it as a JPEG file while approximating a quality level of 80 by tweaking resolution and anti‑aliasing.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to create barcodes. Typical use cases include creating printable barcode images for inventory, shipping, or retail. Developers often need to control image size and readability, adjusting parameters such as resolution and anti‑aliasing when direct JPEG quality control is unavailable.
// Prompt: Save a barcode as a JPEG with quality level set to 80 to balance size and readability.
// Tags: code128, generation, jpeg, barcodegenerator, barcodimageformat

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode and saves it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates the barcode, configures image parameters, and writes the file to disk.
    /// </summary>
    static void Main()
    {
        // Build the full path for the output JPEG file in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.jpg");

        try
        {
            // Create a BarcodeGenerator for Code128 with the desired data.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
            {
                // Adjust image resolution and disable anti‑aliasing to approximate JPEG quality level 80.
                generator.Parameters.Resolution = 72f;      // Lower resolution reduces file size.
                generator.Parameters.UseAntiAlias = false; // Disabling anti‑aliasing further reduces size.

                // Save the generated barcode as a JPEG file.
                generator.Save(outputPath, BarCodeImageFormat.Jpeg);
            }

            // Inform the user that the barcode was saved successfully.
            Console.WriteLine($"Barcode saved to '{outputPath}'.");
            Console.WriteLine("Note: Aspose.BarCode does not provide a direct JPEG quality setting; resolution and anti-aliasing were adjusted to approximate quality level 80.");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during generation or saving.
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}