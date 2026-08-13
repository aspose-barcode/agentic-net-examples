// Title: Batch generate Code128 barcodes from identifiers and save as JPEG files
// Description: Demonstrates how to generate a series of Code128 barcodes using Aspose.BarCode, assigning each record's identifier as the CodeText and storing the images as JPEG files.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating batch creation of barcodes from a data source. It showcases the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce images suitable for printing, labeling, or digital distribution. Developers often need to generate many barcodes programmatically—for inventory, shipping, or ticketing—by iterating over database records or other collections.
// Prompt: Batch generate barcodes from a database query, using each record’s identifier as CodeText and saving as JPEG.
// Tags: barcode symbology, batch generation, jpeg output, aspose.barcode, code128, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates batch barcode generation using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes for a set of identifiers and saves them as JPEG images.
    /// </summary>
    static void Main()
    {
        // Define the output folder for generated barcode images
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputFolder))
        {
            // Create the folder if it does not already exist
            Directory.CreateDirectory(outputFolder);
        }

        // -----------------------------------------------------------------
        // NOTE: In a real scenario you would retrieve identifiers from a
        // database using ADO.NET, Entity Framework, Dapper, etc.
        // Example (pseudo‑code):
        //   using (var connection = new SqlConnection(connectionString))
        //   {
        //       connection.Open();
        //       var ids = connection.Query<string>("SELECT Identifier FROM MyTable");
        //       foreach (var id in ids) { GenerateBarcode(id, outputFolder); }
        //   }
        // The required database packages are not available in the snippet runner,
        // so we substitute with a local sample collection.
        // -----------------------------------------------------------------

        // Sample identifiers to simulate database records
        string[] sampleIds = new string[]
        {
            "ID001",
            "ID002",
            "ID003",
            "ID004",
            "ID005"
        };

        // Generate a barcode for each identifier
        foreach (string id in sampleIds)
        {
            GenerateBarcode(id, outputFolder);
        }

        Console.WriteLine("Barcode generation completed.");
    }

    /// <summary>
    /// Generates a Code128 barcode image for the specified text and saves it as a JPEG file.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode (e.g., a database identifier).</param>
    /// <param name="outputFolder">The folder where the JPEG image will be saved.</param>
    static void GenerateBarcode(string codeText, string outputFolder)
    {
        // Build the full file path for the JPEG image
        string filePath = Path.Combine(outputFolder, $"{codeText}.jpg");

        // Create a BarcodeGenerator for Code128 symbology with the given code text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Save the barcode image as JPEG
            generator.Save(filePath, BarCodeImageFormat.Jpeg);
        }

        Console.WriteLine($"Saved barcode for '{codeText}' to '{filePath}'.");
    }
}