using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a semi‑transparent barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Code128 barcode with a semi‑transparent red color
    /// and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file name (saved in the current working directory)
        string outputPath = "semiTransparentBarcode.png";

        // Initialize the barcode generator with Code128 symbology and the data to encode
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Configure the barcode bar color to be semi‑transparent red (alpha = 128)
            generator.Parameters.Barcode.BarColor = Color.FromArgb(128, 255, 0, 0);

            // Render and save the barcode image in PNG format
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the full path of the saved barcode image to the console
        Console.WriteLine($"Barcode saved to: {Path.GetFullPath(outputPath)}");
    }
}