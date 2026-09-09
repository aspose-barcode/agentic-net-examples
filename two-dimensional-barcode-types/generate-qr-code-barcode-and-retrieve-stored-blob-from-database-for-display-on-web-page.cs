// Title: Generate QR Code, store as BLOB, and display as Base64
// Description: This example creates a QR Code barcode, saves it to a memory stream, simulates storing the image as a BLOB in a database, retrieves it, converts it to a Base64 string for web embedding, and decodes the barcode.
// Category-Description: Demonstrates core Aspose.BarCode operations—barcode generation (BarcodeGenerator), image handling (BarCodeImageFormat), and barcode recognition (BarCodeReader). Typical use cases include creating QR codes for web pages, persisting barcode images as BLOBs in databases, and later retrieving and decoding them. Developers working with Aspose.BarCode often need to convert barcodes to various formats, store them efficiently, and extract encoded data on demand.
// Prompt: Generate QR Code barcode and retrieve stored BLOB from database for display on web page.
// Tags: qr code, barcode generation, barcode recognition, blob storage, base64, aspose.barcode, image, web

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates QR Code generation, simulated BLOB storage, retrieval, Base64 conversion, and decoding using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that runs the QR Code generation and retrieval workflow.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the QR Code.
        string codeText = "https://example.com";

        // Create a QR Code generator with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Set the error correction level (optional).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated barcode image to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                // Simulate storing the image BLOB in a database by writing to a temporary file.
                string simulatedDbPath = Path.Combine(Path.GetTempPath(), "qr_blob.bin");
                File.WriteAllBytes(simulatedDbPath, imageBytes);
                Console.WriteLine($"Barcode image stored to simulated DB file: {simulatedDbPath}");

                // Simulate retrieving the BLOB from the database.
                if (File.Exists(simulatedDbPath))
                {
                    byte[] retrievedBytes = File.ReadAllBytes(simulatedDbPath);

                    // Convert the retrieved image bytes to a Base64 string for embedding in a web page.
                    string base64 = Convert.ToBase64String(retrievedBytes);
                    Console.WriteLine("Base64 Image for web page:");
                    Console.WriteLine(base64);

                    // Decode the barcode from the retrieved image to verify correctness.
                    using (var msRead = new MemoryStream(retrievedBytes))
                    {
                        using (var reader = new BarCodeReader(msRead, DecodeType.QR))
                        {
                            var results = reader.ReadBarCodes();
                            foreach (var result in results)
                            {
                                Console.WriteLine($"Decoded text: {result.CodeText}");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Failed to retrieve barcode image from simulated DB.");
                }
            }
        }
    }
}