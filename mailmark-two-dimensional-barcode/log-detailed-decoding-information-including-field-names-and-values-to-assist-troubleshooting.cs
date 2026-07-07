// Title: Generate and Decode a Code128 Barcode with Detailed Logging
// Description: This example creates a Code128 barcode image, saves it to disk, then reads the image back while logging all decoding fields to aid troubleshooting.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition workflows. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them, covering typical scenarios such as visual customization, multi‑symbology support, and detailed result inspection. Developers working with barcode imaging, inventory systems, or document automation often need these APIs to produce and validate barcodes programmatically.
// Prompt: Log detailed decoding information, including field names and values, to assist troubleshooting.
// Tags: code128, barcode generation, barcode recognition, decoding, logging, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates how to generate a Code128 barcode, save it as an image,
/// and then read it back while outputting detailed decoding information.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, saves it, and logs
    /// all available decoding details for each detected barcode.
    /// </summary>
    static void Main()
    {
        // Define the file name for the generated barcode image.
        string imagePath = "sample_barcode.png";

        // Generate a simple Code128 barcode and save it to a PNG file.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Optional: set visual properties for better contrast.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the barcode image.
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was successfully created.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Failed to create barcode image at '{imagePath}'.");
            return;
        }

        // Read the barcode from the generated image and log detailed information.
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Iterate through all detected barcodes in the image.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine("=== Detected Barcode ===");
                Console.WriteLine($"Type Name        : {result.CodeTypeName}");
                Console.WriteLine($"Code Text        : {result.CodeText}");
                Console.WriteLine($"Confidence       : {result.Confidence}");
                Console.WriteLine($"Reading Quality  : {result.ReadingQuality}");

                // Output the region bounds of the detected barcode.
                var rect = result.Region.Rectangle;
                Console.WriteLine($"Region X         : {rect.X}");
                Console.WriteLine($"Region Y         : {rect.Y}");
                Console.WriteLine($"Region Width     : {rect.Width}");
                Console.WriteLine($"Region Height    : {rect.Height}");

                // Output extended information if it is available.
                if (result.Extended != null)
                {
                    // Example for 1D barcodes (e.g., Code128).
                    var oneD = result.Extended.OneD;
                    if (oneD != null)
                    {
                        Console.WriteLine($"Extended Value   : {oneD.Value}");
                        Console.WriteLine($"Extended CheckSum: {oneD.CheckSum}");
                    }

                    // Example for QR codes (if a QR code were present).
                    var qr = result.Extended.QR;
                    if (qr != null)
                    {
                        Console.WriteLine($"QR Version       : {qr.Version}");
                        Console.WriteLine($"QR Error Level   : {qr.ErrorLevel}");
                    }
                }

                Console.WriteLine();
            }
        }
    }
}