// Title: Decode Swiss Post Parcel Additional Service Barcode from SVG
// Description: Demonstrates how to read a Swiss Post Parcel barcode stored in an SVG file, parse additional service codes, and map them to human‑readable descriptions.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, focusing on decoding Swiss Post Parcel symbology from vector graphics. It showcases the use of BarCodeReader, DecodeType, and QualitySettings classes to extract raw barcode data, then illustrates typical post‑processing such as splitting service codes and looking up their descriptions—common tasks for developers integrating postal services or logistics solutions.
/// Prompt: Decode a Swiss Post Parcel additional service code barcode from a SVG file and extract service description.
/// Tags: swisspost, parcel, barcode, decode, svg, aspose.barcode, recognition

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that reads a Swiss Post Parcel barcode from an SVG file,
/// extracts any additional service codes, and prints their human‑readable descriptions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs file validation, barcode decoding,
    /// and service code lookup.
    /// </summary>
    static void Main()
    {
        // Path to the SVG file containing the Swiss Post Parcel barcode
        string svgPath = "parcel.svg";

        // Verify that the file exists before attempting to read it
        if (!File.Exists(svgPath))
        {
            Console.WriteLine($"File not found: {svgPath}");
            return;
        }

        // Mapping of known Swiss Post additional service codes to their descriptions
        var serviceDescriptions = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "A", "Registered Mail" },
            { "B", "Express Delivery" },
            { "C", "Cash on Delivery" },
            { "D", "Insurance" },
            // Add more mappings as needed
        };

        // Create a BarCodeReader configured for the Swiss Post Parcel symbology
        using (BarCodeReader reader = new BarCodeReader(svgPath, DecodeType.SwissPostParcel))
        {
            // Use a higher quality preset to improve detection accuracy for vector graphics
            reader.QualitySettings = QualitySettings.HighQuality;

            // Iterate through all barcodes detected in the SVG file
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"Raw Code Text: {result.CodeText}");

                // Parse the raw text assuming service codes follow the main parcel number,
                // separated by spaces, semicolons, or commas (e.g., "1234567890 A B D")
                var parts = result.CodeText.Split(new[] { ' ', ';', ',' }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length > 1)
                {
                    Console.WriteLine("Additional Services:");
                    // Start from index 1 to skip the main parcel number
                    for (int i = 1; i < parts.Length; i++)
                    {
                        string code = parts[i];
                        if (serviceDescriptions.TryGetValue(code, out string description))
                        {
                            Console.WriteLine($"  {code}: {description}");
                        }
                        else
                        {
                            Console.WriteLine($"  {code}: (unknown service code)");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No additional service codes detected.");
                }

                Console.WriteLine(); // Blank line between results for readability
            }
        }
    }
}