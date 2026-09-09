// Title: Toggle Barcode Text Visibility and Alignment with Aspose.BarCode
// Description: Demonstrates how to generate PDF417 barcodes with customizable text visibility and horizontal alignment using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to control barcode text appearance. Typical scenarios include creating printable labels, receipts, or web‑based barcode images where end users need to show or hide the human‑readable text and align it left, center, or right. Developers often need to adjust text location, alignment, and spacing to meet design requirements.
// Prompt: Integrate barcode text customization into an ASP.NET MVC view, allowing end users to toggle visibility and alignment.
// Tags: pdf417, barcode, text-visibility, alignment, aspnet-mvc, aspnet, aspnet-mvc-view, aspnet-mvc-example, aspose.barcode, image-output, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode text visibility and alignment customization using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes with different text settings and writes the output folder path.
    /// </summary>
    static void Main()
    {
        // Note: This console example simulates the logic that would be used in an ASP.NET MVC view.
        // It creates a temporary folder to store generated barcode images.

        // Create a unique temporary directory for the demo output.
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Sample barcode data.
        string codeText = "Sample123";

        // Generate a barcode with hidden text (no human‑readable code text).
        GenerateBarcode(Path.Combine(tempFolder, "Barcode_Hidden.png"), codeText, CodeLocation.None, TextAlignment.Center);

        // Generate a barcode with visible text left‑aligned below the barcode.
        GenerateBarcode(Path.Combine(tempFolder, "Barcode_Left.png"), codeText, CodeLocation.Below, TextAlignment.Left);

        // Generate a barcode with visible text center‑aligned below the barcode.
        GenerateBarcode(Path.Combine(tempFolder, "Barcode_Center.png"), codeText, CodeLocation.Below, TextAlignment.Center);

        // Generate a barcode with visible text right‑aligned below the barcode.
        GenerateBarcode(Path.Combine(tempFolder, "Barcode_Right.png"), codeText, CodeLocation.Below, TextAlignment.Right);

        // Inform the user where the images were saved.
        Console.WriteLine("Barcodes generated in: " + tempFolder);
    }

    /// <summary>
    /// Generates a PDF417 barcode image with specified text visibility and alignment.
    /// </summary>
    /// <param name="filePath">Full path where the PNG image will be saved.</param>
    /// <param name="codeText">The data encoded in the barcode.</param>
    /// <param name="location">Location of the human‑readable text relative to the barcode.</param>
    /// <param name="alignment">Horizontal alignment of the text.</param>
    static void GenerateBarcode(string filePath, string codeText, CodeLocation location, TextAlignment alignment)
    {
        // Initialize the barcode generator for PDF417 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, codeText))
        {
            // Configure PDF417 specific settings.
            generator.Parameters.Barcode.Pdf417.Rows = 12;
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Apply text visibility and alignment settings.
            generator.Parameters.Barcode.CodeTextParameters.Location = location;
            generator.Parameters.Barcode.CodeTextParameters.Alignment = alignment;

            // Optional: increase spacing between the barcode and the text for better readability.
            generator.Parameters.Barcode.CodeTextParameters.Space.Point = 5f;

            // Save the generated barcode as a PNG image.
            generator.Save(filePath, BarCodeImageFormat.Png);
        }
    }
}