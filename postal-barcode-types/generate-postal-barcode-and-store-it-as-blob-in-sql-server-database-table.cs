// Title: Generate Australia Post barcode and store as BLOB
// Description: Demonstrates creating an Australia Post (FCC 11) barcode, converting it to a PNG byte array, and persisting the image as a BLOB (simulated via file storage).
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator to produce postal symbologies, retrieve raw image bytes, and handle binary data for database storage. Typical use cases include printing postal labels, archiving barcode images, and integrating barcode generation with SQL Server BLOB columns. Developers often need to convert generated barcodes to byte arrays for insertion into VARBINARY fields, and this snippet illustrates that workflow.
// Prompt: Generate a postal barcode and store it as a BLOB in a SQL Server database table.
// Tags: australia post,postal barcode,blob storage,sql server,aspose.barcode,generation,png,byte array

using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates an Australia Post barcode and demonstrates how to store the image as a BLOB.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, writes the image bytes to a file (simulating a BLOB), and creates a mock JSON record.
    /// </summary>
    static void Main()
    {
        // Define the sample Australia Post barcode (FCC 11, no customer info)
        string codeText = "1100000000";
        BaseEncodeType encodeType = EncodeTypes.AustraliaPost;

        // Create a BarcodeGenerator instance for the specified symbology and data
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Optional: set encoding table if needed (default is NTable)
            // generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.NTable;

            // Render the barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                // Simulate storing the barcode image as a BLOB in a database by writing to a local file
                string blobPath = "barcode_blob.bin";
                File.WriteAllBytes(blobPath, imageBytes);
                Console.WriteLine($"Barcode image saved to {blobPath} ({imageBytes.Length} bytes).");

                // Create a simple record to represent a database row
                var record = new
                {
                    Id = 1,
                    CodeText = codeText,
                    Symbology = encodeType.GetType().Name, // for illustration
                    ImageData = Convert.ToBase64String(imageBytes)
                };

                // Serialize the mock record collection to a JSON file (acts as a mock table)
                string jsonPath = "barcode_records.json";
                var records = new List<object> { record };
                string json = JsonSerializer.Serialize(records, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(jsonPath, json);
                Console.WriteLine($"Record saved to {jsonPath}.");

                // Real SQL Server implementation (commented out - requires System.Data.SqlClient)
                /*
                using (var connection = new System.Data.SqlClient.SqlConnection("Data Source=SERVER;Initial Catalog=BarcodesDb;Integrated Security=True"))
                {
                    connection.Open();
                    string insertSql = "INSERT INTO Barcodes (CodeText, Symbology, ImageData) VALUES (@CodeText, @Symbology, @ImageData)";
                    using (var command = new System.Data.SqlClient.SqlCommand(insertSql, connection))
                    {
                        command.Parameters.AddWithValue("@CodeText", codeText);
                        command.Parameters.AddWithValue("@Symbology", "AustraliaPost");
                        command.Parameters.Add("@ImageData", System.Data.SqlDbType.VarBinary, imageBytes.Length).Value = imageBytes;
                        command.ExecuteNonQuery();
                    }
                }
                */
            }
        }
    }
}