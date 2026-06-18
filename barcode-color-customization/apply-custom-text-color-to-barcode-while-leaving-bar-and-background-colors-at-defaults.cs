using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with custom human‑readable text color using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode image and saves it to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file name and location.
        string outputPath = "custom_text_color_barcode.png";

        // Initialize a BarcodeGenerator for Code128 with the sample data "123456".
        // The generator implements IDisposable, so we use a using block to ensure proper cleanup.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set the color of the human‑readable text (the code text) to red.
            // The barcode bars and background retain their default colors.
            generator.Parameters.Barcode.CodeTextParameters.Color = Color.Red;

            // Save the generated barcode image to the specified file path.
            generator.Save(outputPath);
        }

        // Inform the user that the barcode has been saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}