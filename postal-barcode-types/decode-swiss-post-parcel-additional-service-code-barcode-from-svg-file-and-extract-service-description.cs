// Title: Decode Swiss Post Parcel Additional Service Barcode from SVG
// Description: Demonstrates how to read a Swiss Post Parcel additional service barcode stored in an SVG file and map the decoded value to a human‑readable service description.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing the use of BarCodeReader with DecodeType.SwissPostParcel. It illustrates typical scenarios where developers need to extract service codes from Swiss Post parcel labels, translate them into meaningful descriptions, and integrate barcode data processing into .NET applications.
// Prompt: Decode a Swiss Post Parcel additional service code barcode from a SVG file and extract service description.
// Tags: swisspost, parcel, barcode, decode, svg, aspose.barcode, barcoderecognition

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that decodes a Swiss Post Parcel additional service barcode from an SVG file
/// and prints the corresponding service description.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Loads the SVG, decodes the barcode, and looks up the service description.
    /// </summary>
    static void Main()
    {
        // Path to the SVG file containing the Swiss Post Parcel additional service barcode
        string svgPath = "SwissPostAdditionalService.svg";

        // Verify that the SVG file exists before attempting to read it
        if (!File.Exists(svgPath))
        {
            Console.WriteLine($"File not found: {svgPath}");
            return;
        }

        // Mapping of additional service codes to their human‑readable descriptions
        var serviceDescriptions = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "0203", "Business reply label (GAS)" },
            { "0322", "Personal delivery (RMP)" },
            { "0327", "Return receipt (AR)" },
            { "0328", "Electronic return receipt (eAR)" },
            { "0340", "Cash on delivery (obsolete)" },
            { "0341", "Electronic cash on delivery (BLN)" },
            { "0470", "ID Check (ID+RMP)" },
            { "0610", "Items for the blind (CEC)" },
            { "1007", "Military mail (MIL)" },
            { "2512", "Second attempted delivery on the following Saturday" }
        };

        // Create a barcode reader configured for Swiss Post Parcel barcodes
        using (var reader = new BarCodeReader(svgPath, DecodeType.SwissPostParcel))
        {
            // Read all barcodes present in the SVG file
            BarCodeResult[] results = reader.ReadBarCodes();

            // If no barcodes were detected, inform the user and exit
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected in the SVG file.");
                return;
            }

            // Process each detected barcode
            foreach (var result in results)
            {
                // Retrieve and trim the decoded text
                string code = result.CodeText?.Trim() ?? string.Empty;
                Console.WriteLine($"Detected code: {code}");

                // Look up the service description based on the decoded code
                if (serviceDescriptions.TryGetValue(code, out string description))
                {
                    Console.WriteLine($"Service description: {description}");
                }
                else
                {
                    Console.WriteLine("Service description: Unknown or not defined.");
                }
            }
        }
    }
}