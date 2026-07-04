// Title: Extract GS1 Composite component count and AIs from PNG
// Description: Demonstrates reading a GS1 Composite barcode from a PNG image, retrieving the linear and 2D component texts, counting application identifiers, and reporting component count.
// Prompt: Extract GS1 Composite component count and application identifiers from a PNG image.
// Tags: gs1 composite, barcode reading, png, aspose.barcode, csharp

using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Program to extract GS1 Composite component count and application identifiers from a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Reads the PNG, extracts barcode data, and displays component texts, AI count, and component count.
    /// </summary>
    static void Main()
    {
        // Path to the PNG image containing the GS1 Composite barcode
        const string imagePath = "gs1composite.png";

        // Verify that the image file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Create a BarCodeReader configured for GS1 Composite Bar symbology
        using (var reader = new BarCodeReader(imagePath, DecodeType.GS1CompositeBar))
        {
            // Read all barcodes present in the image
            var results = reader.ReadBarCodes();

            // If no barcodes were detected, inform the user and exit
            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes detected.");
                return;
            }

            // Process each detected barcode result
            foreach (var result in results)
            {
                // Access extended GS1 Composite parameters (linear and 2D components)
                var gs1Ext = result.Extended.GS1CompositeBar;

                // Retrieve the linear (1D) component text; use empty string if null
                string linearText = gs1Ext.OneDCodeText ?? string.Empty;
                // Retrieve the 2D component text; use empty string if null
                string twoDText = gs1Ext.TwoDCodeText ?? string.Empty;

                Console.WriteLine($"Linear component text: {linearText}");
                Console.WriteLine($"2D component text: {twoDText}");

                // Use a regular expression to find all Application Identifiers (AIs) in the linear component
                var aiMatches = Regex.Matches(linearText, @"\((\d{2,4})\)");
                int aiCount = aiMatches.Count;

                Console.WriteLine($"Number of Application Identifiers: {aiCount}");

                // If any AIs were found, list them
                if (aiCount > 0)
                {
                    Console.WriteLine("Application Identifiers:");
                    foreach (Match match in aiMatches)
                    {
                        // Extract the numeric AI without parentheses
                        string ai = match.Groups[1].Value;
                        Console.WriteLine($"- {ai}");
                    }
                }

                // GS1 Composite always consists of two components: linear and 2D
                Console.WriteLine($"GS1 Composite component count: 2");
                Console.WriteLine();
            }
        }
    }
}