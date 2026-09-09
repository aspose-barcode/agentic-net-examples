// Title: QR Barcode Generation and Reading with Quality Mapping
// Description: Demonstrates generating a QR barcode, reading it, and mapping reading quality values 1‑99 to a moderate quality warning.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create a QR code image and BarCodeReader to decode it. Developers often need to evaluate the reading quality of scanned barcodes; this sample shows how to access the ReadingQuality property and log warnings for moderate-quality results.
// Prompt: Map ReadingQuality values 1‑99 to moderate quality and trigger a warning log for each occurrence.
// Tags: qr,barcode,generation,recognition,readingquality,warning,aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates a QR barcode, reads it back, and logs a warning when the reading quality
/// falls within the moderate range (1‑99).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, reads it, evaluates reading quality, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to store the generated barcode image.
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the barcode image file.
        string barcodePath = Path.Combine(tempFolder, "sample_qr.png");

        // Generate a QR barcode using the Aspose.BarCode BarcodeGenerator.
        BaseEncodeType encodeType = EncodeTypes.QR;
        using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, "SampleText"))
        {
            // Save the generated barcode as a PNG image.
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Read the barcode image and evaluate the reading quality of each detected result.
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"Confidence: {result.Confidence}");
                Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");

                // Map reading quality values 1‑99 to "moderate" and log a warning.
                if (result.ReadingQuality >= 1 && result.ReadingQuality <= 99)
                {
                    Console.WriteLine($"Warning: ReadingQuality {result.ReadingQuality} maps to Moderate quality.");
                }
            }
        }

        // Clean up temporary files and directories.
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored – cleanup failures should not affect program exit.
        }
    }
}