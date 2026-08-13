// Title: Retrieve and Decode Barcode BLOB from Database
// Description: Demonstrates generating a barcode, storing it as a BLOB, retrieving it, and decoding to verify data integrity.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create a barcode image, store it as a binary BLOB, and then use BarCodeReader with DecodeType.AllSupportedTypes to detect and decode the barcode. Developers often need to persist barcodes in databases and later validate them, making this pattern useful for inventory, ticketing, and authentication systems.
// Prompt: Retrieve a stored barcode BLOB from the database and decode it to verify data integrity.
// Tags: barcode, code128, generation, recognition, blob, data integrity, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a barcode, persisting it as a BLOB, retrieving it, and decoding to verify data integrity.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, saves it to a temporary file,
    /// reads the file into a byte array (simulating a database BLOB), decodes the barcode,
    /// and checks that the decoded text matches the original.
    /// </summary>
    static void Main()
    {
        // Sample data to encode
        const string originalText = "HelloAspose";

        // Path for temporary barcode image
        string imagePath = Path.Combine(Path.GetTempPath(), "sample_barcode.png");

        // -------------------------------------------------
        // Step 1: Generate a barcode and save it to a file
        // -------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, originalText))
        {
            // Save as PNG
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // -------------------------------------------------
        // Step 2: Simulate retrieving the barcode BLOB from a database
        // -------------------------------------------------
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        byte[] barcodeBlob;
        using (FileStream readStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
        using (MemoryStream ms = new MemoryStream())
        {
            // Copy file contents into memory stream
            readStream.CopyTo(ms);
            barcodeBlob = ms.ToArray();
        }

        // -------------------------------------------------
        // Step 3: Decode the barcode from the BLOB and verify integrity
        // -------------------------------------------------
        using (MemoryStream blobStream = new MemoryStream(barcodeBlob))
        using (BarCodeReader reader = new BarCodeReader(blobStream, DecodeType.AllSupportedTypes))
        {
            // Read all barcodes in the image
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
            }
            else
            {
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                    Console.WriteLine($"Decoded Text: {result.CodeText}");

                    // Verify data integrity
                    bool isValid = string.Equals(originalText, result.CodeText, StringComparison.Ordinal);
                    Console.WriteLine($"Data Integrity Check: {(isValid ? "PASS" : "FAIL")}");
                }
            }
        }

        // Cleanup temporary file
        try
        {
            File.Delete(imagePath);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program flow
        }
    }
}