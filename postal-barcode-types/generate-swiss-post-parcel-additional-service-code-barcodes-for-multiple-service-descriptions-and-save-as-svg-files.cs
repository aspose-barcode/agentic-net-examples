// Title: Generate Swiss Post Parcel Additional Service Barcodes and Save as SVG
// Description: The example generates Swiss Post Parcel barcodes for multiple additional service codes, adds a caption with the service abbreviation, and writes each barcode to an SVG file.
// Category-Description: This sample belongs to the Aspose.BarCode barcode generation category, demonstrating how to use the BarcodeGenerator class with EncodeTypes.SwissPostParcel to create custom barcodes for shipping and logistics applications. It shows configuring barcode dimensions, hiding the default code text, adding custom captions, and exporting to vector SVG format—common tasks for developers building parcel labeling or tracking solutions.
// Prompt: Generate Swiss Post Parcel additional service code barcodes for multiple service descriptions and save as SVG files.
// Tags: swisspost, parcel, additional-service, barcode, svg, generation, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating Swiss Post Parcel additional service barcodes and saving them as SVG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates barcodes for a predefined list of service codes,
    /// applies visual settings, and saves each barcode as an SVG file in a temporary folder.
    /// </summary>
    static void Main()
    {
        // Define service codes and their abbreviations.
        var services = new List<(string Code, string Abbreviation)>
        {
            ("0203", "GAS"),
            ("0322", "RMP"),
            ("0327", "AR"),
            ("0328", "eAR"),
            ("0340", "-"),
            ("0341", "BLN"),
            ("0470", "ID+RMP"),
            ("0610", "CEC"),
            ("1007", "MIL"),
            ("2512", "-")
        };

        // Create a unique output folder in the system's temporary directory.
        string outputFolder = Path.Combine(Path.GetTempPath(), "SwissPostAdditionalServices_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Iterate over each service definition and generate the corresponding barcode.
        foreach (var service in services)
        {
            string fileName = $"SwissPostAdditionalService_{service.Code}.svg";
            string filePath = Path.Combine(outputFolder, fileName);

            // Initialize the barcode generator for Swiss Post Parcel symbology with the service code.
            using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, service.Code))
            {
                // Set basic barcode appearance.
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Parameters.Barcode.BarHeight.Pixels = 40f;

                // Hide the default human‑readable code text; a custom caption will be used instead.
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

                // Configure the caption displayed above the barcode.
                generator.Parameters.CaptionAbove.Visible = true;
                generator.Parameters.CaptionAbove.Alignment = TextAlignment.Left;
                generator.Parameters.CaptionAbove.Text = service.Abbreviation;
                generator.Parameters.CaptionAbove.Font.Size.Pixels = 24f;

                try
                {
                    // Save the barcode as an SVG file.
                    generator.Save(filePath, BarCodeImageFormat.Svg);
                    Console.WriteLine($"Saved SVG: {filePath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to save SVG for code {service.Code}: {ex.Message}");
                }
            }
        }

        Console.WriteLine("Processing completed.");
    }
}