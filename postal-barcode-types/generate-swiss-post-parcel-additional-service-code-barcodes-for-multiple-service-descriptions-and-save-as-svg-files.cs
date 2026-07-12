// Title: Generate Swiss Post Parcel Service Barcodes as SVG
// Description: Demonstrates creating Swiss Post Parcel additional service code barcodes for various service descriptions and saving them as SVG files.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator with EncodeTypes.SwissPostParcel. It covers setting visual parameters, handling output directories, and saving barcodes in vector SVG format—common tasks for developers integrating postal barcode printing into applications.
// Prompt: Generate Swiss Post Parcel additional service code barcodes for multiple service descriptions and save as SVG files.
// Tags: swisspostparcel, barcode, generation, svg, aspose.barcode, encode types

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates Swiss Post Parcel additional service code barcodes for multiple service descriptions and saves them as SVG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// </summary>
    static void Main()
    {
        // Define sample service descriptions and their corresponding Swiss Post Parcel code texts.
        var services = new (string Description, string CodeText)[]
        {
            ("Standard Delivery", "123456789012"),
            ("Express Delivery", "234567890123"),
            ("Cash on Delivery", "345678901234"),
            ("Registered Mail", "456789012345")
        };

        // Ensure the output directory exists.
        string outputDir = "SwissPostParcelBarcodes";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Iterate over each service and generate its barcode.
        foreach (var service in services)
        {
            // Create a barcode generator for the Swiss Post Parcel symbology.
            using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, service.CodeText))
            {
                // Configure basic visual settings.
                generator.Parameters.Barcode.BarColor = Color.Black;   // Barcode bars color
                generator.Parameters.BackColor = Color.White;          // Background color
                generator.Parameters.Barcode.XDimension.Point = 2f;   // Module size (point size)

                // Build a safe file name by replacing spaces with underscores.
                string safeDescription = service.Description.Replace(' ', '_');
                string outputPath = Path.Combine(outputDir, $"{safeDescription}.svg");

                // Attempt to save the barcode as an SVG file.
                try
                {
                    generator.Save(outputPath, BarCodeImageFormat.Svg);
                    Console.WriteLine($"Saved {service.Description} barcode to {outputPath}");
                }
                catch (Exception ex)
                {
                    // Inform the user if the format is not supported (e.g., evaluation license limitation).
                    Console.WriteLine($"Failed to save {service.Description} barcode as SVG: {ex.Message}");
                }
            }
        }
    }
}