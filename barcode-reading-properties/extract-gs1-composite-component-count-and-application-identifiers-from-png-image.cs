// Title: Extract GS1 Composite component count and application identifiers from a PNG image
// Description: Demonstrates how to read a GS1 Composite barcode from a PNG file, determine the number of components (1D and 2D), and list the embedded Application Identifiers.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, focusing on GS1 Composite symbology. It uses BarCodeReader, BarCodeResult, and the Extended.GS1CompositeBar properties to access 1D and 2D component data. Typical use cases include inventory management, product tracking, and supply‑chain applications where extracting AI information from composite barcodes is required. Developers often need to decode composite barcodes, count their components, and parse AI strings for further processing.
// Prompt: Extract GS1 Composite component count and application identifiers from a PNG image.
// Tags: barcode, gs1, composite, extraction, png, aspose.barcode, recognition

using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates extracting GS1 Composite component count and application identifiers from a PNG image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Reads the barcode, computes component count, and prints found Application Identifiers.
    /// </summary>
    static void Main()
    {
        // Path to the PNG image containing the GS1 Composite barcode
        string imagePath = "gs1composite.png";

        // Verify that the image file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Specify the decode type for GS1 Composite barcodes
        BaseDecodeType decodeType = DecodeType.GS1CompositeBar;

        // Initialize the barcode reader with the image path and decode type
        using (BarCodeReader reader = new BarCodeReader(imagePath, decodeType))
        {
            // Read all barcodes found in the image
            BarCodeResult[] results = reader.ReadBarCodes();

            // If no barcodes were detected, inform the user and exit
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
                return;
            }

            // Process each detected barcode result
            foreach (BarCodeResult result in results)
            {
                // Access the extended GS1 Composite information (1D and 2D components)
                var ext = result.Extended.GS1CompositeBar;
                string oneD = ext.OneDCodeText ?? string.Empty;
                string twoD = ext.TwoDCodeText ?? string.Empty;

                // Calculate component count: 1 for each non‑empty component
                int componentCount = (string.IsNullOrEmpty(oneD) ? 0 : 1) + (string.IsNullOrEmpty(twoD) ? 0 : 1);
                Console.WriteLine($"Component Count: {componentCount}");

                // Combine the 1D and 2D texts to search for Application Identifiers (AIs)
                string combined = oneD + twoD;
                var matches = Regex.Matches(combined, @"\(\d{2,4}\)");

                // Output the found AIs
                Console.WriteLine("Application Identifiers found:");
                foreach (Match m in matches)
                {
                    Console.WriteLine($"  {m.Value}");
                }
            }
        }
    }
}