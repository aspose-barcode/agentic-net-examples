using System;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code39 barcode with full ASCII charset using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode image and saves it to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "code39.png";

        // Initialize a BarcodeGenerator for Code39 with full ASCII support.
        // The 'using' statement ensures the generator is disposed properly.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII))
        {
            // Set the text that will be encoded into the barcode.
            generator.CodeText = "CODE39";

            // Configure the human‑readable text (the text displayed below the barcode):
            // - Font family: Arial
            // - Font size: 6 points
            // - Font style: regular (default)
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 6f;

            // Save the generated barcode as a PNG image to the specified path.
            generator.Save(outputPath);
        }

        // Inform the user that the barcode has been saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}