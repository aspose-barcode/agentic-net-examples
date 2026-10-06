// Title: Save Barcode with Custom Colors and Unique GUID Filename
// Description: Demonstrates generating a Code128 barcode, applying custom background and bar colors, and saving it as a PNG file with a unique GUID-based filename.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class together with EncodeTypes, BarCodeImageFormat, and the Parameters property to customize barcode appearance. Typical use cases include creating branded barcodes, applying corporate color schemes, and ensuring file name uniqueness in batch processing scenarios. Developers often need to adjust colors, select symbologies, and store results in various image formats.
// Prompt: Save a barcode with custom colors to a file path that includes a GUID for uniqueness.
// Tags: barcode symbology, generation, custom colors, png, guid, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode with custom colors and saves it to a uniquely named PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary output directory, generates the barcode,
    /// applies custom colors, and saves the image using a GUID-based filename.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory for barcode images
        string outputDir = Path.Combine(Path.GetTempPath(), "Barcodes");
        Directory.CreateDirectory(outputDir);

        // Build a unique file path using a GUID to avoid name collisions
        string filePath = Path.Combine(outputDir, $"barcode_{Guid.NewGuid():N}.png");

        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Set custom background and bar colors
            generator.Parameters.BackColor = Color.LightGray;
            generator.Parameters.Barcode.BarColor = Color.DarkBlue;

            // Save the generated barcode as a PNG image to the unique file path
            generator.Save(filePath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {filePath}");
    }
}