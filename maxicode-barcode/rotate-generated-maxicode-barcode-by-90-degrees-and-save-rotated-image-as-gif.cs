// Title: Rotate MaxiCode Barcode and Save as GIF
// Description: Generates a MaxiCode barcode, rotates it 90 degrees, and saves the rotated image as a GIF file.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It demonstrates how to use the ComplexBarcodeGenerator with MaxiCodeCodetextMode2 to create a MaxiCode symbol, then employs Aspose.Drawing to manipulate the resulting image (rotation) before saving. Developers working with high‑density 2‑D barcodes often need to adjust orientation for printing or display purposes, and this pattern shows the typical workflow using key API classes such as ComplexBarcodeGenerator, MaxiCodeCodetextMode2, Image, and RotateFlipType.
/// Prompt: Rotate a generated MaxiCode barcode by 90 degrees and save the rotated image as GIF.
/// Tags: maxicode, rotation, gif, aspose.barcode, complexbarcode, imageprocessing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a MaxiCode barcode, rotating it 90°,
/// and saving the rotated image as a GIF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a MaxiCode barcode, rotates the image,
    /// and writes the result to disk.
    /// </summary>
    static void Main()
    {
        // Prepare MaxiCode codetext (Mode 2 example) with required fields.
        var maxiCode = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",
            CountryCode = 56,
            ServiceCategory = 999
        };

        // Add a second message to the MaxiCode payload.
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Sample Message"
        };
        maxiCode.SecondMessage = secondMessage;

        // Generate the MaxiCode image into a memory stream using ComplexBarcodeGenerator.
        using (var generator = new ComplexBarcodeGenerator(maxiCode))
        using (var memory = new MemoryStream())
        {
            generator.Save(memory, BarCodeImageFormat.Png);
            memory.Position = 0; // Reset stream position for reading.

            // Load the image with Aspose.Drawing, rotate 90 degrees, and save as GIF.
            using (var image = Image.FromStream(memory))
            {
                image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                image.Save("maxicode_rotated.gif", ImageFormat.Gif);
            }
        }
    }
}