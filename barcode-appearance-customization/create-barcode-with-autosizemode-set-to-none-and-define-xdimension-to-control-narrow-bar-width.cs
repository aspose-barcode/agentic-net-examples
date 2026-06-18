using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode and saving it as an image file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "barcode.png";

        // Create a BarcodeGenerator for Code128 with the specified data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Disable automatic sizing so we can set dimensions manually.
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Set the narrow bar width (XDimension) to 2 points.
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Set the bar height for the 1D barcode (since AutoSizeMode is None).
            generator.Parameters.Barcode.BarHeight.Point = 40f;

            // Save the generated barcode image to the specified path.
            generator.Save(outputPath);
        }

        // Inform the user that the barcode has been saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}