// Title: Retrieve DotCode version and error correction level
// Description: Demonstrates how to scan a DotCode barcode and attempt to obtain its version and error correction level, handling cases where the API does not expose these details.
// Prompt: Obtain DotCode version information and error correction level from a scanned DotCode barcode.
// Tags: dotcode, barcode, version, error correction, aspose.barcode, barcoderecognition

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that reads a DotCode barcode from an image and tries to
/// extract version information and error correction level using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Scans the specified image for DotCode barcodes and reports available details.
    /// </summary>
    static void Main()
    {
        // Path to the image containing a DotCode barcode.
        const string imagePath = "dotcode_sample.png";

        // Verify that the image file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Initialize a BarCodeReader configured for DotCode symbology.
        using (var reader = new BarCodeReader(imagePath, DecodeType.DotCode))
        {
            // Iterate through all detected barcodes in the image.
            foreach (var result in reader.ReadBarCodes())
            {
                // Output basic barcode information.
                Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                Console.WriteLine($"BarCode CodeText: {result.CodeText}");

                // Attempt to retrieve extended DotCode parameters (version, error correction level).
                // The current Aspose.BarCode API does not expose these fields.
                var dotExt = result.Extended?.DotCode;
                if (dotExt != null)
                {
                    Console.WriteLine("DotCode version information: not available via Aspose.BarCode API.");
                    Console.WriteLine("DotCode error correction level: not available via Aspose.BarCode API.");
                }
                else
                {
                    // No extended parameters were provided for this barcode.
                    Console.WriteLine("No DotCode extended parameters were found.");
                }
            }
        }
    }
}