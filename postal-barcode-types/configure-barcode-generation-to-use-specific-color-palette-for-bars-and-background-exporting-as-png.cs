// Title: Generate Code128 Barcode with Custom Colors and Save as PNG
// Description: Demonstrates how to set foreground and background colors for a Code128 barcode and export it as a PNG image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to customize visual appearance such as bar and background colors. It uses the BarcodeGenerator class and related Parameter objects to configure colors before saving. Developers often need to match branding or UI themes when generating barcodes, and this pattern shows the typical steps for color customization and image export.
// Prompt: Configure barcode generation to use a specific color palette for bars and background, exporting as PNG.
// Tags: code128, barcode generation, png, color palette, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code128 barcode with custom bar and background colors
/// and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated PNG image
        string outputPath = "barcode.png";

        // Initialize a BarcodeGenerator for Code128 symbology with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set the foreground (bar) color to red
            generator.Parameters.Barcode.BarColor = Color.Red;

            // Set the background color of the image to light gray
            generator.Parameters.BackColor = Color.LightGray;

            // Save the configured barcode as a PNG file at the specified path
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Barcode image saved to: {Path.GetFullPath(outputPath)}");
    }
}