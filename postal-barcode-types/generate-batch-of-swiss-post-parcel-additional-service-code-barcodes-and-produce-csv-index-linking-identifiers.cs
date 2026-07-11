// Title: Generate Swiss Post Parcel Additional Service Code Barcodes with CSV Index
// Description: Creates PNG barcodes for Swiss Post Parcel additional service codes and writes a CSV file mapping each code to its image file.
// Category-Description: This example demonstrates how to use Aspose.BarCode to generate Swiss Post Parcel additional service code barcodes, save them as images, and build an index CSV. It covers the BarcodeGenerator class, EncodeTypes.SwissPostParcel, and file I/O for batch processing—common tasks for developers automating barcode creation for shipping and logistics.
// Prompt: Generate a batch of Swiss Post Parcel additional service code barcodes and produce a CSV index linking identifiers.
// Tags: barcode, swisspost, parcel, additional service code, csv, batch generation, aspose.barcode, image output

using System;
using System.IO;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch generation of Swiss Post Parcel additional service code barcodes
/// and creation of a CSV index linking each identifier to its image file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcode images and writes an index CSV.
    /// </summary>
    static void Main()
    {
        // Define the output directory for barcode images
        string outputDir = "Barcodes";
        Directory.CreateDirectory(outputDir); // Ensure the directory exists

        // Define the path for the CSV index file
        string csvPath = Path.Combine(outputDir, "index.csv");

        // Sample additional service codes for Swiss Post Parcel
        string[] serviceCodes = new string[]
        {
            "1234567890",
            "0987654321",
            "1122334455",
            "5566778899",
            "0001112223"
        };

        // Create the CSV file and write the header row
        using (StreamWriter writer = new StreamWriter(csvPath))
        {
            writer.WriteLine("Identifier,FileName");

            // Iterate over each service code to generate a barcode
            for (int i = 0; i < serviceCodes.Length; i++)
            {
                string code = serviceCodes[i];
                string fileName = $"SwissPost_{i + 1}.png";
                string filePath = Path.Combine(outputDir, fileName);

                // Generate the barcode image using Aspose.BarCode
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, code))
                {
                    generator.Save(filePath); // Save the barcode as a PNG file
                }

                // Record the mapping of identifier to file name in the CSV
                writer.WriteLine($"{code},{fileName}");
            }
        }

        // Inform the user where the output files are located
        Console.WriteLine($"Barcodes generated and CSV index created at: {csvPath}");
    }
}