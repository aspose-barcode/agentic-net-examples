// Title: Set XDimension for Code 16K barcode and verify quiet zone scaling
// Description: Demonstrates how to configure the XDimension of a Code 16K barcode to 0.33 mm, retrieve quiet‑zone coefficients, calculate their sizes, and save the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and barcode parameter settings. Typical use cases include customizing barcode dimensions, quiet‑zone handling, and exporting images for printing or display. Developers often need to adjust XDimension and quiet‑zone values to meet specific scanning standards.
// Prompt: Set XDimension to 0.33 mm for Code 16K generation, verify quiet zone scaling.
// Tags: barcode, code16k, xdimension, quietzone, generation, png, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code 16K barcode, sets a custom XDimension, verifies quiet‑zone scaling,
/// and saves the barcode as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a BarcodeGenerator, configures parameters,
    /// outputs verification data, and writes the barcode image to disk.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for the Code16K symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K))
        {
            // Assign sample data to be encoded
            generator.CodeText = "1234567890123456789012345678901234567890";

            // Set the XDimension (module width) to 0.33 mm
            generator.Parameters.Barcode.XDimension.Millimeters = 0.33f;

            // Retrieve the default quiet‑zone coefficients for Code16K
            int leftCoef = generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef;
            int rightCoef = generator.Parameters.Barcode.Code16K.QuietZoneRightCoef;

            // Compute the actual quiet‑zone sizes in millimeters
            float leftQuietZone = leftCoef * generator.Parameters.Barcode.XDimension.Millimeters;
            float rightQuietZone = rightCoef * generator.Parameters.Barcode.XDimension.Millimeters;

            // Output the configuration and calculated quiet‑zone values
            Console.WriteLine($"XDimension set to {generator.Parameters.Barcode.XDimension.Millimeters} mm");
            Console.WriteLine($"QuietZoneLeftCoef = {leftCoef}, QuietZoneRightCoef = {rightCoef}");
            Console.WriteLine($"Calculated left quiet zone: {leftQuietZone} mm");
            Console.WriteLine($"Calculated right quiet zone: {rightQuietZone} mm");

            // Save the generated barcode as a PNG file
            string outputPath = "code16k.png";
            generator.Save(outputPath, BarCodeImageFormat.Png);
            Console.WriteLine($"Barcode image saved to '{outputPath}'.");
        }
    }
}