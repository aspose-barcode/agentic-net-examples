// Title: Adjust ITF14 Barcode Quiet Zone Coefficient and Save as JPEG
// Description: Demonstrates how to modify the quiet zone coefficient of an ITF14 barcode and export the result as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to configure barcode parameters such as XDimension and ITF quiet zone. It uses the BarcodeGenerator class to create barcodes, a common task for developers needing custom barcode layouts for packaging, inventory, or labeling solutions. Typical use cases include adjusting visual spacing, size, and output format for downstream printing or digital distribution.
// Prompt: Create function adjusting ITF quiet zone coefficient based on user margin, return JPEG image.
// Tags: itf14, quietzone, barcode, generation, jpeg, aspose.barcode, imageoutput

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that adjusts the quiet zone coefficient of an ITF14 barcode and saves it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode with a custom quiet zone and writes the JPEG to disk.
    /// </summary>
    static void Main()
    {
        // Sample margin; in a real scenario this could come from command‑line arguments or configuration
        int margin = 20;

        // Generate the barcode image bytes with the specified quiet zone coefficient
        byte[] jpegData = AdjustITFQuietZone(margin);

        // Determine the output file path in the current working directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "ITFQuietZone.jpg");

        // Write the JPEG data to disk
        File.WriteAllBytes(outputPath, jpegData);

        // Inform the user where the file was saved
        Console.WriteLine($"ITF barcode with QuietZoneCoef={margin} saved to {outputPath}");
    }

    /// <summary>
    /// Creates an ITF14 barcode, sets a custom quiet zone coefficient, and returns the image as a JPEG byte array.
    /// </summary>
    /// <param name="quietZoneCoef">The quiet zone coefficient; must be at least 10.</param>
    /// <returns>Byte array containing the JPEG representation of the barcode.</returns>
    static byte[] AdjustITFQuietZone(int quietZoneCoef)
    {
        // Validate the quiet zone coefficient to avoid runtime errors
        if (quietZoneCoef < 10)
            throw new ArgumentOutOfRangeException(nameof(quietZoneCoef), "Quiet zone coefficient must be at least 10.");

        // Initialize the barcode generator for ITF14 with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.ITF14, "12345678901231"))
        {
            // Set XDimension for a reasonable barcode size (2 pixels per module)
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Apply the custom quiet zone coefficient
            generator.Parameters.Barcode.ITF.QuietZoneCoef = quietZoneCoef;

            // Render the barcode into a memory stream as JPEG
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Jpeg);
                return ms.ToArray(); // Return the JPEG data
            }
        }
    }
}