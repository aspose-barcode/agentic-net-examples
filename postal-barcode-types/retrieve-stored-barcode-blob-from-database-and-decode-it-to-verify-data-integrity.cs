using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.BarCodeRecognition; // For ChecksumValidation enum

/// <summary>
/// Demonstrates reading a barcode image from a file (or database BLOB) and decoding it using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Reads a barcode image, decodes it, and prints barcode details to the console.
    /// </summary>
    static void Main()
    {
        // NOTE: In a real scenario the barcode image bytes would be retrieved from a database BLOB.
        // Example (commented out because the required database packages are not available in this runner):
        // byte[] barcodeBytes;
        // using (var connection = new SqlConnection("your-connection-string"))
        // {
        //     connection.Open();
        //     using (var command = new SqlCommand("SELECT BarcodeImage FROM Barcodes WHERE Id = @id", connection))
        //     {
        //         command.Parameters.AddWithValue("@id", 1);
        //         barcodeBytes = (byte[])command.ExecuteScalar();
        //     }
        // }

        // For the runnable example we read the barcode image from a local file.
        string imagePath = "barcode.png";

        // Verify that the image file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Read the image bytes (simulating the BLOB retrieved from the database).
        byte[] barcodeBytes = File.ReadAllBytes(imagePath);

        // Decode the barcode from the byte array using a memory stream.
        using (var ms = new MemoryStream(barcodeBytes))
        using (var reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
        {
            // Enable checksum validation to verify data integrity (if applicable).
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Perform the barcode reading operation.
            BarCodeResult[] results = reader.ReadBarCodes();

            // If no barcodes were detected, inform the user and exit.
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
                return;
            }

            // Iterate through each detected barcode and display its details.
            foreach (var result in results)
            {
                Console.WriteLine($"Type: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"Confidence: {result.Confidence}");
                Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");
                Console.WriteLine($"Region Angle: {result.Region.Angle}");
                Console.WriteLine();
            }
        }
    }
}