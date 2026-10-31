// Title: Read barcodes from a JPEG image using Aspose.BarCodeReader
// Description: Demonstrates how to load a JPEG file, detect all supported barcode symbologies, and output each barcode's type, text, and location.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating the use of BarCodeReader with the DecodeType.AllSupportedTypes enumeration. Developers commonly use these APIs to batch‑process images, extract barcode data, and obtain geometric information for further processing such as cropping or overlay. The snippet shows typical file existence checks, resource handling with using, and iteration over BarCodeResult objects.
// Prompt: Read barcodes from a JPEG file using BarCodeReader constructor and retrieve detection results.
// Tags: barcode, jpeg, read, detection, aspose.barcode, decode, region, rectangle

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates reading barcodes from a JPEG file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Loads the image, checks its existence, reads all supported barcodes, and prints details.
    /// </summary>
    static void Main()
    {
        // Path to the JPEG image
        string imagePath = "sample.jpg";

        // Verify that the file exists before attempting to read
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Initialize BarCodeReader for all supported barcode types
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Read all barcodes present in the image
            BarCodeResult[] results = reader.ReadBarCodes();

            // If no barcodes were detected, inform the user
            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes detected.");
            }
            else
            {
                // Iterate through each detected barcode and output its details
                foreach (BarCodeResult result in results)
                {
                    // Print barcode type and decoded text
                    Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");

                    // Retrieve the bounding rectangle of the barcode region
                    var bounds = result.Region.Rectangle;

                    // Print region coordinates, size, and rotation angle
                    Console.WriteLine($"Region - X:{bounds.X}, Y:{bounds.Y}, Width:{bounds.Width}, Height:{bounds.Height}, Angle:{result.Region.Angle}");
                }
            }
        }
    }
}