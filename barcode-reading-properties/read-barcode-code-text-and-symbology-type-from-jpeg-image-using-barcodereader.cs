// Title: Read barcode text and symbology from a JPEG image using BarCodeReader
// Description: Demonstrates generating a QR barcode, saving it as a JPEG, and then extracting the barcode text and symbology type from the image with Aspose.BarCode's BarCodeReader.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category. It showcases the use of core API classes such as BarcodeGenerator for barcode creation, BarCodeReader for decoding, and QualitySettings for performance tuning. Typical scenarios include reading barcodes from scanned documents, images, or camera captures, where developers need to quickly obtain the encoded data and its symbology.
// Prompt: Read barcode code text and symbology type from a JPEG image using BarCodeReader.
// Tags: barcode, symbology, read, jpeg, aspose.barcode, barcodereader, generation, qr, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that creates a QR barcode image and reads its content using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR code, saves it as JPEG, then reads and displays the barcode text and type.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo
        string tempFolder = Path.Combine(Path.GetTempPath(), "ReadBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Path to the sample JPEG image
        string imagePath = Path.Combine(tempFolder, "sample.jpg");

        // Generate a sample QR barcode and save it as JPEG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "HelloWorld"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Jpeg);
        }

        // Verify that the image file exists before attempting to read
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Read barcodes from the JPEG image (all supported symbologies)
        BaseDecodeType decodeType = DecodeType.AllSupportedTypes;
        using (var reader = new BarCodeReader(imagePath, decodeType))
        {
            // Set high‑performance quality settings (optional)
            reader.QualitySettings = QualitySettings.HighPerformance;

            // Perform the decoding operation
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes detected.");
            }
            else
            {
                // Output each detected barcode's details
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"Code Text   : {result.CodeText}");
                    Console.WriteLine($"Symbology   : {result.CodeTypeName}");
                    Console.WriteLine($"Quality (%) : {result.ReadingQuality}");
                    Console.WriteLine(new string('-', 30));
                }
            }
        }

        // Clean up temporary files (optional)
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect demo execution
        }
    }
}