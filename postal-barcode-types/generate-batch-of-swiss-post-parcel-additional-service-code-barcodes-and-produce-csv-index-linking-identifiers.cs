// Title: Generate Swiss Post Parcel Additional Service Code Barcodes with CSV Index
// Description: Creates PNG barcodes for Swiss Post parcel additional service codes and writes a CSV file linking each code to its image file.
// Category-Description: This example demonstrates how to use Aspose.BarCode to generate Swiss Post Parcel barcodes (EncodeTypes.SwissPostParcel). It shows configuring barcode dimensions, hiding the code text, adding a caption, and saving images. Typical use cases include logistics and shipping label creation where additional service identifiers must be encoded and indexed. Developers often need to batch‑process multiple service codes and produce a reference file for downstream systems.
// Prompt: Generate a batch of Swiss Post Parcel additional service code barcodes and produce a CSV index linking identifiers.
// Tags: barcode, swisspost, parcel, additional-service, csv, generation, aspose.barcode, png

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch generation of Swiss Post Parcel additional service code barcodes
/// and creation of a CSV index that maps each service code to its image file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcode images and a CSV index in a temporary folder.
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
            ("0340", "COD"),
            ("0341", "BLN"),
            ("0470", "ID+RMP"),
            ("0610", "CEC"),
            ("1007", "MIL"),
            ("2512", "SAT")
        };

        // Create a unique temporary output folder for the generated files.
        string outputFolder = Path.Combine(Path.GetTempPath(), "SwissPostAdditional_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Prepare the CSV index file.
        string csvPath = Path.Combine(outputFolder, "index.csv");
        using (var csvWriter = new StreamWriter(csvPath, false))
        {
            // Write CSV header.
            csvWriter.WriteLine("ServiceCode,Abbreviation,FileName");

            // Iterate over each service definition and generate its barcode.
            foreach (var service in services)
            {
                // Determine file name and full path for the barcode image.
                string fileName = $"SwissPost_{service.Code}.png";
                string filePath = Path.Combine(outputFolder, fileName);

                // Create a barcode generator for the Swiss Post Parcel symbology.
                using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, service.Code))
                {
                    // Set visual dimensions of the barcode.
                    generator.Parameters.Barcode.XDimension.Pixels = 2f;
                    generator.Parameters.Barcode.BarHeight.Pixels = 40f;

                    // Hide the encoded text; we will display a custom caption instead.
                    generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

                    // Configure a caption above the barcode to show the abbreviation.
                    generator.Parameters.CaptionAbove.Visible = true;
                    generator.Parameters.CaptionAbove.Alignment = TextAlignment.Left;
                    generator.Parameters.CaptionAbove.Text = service.Abbreviation;
                    generator.Parameters.CaptionAbove.Font.Size.Pixels = 24f;

                    // Save the barcode as a PNG image.
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }

                // Record the entry in the CSV index.
                csvWriter.WriteLine($"{service.Code},{service.Abbreviation},{fileName}");
            }
        }

        // Inform the user where the output files are located.
        Console.WriteLine($"Barcodes and CSV index have been generated in: {outputFolder}");
    }
}