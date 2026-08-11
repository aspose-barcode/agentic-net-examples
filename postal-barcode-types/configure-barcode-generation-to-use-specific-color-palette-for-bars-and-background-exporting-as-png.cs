// Title: Generate Code128 barcode with custom colors and PNG output
// Description: Demonstrates how to generate a Code128 barcode, apply specific bar and background colors, and export the result as a PNG image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to customize visual appearance (foreground and background colors) of generated barcodes. It showcases key API classes such as BarcodeGenerator, EncodeTypes, and BarCodeImageFormat, which developers commonly use to create, style, and save barcodes in various image formats for integration into web, desktop, or mobile applications.
// Prompt: Configure barcode generation to use a specific color palette for bars and background, exporting as PNG.
// Tags: barcode, symbology, generation, color, png, aspose.barcode, code128

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code128 barcode with custom colors and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Accepts optional command‑line arguments for the barcode text and output file path.
    /// </summary>
    /// <param name="args">Command‑line arguments: [0] = barcode text, [1] = output file path.</param>
    static void Main(string[] args)
    {
        // Determine the barcode text: use first argument if provided, otherwise default to "Sample123".
        string codeText = args.Length > 0 ? args[0] : "Sample123";

        // Determine the output file path: use second argument if provided, otherwise default to "barcode.png".
        string outputPath = args.Length > 1 ? args[1] : "barcode.png";

        // Initialize a BarcodeGenerator for the Code128 symbology with the specified text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set the foreground (bar) color to blue.
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the background color of the image to light gray.
            generator.Parameters.BackColor = Color.LightGray;

            // Save the generated barcode as a PNG file at the specified location.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}