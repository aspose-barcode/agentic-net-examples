// Title: Save barcode with custom colors to a GUID-based file path
// Description: Demonstrates generating a Code128 barcode, applying custom foreground and background colors, and saving it to a uniquely named PNG file using a GUID.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize barcode appearance with the BarcodeGenerator class and its Parameters properties. Typical use cases include branding, UI integration, and generating distinct barcode files for batch processing. Developers often need to set bar and background colors and ensure file name uniqueness when saving images.
// Prompt: Save a barcode with custom colors to a file path that includes a GUID for uniqueness.
// Tags: barcode, code128, custom colors, png, guid, file output, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates creating a barcode with custom colors and saving it to a uniquely named file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates a Code128 barcode with custom colors and saves it as a PNG file with a GUID-based name.
    /// </summary>
    static void Main()
    {
        // Define the barcode text and symbology type
        string codeText = "1234567890";
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // Build a unique file name using a GUID and combine it with the temp folder path
        string fileName = $"barcode_{Guid.NewGuid():N}.png";
        string outputPath = Path.Combine(Path.GetTempPath(), fileName);

        // Initialize the barcode generator with the chosen symbology and text
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Apply a custom foreground (bar) color
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Apply a custom background color
            generator.Parameters.BackColor = Color.LightYellow;

            // Save the generated barcode image to the unique file path in PNG format
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}