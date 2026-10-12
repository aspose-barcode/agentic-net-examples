// Title: Batch generation of GS1 Composite barcodes from CSV
// Description: Demonstrates how to read a CSV file and generate a GS1 Composite barcode image for each record using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode creation and recognition category. It shows the use of BarcodeGenerator for GS1 Composite symbology, BarCodeReader for decoding, and related parameter classes. Typical use cases include bulk barcode production for inventory, shipping, or retail labeling where data is stored in CSV files. Developers often need to automate barcode generation and optionally verify the output programmatically.
// Prompt: Create a batch job that reads a CSV file and produces GS1 Composite barcodes for each record.
// Tags: gs1 composite, barcode generation, csv processing, batch, aspose.barcode, png output, barcode reading

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates batch creation of GS1 Composite barcodes from a CSV file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes for each CSV record and optionally reads back the first barcode.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary working folder for this run
        string workFolder = Path.Combine(Path.GetTempPath(), "Gs1CompositeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Prepare a sample CSV file if one does not already exist
        string csvPath = Path.Combine(workFolder, "input.csv");
        if (!File.Exists(csvPath))
        {
            var sampleLines = new List<string>
            {
                "Linear,TwoD",                                 // header
                "(01)01234567890128,(21)ABC123",               // first record
                "(01)01234567890128,(21)XYZ789",               // second record
                "(01)01234567890128,(21)LMN456"                // third record
            };
            File.WriteAllLines(csvPath, sampleLines);
        }

        // Read all lines from the CSV file
        var lines = File.ReadAllLines(csvPath);
        var records = new List<(string Linear, string TwoD)>();

        // Parse CSV records, skipping the header and any empty lines
        for (int i = 0; i < lines.Length; i++)
        {
            if (i == 0) continue; // skip header row
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            var parts = lines[i].Split(',');
            if (parts.Length < 2) continue;

            records.Add((parts[0].Trim(), parts[1].Trim()));
        }

        // Create an output folder for generated barcode images
        string outputFolder = Path.Combine(workFolder, "Barcodes");
        Directory.CreateDirectory(outputFolder);

        // Generate a GS1 Composite barcode for each CSV record
        int index = 1;
        foreach (var rec in records)
        {
            // Combine linear and 2D components using the '|' separator required by GS1 Composite
            string codeText = $"{rec.Linear}|{rec.TwoD}";
            string outPath = Path.Combine(outputFolder, $"barcode_{index}.png");

            using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
            {
                // Set barcode appearance and component types
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;
                generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
                generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_C;
                generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;

                // Save the barcode image as PNG
                generator.Save(outPath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Generated barcode {index}: {outPath}");
            index++;
        }

        // Optional: read back the first generated barcode and display its metadata
        using (var reader = new BarCodeReader(Path.Combine(outputFolder, "barcode_1.png"), DecodeType.GS1CompositeBar))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("Read back barcode 1:");
                Console.WriteLine($"  OneDType: {result.Extended.GS1CompositeBar.OneDType}");
                Console.WriteLine($"  OneDCodeText: {result.Extended.GS1CompositeBar.OneDCodeText}");
                Console.WriteLine($"  TwoDType: {result.Extended.GS1CompositeBar.TwoDType}");
                Console.WriteLine($"  TwoDCodeText: {result.Extended.GS1CompositeBar.TwoDCodeText}");
            }
        }
    }
}