// Title: QR Code Generation and Reading with ReadingQuality Check
// Description: Generates a QR code, saves it to a temporary file, reads it back, and interprets a ReadingQuality of 0 as no reliable reading, prompting the user to rescan.
// Category-Description: This example demonstrates core Aspose.BarCode operations: barcode generation using BarcodeGenerator and barcode recognition using BarCodeReader. It shows how to create a QR code, persist it as an image, and evaluate the ReadingQuality property of detection results. Developers working with barcode scanning, quality assessment, or automated rescan workflows will find these patterns useful.
// Prompt: Interpret a ReadingQuality value of 0 as none and prompt the user to rescan the barcode.
// Tags: qr code, generation, recognition, readingquality, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR code, reading it, and handling low reading quality.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the sample. Generates a QR code, reads it, and checks the ReadingQuality.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Full path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a simple QR code barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify the file exists before attempting to read it
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine($"Barcode image not found at '{barcodePath}'.");
            return;
        }

        // Read the barcode from the generated image using QR decode type
        using (var reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            bool anyResult = false;

            // Iterate through all detected barcodes (should be one in this sample)
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                anyResult = true;

                // ReadingQuality: 0 means none (no reliable reading)
                if (result.ReadingQuality == 0)
                {
                    Console.WriteLine("ReadingQuality is none. Please rescan the barcode.");
                }
                else
                {
                    Console.WriteLine($"Decoded Text: {result.CodeText}");
                    Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");
                }
            }

            // If no barcode was detected at all, inform the user
            if (!anyResult)
            {
                Console.WriteLine("No barcode detected. Please rescan the image.");
            }
        }

        // Clean up temporary files (optional)
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore cleanup errors in this sample
        }
    }
}