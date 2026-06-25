using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode and saves it to a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image
        string outputPath = "barcode.png";

        // Initialize a BarcodeGenerator for Code128 symbology with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Configure the human‑readable text to appear above the barcode bars
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Above;

            // Set a small vertical gap (2 points) between the text and the barcode
            generator.Parameters.Barcode.CodeTextParameters.Space.Point = 2f;

            // Persist the generated barcode image to the specified file path
            generator.Save(outputPath);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}