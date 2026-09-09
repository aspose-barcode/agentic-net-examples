// Title: Generate Swiss Post Parcel Additional Service Code Barcodes and CSV Index
// Description: Demonstrates how to generate a batch of Swiss Post Parcel additional service code barcodes using Aspose.BarCode and create a CSV file that maps each service code to its image file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It shows how to use the BarcodeGenerator class with EncodeTypes.SwissPostParcel, configure barcode dimensions, add captions, and save images. Typical use cases include bulk creation of service‑specific barcodes for shipping labels and maintaining an index file for downstream processing. Developers often need to automate barcode batch creation and produce metadata files such as CSV for integration with logistics systems.
// Prompt: Generate a batch of Swiss Post Parcel additional service code barcodes and produce a CSV index linking identifiers.
// Tags: barcode, swisspost, swisspostparcel, csv, generation, aspose.barcode, batch, automation

using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates Swiss Post Parcel additional service code barcodes and writes a CSV index file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a temporary folder, generates barcodes for each service code,
    /// and writes a CSV file that links service codes, abbreviations, and image file names.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch
        string batchFolder = Path.Combine(Path.GetTempPath(), "SwissPostBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Define additional service codes and their abbreviations
        var services = new List<(string Code, string Abbreviation)>
        {
            ("0203", "GAS"),
            ("0322", "RMP"),
            ("0327", "AR"),
            ("0328", "eAR"),
            ("0340", "COD"),
            ("0341", "BLN"),
            ("0470", "ID+RMP"),
            ("0610", "CEC"),
            ("1007", "MIL"),
            ("2512", "SAT")
        };

        // Prepare CSV header
        var csvLines = new List<string>();
        csvLines.Add("ServiceCode,Abbreviation,FileName");

        // Iterate over each service definition and generate its barcode
        foreach (var service in services)
        {
            string fileName = $"SwissPostAdditional_{service.Code}.png";
            string filePath = Path.Combine(batchFolder, fileName);

            // Generate barcode image with specific parameters
            using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, service.Code))
            {
                // Set barcode size
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Parameters.Barcode.BarHeight.Pixels = 40f;
                // Hide the encoded text (service code) on the barcode
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

                // Configure caption (abbreviation) above the barcode
                generator.Parameters.CaptionAbove.Visible = true;
                generator.Parameters.CaptionAbove.Alignment = TextAlignment.Left;
                generator.Parameters.CaptionAbove.Text = service.Abbreviation;
                generator.Parameters.CaptionAbove.Font.Size.Pixels = 24f;
                generator.Parameters.CaptionAbove.Font.Style = FontStyle.Bold;

                // Save the barcode as a PNG file
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Add entry to CSV index
            csvLines.Add($"{service.Code},{service.Abbreviation},{fileName}");
        }

        // Write CSV index file to the batch folder
        string csvPath = Path.Combine(batchFolder, "BarcodeIndex.csv");
        File.WriteAllText(csvPath, string.Join(Environment.NewLine, csvLines), Encoding.UTF8);

        // Output locations for verification
        Console.WriteLine("Barcodes generated in: " + batchFolder);
        Console.WriteLine("CSV index file: " + csvPath);
    }
}