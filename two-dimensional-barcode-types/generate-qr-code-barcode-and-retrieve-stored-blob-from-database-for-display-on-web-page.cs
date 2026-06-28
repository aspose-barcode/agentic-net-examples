using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR Code, storing it as a binary BLOB, retrieving it,
/// and converting it to a Base64 string for display.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR Code, simulates BLOB storage/retrieval, and outputs the Base64 image.
    /// </summary>
    static void Main()
    {
        // Create a QR Code generator with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Configure the QR Code: set high error correction level.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Set the image resolution to 300 DPI.
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading.

                // Extract the byte array representing the PNG image.
                byte[] barcodeBytes = ms.ToArray();

                // Simulate storing the barcode image as a BLOB in a database.
                // Here we write the bytes to a local file as a placeholder.
                const string blobPath = "qr_blob.bin";
                File.WriteAllBytes(blobPath, barcodeBytes);

                // Simulate retrieving the BLOB from the database.
                // In this demo, we read the bytes back from the local file.
                byte[] retrievedBytes = File.ReadAllBytes(blobPath);

                // Convert the retrieved image bytes to a Base64 string for web display.
                string base64Image = Convert.ToBase64String(retrievedBytes);
                Console.WriteLine("Base64 QR Code Image:");
                Console.WriteLine(base64Image);
            }
        }

        // NOTE: In a real application, the BLOB would be stored/retrieved from a database.
        // The above file I/O serves as a substitute for demonstration purposes.
    }
}