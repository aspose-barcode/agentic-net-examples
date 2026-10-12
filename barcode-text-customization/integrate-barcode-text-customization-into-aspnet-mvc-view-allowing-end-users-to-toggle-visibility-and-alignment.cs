// Title: Barcode Text Visibility and Alignment Demo
// Description: Demonstrates how to generate Code128 barcodes with customizable human‑readable text visibility and horizontal alignment using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to control barcode text display properties such as location and alignment. It uses the BarcodeGenerator class and its Parameters.Barcode.CodeTextParameters to hide or show text and set left, center, or right alignment. Developers working on barcode rendering, especially in web or desktop applications, often need to customize text appearance for branding or readability.
// Prompt: Integrate barcode text customization into an ASP.NET MVC view, allowing end users to toggle visibility and alignment.
// Tags: code128, barcode, text visibility, text alignment, aspose.barcode, generation, png, console

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates barcode text customization (visibility and alignment) using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Generates sample barcode images with various text visibility and alignment settings.
    /// </summary>
    static void Main()
    {
        // Create a temporary directory to store generated barcode images.
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeTextDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Base barcode data to encode.
        string codeText = "Demo12345";

        // 1. Generate a barcode with the human‑readable text hidden.
        string hiddenPath = Path.Combine(outputDir, "Barcode_TextHidden.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Hide the text by setting its location to None.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;
            generator.Save(hiddenPath, BarCodeImageFormat.Png);
        }

        // 2. Generate a barcode with text visible below, left aligned.
        string leftPath = Path.Combine(outputDir, "Barcode_TextBelow_Left.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Left;
            generator.Save(leftPath, BarCodeImageFormat.Png);
        }

        // 3. Generate a barcode with text visible below, center aligned.
        string centerPath = Path.Combine(outputDir, "Barcode_TextBelow_Center.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;
            generator.Save(centerPath, BarCodeImageFormat.Png);
        }

        // 4. Generate a barcode with text visible below, right aligned.
        string rightPath = Path.Combine(outputDir, "Barcode_TextBelow_Right.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Right;
            generator.Save(rightPath, BarCodeImageFormat.Png);
        }

        // Output the locations of the generated images.
        Console.WriteLine("Barcode images generated in: " + outputDir);
        Console.WriteLine("Hidden text: " + hiddenPath);
        Console.WriteLine("Below left aligned: " + leftPath);
        Console.WriteLine("Below center aligned: " + centerPath);
        Console.WriteLine("Below right aligned: " + rightPath);
    }
}