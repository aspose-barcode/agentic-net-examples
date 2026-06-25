using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating an ITF-14 barcode from a GTIN and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates an ITF-14 barcode with a framed border and saves it.
    /// </summary>
    static void Main()
    {
        // Define a sample GTIN (14‑digit) to encode as ITF‑14.
        string gtin = "01234567890123";

        // Specify the output file path for the generated PNG image.
        string outputPath = "itf14.png";

        // Initialize the barcode generator for ITF‑14 using the provided GTIN.
        using (var generator = new BarcodeGenerator(EncodeTypes.ITF14, gtin))
        {
            // Configure the barcode to have a frame border.
            generator.Parameters.Barcode.ITF.BorderType = ITF14BorderType.Frame;

            // Set the thickness of the frame border to 2 points.
            generator.Parameters.Barcode.ITF.BorderThickness.Point = 2f;

            // Save the generated barcode image to the specified path in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user that the barcode has been saved.
        Console.WriteLine($"ITF‑14 barcode saved to: {outputPath}");
    }
}