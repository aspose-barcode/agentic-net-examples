using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates how to read Aztec barcodes from an image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Accepts an optional command‑line argument specifying the image file path.
    /// </summary>
    /// <param name="args">Command‑line arguments.</param>
    static void Main(string[] args)
    {
        // Determine the image path: use the first argument if provided, otherwise default to "aztec.png".
        string imagePath = args.Length > 0 ? args[0] : "aztec.png";

        // Verify that the specified file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Initialize a BarCodeReader configured to decode only Aztec barcodes.
        using (var reader = new BarCodeReader(imagePath, DecodeType.Aztec))
        {
            // Read all barcodes found in the image.
            var results = reader.ReadBarCodes();

            // If no barcodes were detected, inform the user and exit.
            if (results.Length == 0)
            {
                Console.WriteLine("No Aztec barcode detected in the image.");
                return;
            }

            // Iterate through each detected barcode and display its details.
            foreach (var result in results)
            {
                Console.WriteLine($"Barcode type: {result.CodeTypeName}");
                Console.WriteLine($"Codetext   : {result.CodeText}");

                // Note: The Aspose.BarCode API does not expose layer count or compact mode for Aztec barcodes.
                // Inform the user that this information is unavailable.
                Console.WriteLine("Layer count and compact mode information are not accessible through the current Aspose.BarCode API.");
            }
        }
    }
}