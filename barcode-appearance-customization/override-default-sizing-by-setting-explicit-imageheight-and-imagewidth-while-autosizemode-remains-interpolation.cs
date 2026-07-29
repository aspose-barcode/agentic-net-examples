// Title: Override barcode image size with explicit dimensions while using Interpolation auto-size mode
// Description: Demonstrates how to set ImageWidth and ImageHeight on a BarcodeGenerator, keeping AutoSizeMode set to Interpolation, and save the result as PNG.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating image sizing controls. It showcases the BarcodeGenerator class, its Parameters property, and the AutoSizeMode enumeration. Developers often need to produce barcodes with specific dimensions for UI layout, printing, or integration with other graphics pipelines.
// Prompt: Override default sizing by setting explicit ImageHeight and ImageWidth while AutoSizeMode remains Interpolation.
// Tags: code128, barcode generation, image sizing, autosizemode, png, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a Code128 barcode with custom image dimensions while retaining the Interpolation auto‑size mode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a barcode, configures sizing, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Initialize the barcode generator with Code128 symbology and sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Preserve automatic sizing behavior using the Interpolation mode.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Explicitly set the desired image dimensions (in points).
            // The API expects float values, hence the 'f' suffix.
            generator.Parameters.ImageWidth.Point = 300f;   // Width = 300 points
            generator.Parameters.ImageHeight.Point = 150f; // Height = 150 points

            // Persist the generated barcode to a PNG file.
            generator.Save("barcode.png");
        }

        // Inform the user that the barcode has been created.
        Console.WriteLine("Barcode generated and saved as 'barcode.png'.");
    }
}