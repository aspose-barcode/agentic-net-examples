using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode using Aspose.BarCode and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode and writes a confirmation message to the console.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator with Code128 symbology and the data "123456"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Disable automatic sizing so that the explicit BarHeight setting is applied
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Set the height of the barcode bars to 50 millimeters.
            // The XDimension (module width) remains at its default value.
            generator.Parameters.Barcode.BarHeight.Millimeters = 50f;

            // Render the barcode and save it as a PNG image file named "barcode.png"
            generator.Save("barcode.png");
        }

        // Output a confirmation message indicating successful generation
        Console.WriteLine("Barcode generated and saved to barcode.png");
    }
}