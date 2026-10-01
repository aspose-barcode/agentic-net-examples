// Title: Demonstrate enabling AllowIncorrectBarcodes for debugging barcode recognition
// Description: Shows how to generate a Code128 barcode, then read it while allowing potentially unreadable barcodes to be captured for debugging.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It illustrates using BarcodeGenerator to create a barcode image and BarCodeReader with QualitySettings to adjust decoding behavior, such as AllowIncorrectBarcodes, Deconvolution, and InverseImage. Developers working with barcode scanning often need to fine‑tune these settings to handle low‑quality or damaged images, making this a common scenario for debugging and testing barcode applications.
// Prompt: Enable QualitySettings.AllowIncorrectBarcodes to capture potentially unreadable barcodes during debugging sessions.
// Tags: barcode generation, barcode recognition, allowincorrectbarcodes, debugging, code128, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program demonstrating how to enable AllowIncorrectBarcodes in Aspose.BarCode
/// for debugging purposes while generating and reading a Code128 barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, reads it with relaxed quality settings, and prints results.
    /// </summary>
    static void Main()
    {
        // Generate a barcode image in memory
        using (var barcodeStream = new MemoryStream())
        {
            // Create a generator for Code128 with the specified text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
            {
                // Save the barcode as PNG into the memory stream
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
            }

            // Reset stream position before reading
            barcodeStream.Position = 0;

            // Create a reader for the generated image, specifying the expected symbology
            using (var reader = new BarCodeReader(barcodeStream, DecodeType.Code128))
            {
                // Enable capturing potentially unreadable barcodes for debugging
                reader.QualitySettings.AllowIncorrectBarcodes = true;

                // Optional: adjust other quality settings for debugging
                reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;
                reader.QualitySettings.InverseImage = InverseImageMode.Enabled;

                // Decode and output results
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"CodeText: {result.CodeText}");
                    Console.WriteLine($"CodeType: {result.CodeTypeName}");
                    Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");
                }
            }
        }
    }
}