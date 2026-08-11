// Title: Configure ITF14 barcode with thick frame border and quiet zone coefficient, save as TIFF
// Description: Demonstrates how to set a thick frame border and adjust the quiet zone coefficient for an ITF14 barcode, then render the result as a TIFF image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Typical use cases include customizing barcode appearance such as border style, thickness, and quiet zone settings before exporting to image formats. Developers often need to fine‑tune these parameters for compliance with printing standards and visual requirements.
// Prompt: Configure ITF parameters with thick frame style and quiet zone coefficient 0.3, render TIFF.
// Tags: itf14, barcode, configuration, tiff, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates configuring ITF14 barcode parameters and saving as a TIFF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates, configures, and saves an ITF14 barcode.
    /// </summary>
    static void Main()
    {
        // Define the output file path (current directory + filename)
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "itf.tiff");

        // Initialize a barcode generator for ITF14 (requires exactly 14 digits)
        using (var generator = new BarcodeGenerator(EncodeTypes.ITF14, "12345678901231"))
        {
            // ------------------------------
            // Configure ITF-specific settings
            // ------------------------------

            // Set a thick frame border around the barcode
            generator.Parameters.Barcode.ITF.BorderType = ITF14BorderType.Frame;
            // Example thick border: 5 points
            generator.Parameters.Barcode.ITF.BorderThickness.Point = 5f;

            // Attempt to set quiet zone coefficient to 0.3.
            // The API expects an integer >= 10, so we handle an invalid value gracefully.
            try
            {
                // 0.3 expressed as an integer multiplier (e.g., 3) will trigger an exception.
                generator.Parameters.Barcode.ITF.QuietZoneCoef = 3;
            }
            catch (ArgumentException ex)
            {
                // Log the failure and fall back to the minimum allowed value.
                Console.WriteLine($"QuietZoneCoef setting failed: {ex.Message}");
                generator.Parameters.Barcode.ITF.QuietZoneCoef = 10;
            }

            // ------------------------------
            // Save the generated barcode
            // ------------------------------

            // Render the barcode as a TIFF image and write it to the output path
            generator.Save(outputPath, BarCodeImageFormat.Tiff);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}