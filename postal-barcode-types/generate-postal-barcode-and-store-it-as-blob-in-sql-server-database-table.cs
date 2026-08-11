// Title: Generate Australia Post Barcode and Save as BLOB
// Description: Demonstrates how to create an Australia Post barcode using Aspose.BarCode, convert it to a PNG byte array, and store it as a binary BLOB (simulated via a file).
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to configure barcode parameters, render the barcode to an image stream, and obtain raw byte data for storage. Typical use cases include persisting barcodes in databases, transmitting them over networks, or embedding them in documents. Developers often work with BarcodeGenerator, EncodeTypes, and image format classes to produce and handle barcode images programmatically.
// Prompt: Generate a postal barcode and store it as a BLOB in a SQL Server database table.
// Tags: barcode, australia post, generation, blob, sql server, aspose.barcode, png, memorystream

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates an Australia Post barcode and saves it as a binary BLOB.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Initiates barcode generation and storage.
    /// </summary>
    static void Main()
    {
        // Generate an Australia Post barcode and store it as a BLOB.
        GenerateAndStoreBarcode();
    }

    /// <summary>
    /// Creates a barcode, converts it to a PNG byte array, and writes the bytes to a file
    /// to simulate storing the data in a SQL Server BLOB column.
    /// </summary>
    static void GenerateAndStoreBarcode()
    {
        // Sample valid Australia Post code text (FCC=59, DPID=8 digits, 2 CTable chars)
        const string codeText = "5980123456AB";

        // Initialize the barcode generator for the Australia Post symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
        {
            // Configure the encoding table to use the CTable customer information interpreting type.
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;

            // Render the barcode image into a memory stream in PNG format.
            using (var memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);
                byte[] barcodeBytes = memoryStream.ToArray();

                // Simulate storing the byte array as a BLOB by writing it to a binary file.
                const string blobFilePath = "barcode_blob.bin";
                File.WriteAllBytes(blobFilePath, barcodeBytes);
                Console.WriteLine($"Barcode BLOB saved to '{blobFilePath}' ({barcodeBytes.Length} bytes).");

                // Optionally save the PNG image for visual verification.
                const string imageFilePath = "barcode.png";
                File.WriteAllBytes(imageFilePath, barcodeBytes);
                Console.WriteLine($"Barcode image saved to '{imageFilePath}'.");
            }
        }

        /*
        // Real SQL Server implementation (requires System.Data.SqlClient package and a reachable DB):
        // string connectionString = "Data Source=SERVER;Initial Catalog=Database;Integrated Security=True;";
        // using (var connection = new SqlConnection(connectionString))
        // {
        //     connection.Open();
        //     string insertSql = "INSERT INTO Barcodes (Id, ImageBlob) VALUES (@Id, @Blob)";
        //     using (var command = new SqlCommand(insertSql, connection))
        //     {
        //         command.Parameters.Add("@Id", SqlDbType.Int).Value = 1;
        //         command.Parameters.Add("@Blob", SqlDbType.VarBinary, -1).Value = barcodeBytes;
        //         command.ExecuteNonQuery();
        //     }
        // }
        // Note: The above database code is commented out because the snippet runner does not have
        // the necessary database libraries or environment to execute it.
        */
    }
}