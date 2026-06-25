using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with a custom purple bar color using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode image and saves it to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image
        string outputPath = "purple_barcode.png";

        // Initialize a BarcodeGenerator for Code128 symbology with the data "123456"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Configure the barcode appearance: set bar (foreground) color to purple (RGB 128,0,128)
            generator.Parameters.Barcode.BarColor = Color.FromArgb(128, 0, 128);

            // Save the generated barcode as a PNG file to the specified path
            generator.Save(outputPath);
        }

        // Inform the user that the barcode has been saved
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}