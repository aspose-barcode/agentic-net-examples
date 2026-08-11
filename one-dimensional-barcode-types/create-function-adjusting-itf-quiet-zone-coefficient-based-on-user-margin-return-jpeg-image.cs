// Title: Adjust ITF-14 Quiet Zone Coefficient and Export as JPEG
// Description: Demonstrates how to calculate and set the quiet zone coefficient for an ITF‑14 barcode based on a user‑specified margin, then save the result as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and ITF parameters to customize barcode appearance. Typical use cases include fine‑tuning quiet zones for printing requirements or meeting specific scanner specifications. Developers often need to adjust module size, quiet zone, and output format when integrating barcodes into documents or labels.
// Prompt: Create function adjusting ITF quiet zone coefficient based on user margin, return JPEG image.
// Tags: itf, quiet zone, barcode generation, jpeg, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides an example of adjusting the quiet zone coefficient for an ITF‑14 barcode
/// and exporting the generated barcode as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Adjusts the ITF quiet zone coefficient based on the provided margin (in points)
    /// and returns the generated barcode image as a JPEG byte array.
    /// </summary>
    /// <param name="marginPoints">The desired quiet zone margin expressed in points.</param>
    /// <returns>Byte array containing the JPEG image of the generated barcode.</returns>
    static byte[] AdjustITFQuietZone(float marginPoints)
    {
        // Sample ITF-14 barcode requires exactly 14 digits.
        const string sampleCode = "12345678901231";

        // Create the barcode generator for ITF-14.
        using (var generator = new BarcodeGenerator(EncodeTypes.ITF14, sampleCode))
        {
            // Set a reasonable XDimension (module size) in points.
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Calculate the quiet zone coefficient.
            // QuietZoneCoef = ceil(margin / XDimension). Minimum allowed value is 10.
            int coef = (int)Math.Ceiling(marginPoints / generator.Parameters.Barcode.XDimension.Point);
            if (coef < 10)
                coef = 10;

            // Apply the calculated coefficient to the ITF parameters.
            generator.Parameters.Barcode.ITF.QuietZoneCoef = coef;

            // Save the barcode to a memory stream as JPEG.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Jpeg);
                return ms.ToArray();
            }
        }
    }

    /// <summary>
    /// Entry point of the program. Demonstrates usage of <see cref="AdjustITFQuietZone"/>
    /// and writes the resulting JPEG image to disk.
    /// </summary>
    static void Main()
    {
        // Example usage: set a margin of 30 points.
        float userMargin = 30f;
        byte[] jpegData = AdjustITFQuietZone(userMargin);

        // Write the JPEG image to a file for verification.
        const string outputPath = "ITF_QuietZoneAdjusted.jpg";
        File.WriteAllBytes(outputPath, jpegData);
        Console.WriteLine($"Barcode image saved to {outputPath}");
    }
}