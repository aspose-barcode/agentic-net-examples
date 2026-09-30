// Title: Save Barcode with Custom Colors and Timestamped Filename
// Description: Demonstrates generating a Code128 barcode, applying custom foreground and background colors, and saving it to a uniquely named PNG file using a timestamp.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to create styled barcodes. Typical use cases include branding, visual differentiation, and ensuring file uniqueness in batch processing. Developers often need to customize colors and manage output paths for automated workflows.
/// Prompt: Save a barcode with custom colors to a file path that includes a timestamp for uniqueness.
/// Tags: barcode, code128, custom-colors, timestamp-filename, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code128 barcode with custom colors
/// and saves it to a PNG file whose name includes a timestamp for uniqueness.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates the barcode, applies color settings, builds a unique file name,
    /// and saves the image to the current directory.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the barcode
        string codeText = "1234567890";

        // Initialize the BarcodeGenerator with Code128 symbology and the specified text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Apply a custom foreground (bar) color
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Apply a custom background color
            generator.Parameters.BackColor = Color.Yellow;

            // Construct a unique file name using the current timestamp
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmssfff");
            string fileName = $"barcode_{timestamp}.png";

            // Combine the file name with the current working directory to get the full output path
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

            // Save the generated barcode image as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // Inform the user where the file was saved
            Console.WriteLine($"Barcode saved to: {outputPath}");
        }
    }
}