using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code39 barcode using Aspose.BarCode library.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Code39 barcode image and saves it to a file.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code39FullASCII with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, "CODE39"))
        {
            // Set the X-dimension (module width) to 2 points, making bars and spaces wider.
            generator.Parameters.Barcode.XDimension.Point = 2f; // 2 points per module

            // Define a bar height of 50 points for improved readability.
            generator.Parameters.Barcode.BarHeight.Point = 50f;

            // Save the generated barcode as a PNG image file.
            generator.Save("code39.png");
        }

        // Inform the user that the barcode has been created.
        Console.WriteLine("Barcode generated: code39.png");
    }
}