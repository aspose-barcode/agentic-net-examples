// Title: Retrieve and Decode Barcode BLOB from Database
// Description: Demonstrates generating a Code128 barcode, storing it as a binary BLOB, retrieving the BLOB, and decoding it to verify data integrity.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator (generation) and BarCodeReader (recognition) classes to handle barcode images in typical scenarios such as persisting barcode graphics in a database, retrieving them later, and validating the encoded information. Developers often need to ensure that stored barcodes can be accurately read back, making this pattern essential for inventory, logistics, and document management systems.
// Prompt: Retrieve a stored barcode BLOB from the database and decode it to verify data integrity.
// Tags: barcode symbology, generation, recognition, decode, blob, database, integrity, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a barcode, simulates storing it as a BLOB,
/// retrieves the BLOB, decodes the barcode, and verifies the original data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the generate‑store‑retrieve‑decode workflow.
    /// </summary>
    static void Main()
    {
        // Define the original text to encode in the barcode.
        string originalText = "1234567890";

        // Create a temporary file path for the generated barcode image.
        string tempImagePath = Path.Combine(Path.GetTempPath(), "sample_barcode.png");

        // ------------------------------------------------------------
        // Generate a Code128 barcode image and save it to the temp file.
        // ------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, originalText))
        {
            generator.Save(tempImagePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Simulate retrieving the barcode BLOB from a database by reading the file bytes.
        // ------------------------------------------------------------
        byte[] imageBytes;
        if (!File.Exists(tempImagePath))
        {
            Console.WriteLine("Failed to locate the generated barcode image.");
            return;
        }

        using (FileStream fs = new FileStream(tempImagePath, FileMode.Open, FileAccess.Read))
        using (MemoryStream ms = new MemoryStream())
        {
            fs.CopyTo(ms);
            imageBytes = ms.ToArray();
        }

        // ------------------------------------------------------------
        // Decode the barcode from the retrieved byte array.
        // ------------------------------------------------------------
        using (MemoryStream imageStream = new MemoryStream(imageBytes))
        {
            BaseDecodeType decodeType = DecodeType.AllSupportedTypes;
            using (BarCodeReader reader = new BarCodeReader(imageStream, decodeType))
            {
                BarCodeResult[] results = reader.ReadBarCodes();

                if (results.Length == 0)
                {
                    Console.WriteLine("No barcode detected.");
                }
                else
                {
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"Decoded Text: {result.CodeText}");
                        Console.WriteLine($"Decoded Type: {result.CodeTypeName}");

                        // Verify that the decoded text matches the original text.
                        bool integrityOk = string.Equals(result.CodeText, originalText, StringComparison.Ordinal);
                        Console.WriteLine($"Data Integrity Verified: {integrityOk}");
                    }
                }
            }
        }

        // ------------------------------------------------------------
        // Clean up the temporary barcode image file.
        // ------------------------------------------------------------
        try
        {
            File.Delete(tempImagePath);
        }
        catch
        {
            // Ignore any cleanup errors.
        }
    }
}