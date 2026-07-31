// Title: Auto-rotate barcode reading with Aspose.BarCode
// Description: Demonstrates enabling the autoRotate option to automatically correct a barcode's orientation before decoding it.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing how to use BarCodeReader with auto‑rotation support. It highlights key API classes such as BarcodeGenerator, BarCodeReader, and ImageFormat, typical for scenarios where barcodes may be captured at arbitrary angles (e.g., scanned documents or camera images). Developers often need to ensure reliable decoding regardless of image orientation, and this snippet illustrates the straightforward configuration to achieve that.
// Prompt: Enable autoRotate option to automatically correct barcode orientation before reading each processed image.
// Tags: code128, auto-rotate, barcode-reading, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a rotated Code128 barcode, then reads it back
/// using the auto‑rotate feature of <see cref="BarCodeReader"/>.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a rotated barcode image, saves it to a memory stream,
    /// and reads it back while automatically correcting its orientation.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            // Simulate a mis‑oriented barcode by rotating the image 90 degrees
            generator.Parameters.RotationAngle = 90f;

            // Generate the barcode image as a bitmap
            using (var bitmap = generator.GenerateBarCodeImage())
            {
                // Store the bitmap in a memory stream in PNG format
                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    ms.Position = 0; // Reset stream position for reading

                    // Create a reader that automatically corrects orientation (autoRotate is enabled by default)
                    using (var reader = new BarCodeReader(ms, DecodeType.Code128))
                    {
                        // Iterate through all detected barcodes (should be only one)
                        foreach (var result in reader.ReadBarCodes())
                        {
                            // Output the decoded text; orientation has been corrected automatically
                            Console.WriteLine($"Detected CodeText: {result.CodeText}");
                        }
                    }
                }
            }
        }
    }
}