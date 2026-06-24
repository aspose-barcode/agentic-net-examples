using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a GS1 Code 128 barcode and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a GS1 Code 128 barcode with custom font settings and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Define the GS1 Code 128 data (Application Identifier 01 for GTIN)
        string codeText = "(01)12345678901231";

        // Build the full output file path in the current directory
        string outputPath = Path.Combine(Environment.CurrentDirectory, "gs1code128.png");

        // Initialize the barcode generator for GS1 Code 128 with the specified data
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
        {
            // Set a custom font (Arial, 12pt) for the human‑readable text below the barcode
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Ensure the human‑readable text appears below the barcode (default behavior)
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Save the generated barcode image as a PNG file to the specified path
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"GS1 Code 128 barcode saved to: {outputPath}");
    }
}