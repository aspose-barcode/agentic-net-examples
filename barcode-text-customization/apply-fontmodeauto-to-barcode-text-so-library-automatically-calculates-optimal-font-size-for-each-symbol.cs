// Title: Apply FontMode.Auto for Automatic Font Sizing in Barcode Generation
// Description: Demonstrates how to enable automatic font size calculation for barcode text using Aspose.BarCode, ensuring optimal readability across different symbols.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to customize text rendering. Developers often need to adjust font sizing, image dimensions, and auto‑size modes when creating barcodes for labels, packaging, or inventory systems. The snippet illustrates typical API calls for setting FontMode, AutoSizeMode, and image size.
// Prompt: Apply FontMode.Auto to barcode text so the library automatically calculates optimal font size for each symbol.
// Tags: code128, fontmode, autosize, png, barcodelibrary, generation, aspnet, csharp

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates applying FontMode.Auto to barcode text for automatic font sizing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode with automatic font size calculation and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Output file path for the generated barcode image
        string outputPath = "barcode.png";

        // Initialize a BarcodeGenerator for Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the data to encode in the barcode
            generator.CodeText = "1234567890";

            // Enable automatic font size calculation for the barcode text
            generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Auto;

            // Optional: configure auto‑size mode and define image dimensions
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Save the generated barcode image to the specified file
            generator.Save(outputPath);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}