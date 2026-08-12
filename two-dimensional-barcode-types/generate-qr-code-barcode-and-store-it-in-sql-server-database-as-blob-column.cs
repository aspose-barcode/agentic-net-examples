// Title: Generate QR Code and Store as BLOB in SQL Server
// Description: This example creates a QR Code barcode, saves it as a PNG file, and demonstrates how to store the barcode image bytes as a BLOB in a SQL Server database.
// Category-Description: Aspose.BarCode QR Code generation and binary storage examples. Shows how to use BarcodeGenerator with EncodeTypes.QR, configure error correction, save the image to a stream, and insert the byte array into a VARBINARY column using ADO.NET. Useful for developers needing to embed barcodes in databases for later retrieval and printing.
// Prompt: Generate QR Code barcode and store it in SQL Server database as BLOB column.
// Tags: qr code, barcode generation, sql server, blob storage, aspose.barcode, image format, varbinary

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates QR Code generation with Aspose.BarCode and how to persist the resulting image bytes as a BLOB.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR Code, saves it locally, and shows how to store it in a SQL Server BLOB column.
    /// </summary>
    static void Main()
    {
        // Define the content to encode in the QR code.
        string codeText = "https://example.com";

        // Initialize the QR code generator with the desired symbology and content.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Optional: set a high error correction level to improve readability after damage.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Generate the barcode image into a memory stream.
            using (var memoryStream = new MemoryStream())
            {
                // Save the barcode as PNG into the stream.
                generator.Save(memoryStream, BarCodeImageFormat.Png);
                byte[] imageBytes = memoryStream.ToArray();

                // Persist the image to a file for visual verification.
                File.WriteAllBytes("qr_code.png", imageBytes);
                Console.WriteLine("QR code image saved to 'qr_code.png'.");

                // Simulate storing the raw bytes in a database BLOB column by writing to a binary file.
                File.WriteAllBytes("qr_blob.bin", imageBytes);
                Console.WriteLine("QR code bytes written to 'qr_blob.bin' (simulating DB BLOB storage).");

                // -----------------------------------------------------------------
                // Real SQL Server storage (requires System.Data.SqlClient and a valid DB)
                // -----------------------------------------------------------------
                /*
                using (var connection = new System.Data.SqlClient.SqlConnection(
                    "Data Source=YOUR_SERVER;Initial Catalog=YOUR_DATABASE;Integrated Security=True"))
                {
                    connection.Open();
                    using (var command = new System.Data.SqlClient.SqlCommand(
                        "INSERT INTO Barcodes (Id, ImageData) VALUES (@Id, @Image)", connection))
                    {
                        command.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = 1;
                        command.Parameters.Add("@Image", System.Data.SqlDbType.VarBinary, -1).Value = imageBytes;
                        command.ExecuteNonQuery();
                    }
                }
                */
                // Note: The above database code is commented out because the snippet runner
                // does not have access to a SQL Server instance. Replace the connection string
                // and table/column names as appropriate in a real environment.
            }
        }
    }
}