// Title: Convert Inches and Millimeters for Barcode XDimension
// Description: Demonstrates conversion between inches and millimeters and applying the values to the XDimension property of a barcode generated with Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode size parameters using the XDimension property. It covers the EncodeTypes, BarcodeGenerator, and BarCodeImageFormat classes, typical for developers needing precise module sizing in different measurement units. Such examples help when integrating barcode creation into reporting, labeling, or packaging solutions.
// Prompt: Create helper method converting values between Inches and Millimeters for barcode size calculations.
// Tags: barcode symbology, size conversion, inches, millimeters, aspose.barcode, generation, png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;
using Aspose.Drawing;

/// <summary>
/// Demonstrates conversion between inches and millimeters and uses the values to set barcode XDimension.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates helper conversions, generates two barcodes (one using inches, one using millimeters), and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Demonstrate helper methods for unit conversion
        float inches = 0.1f; // 0.1 inch
        double millimeters = InchesToMillimeters(inches);
        double backToInches = MillimetersToInches(millimeters);

        Console.WriteLine($"Inches: {inches} -> Millimeters: {millimeters:F2} -> Back to Inches: {backToInches:F4}");

        // Prepare output directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Generate barcode with XDimension specified in inches
        using (BarcodeGenerator generatorInches = new BarcodeGenerator(EncodeTypes.Code128, "INCHES"))
        {
            generatorInches.Parameters.Barcode.XDimension.Inches = inches;
            string pathInches = Path.Combine(outputDir, "Barcode_Inches.png");
            generatorInches.Save(pathInches, BarCodeImageFormat.Png);
            Console.WriteLine($"Saved barcode with XDimension in inches to: {pathInches}");
        }

        // Generate barcode with XDimension specified in millimeters (converted from inches)
        using (BarcodeGenerator generatorMillimeters = new BarcodeGenerator(EncodeTypes.Code128, "MILLIMETERS"))
        {
            generatorMillimeters.Parameters.Barcode.XDimension.Millimeters = (float)millimeters;
            string pathMillimeters = Path.Combine(outputDir, "Barcode_Millimeters.png");
            generatorMillimeters.Save(pathMillimeters, BarCodeImageFormat.Png);
            Console.WriteLine($"Saved barcode with XDimension in millimeters to: {pathMillimeters}");
        }
    }

    /// <summary>
    /// Converts inches to millimeters.
    /// </summary>
    /// <param name="inches">Value in inches.</param>
    /// <returns>Equivalent value in millimeters.</returns>
    static double InchesToMillimeters(double inches)
    {
        return inches * 25.4;
    }

    /// <summary>
    /// Converts millimeters to inches.
    /// </summary>
    /// <param name="millimeters">Value in millimeters.</param>
    /// <returns>Equivalent value in inches.</returns>
    static double MillimetersToInches(double millimeters)
    {
        return millimeters / 25.4;
    }
}