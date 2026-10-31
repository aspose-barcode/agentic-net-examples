// Title: Adjust ITF14 Quiet Zone Coefficient and Save as JPEG
// Description: Demonstrates how to set a custom quiet‑zone coefficient for an ITF‑14 barcode and export the result as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and barcode parameter objects (XDimension, ITF) to customize barcode appearance. Typical scenarios include adjusting quiet‑zone margins for printing standards or scanner requirements. Developers often need to modify quiet‑zone settings, change image formats, and save the output to files or streams.
// Prompt: Create function adjusting ITF quiet zone coefficient based on user margin, return JPEG image.
// Tags: itf14, quietzone, barcode, generation, jpeg, aspose.barcode, imageformat, encoding

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating an ITF‑14 barcode with a user‑defined quiet‑zone coefficient and saving it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, writes the JPEG to disk, and reports the result.
    /// </summary>
    static void Main()
    {
        // Sample margin value for the quiet‑zone coefficient.
        int margin = 20;

        try
        {
            // Generate the barcode image as a JPEG byte array.
            byte[] jpegData = GenerateItfBarcodeWithQuietZone(margin);

            // Determine the output file path in the current directory.
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "ITF_QuietZoneMargin.jpg");

            // Write the JPEG data to the file system.
            File.WriteAllBytes(outputPath, jpegData);

            Console.WriteLine($"Barcode image saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during generation or file I/O.
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Generates an ITF‑14 barcode with the specified quiet‑zone coefficient and returns the image as a JPEG byte array.
    /// </summary>
    /// <param name="margin">Quiet‑zone coefficient; must be at least 10.</param>
    /// <returns>Byte array containing the JPEG representation of the barcode.</returns>
    static byte[] GenerateItfBarcodeWithQuietZone(int margin)
    {
        // Validate the quiet‑zone coefficient.
        if (margin < 10)
            throw new ArgumentOutOfRangeException(nameof(margin), "Quiet zone coefficient must be at least 10.");

        // Create a barcode generator for ITF‑14 with a sample value.
        using (var generator = new BarcodeGenerator(EncodeTypes.ITF14, "12345678901231"))
        {
            // Set the X‑dimension (module width) in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Apply the user‑defined quiet‑zone coefficient.
            generator.Parameters.Barcode.ITF.QuietZoneCoef = margin;

            // Save the barcode to a memory stream in JPEG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Jpeg);
                return ms.ToArray();
            }
        }
    }
}