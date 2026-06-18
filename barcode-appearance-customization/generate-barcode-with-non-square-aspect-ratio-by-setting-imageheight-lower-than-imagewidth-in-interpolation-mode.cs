using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode with a custom aspect ratio using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode image and saves it to disk.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for Code128 with the sample text "1234567890"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Configure the generator to use interpolation for auto-sizing the image
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set a non‑square aspect ratio: width (300 points) is twice the height (150 points)
            generator.Parameters.ImageWidth.Point = 300f;   // Width in points
            generator.Parameters.ImageHeight.Point = 150f;  // Height in points

            // Save the generated barcode as a PNG file
            generator.Save("barcode.png");
        }

        // Inform the user that the barcode has been created
        Console.WriteLine("Barcode generated: barcode.png");
    }
}