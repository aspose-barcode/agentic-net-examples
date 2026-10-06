// Title: Render GS1 Code 128 barcode and store image bytes
// Description: Demonstrates generating a GS1 Code 128 barcode, extracting the PNG image bytes, and persisting them (simulated) as a varbinary column.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Typical use cases include creating product identification barcodes for inventory systems and saving the resulting images to databases. Developers often need to adjust barcode parameters, render to memory, and handle binary storage for later retrieval.
// Prompt: Render a GS1 Code 128 barcode, retrieve image bytes, and store them in a database column.
// Tags: gs1 code128, barcode generation, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a GS1 Code 128 barcode, retrieving its image bytes,
/// and storing them (simulated) as a binary column.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, saves the image to a file, and shows how to store the bytes.
    /// </summary>
    static void Main()
    {
        // Define the GS1 Code 128 data: (01) AI followed by a 14‑digit GTIN.
        string codeText = "(01)12345678901231";

        // Create a BarcodeGenerator for GS1 Code 128.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
        {
            // Optional: set the module (X) dimension to control image size.
            generator.Parameters.Barcode.XDimension.Pixels = 3f;

            // Render the barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                // Simulate storing the image bytes in a database by writing to a file.
                string imagePath = "gs1code128.png";
                File.WriteAllBytes(imagePath, imageBytes);
                Console.WriteLine($"Barcode generated and saved to '{imagePath}'. Bytes: {imageBytes.Length}");

                // Example code for real database storage (commented out):
                // using var connection = new SqlConnection("your-connection-string");
                // connection.Open();
                // using var command = new SqlCommand("INSERT INTO Barcodes (CodeText, Symbology, Image) VALUES (@ct, @sym, @img)", connection);
                // command.Parameters.AddWithValue("@ct", codeText);
                // command.Parameters.AddWithValue("@sym", "GS1Code128");
                // command.Parameters.Add("@img", SqlDbType.VarBinary, imageBytes.Length).Value = imageBytes;
                // command.ExecuteNonQuery();
            }
        }
    }
}