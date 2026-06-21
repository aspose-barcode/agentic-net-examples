using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode with a custom XDimension using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode image and writes a confirmation to the console.
    /// </summary>
    static void Main()
    {
        // Initialize a Code128 barcode generator with the sample text "1234567890"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Configure the barcode's module width (XDimension) to 3 pixels
            generator.Parameters.Barcode.XDimension.Pixels = 3f;

            // Persist the generated barcode as a PNG file named "barcode.png"
            generator.Save("barcode.png");
        }

        // Inform the user that the barcode has been generated with the specified XDimension
        Console.WriteLine("Barcode generated with XDimension = 3 pixels.");
    }
}