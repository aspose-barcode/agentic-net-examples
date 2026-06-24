using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for Code128 symbology within a using block
        // to ensure resources are released after use.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Assign the data to be encoded in the barcode.
            generator.CodeText = "123456";

            // Define the output image size in inches.
            generator.Parameters.ImageWidth.Inches = 2f;   // Width: 2 inches
            generator.Parameters.ImageHeight.Inches = 1f;  // Height: 1 inch

            // Persist the generated barcode to a PNG file.
            generator.Save("barcode.png");
        }

        // Inform the user that the barcode image has been created.
        Console.WriteLine("Barcode image generated: barcode.png");
    }
}