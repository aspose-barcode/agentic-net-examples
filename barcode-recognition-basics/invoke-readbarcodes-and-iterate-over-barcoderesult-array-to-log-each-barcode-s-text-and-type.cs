// Title: Read and Log Barcodes from a Generated QR Image
// Description: This example generates a QR code image, reads all barcodes from it, and logs each barcode's text and type to the console.
// Category-Description: Demonstrates Aspose.BarCode barcode generation and recognition workflow. Uses BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and BarCodeResult for accessing decoded data. Typical use cases include generating QR codes, scanning images for multiple symbologies, and extracting barcode information in .NET applications. Developers often need to iterate over detection results to process each barcode's content and type.
// Prompt: Invoke ReadBarCodes and iterate over the BarCodeResult array to log each barcode's text and type.
// Tags: qr,barcode,read,recognition,generation,aspobarcodes,decode,code-text,code-type

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a QR code, reading it back, and outputting each detected barcode's text and type.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the sample. Generates a QR barcode, reads it, and logs results.
    /// </summary>
    static void Main()
    {
        // Create a temporary directory for the sample barcode image
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define the full path for the generated barcode image
        string barcodePath = Path.Combine(tempDir, "sample.png");

        // Generate a QR barcode and save it to a file
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "HelloWorld"))
        {
            // Set the module size (pixel dimension) for the QR code
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify the file was created before attempting to read it
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read barcodes from the generated image using the most permissive decode type
        using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            BarCodeResult[] results = reader.ReadBarCodes();

            // Check whether any barcodes were detected
            if (results == null || results.Length == 0)
            {
                Console.WriteLine("No barcodes were detected.");
            }
            else
            {
                // Iterate over each detection result and log its text and symbology name
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"CodeText: {result.CodeText}");
                    Console.WriteLine($"CodeTypeName: {result.CodeTypeName}");
                }
            }
        }

        // Clean up temporary files and directory
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }
}