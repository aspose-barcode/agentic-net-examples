// Title: Generate ITF14 Barcode with Adjustable Quiet Zone and Save as JPEG
// Description: Demonstrates how to create an ITF14 barcode, adjust its quiet zone coefficient based on a margin value, and output the result as a JPEG image.
// Category-Description: This example belongs to the barcode generation category of Aspose.BarCode, illustrating how to configure barcode parameters such as quiet zone, symbology, and image format. It uses the BarcodeGenerator class together with EncodeTypes and BarCodeImageFormat to produce raster images. Developers often need to customize barcode appearance for printing or embedding in documents, and this snippet shows a typical workflow for generating and saving a barcode image.
// Prompt: Create function adjusting ITF quiet zone coefficient based on user margin, return JPEG image.
// Tags: itf, quietzone, jpeg, barcode generation, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates an ITF14 barcode with a configurable quiet zone
/// and saves the result as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Define a sample margin that will be used to calculate the quiet zone coefficient.
        int sampleMargin = 20;

        // Generate the barcode image bytes with the specified margin.
        byte[] jpegData = GenerateItfBarcodeWithQuietZone(sampleMargin);

        // Determine the full path for the output JPEG file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "itf_quietzone.jpg");

        // Write the JPEG byte array to the file system.
        File.WriteAllBytes(outputPath, jpegData);

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }

    /// <summary>
    /// Generates an ITF14 barcode image with a quiet zone coefficient derived from the provided margin.
    /// </summary>
    /// <param name="margin">The desired margin; values below 10 are clamped to 10.</param>
    /// <returns>A byte array containing the JPEG representation of the barcode.</returns>
    static byte[] GenerateItfBarcodeWithQuietZone(int margin)
    {
        // Ensure the quiet zone coefficient is at least 10.
        int quietZoneCoef = margin < 10 ? 10 : margin;

        // Create a barcode generator for the ITF14 symbology with sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.ITF14, "12345678901231"))
        {
            // Apply the calculated quiet zone coefficient to the ITF settings.
            generator.Parameters.Barcode.ITF.QuietZoneCoef = quietZoneCoef;

            // Save the generated barcode to a memory stream in JPEG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Jpeg);
                // Return the JPEG data as a byte array.
                return ms.ToArray();
            }
        }
    }
}