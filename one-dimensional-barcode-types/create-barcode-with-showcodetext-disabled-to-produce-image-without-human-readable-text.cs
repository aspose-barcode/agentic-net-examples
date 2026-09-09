// Title: Generate Code128 barcode without human‑readable text
// Description: Demonstrates how to create a Code128 barcode image with the human‑readable text hidden, using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It showcases the use of BarcodeGenerator, EncodeTypes, BarCodeImageFormat, and CodeTextParameters to produce barcode images. Typical scenarios include creating barcodes for packaging, inventory, or point‑of‑sale systems where the visual text is not required. Developers often need to control the visibility of the code text to meet design or regulatory requirements.
// Prompt: Create a barcode with ShowCodeText disabled to produce an image without human‑readable text.
// Tags: code128, barcode, generation, hidecodetext, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode image without displaying the human‑readable code text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a temporary folder, generates the barcode, saves it as PNG, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define a temporary output folder and ensure it exists.
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodeExample");
        Directory.CreateDirectory(outputFolder);

        // Build the full file path for the resulting barcode image.
        string outputPath = Path.Combine(outputFolder, "barcode_no_text.png");

        // Initialize the barcode generator for Code128 symbology with the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Disable the human‑readable text by setting its location to None.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Save the barcode image as a PNG file to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}