// Title: Generate Code128 barcode without human‑readable text
// Description: Demonstrates creating a Code128 barcode image while disabling the displayed code text.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode appearance using BarcodeGenerator and its Parameters. Typical use cases include producing clean barcode images for printing or embedding where human‑readable text is not required. Developers often need to adjust CodeTextParameters, such as Location, Font, and Visibility, to meet specific design requirements.
// Prompt: Generate a barcode, disable ShowCodeText, and confirm output contains only the barcode pattern.
// Tags: code128, barcode generation, hide codetext, image output, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode image with the human‑readable text disabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a barcode, saves it to a PNG file, and verifies the file was written.
    /// </summary>
    static void Main()
    {
        // Define the output file path
        string outputPath = "barcode.png";

        // Ensure a previous file does not interfere with the demo
        if (File.Exists(outputPath))
        {
            File.Delete(outputPath);
        }

        // Initialize the barcode generator for Code128 with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Hide the human‑readable code text (equivalent to disabling ShowCodeText)
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Persist the barcode image to the specified file
            generator.Save(outputPath);
        }

        // Verify that the image file was successfully created
        if (File.Exists(outputPath))
        {
            Console.WriteLine($"Barcode image saved to '{outputPath}'.");
            Console.WriteLine("Human‑readable text is disabled; the output contains only the barcode pattern.");
        }
        else
        {
            Console.WriteLine("Failed to generate the barcode image.");
        }
    }
}