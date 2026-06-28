using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a QR Code barcode, saving it to a file,
/// and (optionally) storing the image bytes in a SQL Server database.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR Code, writes it to a PNG file, and contains
    /// commented-out code for persisting the image to a database.
    /// </summary>
    static void Main()
    {
        // -------------------------------------------------------------
        // Generate QR Code barcode and obtain its image bytes
        // -------------------------------------------------------------
        byte[] barcodeBytes;
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Optional: set error correction level to Medium (Level M)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Write the generated barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                barcodeBytes = ms.ToArray(); // Capture the image bytes
            }
        }

        // -------------------------------------------------------------
        // Save the barcode image to a local file as a fallback
        // (useful when a SQL Server instance is not available)
        // -------------------------------------------------------------
        const string fallbackPath = "qr_code.png";
        File.WriteAllBytes(fallbackPath, barcodeBytes);
        Console.WriteLine($"QR Code image saved to {fallbackPath}");

        // -------------------------------------------------------------------------
        // Real implementation: store the barcode image bytes in a SQL Server BLOB column
        // -------------------------------------------------------------------------
        /*
        // NOTE: The following code requires a reachable SQL Server instance and the
        // appropriate table (e.g., Barcodes(Id UNIQUEIDENTIFIER, ImageData VARBINARY(MAX))).
        // Replace the connection string with your actual database details.

        string connectionString = "Data Source=YOUR_SERVER;Initial Catalog=YOUR_DATABASE;Integrated Security=True";

        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            // Insert a new row with a generated GUID and the barcode image data
            string insertSql = "INSERT INTO Barcodes (Id, ImageData) VALUES (@Id, @Image)";
            using (var command = new SqlCommand(insertSql, connection))
            {
                command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = Guid.NewGuid();
                command.Parameters.Add("@Image", SqlDbType.VarBinary, -1).Value = barcodeBytes;

                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine($"{rowsAffected} row(s) inserted into the database.");
            }
        }
        */
    }
}