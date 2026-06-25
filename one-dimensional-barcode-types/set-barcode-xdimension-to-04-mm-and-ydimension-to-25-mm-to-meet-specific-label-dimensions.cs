using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Contains the entry point for the barcode generation sample.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Code128 barcode with sample data and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with the sample text "123456"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Configure the module width (X dimension) to 0.4 mm
            generator.Parameters.Barcode.XDimension.Millimeters = 0.4f;

            // Configure the bar height (Y dimension) to 25 mm
            generator.Parameters.Barcode.BarHeight.Millimeters = 25f;

            // Persist the generated barcode image to a PNG file named "barcode.png"
            generator.Save("barcode.png");
        }

        // Inform the user that the barcode has been successfully generated and saved
        Console.WriteLine("Barcode generated and saved as 'barcode.png'.");
    }
}