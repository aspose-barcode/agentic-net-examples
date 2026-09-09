// Title: Render GS1 Code 128 barcode and store image bytes as Base64 in JSON
// Description: This example generates a GS1 Code 128 barcode, extracts the PNG image bytes, converts them to a Base64 string, and saves the data to a JSON file that simulates a database column.
// Category-Description: Aspose.BarCode barcode generation examples demonstrate how to create various symbologies, configure rendering options, and retrieve image data for storage or transmission. Typical use cases include product labeling, inventory systems, and integration with databases. Developers often need to generate barcodes, obtain raw image bytes, and persist them in a format such as Base64 for database storage.
// Prompt: Render a GS1 Code 128 barcode, retrieve image bytes, and store them in a database column.
// Tags: barcode, gs1code128, generation, image, png, base64, aspose.barcode, json, csharp

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a GS1 Code 128 barcode, extracting its image bytes,
/// converting them to Base64, and persisting the result in a JSON file (simulating a DB column).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, processes the image,
    /// and writes the record to a JSON file.
    /// </summary>
    static void Main()
    {
        // Define the GS1 Code 128 data string (includes Application Identifiers)
        string codeText = "(01)12345678901231(21)ASPOSE";

        // Generate the barcode and capture the PNG image bytes in memory
        byte[] imageBytes;
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
        {
            // Optional: increase X dimension for better readability on the rendered image
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Allow the generator to accept GS1 formatting without throwing an exception
            generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

            // Save the barcode to a memory stream in PNG format and retrieve the byte array
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                imageBytes = ms.ToArray();
            }
        }

        // Simulate persisting the barcode record in a database by writing to a JSON file
        var record = new BarcodeRecord
        {
            Id = 1,
            CodeText = codeText,
            Symbology = "GS1Code128",
            ImageBase64 = Convert.ToBase64String(imageBytes)
        };

        // Serialize the record with indentation for readability
        string json = JsonSerializer.Serialize(record, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText("barcode_record.json", json);

        Console.WriteLine("Barcode generated and stored in 'barcode_record.json'.");
    }
}

/// <summary>
/// Simple data model representing a barcode record that could be stored in a database.
/// </summary>
class BarcodeRecord
{
    public int Id { get; set; }
    public string CodeText { get; set; }
    public string Symbology { get; set; }
    public string ImageBase64 { get; set; }
}