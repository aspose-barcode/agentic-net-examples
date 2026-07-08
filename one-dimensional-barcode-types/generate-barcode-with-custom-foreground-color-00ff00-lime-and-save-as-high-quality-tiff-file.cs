// Title: Generate a lime-colored Code128 barcode and save as high‑resolution TIFF
// Description: Demonstrates how to set a custom foreground color (#00FF00) for a barcode and export it as a high‑quality TIFF image.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating usage of BarcodeGenerator, EncodeTypes, and rendering parameters such as BarColor and Resolution. Developers often need to customize barcode appearance and output format for printing or archival purposes. The snippet shows typical steps for creating, styling, and saving barcodes in .NET applications.
// Prompt: Generate a barcode with custom foreground color #00FF00 (lime) and save as a high‑quality TIFF file.
// Tags: code128, barcode, color, tiff, highresolution, aspnet, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Entry point for the barcode generation example.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Code128 barcode with lime foreground color and saves it as a 300 DPI TIFF file.
    /// </summary>
    static void Main()
    {
        // Initialize the barcode generator with Code128 symbology and sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Apply lime color (#00FF00) to the bars.
            generator.Parameters.Barcode.BarColor = Color.FromArgb(0, 255, 0);

            // Set resolution to 300 DPI for high‑quality output.
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode as a TIFF image.
            generator.Save("barcode.tiff");
        }
    }
}