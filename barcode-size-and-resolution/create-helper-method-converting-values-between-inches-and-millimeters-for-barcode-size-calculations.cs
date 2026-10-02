// Title: Inches to Millimeters Conversion for Barcode XDimension
// Description: Demonstrates how to convert between inches and millimeters when setting the XDimension of a barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and XDimension properties for size calculations. Developers often need to switch measurement units to meet printing or design requirements, and this snippet shows the typical pattern for unit conversion and barcode image creation.
// Prompt: Create helper method converting values between Inches and Millimeters for barcode size calculations.
// Tags: barcode, conversion, inches, millimeters, xdimension, code128, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides helper methods and a demo for converting measurement units and generating barcodes with Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Converts inches to millimeters.
    /// </summary>
    static float InchesToMillimeters(float inches) => inches * 25.4f;

    /// <summary>
    /// Converts millimeters to inches.
    /// </summary>
    static float MillimetersToInches(float millimeters) => millimeters / 25.4f;

    /// <summary>
    /// Entry point that creates barcodes using both inches and millimeters for the XDimension and prints conversion results.
    /// </summary>
    static void Main()
    {
        // Prepare output directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Define XDimension in inches and convert to millimeters
        float xDimInches = 0.1f;
        float xDimMillimeters = InchesToMillimeters(xDimInches);

        // Generate barcode using inches as the measurement unit
        using (var generatorInches = new BarcodeGenerator(EncodeTypes.Code128, "INCHES"))
        {
            generatorInches.Parameters.Barcode.XDimension.Inches = xDimInches;
            string pathInches = Path.Combine(outputDir, "barcode_inches.png");
            generatorInches.Save(pathInches, BarCodeImageFormat.Png);
            Console.WriteLine($"Barcode saved (inches) to {pathInches}");
        }

        // Generate barcode using millimeters as the measurement unit
        using (var generatorMm = new BarcodeGenerator(EncodeTypes.Code128, "MILLIMETERS"))
        {
            generatorMm.Parameters.Barcode.XDimension.Millimeters = xDimMillimeters;
            string pathMm = Path.Combine(outputDir, "barcode_mm.png");
            generatorMm.Save(pathMm, BarCodeImageFormat.Png);
            Console.WriteLine($"Barcode saved (mm) to {pathMm}");
        }

        // Demonstrate reverse conversion from millimeters to inches
        float mmValue = 5.0f;
        float inchesValue = MillimetersToInches(mmValue);
        Console.WriteLine($"{mmValue} mm = {inchesValue:F4} inches");
    }
}