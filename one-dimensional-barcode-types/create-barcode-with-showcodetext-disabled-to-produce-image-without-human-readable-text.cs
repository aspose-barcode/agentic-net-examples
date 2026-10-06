// Title: Generate Code128 Barcode without Human‑Readable Text
// Description: Demonstrates how to create a Code128 barcode image with the human‑readable text hidden, saving the result as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to produce barcodes. Typical scenarios include creating labels or tickets where the visual barcode is required without accompanying text. Developers often need to control text visibility, symbology, and output format when integrating barcode creation into automated workflows.
// Prompt: Create a barcode with ShowCodeText disabled to produce an image without human‑readable text.
// Tags: code128, barcode generation, hide codetext, png output, aspose.barcode, barcodegenerator

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
    /// Entry point of the application. Creates the output directory, generates the barcode, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Determine a temporary folder to store the generated image
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeExample");
        if (!Directory.Exists(outputDir))
        {
            // Create the directory if it does not already exist
            Directory.CreateDirectory(outputDir);
        }

        // Full path for the resulting PNG file
        string outputPath = Path.Combine(outputDir, "barcode_no_text.png");

        // Initialize the barcode generator with Code128 symbology and the desired value
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Hide the human‑readable text by setting its location to None
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Save the barcode image in PNG format
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}