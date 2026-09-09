// Title: Generate QR Code and store as BLOB
// Description: Demonstrates creating a QR Code image using Aspose.BarCode and saving the resulting PNG bytes as a binary BLOB, which can be persisted to a SQL Server column.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation and binary data handling for database storage. It showcases the BarcodeGenerator class, EncodeTypes enumeration, and image export to a MemoryStream, which are commonly used by developers needing to embed barcodes in databases or other binary storage systems.
// Prompt: Generate QR Code barcode and store it in SQL Server database as BLOB column.
// Tags: qr code, barcode generation, sql server, blob, aspose.barcode, image export, binary storage

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code barcode and saving its image bytes as a binary BLOB,
/// suitable for storage in a SQL Server database.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR Code, writes the image bytes to a temporary file,
    /// and includes sample code for inserting the bytes into a SQL Server BLOB column.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the QR code.
        string qrText = "Hello, Aspose QR!";

        // Generate QR code image bytes using the helper method.
        byte[] barcodeBytes = GenerateQrCode(qrText);

        // Store the bytes locally as a stand‑in for a SQL Server BLOB column.
        string outputPath = Path.Combine(Path.GetTempPath(), "qr_blob.bin");
        File.WriteAllBytes(outputPath, barcodeBytes);
        Console.WriteLine($"QR code image saved as binary blob to: {outputPath}");

        // -----------------------------------------------------------------
        // Real SQL Server storage (requires System.Data.SqlClient and a
        // reachable SQL Server instance). This code is commented out because
        // the execution environment does not provide a database.
        /*
        using (var connection = new System.Data.SqlClient.SqlConnection(
            "Data Source=YOUR_SERVER;Initial Catalog=YOUR_DB;Integrated Security=True"))
        {
            connection.Open();
            string insertSql = "INSERT INTO Barcodes (CodeImage) VALUES (@Image)";
            using (var command = new System.Data.SqlClient.SqlCommand(insertSql, connection))
            {
                command.Parameters.Add("@Image", System.Data.SqlDbType.VarBinary, barcodeBytes.Length)
                                  .Value = barcodeBytes;
                command.ExecuteNonQuery();
            }
        }
        */
        // -----------------------------------------------------------------
    }

    /// <summary>
    /// Generates a QR Code image for the specified text and returns the image bytes in PNG format.
    /// </summary>
    /// <param name="text">The text to encode in the QR Code.</param>
    /// <returns>Byte array containing the PNG image of the generated QR Code.</returns>
    static byte[] GenerateQrCode(string text)
    {
        // Initialize the barcode generator with QR encoding and the provided text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, text))
        {
            // Optional: adjust module size (pixel dimension) and error correction level.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                // Return the stream contents as a byte array.
                return ms.ToArray();
            }
        }
    }
}