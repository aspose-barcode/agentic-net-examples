// Title: Read DataMatrix Symbol Size and Encoding Mode from a TIFF Image
// Description: Demonstrates loading a TIFF image containing DataMatrix barcodes, detecting them, and retrieving the barcode region (size). It also shows the current limitation regarding encoding mode retrieval via the Aspose.BarCode API.
// Category-Description: This example belongs to the barcode recognition category of Aspose.BarCode, illustrating how to use BarCodeReader to decode DataMatrix symbols from raster images. It covers image loading, specifying symbology, iterating over detection results, and extracting geometric information such as region bounds. Developers commonly use these techniques for locating barcodes in documents, analyzing symbol dimensions, or extracting additional metadata when available.
// Prompt: Read DataMatrix symbol size and encoding mode from a TIFF image with DataMatrix barcodes.
// Tags: datamatrix, barcode recognition, tiff, symbol size, encoding mode, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Provides an example that reads DataMatrix barcodes from a TIFF image,
/// outputs their text, symbology, region size, and notes the unavailability of encoding mode information.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Loads the image, detects DataMatrix barcodes,
    /// and prints details about each detected symbol.
    /// </summary>
    static void Main()
    {
        // Path to the TIFF image containing DataMatrix barcodes
        string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "datamatrix.tif");

        // Verify that the file exists
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Create a BarCodeReader for DataMatrix symbology
        using (var reader = new BarCodeReader(imagePath, DecodeType.DataMatrix))
        {
            // Read all barcodes from the image
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No DataMatrix barcode detected in the image.");
                return;
            }

            foreach (var result in results)
            {
                // Ensure the detected barcode is a DataMatrix
                if (!string.Equals(result.CodeTypeName, "DataMatrix", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                Console.WriteLine("=== DataMatrix Barcode Detected ===");
                Console.WriteLine($"Code Text : {result.CodeText}");
                Console.WriteLine($"Symbology : {result.CodeTypeName}");

                // Region (size) of the detected barcode in the image
                var bounds = result.Region.Rectangle;
                Console.WriteLine($"Region X      : {bounds.X}");
                Console.WriteLine($"Region Y      : {bounds.Y}");
                Console.WriteLine($"Region Width  : {bounds.Width}");
                Console.WriteLine($"Region Height : {bounds.Height}");

                // Encoding mode is not exposed via the Aspose.BarCode recognition API
                Console.WriteLine("Encoding Mode : Not available via API");
                Console.WriteLine();
            }
        }
    }
}