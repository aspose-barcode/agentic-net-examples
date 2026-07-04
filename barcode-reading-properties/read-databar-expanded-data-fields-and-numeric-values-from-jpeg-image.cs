// Title: Read DataBar Expanded barcode fields from JPEG
// Description: Demonstrates loading a JPEG image, recognizing a GS1 DataBar Expanded barcode, and extracting its data fields and numeric values.
// Prompt: Read DataBar expanded data fields and numeric values from a JPEG image.
// Tags: databar expanded, barcode recognition, jpeg, numeric extraction, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that reads GS1 DataBar Expanded barcodes from a JPEG image
/// and extracts numeric values from the decoded text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Loads the image, performs barcode recognition, and prints extracted data.
    /// </summary>
    static void Main()
    {
        // Path to the JPEG image containing a GS1 DataBar Expanded barcode.
        string imagePath = "databar_expanded.jpg";

        // Verify that the image file exists before attempting to load it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Load the image as a bitmap using Aspose.Drawing.
        using (var bitmap = new Bitmap(imagePath))
        {
            // Initialize a BarCodeReader to detect only DataBar Expanded barcodes.
            using (var reader = new BarCodeReader(bitmap, DecodeType.DatabarExpanded))
            {
                // Execute the recognition process.
                BarCodeResult[] results = reader.ReadBarCodes();

                // If no barcodes were found, inform the user and exit.
                if (results.Length == 0)
                {
                    Console.WriteLine("No barcodes were detected.");
                    return;
                }

                // Iterate through each detected barcode and display its details.
                foreach (var result in results)
                {
                    Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                    Console.WriteLine($"Code Text: {result.CodeText}");

                    // Use a regular expression to extract all numeric substrings from the code text.
                    var matches = Regex.Matches(result.CodeText ?? string.Empty, @"\d+");
                    if (matches.Count > 0)
                    {
                        Console.WriteLine("Numeric values:");
                        foreach (Match match in matches)
                        {
                            Console.WriteLine($"  {match.Value}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No numeric values found.");
                    }

                    Console.WriteLine(); // Blank line between barcodes for readability.
                }
            }
        }
    }
}