// Title: Barcode Text Visibility and Alignment Demo
// Description: Demonstrates how to generate barcodes with customizable human‑readable text visibility and alignment, suitable for integration into an ASP.NET MVC view.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to control text display. Developers often need to show or hide barcode text and align it for UI consistency; this snippet shows typical settings for such scenarios.
// Prompt: Integrate barcode text customization into an ASP.NET MVC view, allowing end users to toggle visibility and alignment.
// Tags: barcode, text visibility, alignment, aspnet mvc, code128, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Program demonstrating barcode text visibility and alignment customization.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates two barcode images: one with visible centered text and one with hidden text.
    /// </summary>
    static void Main()
    {
        // NOTE: The original request was for an ASP.NET MVC view.
        // The snippet runner only supports a console application, so we demonstrate the core barcode logic here.
        // The generated images can be used in an MVC view to allow users to toggle text visibility and alignment.

        // Sample data
        string codeText = "1234567890";
        string outputDir = Directory.GetCurrentDirectory();

        // ------------------------------------------------------------
        // 1. Barcode with human‑readable text visible and centered
        // ------------------------------------------------------------
        string visiblePath = Path.Combine(outputDir, "barcode_visible.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Enable auto‑size mode so we don't need to set BarHeight manually
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set text location to appear below the barcode and align it to the center
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

            // Optional: customize the font of the human‑readable text
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Save the barcode image to disk
            generator.Save(visiblePath);
        }

        Console.WriteLine($"Visible barcode saved to: {visiblePath}");

        // ------------------------------------------------------------
        // 2. Barcode with human‑readable text hidden
        // ------------------------------------------------------------
        string hiddenPath = Path.Combine(outputDir, "barcode_hidden.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Keep auto‑size mode consistent with the first example
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Hide the text by setting its location to None
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Alignment is irrelevant when text is hidden, but set it to Right for demonstration purposes
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Right;

            // Save the barcode image without human‑readable text
            generator.Save(hiddenPath);
        }

        Console.WriteLine($"Hidden barcode saved to: {hiddenPath}");
    }
}