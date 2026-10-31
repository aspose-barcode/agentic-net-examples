// Title: Extract GS1 Composite component count and application identifiers from a PNG image
// Description: Demonstrates reading a GS1 Composite barcode from a PNG file, displaying its linear and 2D component details, and extracting embedded Application Identifiers (AIs).
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, focusing on GS1 Composite symbology. It showcases the use of BarCodeReader, DecodeType.GS1CompositeBar, and the Extended.GS1CompositeBar properties to access component information. Typical use cases include inventory management, logistics, and retail where GS1 Composite barcodes encode both linear and 2D data with AIs.
// Prompt: Extract GS1 Composite component count and application identifiers from a PNG image.
// Tags: gs1 composite, barcode recognition, png, application identifiers, aspose.barcode, c#

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that reads a GS1 Composite barcode from a PNG image,
/// prints component details, and extracts Application Identifiers.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Path to the PNG image containing the GS1 Composite barcode.
        string imagePath = "gs1composite.png";

        // Verify that the image file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Initialize a BarCodeReader for GS1 Composite barcodes.
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.GS1CompositeBar))
        {
            // Read all barcodes found in the image.
            BarCodeResult[] results = reader.ReadBarCodes();

            // Handle case where no barcode was detected.
            if (results == null || results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
                return;
            }

            // Process each detected barcode result.
            foreach (BarCodeResult result in results)
            {
                // Access extended GS1 Composite information.
                var ext = result.Extended.GS1CompositeBar;

                Console.WriteLine("=== GS1 Composite Barcode ===");
                Console.WriteLine($"Component count: 2 (linear + 2D)");
                Console.WriteLine($"Linear component type: {ext.OneDType}");
                Console.WriteLine($"Linear component text: {ext.OneDCodeText}");
                Console.WriteLine($"2D component type: {ext.TwoDType}");
                Console.WriteLine($"2D component text: {ext.TwoDCodeText}");

                // Extract Application Identifiers (AIs) from both linear and 2D parts.
                List<string> ais = new List<string>();
                ais.AddRange(ExtractApplicationIdentifiers(ext.OneDCodeText));
                ais.AddRange(ExtractApplicationIdentifiers(ext.TwoDCodeText));

                // Output extracted AIs, if any.
                if (ais.Count > 0)
                {
                    Console.WriteLine("Application Identifiers found:");
                    foreach (string ai in ais)
                    {
                        Console.WriteLine($"- {ai}");
                    }
                }
                else
                {
                    Console.WriteLine("No Application Identifiers detected.");
                }

                Console.WriteLine();
            }
        }
    }

    // Simple parser that extracts AI codes formatted as "(xx)" from a barcode text string.
    private static List<string> ExtractApplicationIdentifiers(string codeText)
    {
        List<string> ais = new List<string>();
        if (string.IsNullOrEmpty(codeText))
            return ais;

        int index = 0;
        while (index < codeText.Length)
        {
            int open = codeText.IndexOf('(', index);
            if (open == -1)
                break;
            int close = codeText.IndexOf(')', open + 1);
            if (close == -1)
                break;

            string ai = codeText.Substring(open, close - open + 1);
            ais.Add(ai);
            index = close + 1;
        }
        return ais;
    }
}