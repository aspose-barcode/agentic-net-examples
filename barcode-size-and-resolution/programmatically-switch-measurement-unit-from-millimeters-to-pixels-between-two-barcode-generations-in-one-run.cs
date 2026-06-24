using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating Code128 barcodes using Aspose.BarCode with different measurement units.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates two barcode images:
    /// one using millimeter units and another using pixel units.
    /// </summary>
    static void Main()
    {
        // Define output file name for barcode using millimeter units
        string mmPath = "barcode_mm.png";

        // Create a barcode generator for Code128 with the data "123456"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Configure bar height and X dimension in millimeters
            generator.Parameters.Barcode.BarHeight.Millimeters = 20f;   // 20 mm height
            generator.Parameters.Barcode.XDimension.Millimeters = 0.5f; // 0.5 mm module width

            // Save the generated barcode image to the specified path
            generator.Save(mmPath);
        }

        // Inform the user that the millimeter-based barcode has been saved
        Console.WriteLine($"Barcode saved with millimeter units: {mmPath}");

        // Define output file name for barcode using pixel units
        string pxPath = "barcode_px.png";

        // Create another barcode generator for the same data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Configure bar height and X dimension in pixels
            generator.Parameters.Barcode.BarHeight.Pixels = 80f;   // 80 pixel height
            generator.Parameters.Barcode.XDimension.Pixels = 2f; // 2 pixel module width

            // Save the generated barcode image to the specified path
            generator.Save(pxPath);
        }

        // Inform the user that the pixel-based barcode has been saved
        Console.WriteLine($"Barcode saved with pixel units: {pxPath}");
    }
}