using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates reading a GS1 Composite barcode from an image file
/// and extracting its components and Application Identifiers (AIs).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Reads the specified PNG image, detects GS1 Composite barcodes,
    /// and prints details such as barcode type, code text, AI count,
    /// and component count.
    /// </summary>
    static void Main()
    {
        // Path to the PNG image containing the GS1 Composite barcode.
        string imagePath = "gs1composite.png";

        // Verify that the image file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a BarCodeReader configured for GS1 Composite barcodes.
        using (var reader = new BarCodeReader(imagePath, DecodeType.GS1CompositeBar))
        {
            bool anyFound = false; // Tracks whether any barcode was detected.

            // Iterate over all detected barcodes in the image.
            foreach (var result in reader.ReadBarCodes())
            {
                anyFound = true;

                // Output basic barcode information.
                Console.WriteLine($"Detected Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");

                // GS1 Composite codetext consists of linear and 2D parts separated by '|'.
                // Split the codetext into its constituent parts.
                string[] parts = result.CodeText.Split('|');

                // Combine parts with a space to simplify AI extraction via regex.
                string combined = string.Join(" ", parts);

                // Find all Application Identifiers (AI) in the format (nn) where n is a digit.
                var matches = Regex.Matches(combined, @"\(\d{2,4}\)");
                int aiCount = matches.Count;

                // Display the number of AIs found.
                Console.WriteLine($"Application Identifier Count: {aiCount}");

                // If any AIs are present, list them.
                if (aiCount > 0)
                {
                    Console.WriteLine("Application Identifiers found:");
                    foreach (Match m in matches)
                    {
                        Console.WriteLine($"  {m.Value}");
                    }
                }

                // Component count corresponds to the number of distinct parts
                // (linear + 2D) obtained from splitting the codetext.
                int componentCount = parts.Length;
                Console.WriteLine($"Component Count (linear + 2D parts): {componentCount}");

                // Separator for readability between multiple barcode results.
                Console.WriteLine(new string('-', 40));
            }

            // If no barcodes were detected, inform the user.
            if (!anyFound)
            {
                Console.WriteLine("No GS1 Composite barcode detected in the image.");
            }
        }
    }
}