// Title: Generate QR Code and Store/Retrieve as BLOB for Web Display
// Description: Demonstrates creating a QR Code barcode, saving it as a binary BLOB, and retrieving it for use on a web page.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and storage category. It shows how to use BarcodeGenerator with QR encoding, configure error correction, save the image to a memory stream, and simulate persisting the image as a BLOB. Developers often need to generate barcodes, store them in databases, and later render them in web applications using Base64 or image files.
// Prompt: Generate QR Code barcode and retrieve stored BLOB from database for display on web page.
// Tags: qr code, barcode generation, blob storage, image output, base64, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code, stores it as a binary BLOB,
/// and retrieves it for display on a web page (as an image file and Base64 string).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the QR Code, simulates BLOB storage,
    /// and demonstrates retrieval for web usage.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the QR Code.
        string codeText = "https://example.com";

        // ------------------------------------------------------------
        // Generate QR Code and store it as a BLOB (simulated with a file)
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Configure a high error correction level for better resilience.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            using (var ms = new MemoryStream())
            {
                // Save the barcode image to the memory stream in PNG format.
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                // Simulate persisting the BLOB in a database by writing to a file.
                File.WriteAllBytes("barcode_blob.bin", imageBytes);
                Console.WriteLine("QR Code generated and stored as BLOB.");
            }
        }

        // ------------------------------------------------------------
        // Retrieve the stored BLOB and prepare it for web display
        // ------------------------------------------------------------
        if (File.Exists("barcode_blob.bin"))
        {
            // Read the binary data that represents the stored QR Code image.
            byte[] retrievedBytes = File.ReadAllBytes("barcode_blob.bin");

            // Save the retrieved image to a file that could be served by a web server.
            File.WriteAllBytes("retrieved_qr.png", retrievedBytes);
            Console.WriteLine("Retrieved QR Code saved as 'retrieved_qr.png'.");

            // Convert the image bytes to a Base64 string for embedding directly in HTML.
            string base64 = Convert.ToBase64String(retrievedBytes);
            Console.WriteLine("Base64 representation of the QR Code image:");
            Console.WriteLine(base64);
        }
        else
        {
            Console.WriteLine("Stored barcode BLOB not found.");
        }
    }
}