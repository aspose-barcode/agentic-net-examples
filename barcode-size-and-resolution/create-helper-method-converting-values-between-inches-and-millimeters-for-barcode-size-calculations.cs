// Title: Barcode size conversion between inches and millimeters
// Description: Demonstrates helper methods for converting inches to millimeters and vice versa, used for setting barcode image dimensions.
// Category-Description: This example belongs to the Aspose.BarCode image sizing category, illustrating how to use the AutoSizeMode.Interpolation mode and the ImageWidth/ImageHeight properties (Millimeters unit) to control barcode dimensions. Developers often need to convert measurement units when integrating barcode generation into print layouts or UI designs, and these helper methods simplify that process.
// Prompt: Create helper method converting values between Inches and Millimeters for barcode size calculations.
// Tags: barcode, size conversion, inches, millimeters, autosizemode, imagewidth, imageheight, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates conversion helpers and barcode generation with size set in millimeters.
/// </summary>
class Program
{
    // Convert inches to millimeters (1 inch = 25.4 mm)
    static float InchesToMillimeters(float inches)
    {
        return inches * 25.4f;
    }

    // Convert millimeters to inches
    static float MillimetersToInches(float millimeters)
    {
        return millimeters / 25.4f;
    }

    /// <summary>
    /// Entry point: converts sample dimensions, generates a Code128 barcode with specified size, and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Sample dimensions in inches (typical for print layouts)
        float widthInInches = 2.0f;
        float heightInInches = 1.0f;

        // Convert to millimeters for barcode size calculations
        float widthInMillimeters = InchesToMillimeters(widthInInches);
        float heightInMillimeters = InchesToMillimeters(heightInInches);

        // Output conversion results to console
        Console.WriteLine($"Width: {widthInInches} inches = {widthInMillimeters} mm");
        Console.WriteLine($"Height: {heightInInches} inches = {heightInMillimeters} mm");

        // Create a simple barcode and apply the converted size
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Use Interpolation mode to control size via ImageWidth/ImageHeight
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set size using the millimeter values
            generator.Parameters.ImageWidth.Millimeters = widthInMillimeters;
            generator.Parameters.ImageHeight.Millimeters = heightInMillimeters;

            // Optional: set background and bar colors
            generator.Parameters.BackColor = Color.White;
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the barcode image
            string outputPath = "barcode.png";
            generator.Save(outputPath);
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}