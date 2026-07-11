// Title: Retrieve and Decode Barcode BLOB from Database
// Description: Demonstrates reading a barcode image stored as a BLOB, decoding it, and verifying that the decoded data matches the original text.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, showcasing how to work with binary data (BLOBs) using BarcodeGenerator and BarCodeReader classes. Typical use cases include persisting barcodes in databases, retrieving them for verification, and ensuring data integrity in inventory or tracking systems. Developers often need to convert between image files, byte arrays, and decoded values, which this sample illustrates.
// Prompt: Retrieve a stored barcode BLOB from the database and decode it to verify data integrity.
// Tags: barcode, blob, decode, data integrity, code128, aspnet, aspose.barcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that simulates retrieving a barcode image stored as a BLOB,
/// decodes it using Aspose.BarCode, and verifies data integrity against the original text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates or loads a barcode BLOB, decodes it,
    /// and checks that the decoded value matches the expected text.
    /// </summary>
    static void Main()
    {
        // Paths for simulated BLOB storage and temporary image file
        const string blobPath = "barcode_blob.bin";
        const string imagePath = "sample_barcode.png";

        // The original text that we expect to encode/decode
        const string originalCodeText = "Sample123";

        // ------------------------------------------------------------
        // Simulate retrieving a barcode image BLOB from a database.
        // In a real scenario you would fetch the byte[] from the DB,
        // e.g. using ADO.NET. The code is shown as a comment below.
        // ------------------------------------------------------------
        // byte[] barcodeBytesFromDb;
        // using (var connection = new SqlConnection("your-connection-string"))
        // {
        //     connection.Open();
        //     using (var command = new SqlCommand("SELECT BarcodeBlob FROM Barcodes WHERE Id = @id", connection))
        //     {
        //         command.Parameters.AddWithValue("@id", 1);
        //         barcodeBytesFromDb = (byte[])command.ExecuteScalar();
        //     }
        // }

        byte[] barcodeBytes;

        if (File.Exists(blobPath))
        {
            // BLOB file exists – read the stored bytes
            barcodeBytes = File.ReadAllBytes(blobPath);
        }
        else
        {
            // BLOB does not exist – generate a sample barcode, save it,
            // and write its bytes to the simulated BLOB file.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, originalCodeText))
            {
                // Save the barcode image to a PNG file
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }

            // Read the generated image bytes
            barcodeBytes = File.ReadAllBytes(imagePath);

            // Store the bytes as a simulated BLOB
            File.WriteAllBytes(blobPath, barcodeBytes);
        }

        // ------------------------------------------------------------
        // Decode the barcode image from the byte array
        // ------------------------------------------------------------
        using (var ms = new MemoryStream(barcodeBytes))
        using (var reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
        {
            bool anyFound = false;

            // Iterate through all detected barcodes
            foreach (var result in reader.ReadBarCodes())
            {
                anyFound = true;

                // Output detection details
                Console.WriteLine($"Detected Type : {result.CodeTypeName}");
                Console.WriteLine($"Decoded Text  : {result.CodeText}");
                Console.WriteLine($"Confidence    : {result.Confidence}");
                Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");
                Console.WriteLine();

                // Verify data integrity by comparing with the original text
                if (result.CodeText == originalCodeText)
                {
                    Console.WriteLine("Data integrity check: SUCCESS – decoded text matches the original.");
                }
                else
                {
                    Console.WriteLine("Data integrity check: FAILURE – decoded text does NOT match the original.");
                }
            }

            if (!anyFound)
            {
                Console.WriteLine("No barcode was detected in the provided image.");
            }
        }

        // Clean up temporary image file (optional)
        if (File.Exists(imagePath))
        {
            try
            {
                File.Delete(imagePath);
            }
            catch
            {
                // Ignore any errors during cleanup
            }
        }
    }
}