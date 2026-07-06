// Title: Render GS1 Code 128 barcode and store image bytes
// Description: Demonstrates generating a GS1 Code 128 barcode, extracting the PNG image bytes, and showing how to persist them to a database column.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and image handling classes such as Bitmap and ImageFormat. Typical scenarios include creating product barcodes, exporting them as image streams, and saving the binary data to a database for later retrieval. Developers often need to render barcodes, obtain raw bytes, and integrate them with data storage solutions.
// Prompt: Render a GS1 Code 128 barcode, retrieve image bytes, and store them in a database column.
// Tags: gs1 code128, barcode generation, image output, png, database storage, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a GS1 Code 128 barcode,
/// converts it to PNG bytes, and demonstrates how those bytes could be stored in a database.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, extracts image bytes,
    /// and writes the PNG file to disk (simulating database storage).
    /// </summary>
    static void Main()
    {
        // Sample GS1 Code 128 data (AI (01) with a 14‑digit GTIN)
        const string gs1CodeText = "(01)12345678901231";

        // Initialize the barcode generator for GS1 Code 128
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, gs1CodeText))
        {
            // Optional: always show checksum in the human‑readable text
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;

            // Generate the barcode image as a Bitmap object
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save the bitmap to a memory stream to obtain raw PNG bytes
                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    byte[] imageBytes = ms.ToArray();

                    // ---- Substitute for database storage ----
                    // In a real scenario you would store 'imageBytes' into a BLOB column, e.g.:
                    // using (var connection = new SqlConnection(connectionString))
                    // {
                    //     connection.Open();
                    //     using (var command = new SqlCommand("INSERT INTO Barcodes (Image) VALUES (@img)", connection))
                    //     {
                    //         command.Parameters.Add("@img", SqlDbType.VarBinary, imageBytes.Length).Value = imageBytes;
                    //         command.ExecuteNonQuery();
                    //     }
                    // }
                    // -----------------------------------------

                    // For the snippet runner, write the image to a local file instead
                    File.WriteAllBytes("gs1code128.png", imageBytes);
                    Console.WriteLine("Barcode image saved to 'gs1code128.png'.");
                }
            }
        }
    }
}