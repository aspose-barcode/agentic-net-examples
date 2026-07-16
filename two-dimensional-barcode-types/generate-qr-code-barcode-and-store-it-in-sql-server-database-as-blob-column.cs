// Title: Generate QR Code and store as BLOB in SQL Server
// Description: Demonstrates creating a QR Code barcode using Aspose.BarCode, retrieving its PNG bytes, and persisting them to a SQL Server VARBINARY(MAX) column.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class with QR symbology, configure error correction, and obtain image data for storage. Developers often need to embed generated barcodes into databases for later retrieval in reporting or mobile scanning scenarios.
// Prompt: Generate QR Code barcode and store it in SQL Server database as BLOB column.
// Tags: qr code, barcode generation, sql server, blob, aspose.barcode, png, varbinary

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code barcode, extracts its PNG binary data,
/// and demonstrates how to store that data in a SQL Server BLOB column.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the QR Code, optionally saves it to a database,
    /// and writes the image to a local file as a fallback.
    /// </summary>
    static void Main()
    {
        // Text that will be encoded into the QR Code.
        const string qrText = "Hello World";

        // Generate QR Code image and capture its binary representation.
        byte[] qrImageBytes;
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
        {
            // Configure a high error correction level (Level H) for better resilience.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the generated barcode directly to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                qrImageBytes = ms.ToArray();
            }
        }

        // -----------------------------------------------------------------
        // Real implementation: store qrImageBytes into a SQL Server BLOB column
        // -----------------------------------------------------------------
        /*
        // Uncomment and add a reference to System.Data.SqlClient (or Microsoft.Data.SqlClient)
        // Ensure a valid connection string and a table with a VARBINARY(MAX) column exist.

        const string connectionString = "Server=YOUR_SERVER;Database=YOUR_DB;Trusted_Connection=True;";
        const string insertSql = "INSERT INTO QrCodes (Id, ImageData) VALUES (@Id, @Image)";

        using (var connection = new SqlConnection(connectionString))
        using (var command = new SqlCommand(insertSql, connection))
        {
            command.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = 1; // example Id
            command.Parameters.Add("@Image", System.Data.SqlDbType.VarBinary, -1).Value = qrImageBytes;

            connection.Open();
            command.ExecuteNonQuery();
        }
        */

        // Fallback for environments without SQL Server: write the image to a local file.
        const string outputPath = "qr.png";
        File.WriteAllBytes(outputPath, qrImageBytes);
        Console.WriteLine($"QR Code image saved to '{outputPath}'.");
    }
}