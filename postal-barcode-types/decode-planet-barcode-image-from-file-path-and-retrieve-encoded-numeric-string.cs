using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates reading a Planet barcode from an image file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Reads a barcode from the specified image file or a default sample.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument may contain the image file path.</param>
    static void Main(string[] args)
    {
        // Determine the image file path: use first argument if provided, otherwise fallback to a sample name.
        string imagePath = args.Length > 0 ? args[0] : "planet.png";

        // Verify that the file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            // Inform the user that the specified file could not be found and exit.
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Initialize a BarCodeReader for the Planet symbology using the image file.
        using (var reader = new BarCodeReader(imagePath, DecodeType.Planet))
        {
            // Read all barcodes found in the image (expected at most one for this example).
            foreach (var result in reader.ReadBarCodes())
            {
                // Output the decoded text of each detected Planet barcode.
                Console.WriteLine($"Decoded Planet barcode: {result.CodeText}");
            }
        }
    }
}