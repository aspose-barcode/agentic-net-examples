// Title: Generate Swiss Post Parcel Additional Service Code Barcodes with CSV Index
// Description: Creates PNG barcodes for Swiss Post Parcel additional service codes, each labeled with an abbreviation, and writes a CSV file that maps the service code, abbreviation, and file path.
// Category-Description: This example demonstrates how to use Aspose.BarCode to generate barcodes for the Swiss Post Parcel symbology. It covers barcode generation, customizing visual parameters (X‑dimension, bar height, captions), and exporting images. Typical use cases include batch creation of service‑specific barcodes for logistics, shipping labels, and inventory systems. Developers often need to produce a catalog or index of generated barcodes, which this sample provides via a CSV manifest.
// Prompt: Generate a batch of Swiss Post Parcel additional service code barcodes and produce a CSV index linking identifiers.
// Tags: barcode, swisspost, parcel, additional service, csv, generation, aspose.barcode, png

using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch generation of Swiss Post Parcel additional service code barcodes
/// and creation of a CSV index that links each barcode to its identifier and file path.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcodes, saves them as PNG files,
    /// and writes a CSV index file summarizing the output.
    /// </summary>
    static void Main()
    {
        // Define the list of service codes and their human‑readable abbreviations.
        var services = new List<(string Code, string Abbreviation)>
        {
            ("0203", "GAS"),
            ("0322", "RMP"),
            ("0327", "AR"),
            ("0328", "eAR"),
            ("0340", "COD"), // obsolete cash on delivery
            ("0341", "BLN"),
            ("0470", "ID+RMP"),
            ("0610", "CEC"),
            ("1007", "MIL"),
            ("2512", "SAT")
        };

        // Create a unique temporary folder to store generated PNG files and the CSV index.
        string outputFolder = Path.Combine(Path.GetTempPath(), "SwissPostBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Prepare CSV header and collection for lines.
        var csvLines = new List<string>();
        csvLines.Add("ServiceCode,Abbreviation,FilePath");

        // Iterate over each service definition, generate a barcode, and record its details.
        foreach (var service in services)
        {
            // Build the output file name and full path for the current barcode image.
            string fileName = $"{service.Code}_{service.Abbreviation}.png";
            string filePath = Path.Combine(outputFolder, fileName);

            // Configure and generate the barcode using Aspose.BarCode.
            using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, service.Code))
            {
                // Set visual dimensions: X‑dimension (module width) and bar height.
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Parameters.Barcode.BarHeight.Pixels = 40f;

                // Hide the encoded text; we will display a custom caption instead.
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

                // Configure a caption above the barcode that shows the abbreviation.
                generator.Parameters.CaptionAbove.Visible = true;
                generator.Parameters.CaptionAbove.Alignment = TextAlignment.Left;
                generator.Parameters.CaptionAbove.Text = service.Abbreviation;
                generator.Parameters.CaptionAbove.Font.Size.Pixels = 24f;

                // Save the generated barcode as a PNG image.
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Add a line to the CSV index for the current barcode.
            csvLines.Add($"{service.Code},{service.Abbreviation},{filePath}");
        }

        // Write the CSV index file to the output folder using UTF‑8 encoding.
        string csvPath = Path.Combine(outputFolder, "index.csv");
        File.WriteAllLines(csvPath, csvLines, Encoding.UTF8);

        // Output a summary of the operation to the console.
        Console.WriteLine($"Generated {services.Count} barcodes in folder:");
        Console.WriteLine(outputFolder);
        Console.WriteLine("CSV index created at:");
        Console.WriteLine(csvPath);
    }
}