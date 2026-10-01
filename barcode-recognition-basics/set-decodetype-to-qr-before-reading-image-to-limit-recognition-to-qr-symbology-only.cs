// Title: Decode QR Barcodes from Image with Aspose.BarCode
// Description: Demonstrates how to set the DecodeType to QR before reading an image, limiting recognition to QR symbology only.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category. It shows how to use the BarCodeReader class with a specific DecodeType to efficiently detect QR codes in an image. Typical use cases include scanning documents or photos where only QR codes are expected, improving performance and accuracy. Developers often need to configure DecodeType, load images, and process results using BarCodeResult objects.
// Prompt: Set DecodeType to QR before reading an image to limit recognition to QR symbology only.
// Tags: barcode symbology, decode, qr, reading, aspose.barcode, barcoderecognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a QR code image (if missing) and reads it using
/// Aspose.BarCode with DecodeType set to QR, ensuring only QR symbology is recognized.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a sample QR image, configures the reader,
    /// and outputs detection results to the console.
    /// </summary>
    static void Main()
    {
        // Define the temporary path for the sample QR code image
        string imagePath = Path.Combine(Path.GetTempPath(), "qr_sample.png");

        // Generate the QR image if it does not already exist
        if (!File.Exists(imagePath))
        {
            // Create a QR barcode with sample text "Hello QR"
            BaseEncodeType encodeType = EncodeTypes.QR;
            var generator = new BarcodeGenerator(encodeType, "Hello QR");
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Set DecodeType to QR to limit recognition to QR symbology only
        BaseDecodeType decodeType = DecodeType.QR;

        // Initialize the barcode reader with the image path and the specified decode type
        using (var reader = new BarCodeReader(imagePath, decodeType))
        {
            // Explicitly assign the image source (required by the API)
            reader.SetBarCodeImage(imagePath);

            // Perform the barcode detection
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output the detection results
            if (results.Length == 0)
            {
                Console.WriteLine("No QR barcode detected.");
            }
            else
            {
                foreach (var result in results)
                {
                    Console.WriteLine($"CodeText: {result.CodeText}");
                    Console.WriteLine($"Symbology: {result.CodeTypeName}");
                    Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                }
            }
        }
    }
}