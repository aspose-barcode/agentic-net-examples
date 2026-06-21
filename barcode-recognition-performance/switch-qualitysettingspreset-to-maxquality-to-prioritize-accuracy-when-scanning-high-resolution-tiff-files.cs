using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode detection in a TIFF image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Accepts an optional command‑line argument specifying the path to a TIFF file.
    /// If no argument is provided, a default file name "sample.tiff" is used.
    /// The method reads the image, detects all supported barcodes, and prints their type and text.
    /// </summary>
    /// <param name="args">Command‑line arguments.</param>
    static void Main(string[] args)
    {
        // Determine the TIFF file path (command‑line argument or default sample)
        string tiffPath = args.Length > 0 ? args[0] : "sample.tiff";

        // Verify that the file exists before attempting to read it
        if (!File.Exists(tiffPath))
        {
            Console.WriteLine($"File not found: {tiffPath}");
            return;
        }

        // Initialize a BarCodeReader for the TIFF image, requesting all supported barcode types
        using (var reader = new BarCodeReader(tiffPath, DecodeType.AllSupportedTypes))
        {
            // Set the reader to use the highest quality settings for maximum detection accuracy
            reader.QualitySettings = QualitySettings.MaxQuality;

            // Read all barcodes present in the image
            var results = reader.ReadBarCodes();

            // Check whether any barcodes were found
            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes were detected.");
            }
            else
            {
                // Iterate through each detected barcode and output its details
                foreach (var result in results)
                {
                    Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                    Console.WriteLine($"Code Text: {result.CodeText}");
                    Console.WriteLine();
                }
            }
        }
    }
}