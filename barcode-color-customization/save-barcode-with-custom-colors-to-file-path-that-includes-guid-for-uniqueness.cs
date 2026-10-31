// Title: Save a barcode with custom colors to a uniquely named PNG file
// Description: Demonstrates generating a Code128 barcode, applying custom foreground and background colors, and saving it as a PNG file with a GUID‑based filename for uniqueness.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class together with EncodeTypes and BarCodeImageFormat to create barcodes. Typical use cases include creating labeled assets, inventory tags, or receipts where visual customization (colors) and unique file naming are required. Developers often need to customize appearance and manage output paths, making this pattern a common reference for barcode creation tasks.
// Prompt: Save a barcode with custom colors to a file path that includes a GUID for uniqueness.
// Tags: barcode, code128, generate, save, png, aspose.barcodes, color

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
    /// Entry point of the example. Creates a barcode, applies colors, and writes the image to a temporary folder.
    /// </summary>
    static void Main()
    {
        // Define the text to encode and the barcode symbology.
        string codeText = "12345678";
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // Build a unique output file path using a GUID to avoid name collisions.
        string outputDirectory = Path.GetTempPath();
        string fileName = $"barcode_{Guid.NewGuid():N}.png";
        string outputPath = Path.Combine(outputDirectory, fileName);

        // Create the barcode generator with the specified symbology and text.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Set the bar (foreground) color to blue.
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the background color to yellow.
            generator.Parameters.BackColor = Color.Yellow;

            // Save the generated barcode as a PNG image to the unique path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}