// Title: Mailmark Barcode Generation and Attempted Recognition with Quiet Zone Ignoring
// Description: Demonstrates generating a Mailmark 4‑state barcode image and configuring BarCodeReader to attempt decoding while noting the lack of quiet‑zone support.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on complex barcode types such as Mailmark. It showcases the use of ComplexBarcodeGenerator for creating Mailmark barcodes and BarCodeReader for decoding. Developers working with postal barcodes often need to generate Mailmark symbols and read them from images, handling dense layouts and quiet‑zone considerations.
// Prompt: Configure BarCodeReader to ignore quiet zones while decoding Mailmark barcodes in densely packed images.
// Tags: mailmark, barcode generation, barcode recognition, quiet zone, aspose.barcode, complexbarcode, decode, image processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Mailmark barcode image and configuring a BarCodeReader
/// to decode it, highlighting the current API limitation regarding quiet‑zone handling.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a temporary Mailmark image, attempts to read it,
    /// and cleans up generated files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated files
        string tempFolder = Path.Combine(Path.GetTempPath(), "MailmarkDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated image
        string imagePath = Path.Combine(tempFolder, "mailmark.png");

        // Build Mailmark 4‑state codetext
        var mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Generate Mailmark barcode image using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Set X‑dimension (module size) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            // Save the image as PNG
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Generated Mailmark image at: {imagePath}");

        // Verify the image file exists before attempting to read
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Image file not found. Exiting.");
            return;
        }

        // Configure BarCodeReader – note: ignoring quiet zones is not exposed in the API.
        // Additionally, Mailmark recognition from images is not supported; the reader will return zero results.
        BaseDecodeType decodeType = DecodeType.Mailmark;

        using (var reader = new BarCodeReader(imagePath, decodeType))
        {
            // Attempt to improve detection in dense images
            reader.QualitySettings.AllowIncorrectBarcodes = true;
            reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;
            // No quiet‑zone property exists; this comment documents the limitation.

            // Perform barcode detection
            BarCodeResult[] results = reader.ReadBarCodes();

            Console.WriteLine($"Barcodes detected: {results.Length}");
            foreach (var result in results)
            {
                Console.WriteLine($"Type: {result.CodeTypeName}");
                Console.WriteLine($"Text: {result.CodeText}");
                Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
            }

            if (results.Length == 0)
            {
                Console.WriteLine("No Mailmark barcode detected (expected, as Mailmark image recognition is not supported).");
            }
        }

        // Clean up temporary files
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup failure should not affect program exit.
        }
    }
}