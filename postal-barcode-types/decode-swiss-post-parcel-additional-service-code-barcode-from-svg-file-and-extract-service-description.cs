using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates reading a Swiss Post Parcel barcode from an SVG file
/// and mapping its service code to a human‑readable description.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Accepts an optional command‑line argument specifying the SVG file path.
    /// </summary>
    /// <param name="args">Command‑line arguments.</param>
    static void Main(string[] args)
    {
        // Determine the SVG file path: use the first argument if supplied,
        // otherwise fall back to a default sample file name.
        string svgPath = args.Length > 0 ? args[0] : "SwissPostParcel.svg";

        // Verify that the specified file exists before attempting to read it.
        if (!File.Exists(svgPath))
        {
            Console.WriteLine($"File not found: {svgPath}");
            return;
        }

        // Mapping of Swiss Post Parcel additional service codes to their descriptions.
        // This dictionary is case‑insensitive; extend it with all valid codes as needed.
        var serviceDescriptions = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "A", "Express delivery" },
            { "B", "Standard delivery" },
            { "C", "Cash on delivery" },
            { "D", "Signature required" }
            // Add more mappings as needed.
        };

        // Initialize a BarCodeReader for the SVG file, specifying the expected barcode type.
        using (var reader = new BarCodeReader(svgPath, DecodeType.SwissPostParcel))
        {
            // Read all barcodes present in the image.
            BarCodeResult[] results = reader.ReadBarCodes();

            // If no barcodes were detected, inform the user and exit.
            if (results.Length == 0)
            {
                Console.WriteLine("No Swiss Post Parcel barcode detected.");
                return;
            }

            // Iterate over each detected barcode and display its details.
            foreach (var result in results)
            {
                Console.WriteLine($"Detected Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");

                // The first character of the code text may represent a service code.
                // Extract it and look up the corresponding description.
                if (!string.IsNullOrEmpty(result.CodeText))
                {
                    string serviceCode = result.CodeText.Substring(0, 1);
                    if (serviceDescriptions.TryGetValue(serviceCode, out string description))
                    {
                        Console.WriteLine($"Service Description: {description}");
                    }
                    else
                    {
                        Console.WriteLine("Service Description: Unknown code");
                    }
                }

                Console.WriteLine(); // Blank line for readability between results.
            }
        }
    }
}