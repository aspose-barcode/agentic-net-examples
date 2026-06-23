using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode and saving it as a BMP image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, applies a custom color, saves it to disk, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the output file name and location.
        string outputPath = "code128.bmp";

        // Initialize a BarcodeGenerator for Code128 with the sample text "1234567890".
        // The generator implements IDisposable, so we use a using block to ensure proper resource cleanup.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the color of the barcode bars (foreground) to dark green.
            generator.Parameters.Barcode.BarColor = Color.DarkGreen;

            // Save the generated barcode image to the specified path in BMP format.
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Inform the user that the barcode has been saved successfully.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}