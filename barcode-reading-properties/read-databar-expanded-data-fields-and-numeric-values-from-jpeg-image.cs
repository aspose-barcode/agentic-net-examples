using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates reading GS1 DataBar Expanded barcodes from an image file
/// and extracting Application Identifier (AI) fields using regular expressions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Reads a JPEG image, detects DataBar Expanded barcodes, and parses AI/value pairs.
    /// </summary>
    static void Main()
    {
        // Path to the JPEG image containing the GS1 DataBar Expanded barcode
        string imagePath = "databar_expanded.jpg";

        // Verify that the image file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Initialize a BarCodeReader configured for the DataBar Expanded symbology
        using (var reader = new BarCodeReader(imagePath, DecodeType.DatabarExpanded))
        {
            // Attempt to read all barcodes present in the image
            BarCodeResult[] results = reader.ReadBarCodes();

            // If no barcodes were detected, inform the user and exit
            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes were detected.");
                return;
            }

            // Regular expression to extract Application Identifier (AI) and its numeric value
            // Example CodeText: "(01)12345678901231(10)ABC123"
            Regex aiRegex = new Regex(@"\((\d{2,4})\)(\d+)", RegexOptions.Compiled);

            // Process each detected barcode result
            foreach (BarCodeResult result in results)
            {
                // Output basic barcode information
                Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"Raw CodeText: {result.CodeText}");

                // Use the regex to find AI/value pairs within the CodeText
                MatchCollection matches = aiRegex.Matches(result.CodeText);
                if (matches.Count > 0)
                {
                    Console.WriteLine("Extracted DataBar Expanded fields:");
                    foreach (Match match in matches)
                    {
                        // Group 1 = AI, Group 2 = numeric value associated with the AI
                        string ai = match.Groups[1].Value;
                        string value = match.Groups[2].Value;
                        Console.WriteLine($"  AI ({ai}) = {value}");
                    }
                }
                else
                {
                    // No AI/value pairs matched the expected pattern
                    Console.WriteLine("No numeric AI fields found in the CodeText.");
                }

                Console.WriteLine(); // Blank line between results for readability
            }
        }
    }
}