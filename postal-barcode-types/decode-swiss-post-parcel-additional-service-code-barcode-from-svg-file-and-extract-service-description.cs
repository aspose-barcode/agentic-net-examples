// Title: Decode Swiss Post Parcel Additional Service Barcode from SVG
// Description: Demonstrates how to read a Swiss Post Parcel additional service code barcode stored in an SVG file and map the decoded value to a human‑readable service description.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, showcasing the use of BarCodeReader with DecodeType.SwissPostParcel to extract service codes from vector graphics. Developers working with postal logistics, parcel tracking, or document automation often need to decode Swiss Post barcodes and translate them into meaningful service information. The key API classes include BarCodeReader, BarCodeResult, and DecodeType, which together enable fast, reliable barcode extraction from SVG, PDF, or image files.
// Prompt: Decode a Swiss Post Parcel additional service code barcode from a SVG file and extract service description.
// Tags: swisspost, barcode, decode, svg, service-code, aspose.barcode, barcoderecognition

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that decodes a Swiss Post Parcel additional service barcode from an SVG file
/// and prints the corresponding service description.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Reads the SVG, decodes the barcode, and outputs the service description.
    /// </summary>
    static void Main()
    {
        // Path to the SVG file containing the Swiss Post Parcel Additional Service barcode
        string svgPath = "SwissPostAdditionalService.svg";

        // Verify that the SVG file exists before attempting to read it
        if (!File.Exists(svgPath))
        {
            Console.WriteLine($"File not found: {svgPath}");
            return;
        }

        // Mapping of service codes to their human‑readable descriptions (case‑insensitive)
        var serviceDescriptions = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "0203", "Business reply label" },
            { "0322", "Personal delivery" },
            { "0327", "Return receipt" },
            { "0328", "Electronic return receipt" },
            { "0340", "Cash on delivery (obsolete)" },
            { "0341", "Electronic cash on delivery" },
            { "0470", "ID Check" },
            { "0610", "Items for the blind" },
            { "1007", "Military mail" },
            { "2512", "Second attempted delivery on the following Saturday" }
        };

        // Specify the decode type for Swiss Post Parcel barcodes
        BaseDecodeType decodeType = DecodeType.SwissPostParcel;

        // Create a BarCodeReader for the SVG file using the specified decode type
        using (var reader = new BarCodeReader(svgPath, decodeType))
        {
            // Read all barcodes found in the file
            BarCodeResult[] results = reader.ReadBarCodes();

            // If no barcodes were detected, inform the user and exit
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected in the SVG file.");
                return;
            }

            // Process each detected barcode
            foreach (BarCodeResult result in results)
            {
                // Extract and trim the decoded text
                string code = result.CodeText?.Trim();
                Console.WriteLine($"Decoded Service Code: {code}");

                // Look up the service description using the decoded code
                if (!string.IsNullOrEmpty(code) && serviceDescriptions.TryGetValue(code, out string description))
                {
                    Console.WriteLine($"Service Description: {description}");
                }
                else
                {
                    Console.WriteLine("Service description not found for the decoded code.");
                }
            }
        }
    }
}