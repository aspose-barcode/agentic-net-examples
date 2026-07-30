// Title: Save barcode with custom colors and timestamped filename
// Description: Demonstrates generating a Code128 barcode with custom foreground and background colors, saving it to a uniquely named PNG file using a timestamp.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize barcode appearance with color properties and persist the image. It uses BarcodeGenerator, EncodeTypes, and drawing color classes, common tasks for developers needing branded or visually distinct barcodes in applications.
// Prompt: Save a barcode with custom colors to a file path that includes a timestamp for uniqueness.
// Tags: code128, barcode, color, save, png, aspose.barcode, generation

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
    /// Entry point of the example. Creates a barcode, applies custom colors, and writes the image to a timestamped file.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the barcode.
        const string codeText = "Sample12345";

        // Build a timestamp string for a unique file name (e.g., 20231127_154530123).
        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmssfff");

        // Compose the full file name and path in the current working directory.
        string fileName = $"barcode_{timestamp}.png";
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

        // Initialize the barcode generator with Code128 symbology and the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set the color of the barcode bars (foreground).
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;

            // Set the background color of the image.
            generator.Parameters.BackColor = Aspose.Drawing.Color.Yellow;

            // Save the generated barcode image to the specified path.
            generator.Save(outputPath);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}