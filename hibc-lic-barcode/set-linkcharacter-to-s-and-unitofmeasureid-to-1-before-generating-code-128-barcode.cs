using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode using Aspose.BarCode and saving it as an image file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode and writes a confirmation message to the console.
    /// </summary>
    static void Main()
    {
        // Create a BarcodeGenerator for Code128 with the specified data.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Save the generated barcode image to a PNG file.
            generator.Save("code128.png");
        }

        // Inform the user that the barcode has been generated and saved.
        Console.WriteLine("Barcode generated and saved as code128.png");
    }
}