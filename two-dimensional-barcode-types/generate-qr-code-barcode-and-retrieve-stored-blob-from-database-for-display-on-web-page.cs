// Title: Generate QR Code, store as BLOB, and output Base64 for web display
// Description: Demonstrates creating a QR Code barcode, saving its image as a binary BLOB, simulating database storage, retrieving it, verifying via decoding, and producing a Base64 string suitable for embedding in an HTML <img> tag.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for QR Code creation, BarCodeReader for decoding, and common image handling classes. Developers often need to generate barcodes, persist them (e.g., in databases as BLOBs), and later render them on web pages; this snippet provides a concise reference for those workflows.
// Prompt: Generate QR Code barcode and retrieve stored BLOB from database for display on web page.
// Tags: qr code, barcode generation, barcode recognition, blob, base64, aspose.barcode

using System;
using System.IO;
using System.Text;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a QR Code, stores it as a binary BLOB, retrieves it,
/// verifies the content, and outputs a Base64 string for web page embedding.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the QR Code generation, BLOB storage/retrieval,
    /// decoding verification, and Base64 conversion steps.
    /// </summary>
    static void Main()
    {
        // Step 1: Generate a QR Code barcode and obtain its binary representation (BLOB)
        byte[] barcodeBlob;
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the data to encode
            generator.CodeText = "https://example.com";

            // Configure high error correction level for better resilience
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the generated barcode image to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                barcodeBlob = ms.ToArray(); // Capture the image bytes as a BLOB
            }
        }

        // Step 2: Simulate storing the BLOB in a database by writing it to a temporary file
        string blobFilePath = Path.Combine(Path.GetTempPath(), "qr_barcode_blob.bin");
        File.WriteAllBytes(blobFilePath, barcodeBlob);
        Console.WriteLine($"Barcode BLOB stored at: {blobFilePath}");

        // Step 3: Retrieve the BLOB from the simulated database (local file)
        if (!File.Exists(blobFilePath))
        {
            Console.WriteLine("Error: Stored BLOB file not found.");
            return;
        }
        byte[] retrievedBlob = File.ReadAllBytes(blobFilePath);

        // Step 4: Convert the BLOB back to an image and verify it by decoding
        using (var ms = new MemoryStream(retrievedBlob))
        {
            using (var image = new Bitmap(ms))
            {
                // Decode the QR code to ensure it was stored correctly
                using (var reader = new BarCodeReader(image, DecodeType.QR))
                {
                    var result = reader.ReadBarCodes();
                    foreach (var barcode in result)
                    {
                        Console.WriteLine($"Decoded Text: {barcode.CodeText}");
                    }
                }

                // Step 5: Prepare a Base64 string for web display (e.g., <img src="data:image/png;base64,...">)
                string base64 = Convert.ToBase64String(retrievedBlob);
                Console.WriteLine("Base64 representation for web page:");
                Console.WriteLine($"data:image/png;base64,{base64}");
            }
        }

        // Note: In a real application, the BLOB would be stored/retrieved from a database
        // using appropriate data access libraries (e.g., ADO.NET, Entity Framework).
        // The above file-based approach is used because database packages are not available
        // in the snippet runner environment.
    }
}