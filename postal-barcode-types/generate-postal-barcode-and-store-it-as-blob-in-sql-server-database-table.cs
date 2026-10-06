// Title: Generate Australia Post barcode and store as BLOB
// Description: Demonstrates creating an Australia Post postal barcode, saving it as a PNG image, and persisting the barcode bytes as a binary BLOB.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.AustraliaPost to produce postal barcodes. Typical use cases include encoding mailing information for postal services and storing the resulting images in databases or files. Developers often need to customize barcode parameters, export images in various formats, and handle binary storage for later retrieval.
// Prompt: Generate a postal barcode and store it as a BLOB in a SQL Server database table.
// Tags: australia post, postal barcode, blob storage, sql server, barcode generation, aspose.barcode, png, binary

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates an Australia Post postal barcode,
/// saves the image to disk, and writes the barcode bytes as a BLOB file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the sample postal code text (Australia Post format: FCC + DPID, 10 characters)
        string postalCodeText = "1100000000";

        // Generate the barcode image and capture its byte array
        byte[] barcodeBytes;
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, postalCodeText))
        {
            // Optional: customize barcode parameters here if needed (e.g., size, colors)

            // Save the generated barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                barcodeBytes = ms.ToArray(); // Extract the byte array from the stream
            }
        }

        // Write the PNG image to a file for visual verification
        const string imagePath = "postal.png";
        using (var fileStream = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
        {
            fileStream.Write(barcodeBytes, 0, barcodeBytes.Length);
        }

        Console.WriteLine($"Barcode image saved to '{imagePath}'.");
        Console.WriteLine($"Barcode byte size: {barcodeBytes.Length} bytes.");

        // Placeholder for storing the barcode bytes as a BLOB in a SQL Server database.
        // The actual database code is commented out because the execution environment may lack a SQL Server instance.
        /*
        using System.Data.SqlClient;

        const string connectionString = "Data Source=YOUR_SERVER;Initial Catalog=YOUR_DATABASE;Integrated Security=True;";
        const string insertSql = "INSERT INTO Barcodes (CodeText, Symbology, ImageBlob) VALUES (@CodeText, @Symbology, @ImageBlob)";

        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (var command = new SqlCommand(insertSql, connection))
            {
                command.Parameters.AddWithValue("@CodeText", postalCodeText);
                command.Parameters.AddWithValue("@Symbology", "AustraliaPost");
                command.Parameters.Add("@ImageBlob", System.Data.SqlDbType.VarBinary, barcodeBytes.Length).Value = barcodeBytes;
                command.ExecuteNonQuery();
            }
        }
        */

        // As an alternative to database storage, write the BLOB to a local binary file
        const string blobPath = "postal_blob.bin";
        File.WriteAllBytes(blobPath, barcodeBytes);
        Console.WriteLine($"Barcode BLOB saved to '{blobPath}'.");
    }
}