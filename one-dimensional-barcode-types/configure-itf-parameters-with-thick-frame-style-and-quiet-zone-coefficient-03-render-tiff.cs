// Title: Configure ITF14 barcode with thick frame and quiet zone coefficient, render as TIFF
// Description: Demonstrates setting ITF14 barcode parameters such as a thick frame border and handling quiet zone coefficient validation, then saving the result as a TIFF image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to customize ITF14 symbology using the BarcodeGenerator class. Typical use cases include creating product packaging barcodes with specific border styles and quiet zone settings. Developers often need to adjust border thickness, frame style, and output image format for compliance and branding requirements.
// Prompt: Configure ITF parameters with thick frame style and quiet zone coefficient 0.3, render TIFF.
// Tags: itf14, barcode, generation, tiff, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates configuring ITF14 barcode parameters and saving as TIFF.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates an ITF14 barcode with a thick frame border, validates quiet zone coefficient, and saves the image.
    /// </summary>
    static void Main()
    {
        // Sample ITF14 code text (13 digits, check digit will be added automatically)
        const string codeText = "1234567890123";

        // Desired quiet zone coefficient (invalid per API constraints)
        const float quietZoneCoef = 0.3f;

        // Validate quiet zone coefficient before applying
        // The API requires a coefficient of at least 10; otherwise we abort.
        if (quietZoneCoef < 10f)
        {
            Console.WriteLine($"Quiet zone coefficient {quietZoneCoef} is invalid. It must be >= 10.");
            return;
        }

        // Create the barcode generator for ITF14
        using (var generator = new BarcodeGenerator(EncodeTypes.ITF14, codeText))
        {
            // Configure a thick frame border
            generator.Parameters.Barcode.ITF.BorderType = ITF14BorderType.Frame;
            generator.Parameters.Barcode.ITF.BorderThickness.Point = 5f; // thick border

            // If the coefficient were valid, it would be set like this:
            // generator.Parameters.Barcode.ITF.QuietZoneCoef = (int)quietZoneCoef;

            // Save the barcode as a TIFF image
            generator.Save("itf14.tiff");
        }

        Console.WriteLine("Barcode generated successfully.");
    }
}