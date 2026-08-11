// Title: Generate a high‑contrast Code128 barcode image
// Description: Demonstrates how to create a Code128 barcode with a white background and black foreground for optimal scanning contrast.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to customize barcode appearance. Developers often need to adjust colors for readability in printed or scanned documents, and this snippet shows the typical steps for setting bar and background colors before saving the image.
// Prompt: Set barcode background to white and foreground to black for maximum contrast in scanned documents.
// Tags: barcode, code128, color, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with high contrast colors and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates the barcode, configures colors, ensures output directory exists, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image
        string outputPath = "barcode.png";

        // Ensure the target directory exists; create it if necessary
        string directory = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Initialize a BarcodeGenerator for Code128 with the sample text "123456"
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set the barcode (bars) color to black for maximum foreground contrast
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Set the image background color to white for maximum background contrast
            generator.Parameters.BackColor = Color.White;

            // Save the configured barcode as a PNG image to the specified path
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the full path of the saved barcode image for verification
        Console.WriteLine($"Barcode saved to: {Path.GetFullPath(outputPath)}");
    }
}