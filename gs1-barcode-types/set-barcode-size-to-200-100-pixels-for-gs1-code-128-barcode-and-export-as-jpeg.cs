using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a GS1 Code 128 barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode and saves it as a JPEG file.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for GS1 Code 128 with sample GS1 data
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, "(01)12345678901231"))
        {
            // Configure automatic sizing to choose the nearest size that fits the target dimensions
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;

            // Set desired image width and height in points (1 point = 1/72 inch)
            generator.Parameters.ImageWidth.Point = 200f;
            generator.Parameters.ImageHeight.Point = 100f;

            // Save the generated barcode image to a JPEG file
            generator.Save("gs1code128.jpg");
        }

        // Inform the user that the barcode has been generated and saved
        Console.WriteLine("Barcode generated and saved as gs1code128.jpg");
    }
}