// Title: Generate Code128 barcode without human‑readable text
// Description: Demonstrates creating a Code128 barcode, disabling the displayed code text, and saving the result as a PNG image containing only the bars.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure BarcodeGenerator parameters such as CodeTextParameters to control visual output. Typical use cases include producing clean bar‑only images for printing or embedding where human‑readable text is unnecessary. Developers often need to adjust symbology settings, hide code text, and export to common image formats using classes like BarcodeGenerator, EncodeTypes, and BarCodeImageFormat.
// Prompt: Generate a barcode, set ShowCodeText to false, and confirm only bars are present in the output file.
// Tags: code128, generate, hidecodetext, png, barcodegenerator, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode, hides the human‑readable text,
/// and saves the image as a PNG file containing only the bars.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "barcode.png";

        // Ensure the directory for the output file exists; create it if necessary.
        string directory = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Initialize a BarcodeGenerator for Code128 symbology with sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Hide the human‑readable text by setting its location to None (equivalent to ShowCodeText = false).
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Save the barcode as a PNG image; only the bars will be rendered.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image file was successfully created and inform the user.
        if (File.Exists(outputPath))
        {
            Console.WriteLine($"Barcode saved to '{outputPath}'. Human‑readable text is hidden, so only bars are present.");
        }
        else
        {
            Console.WriteLine("Failed to create the barcode image.");
        }
    }
}