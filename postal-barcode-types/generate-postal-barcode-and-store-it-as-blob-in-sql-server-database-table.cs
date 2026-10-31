// Title: Generate Postnet barcode and store as BLOB
// Description: Creates a Postnet postal barcode, saves it to a binary file representing a BLOB, and demonstrates how to insert it into a SQL Server table.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to generate postal symbologies (Postnet) and handle the resulting image as binary data for storage in databases. It uses BarcodeGenerator, EncodeTypes, BarCodeImageFormat, and shows typical use cases such as persisting barcode images in SQL Server. Developers working with barcode generation and data persistence can reference this pattern.
// Prompt: Generate a postal barcode and store it as a BLOB in a SQL Server database table.
// Tags: postnet, barcode generation, sql server, blob, image, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Postnet barcode, converting it to a byte array,
/// and persisting it as a BLOB (binary large object) for database storage.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, saves it as a binary file,
    /// and includes a commented-out sample for inserting the BLOB into SQL Server.
    /// </summary>
    static void Main()
    {
        // The text to encode in the barcode.
        string codeText = "123456789";

        // Initialize the barcode generator with Postnet symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, codeText))
        {
            // Adjust the short bar height for better visual appearance.
            generator.Parameters.Barcode.Postal.ShortBarHeight.Point = 5f;

            // Use a memory stream to capture the generated image.
            using (var ms = new MemoryStream())
            {
                // Save the barcode image to the memory stream in PNG format.
                generator.Save(ms, BarCodeImageFormat.Png);

                // Convert the stream contents to a byte array (BLOB).
                byte[] imageBytes = ms.ToArray();

                // Write the BLOB to a file for demonstration purposes.
                File.WriteAllBytes("barcode_blob.bin", imageBytes);
                Console.WriteLine("Barcode generated and saved as BLOB to 'barcode_blob.bin'.");

                // Example of inserting the BLOB into a SQL Server table (commented out).
                /*
                using (var connection = new SqlConnection("Data Source=SERVER;Initial Catalog=Database;Integrated Security=True"))
                {
                    connection.Open();
                    using (var command = new SqlCommand(
                        "INSERT INTO Barcodes (CodeText, Symbology, ImageData) VALUES (@code, @sym, @img)", connection))
                    {
                        command.Parameters.AddWithValue("@code", codeText);
                        command.Parameters.AddWithValue("@sym", "Postnet");
                        command.Parameters.Add("@img", SqlDbType.VarBinary, imageBytes.Length).Value = imageBytes;
                        command.ExecuteNonQuery();
                    }
                }
                */
            }
        }
    }
}