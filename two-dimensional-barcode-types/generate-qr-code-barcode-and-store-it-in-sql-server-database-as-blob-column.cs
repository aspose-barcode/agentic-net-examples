// Title: Generate QR Code and Store as BLOB in SQL Server
// Description: This example creates a QR Code barcode, saves it as a PNG image in memory, and demonstrates how to store the image bytes as a BLOB in a SQL Server table.
// Category-Description: Aspose.BarCode QR Code generation and binary data persistence. The example uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce a QR Code, then shows how to insert the resulting byte array into a VARBINARY column via ADO.NET. Developers working with barcode imaging and database storage can adapt this pattern for other symbologies and databases.
// Prompt: Generate QR Code barcode and store it in SQL Server database as BLOB column.
// Tags: qr code, barcode generation, sql server, blob, aspose.barcode, image storage, varbinary

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates QR Code generation with Aspose.BarCode and how to persist the image as a BLOB in SQL Server.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR Code, saves it to a file, and contains a commented-out
    /// ADO.NET snippet for inserting the image bytes into a SQL Server table.
    /// </summary>
    static void Main()
    {
        // Text to encode in the QR Code.
        string codeText = "Hello, Aspose QR!";

        // Initialize the barcode generator for QR symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Set QR-specific parameters.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;               // Size of a single module.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM; // Error correction level.

            // Render the barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] imageBytes = ms.ToArray(); // Convert stream to byte array.

                // Write the PNG image to a local file for verification.
                string filePath = "qr_blob.bin";
                File.WriteAllBytes(filePath, imageBytes);
                Console.WriteLine($"QR code saved to file '{filePath}' ({imageBytes.Length} bytes).");

                // Example of inserting the image bytes into a SQL Server VARBINARY column.
                // Uncomment and adjust the connection string and table schema as needed.
                /*
                string connectionString = "Data Source=SERVER;Initial Catalog=YourDatabase;Integrated Security=True;";
                using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO Barcodes (CodeText, Symbology, ImageData) VALUES (@codeText, @symbology, @image)";
                    using (var command = new System.Data.SqlClient.SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@codeText", codeText);
                        command.Parameters.AddWithValue("@symbology", "QR");
                        command.Parameters.Add("@image", System.Data.SqlDbType.VarBinary, imageBytes.Length).Value = imageBytes;
                        command.ExecuteNonQuery();
                    }
                }
                */
            }
        }
    }
}