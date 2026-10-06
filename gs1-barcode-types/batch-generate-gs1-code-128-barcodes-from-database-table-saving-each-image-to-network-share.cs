// Title: Batch generate GS1 Code 128 barcodes and save to network share
// Description: Demonstrates generating GS1 Code 128 barcodes from a list of records and storing each image as a PNG file on a network share or a temporary local folder.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.GS1Code128. Typical scenarios include bulk barcode creation from database tables, configuring visual parameters, and exporting images to various storage locations. Developers often need to automate barcode production for inventory, shipping, or retail labeling workflows.
// Prompt: Batch generate GS1 Code 128 barcodes from a database table, saving each image to a network share.
// Tags: gs1,code128,barcode,generation,batch,output,png,network share,aspose.barcode,aspose.drawing

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch generation of GS1 Code 128 barcodes and saving them as PNG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes for sample records and writes them to a target folder.
    /// </summary>
    static void Main()
    {
        // Simulated database records (replace with real DB access in production)
        List<BarcodeRecord> records = GetSampleRecords();

        // Target folder (network share). Fallback to a temporary local folder if unavailable.
        string targetFolder = @"\\networkshare\Barcodes";
        if (!Directory.Exists(targetFolder))
        {
            // Create a unique temporary directory to avoid collisions
            targetFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(targetFolder);
        }

        // Process each record and generate a barcode image
        foreach (var record in records)
        {
            try
            {
                // Initialize the generator with GS1 Code 128 symbology and the record's data
                using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, record.CodeText))
                {
                    // Configure visual appearance
                    generator.Parameters.Barcode.XDimension.Pixels = 2f;                     // Width of the smallest bar element
                    generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;    // Bar color
                    generator.Parameters.BackColor = Aspose.Drawing.Color.White;          // Background color

                    // Build the full file path for the PNG image
                    string fileName = $"barcode_{record.Id}.png";
                    string filePath = Path.Combine(targetFolder, fileName);

                    // Save the barcode image to disk
                    generator.Save(filePath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Saved barcode ID {record.Id} to {filePath}");
                }
            }
            catch (Exception ex)
            {
                // Log any errors but continue processing remaining records
                Console.WriteLine($"Error generating barcode ID {record.Id}: {ex.Message}");
            }
        }
    }

    // Generates sample records with valid GS1 Code128 data.
    static List<BarcodeRecord> GetSampleRecords()
    {
        // GTIN-14 examples with correct check digits.
        // (01)01234567890128 is a valid GTIN-14.
        var list = new List<BarcodeRecord>();
        for (int i = 0; i < 5; i++)
        {
            // Simple variation to produce distinct GTINs; ensure 14 digits
            string gtin = "0123456789012" + (8 + i).ToString();
            string codeText = $"(01){gtin}";
            list.Add(new BarcodeRecord { Id = i + 1, CodeText = codeText });
        }
        return list;
    }
}

/// <summary>
/// Simple data holder representing a barcode record retrieved from a data source.
/// </summary>
class BarcodeRecord
{
    public int Id { get; set; }
    public string CodeText { get; set; }
}