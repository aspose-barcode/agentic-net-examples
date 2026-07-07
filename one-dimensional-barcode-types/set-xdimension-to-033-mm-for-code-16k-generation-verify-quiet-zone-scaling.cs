// Title: Set XDimension for Code 16K and verify quiet zone scaling
// Description: Demonstrates how to configure the XDimension of a Code 16K barcode to 0.33 mm and calculate the resulting quiet zone sizes.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and barcode parameter settings. Developers often need to adjust module size (XDimension) and quiet zone coefficients for precise barcode printing and scanning requirements. The snippet shows typical steps for configuring dimensions, retrieving default quiet‑zone coefficients, and saving the output image.
// Prompt: Set XDimension to 0.33 mm for Code 16K generation, verify quiet zone scaling.
// Tags: code16k, xdimension, quietzone, barcode generation, png, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates setting XDimension for a Code 16K barcode and verifying quiet zone scaling.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code 16K barcode with XDimension 0.33 mm, computes quiet zones, and saves the image.
    /// </summary>
    static void Main()
    {
        // Sample codetext for Code16K (any alphanumeric string is acceptable)
        const string codeText = "SampleCode16K";

        // Initialize the barcode generator for Code16K with the provided text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
        {
            // Set the module width (XDimension) to 0.33 millimeters
            generator.Parameters.Barcode.XDimension.Millimeters = 0.33f;

            // Retrieve the default quiet zone coefficients for Code16K (left = 10, right = 1)
            int leftCoef = generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef;
            int rightCoef = generator.Parameters.Barcode.Code16K.QuietZoneRightCoef;

            // Calculate the quiet zone sizes in millimeters using the coefficients
            float leftQuietZoneMm = leftCoef * generator.Parameters.Barcode.XDimension.Millimeters;
            float rightQuietZoneMm = rightCoef * generator.Parameters.Barcode.XDimension.Millimeters;

            // Output the configuration and calculated quiet zone values
            Console.WriteLine($"XDimension set to: {generator.Parameters.Barcode.XDimension.Millimeters} mm");
            Console.WriteLine($"Quiet zone left coefficient: {leftCoef}");
            Console.WriteLine($"Quiet zone right coefficient: {rightCoef}");
            Console.WriteLine($"Calculated left quiet zone: {leftQuietZoneMm} mm");
            Console.WriteLine($"Calculated right quiet zone: {rightQuietZoneMm} mm");

            // Save the generated barcode image to a PNG file
            generator.Save("code16k.png");
        }
    }
}