using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode using Aspose.BarCode and saving it as an image file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode and writes a confirmation to the console.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator with Code128 symbology and the data to encode.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Adjust the bar width reduction to 20 points to accommodate narrow label spaces.
            generator.Parameters.Barcode.BarWidthReduction.Point = 20f;

            // Persist the generated barcode as a PNG image file.
            generator.Save("barcode.png");
        }

        // Inform the user that the barcode image has been created.
        Console.WriteLine("Barcode generated and saved as barcode.png");
    }
}