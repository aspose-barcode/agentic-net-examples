// Title: Set barcode height to 50 mm while preserving XDimension
// Description: Demonstrates how to configure a barcode's bar height in millimeters using Aspose.BarCode without altering the default XDimension, ensuring the aspect ratio remains unchanged.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to adjust barcode dimensions via the BarcodeGenerator.Parameters API. Typical use cases include customizing barcode size for printing or display while maintaining visual fidelity. Developers often need to modify bar height, XDimension, or other layout properties to meet design specifications.
// Prompt: Set barcode height to 50 mm while keeping default XDimension to preserve aspect ratio.
// Tags: code128, set-height, png, aspose.barcode, barcodegenerator

using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Demonstrates setting the barcode height to 50 mm while keeping the default XDimension.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode with a specific height and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with the sample text "Sample123"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set the bar height to 50 millimeters; XDimension remains at its default value,
            // preserving the original aspect ratio of the barcode.
            generator.Parameters.Barcode.BarHeight.Millimeters = 50f;

            // Save the generated barcode image to a PNG file in the current directory.
            generator.Save("barcode.png");
        }

        // Inform the user that the barcode has been generated successfully.
        Console.WriteLine("Barcode generated successfully.");
    }
}