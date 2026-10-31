// Title: Inches ↔ Millimeters conversion helper for barcode sizing
// Description: Demonstrates helper methods to convert between inches and millimeters and applies the conversion to set barcode X‑dimension.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode dimensions using the BarcodeGenerator class. It shows typical use cases such as size calculations for printing, where developers need to convert measurement units before assigning them to generator parameters. The snippet covers EncodeTypes, BarcodeGenerator, and image saving, common tasks for barcode creation in .NET applications.
// Prompt: Create helper method converting values between Inches and Millimeters for barcode size calculations.
// Tags: barcode, conversion, inches, millimeters, size, generation, code128, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates conversion helpers between inches and millimeters and generates a Code128 barcode using Aspose.BarCode.
/// </summary>
class Program
{
    // Helper method: convert inches to millimeters
    static float InchesToMillimeters(float inches) => inches * 25.4f;

    // Helper method: convert millimeters to inches
    static float MillimetersToInches(float millimeters) => millimeters / 25.4f;

    /// <summary>
    /// Entry point. Converts measurement units, configures barcode dimensions, and saves the barcode image.
    /// </summary>
    static void Main()
    {
        // Define measurement in inches
        float inches = 0.2f;

        // Convert inches to millimeters for barcode sizing
        float millimeters = InchesToMillimeters(inches);

        // Output conversion results
        Console.WriteLine($"{inches} inches = {millimeters} millimeters");
        Console.WriteLine($"{millimeters} millimeters = {MillimetersToInches(millimeters)} inches");

        // Build full path for the output PNG file
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // Initialize barcode generator with Code128 symbology and data string
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set X-dimension (module width) using the converted millimeter value
            generator.Parameters.Barcode.XDimension.Millimeters = millimeters;

            // Define image resolution (dots per inch)
            generator.Parameters.Resolution = 300;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}