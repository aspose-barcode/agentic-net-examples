// Title: Disable checksum for Code 39 barcode and compare generated images
// Description: Demonstrates how to turn off the checksum for an optional‑checksum symbology (Code 39) and verifies that the resulting PNG image data remains unchanged.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on checksum handling for symbologies that support optional checksums. It showcases the BarcodeGenerator class, EncodeTypes enumeration, and image export via Aspose.Drawing. Developers often need to control checksum settings to meet specific scanning requirements while ensuring visual output consistency.
// Prompt: Validate that disabling checksum for an optional‑checksum barcode like Code 39 does not alter the generated image data.
// Tags: barcode, code39, checksum, image, png, aspose.barcode, generation

using System;
using System.IO;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates disabling checksum for Code 39 barcode and comparing the generated image data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates two barcode images—one with default checksum settings and one with checksum disabled—then checks if they are identical.
    /// </summary>
    static void Main()
    {
        // Text to encode in the barcode
        string codeText = "CODE39";

        // Generate barcode image with default checksum (enabled)
        byte[] imageDefault = GenerateBarcodeImage(codeText, false);

        // Generate barcode image with checksum explicitly disabled
        byte[] imageNoChecksum = GenerateBarcodeImage(codeText, true);

        // Compare the two image byte arrays for equality
        bool identical = imageDefault.SequenceEqual(imageNoChecksum);
        Console.WriteLine($"Images identical after disabling checksum: {identical}");
    }

    /// <summary>
    /// Creates a PNG image of a Code 39 barcode, optionally disabling its checksum.
    /// </summary>
    /// <param name="text">The data to encode.</param>
    /// <param name="disableChecksum">If true, disables the checksum for the generated barcode.</param>
    /// <returns>Byte array containing the PNG image data.</returns>
    static byte[] GenerateBarcodeImage(string text, bool disableChecksum)
    {
        // Initialize the barcode generator with Code 39 Full ASCII symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, text))
        {
            // Apply checksum setting if requested
            if (disableChecksum)
            {
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;
            }

            // Generate the barcode image as a bitmap
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save the bitmap to a memory stream in PNG format
                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    // Return the image data as a byte array
                    return ms.ToArray();
                }
            }
        }
    }
}