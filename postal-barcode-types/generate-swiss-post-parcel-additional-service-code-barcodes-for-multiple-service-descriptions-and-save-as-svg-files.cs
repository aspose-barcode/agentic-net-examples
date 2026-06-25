using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generation of Swiss Post Parcel barcodes for a set of service codes
/// and saves them as SVG files in an output directory.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcodes for predefined service codes
    /// and writes them to SVG files.
    /// </summary>
    static void Main()
    {
        // Define the output directory relative to the current working directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "SwissPostParcelBarcodes");

        // Ensure the output directory exists; create it if it does not
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // List of sample service codes for Swiss Post Parcel additional services
        List<string> serviceCodes = new List<string>
        {
            "D01", // Example: Domestic mail
            "I02", // Example: International mail
            "A03", // Example: Additional service 1
            "B04", // Example: Additional service 2
            "C05"  // Example: Additional service 3
        };

        // Iterate over each service code and generate a corresponding barcode
        foreach (string code in serviceCodes)
        {
            // Construct the file name and full path for the SVG output
            string fileName = $"SwissPostParcel_{code}.svg";
            string filePath = Path.Combine(outputDir, fileName);

            // Create a barcode generator for the Swiss Post Parcel format using the current code
            using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, code))
            {
                // Optional: adjust visual appearance of the barcode
                generator.Parameters.Barcode.XDimension.Point = 2f;   // Width of a single module
                generator.Parameters.Barcode.BarHeight.Point = 30f; // Height of the barcode

                try
                {
                    // Save the generated barcode as an SVG file
                    generator.Save(filePath, BarCodeImageFormat.Svg);
                    Console.WriteLine($"Saved barcode for code '{code}' to '{filePath}'.");
                }
                catch (Exception ex)
                {
                    // Handle potential errors (e.g., licensing restrictions on SVG output)
                    Console.WriteLine($"Failed to save barcode for code '{code}': {ex.Message}");
                }
            }
        }

        // Indicate that the barcode generation process has finished
        Console.WriteLine("Barcode generation completed.");
    }
}