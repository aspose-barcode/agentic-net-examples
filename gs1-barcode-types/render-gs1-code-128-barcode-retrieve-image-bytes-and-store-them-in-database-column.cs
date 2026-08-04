// Title: Render GS1 Code 128 barcode and save image bytes
// Description: Demonstrates generating a GS1 Code 128 barcode, extracting the PNG image bytes, and persisting them (illustrated by writing to a file, with placeholder code for database storage).
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the use of BarcodeGenerator with EncodeTypes.GS1Code128. It covers configuring barcode appearance, rendering to a memory stream, and handling raw image bytes—common tasks for developers integrating barcodes into databases, web services, or file systems. Typical use cases include inventory labeling, product tracking, and POS systems where GS1 standards are required.
// Prompt: Render a GS1 Code 128 barcode, retrieve image bytes, and store them in a database column.
// Tags: gs1, code128, barcode generation, image bytes, aspnet, aspose.barcode, png, database storage

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a GS1 Code 128 barcode, obtaining its PNG bytes,
/// and persisting the image (example writes to file; database insertion code is provided as comment).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, captures image bytes, and saves them.
    /// </summary>
    static void Main()
    {
        // Define the GS1 Code 128 text. AI (01) requires exactly 14 digits.
        string gs1Code128Text = "(01)00123456789012";

        // Generate the barcode and retrieve the image bytes.
        byte[] barcodeBytes;
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, gs1Code128Text))
        {
            // Optional: customize barcode and background colors.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Render the barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                barcodeBytes = ms.ToArray(); // Extract the raw PNG bytes.
            }
        }

        // Placeholder for database storage: insert 'barcodeBytes' into a BLOB column.
        // Example (commented out, requires a database library such as Microsoft.Data.Sqlite):
        /*
        using var connection = new SqliteConnection("Data Source=Barcodes.db");
        connection.Open();
        using var cmd = connection.CreateCommand();
        cmd.CommandText = "CREATE TABLE IF NOT EXISTS Barcodes(Id INTEGER PRIMARY KEY AUTOINCREMENT, Image BLOB)";
        cmd.ExecuteNonQuery();

        cmd.CommandText = "INSERT INTO Barcodes(Image) VALUES (@img)";
        cmd.Parameters.Add("@img", SqliteType.Blob).Value = barcodeBytes;
        cmd.ExecuteNonQuery();
        */

        // For this runnable example, write the PNG file to disk.
        string outputPath = "gs1_code128.png";
        File.WriteAllBytes(outputPath, barcodeBytes);
        Console.WriteLine($"GS1 Code 128 barcode saved to '{Path.GetFullPath(outputPath)}'.");
    }
}