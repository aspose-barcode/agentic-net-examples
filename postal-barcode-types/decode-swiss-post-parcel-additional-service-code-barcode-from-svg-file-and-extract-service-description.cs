// Title: Decode Swiss Post Parcel barcode from SVG and retrieve service description
// Description: Demonstrates generating a Swiss Post Parcel barcode, saving it as SVG (or PNG fallback), decoding it, and mapping the service code to a human‑readable description.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the BarcodeGenerator for creating SwissPostParcel barcodes, BarCodeReader for decoding, and typical file handling. Developers often need to generate parcel barcodes, read them from images, and translate service codes into business‑logic descriptions; this snippet provides a concise reference for those tasks.
// Prompt: Decode a Swiss Post Parcel additional service code barcode from a SVG file and extract service description.
// Tags: swisspostparcel, barcode, generation, recognition, svg, png, servicecode, mapping

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Swiss Post Parcel barcode, saves it as SVG (or PNG fallback),
/// decodes it, and maps the decoded service code to a description.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Performs barcode generation, saving, decoding, and cleanup.
    /// </summary>
    static void Main()
    {
        // Sample Swiss Post Parcel barcode data (additional service code)
        // In a real scenario this would be the actual service code string.
        string sampleCodeText = "1234567890";

        // Paths for temporary files
        string svgPath = Path.Combine(Path.GetTempPath(), "SwissPostParcel.svg");
        string pngPath = Path.Combine(Path.GetTempPath(), "SwissPostParcel.png");

        // Generate a Swiss Post Parcel barcode and save as SVG (fallback to PNG if SVG not supported)
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, sampleCodeText))
            {
                // Optional: adjust appearance
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Save as SVG
                generator.Save(svgPath, BarCodeImageFormat.Svg);
                Console.WriteLine($"Barcode saved as SVG: {svgPath}");
            }
        }
        catch (Exception ex)
        {
            // Evaluation license may not allow SVG export; fallback to PNG
            Console.WriteLine($"SVG export failed ({ex.Message}), saving as PNG instead.");
            using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, sampleCodeText))
            {
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                generator.Save(pngPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode saved as PNG: {pngPath}");
                // Use PNG path for subsequent decoding
                svgPath = pngPath;
            }
        }

        // Verify that the file exists before attempting to read
        if (!File.Exists(svgPath))
        {
            Console.WriteLine("Barcode image file not found. Exiting.");
            return;
        }

        // Decode the barcode from the SVG (or PNG) file
        using (var reader = new BarCodeReader(svgPath, DecodeType.SwissPostParcel))
        {
            bool found = false;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                found = true;
                string decodedText = result.CodeText;
                Console.WriteLine($"Decoded CodeText: {decodedText}");

                // Simple mapping of known service codes to descriptions
                var serviceDescriptions = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { "1234567890", "Standard Parcel Delivery" },
                    { "9876543210", "Express Delivery" },
                    { "5555555555", "Cash on Delivery" }
                    // Add more mappings as needed
                };

                if (serviceDescriptions.TryGetValue(decodedText, out string description))
                {
                    Console.WriteLine($"Service Description: {description}");
                }
                else
                {
                    Console.WriteLine("Service Description: Unknown service code.");
                }
            }

            if (!found)
            {
                Console.WriteLine("No barcode detected in the image.");
            }
        }

        // Cleanup temporary files (optional)
        try
        {
            if (File.Exists(svgPath) && svgPath != pngPath)
                File.Delete(svgPath);
            if (File.Exists(pngPath))
                File.Delete(pngPath);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit
        }
    }
}