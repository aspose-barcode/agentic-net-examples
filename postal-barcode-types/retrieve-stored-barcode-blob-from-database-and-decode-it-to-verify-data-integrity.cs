// Title: Retrieve and Decode a QR Barcode Stored as a BLOB
// Description: Demonstrates generating a QR barcode, saving its image bytes as a BLOB (simulating database storage), retrieving the BLOB, and decoding it to verify data integrity.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases how to use the BarcodeGenerator class to create barcodes, store the resulting image bytes (e.g., in a database), and later employ the BarCodeReader class to decode the stored image. Typical use cases include persisting barcodes for later verification, auditing, or downstream processing. Developers often need to handle barcode BLOBs, convert them to streams, and extract encoded information reliably.
// Prompt: Retrieve a stored barcode BLOB from the database and decode it to verify data integrity.
// Tags: qr, barcode, blob, storage, decode, aspose.barcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR barcode, stores it as a binary BLOB,
/// retrieves the BLOB, and decodes it to verify the encoded data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, simulated DB storage,
    /// retrieval, and decoding.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for all demo files.
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the barcode image and the simulated database BLOB.
        string imagePath = Path.Combine(tempFolder, "barcode.png");
        string blobPath = Path.Combine(tempFolder, "barcode_blob.bin");

        // ------------------------------------------------------------
        // Generate a QR barcode and store its image bytes as a BLOB.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Test123"))
        {
            // Set the module size (pixel dimension) for the QR code.
            generator.Parameters.Barcode.XDimension.Pixels = 4;

            using (var ms = new MemoryStream())
            {
                // Save the barcode image to the memory stream in PNG format.
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] blob = ms.ToArray();

                // Optionally write the image file for visual reference.
                File.WriteAllBytes(imagePath, blob);

                // Simulate storing the BLOB in a database by writing it to a file.
                File.WriteAllBytes(blobPath, blob);
            }
        }

        // ------------------------------------------------------------
        // Retrieve the stored BLOB and decode it.
        // ------------------------------------------------------------
        if (!File.Exists(blobPath))
        {
            Console.WriteLine("Stored barcode BLOB not found.");
            return;
        }

        byte[] storedBlob = File.ReadAllBytes(blobPath);
        using (var ms = new MemoryStream(storedBlob))
        {
            // Specify the expected barcode type for decoding.
            BaseDecodeType decodeType = DecodeType.QR;

            using (var reader = new BarCodeReader(ms, decodeType))
            {
                // Read all barcodes found in the stream.
                BarCodeResult[] results = reader.ReadBarCodes();

                if (results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText))
                {
                    Console.WriteLine("Barcode decoded successfully.");
                    Console.WriteLine($"CodeText: {results[0].CodeText}");
                    Console.WriteLine($"CodeTypeName: {results[0].CodeTypeName}");
                }
                else
                {
                    Console.WriteLine("Failed to decode barcode.");
                }
            }
        }

        // ------------------------------------------------------------
        // Cleanup temporary files (optional).
        // ------------------------------------------------------------
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore any errors during cleanup.
        }
    }
}