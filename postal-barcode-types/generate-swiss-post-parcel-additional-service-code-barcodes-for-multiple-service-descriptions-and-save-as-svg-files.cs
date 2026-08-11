// Title: Generate Swiss Post Parcel Service Barcodes and Save as SVG
// Description: Demonstrates how to create Swiss Post Parcel barcodes for a list of service codes and export each barcode as an SVG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator with EncodeTypes.SwissPostParcel. It illustrates typical scenarios such as batch barcode creation for logistics, customizing barcode dimensions, and exporting to vector formats like SVG. Developers working with postal services, shipping labels, or bulk barcode generation will find this pattern useful.
// Prompt: Generate Swiss Post Parcel additional service code barcodes for multiple service descriptions and save as SVG files.
// Tags: barcode, swisspostparcel, svg, generation, aspose.barcode, encode types

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates Swiss Post Parcel barcodes for multiple service codes and saves them as SVG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates barcodes for predefined service codes and writes them to the file system.
    /// </summary>
    static void Main()
    {
        // Define a set of sample service descriptions for Swiss Post Parcel additional services
        string[] services = new[]
        {
            "A1",               // Example service code
            "B2",               // Another service code
            "C3D4",             // Composite service code
            "E5F6G7",           // Longer service code
            "H8I9J0K1L2"        // Even longer service code
        };

        // Determine the output folder path and ensure it exists
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "SwissPostBarcodes");
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Iterate over each service code and generate a corresponding barcode
        foreach (string service in services)
        {
            // Sanitize the file name by removing invalid characters and replacing spaces with underscores
            string safeFileName = string.Concat(service.Split(Path.GetInvalidFileNameChars()))
                                      .Replace(' ', '_');
            string outputPath = Path.Combine(outputFolder, $"{safeFileName}.svg");

            // Initialize the barcode generator for Swiss Post Parcel using the service description as the code text
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, service))
            {
                // Optionally adjust the module size (x-dimension) for better visual quality
                generator.Parameters.Barcode.XDimension.Point = 2f;

                // Attempt to save the barcode as an SVG file; handle potential licensing restrictions
                try
                {
                    generator.Save(outputPath, BarCodeImageFormat.Svg);
                    Console.WriteLine($"Saved barcode for service '{service}' to '{outputPath}'.");
                }
                catch (Exception ex)
                {
                    // Notify the user if SVG export fails (e.g., due to evaluation license limitations)
                    Console.WriteLine($"Failed to save SVG for service '{service}': {ex.Message}");
                }
            }
        }

        Console.WriteLine("Barcode generation completed.");
    }
}