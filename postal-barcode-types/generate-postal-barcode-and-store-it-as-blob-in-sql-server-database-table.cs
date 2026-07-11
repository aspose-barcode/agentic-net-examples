// Title: Generate and store a Postnet barcode as a PNG BLOB
// Description: Demonstrates creating a Postnet postal barcode, converting it to PNG, and showing how to store it as a BLOB in SQL Server.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, BarcodeParameters, and image handling classes. Typical use cases include generating postal barcodes for mailing systems and persisting them in databases. Developers often need to create barcode images, convert them to byte arrays, and insert them into SQL Server tables as VARBINARY data.
// Prompt: Generate a postal barcode and store it as a BLOB in a SQL Server database table.
// Tags: postnet, postal barcode, barcode generation, image conversion, sql server, blob, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Postnet postal barcode,
/// converts it to a PNG byte array, and demonstrates how it could be stored
/// as a BLOB in a SQL Server database.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Initialize the barcode generator for the Postnet symbology with the data "12345".
        using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, "12345"))
        {
            // Set a short bar height specific to postal barcodes (5 points).
            generator.Parameters.Barcode.Postal.PostalShortBarHeight.Point = 5f;

            // Generate the barcode image as a Bitmap.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Prepare a memory stream to hold the PNG representation.
                using (var ms = new MemoryStream())
                {
                    // Save the bitmap to the memory stream in PNG format.
                    bitmap.Save(ms, ImageFormat.Png);
                    byte[] imageBytes = ms.ToArray(); // Convert stream to byte array.

                    // -----------------------------------------------------------------
                    // Example of inserting the PNG byte array into a SQL Server table:
                    // -----------------------------------------------------------------
                    // using (var connection = new SqlConnection("your_connection_string"))
                    // {
                    //     connection.Open();
                    //     using (var command = new SqlCommand(
                    //         "INSERT INTO Barcodes (Id, Image) VALUES (@Id, @Image)", connection))
                    //     {
                    //         command.Parameters.Add("@Id", SqlDbType.Int).Value = 1;
                    //         command.Parameters.Add("@Image", SqlDbType.VarBinary).Value = imageBytes;
                    //         command.ExecuteNonQuery();
                    //     }
                    // }
                    // -----------------------------------------------------------------
                    // Since the execution environment may lack SQL Server libraries,
                    // write the PNG file locally for demonstration purposes.

                    File.WriteAllBytes("postal_barcode.png", imageBytes);
                    Console.WriteLine("Postal barcode generated and saved to postal_barcode.png");
                }
            }
        }
    }
}