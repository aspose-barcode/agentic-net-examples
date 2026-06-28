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
        const string outputPath = "barcode.png";

        // Create a BarcodeGenerator for Code128 with the specified text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Disable the human‑readable text that would normally appear below the barcode
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Save the generated barcode image to the specified file path
            generator.Save(outputPath);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}