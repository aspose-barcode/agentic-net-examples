// Title: Generate Code128 barcode without human‑readable text
// Description: Creates a Code128 barcode image, disables the display of the code text, and saves it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce barcode images. Typical use cases include creating barcodes for product labeling, inventory tracking, and document automation where developers often need to hide the human‑readable code text for aesthetic or security reasons. The snippet serves as a reference for developers searching for barcode generation patterns in C#.
// Prompt: Generate a barcode, disable ShowCodeText, and confirm output contains only the barcode pattern.
// Tags: code128, barcode generation, hide codetext, png, aspose.barcode, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode image with the human‑readable text hidden.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, disables code text, saves PNG, and writes output path.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the output file.
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define the full path for the generated PNG image.
        string outputPath = Path.Combine(tempDir, "barcode.png");

        // Initialize the barcode generator for Code128 with the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Disable the human‑readable code text by setting its location to None.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Save the barcode image as PNG to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode generated at: {outputPath}");
        Console.WriteLine("Human‑readable text disabled (CodeLocation.None).");
    }
}