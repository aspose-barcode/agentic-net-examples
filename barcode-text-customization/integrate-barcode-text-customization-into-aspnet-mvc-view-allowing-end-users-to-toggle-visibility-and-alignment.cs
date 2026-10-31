// Title: Barcode Text Visibility and Alignment Example
// Description: Demonstrates how to customize barcode text visibility and alignment using Aspose.BarCode in a .NET application.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to control code text display properties such as visibility, location, and alignment. It utilizes the BarcodeGenerator class and related parameter objects, which are commonly used by developers to create barcodes with tailored appearance for reports, labels, and web applications.
// Prompt: Integrate barcode text customization into an ASP.NET MVC view, allowing end users to toggle visibility and alignment.
// Tags: pdf417, barcode, text-visibility, text-alignment, aspnet-mvc, aspose.barcode, png, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides examples of barcode text customization such as visibility toggling and alignment.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates sample barcodes with various text visibility and alignment settings.
    /// </summary>
    static void Main()
    {
        // Create a temporary output directory for the generated barcode images.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // 1. Barcode with default text visibility (below, centered).
        GenerateBarcode(
            EncodeTypes.Pdf417,
            "Visible Text",
            CodeLocation.Below,
            TextAlignment.Center,
            Path.Combine(outputDir, "Barcode_Visible_Center.png"));

        // 2. Barcode with text hidden.
        GenerateBarcode(
            EncodeTypes.Pdf417,
            "Hidden Text",
            CodeLocation.None,
            TextAlignment.Center,
            Path.Combine(outputDir, "Barcode_Hidden.png"));

        // 3. Barcode with text visible and right-aligned.
        GenerateBarcode(
            EncodeTypes.Pdf417,
            "Right Aligned",
            CodeLocation.Below,
            TextAlignment.Right,
            Path.Combine(outputDir, "Barcode_Visible_Right.png"));

        // Inform the user where the barcode images have been saved.
        Console.WriteLine("Barcodes generated in: " + outputDir);
    }

    /// <summary>
    /// Generates a barcode image with specified text visibility and alignment settings.
    /// </summary>
    /// <param name="encodeType">The barcode symbology to use.</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="location">The location of the human‑readable text relative to the barcode.</param>
    /// <param name="alignment">The alignment of the text when it is displayed.</param>
    /// <param name="filePath">The full file path where the PNG image will be saved.</param>
    static void GenerateBarcode(BaseEncodeType encodeType, string codeText, CodeLocation location, TextAlignment alignment, string filePath)
    {
        // Initialize the barcode generator with the chosen symbology and text.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Set X dimension for better visibility of the barcode modules.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Configure text visibility and alignment.
            generator.Parameters.Barcode.CodeTextParameters.Location = location;
            generator.Parameters.Barcode.CodeTextParameters.Alignment = alignment;

            // Optional: increase spacing between barcode and text for readability.
            generator.Parameters.Barcode.CodeTextParameters.Space.Point = 5f;

            // Save the generated barcode as a PNG image.
            generator.Save(filePath, BarCodeImageFormat.Png);
        }
    }
}