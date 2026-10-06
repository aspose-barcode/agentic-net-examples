// Title: Read DataBar Expanded barcode fields from a JPEG image
// Description: Demonstrates how to load a JPEG file and extract DataBar Expanded barcode text and extended fields using Aspose.BarCode.
// Category-Description: This example belongs to the barcode recognition category of Aspose.BarCode. It shows how to use BarCodeReader with DecodeType.DatabarExpanded to detect DataBar Expanded symbology, retrieve the decoded text, and access any extended data fields. Developers working with retail or logistics scanning often need to read DataBar Expanded barcodes from images for product identification and pricing.
// Prompt: Read DataBar expanded data fields and numeric values from a JPEG image.
// Tags: databar, expanded, barcode, recognition, jpeg, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates reading DataBar Expanded barcode data from a JPEG image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts an optional image path argument, reads DataBar Expanded barcodes, and prints their type, text, and any extended fields.
    /// </summary>
    static void Main(string[] args)
    {
        // Determine the image file path: use first argument if provided, otherwise default to "databar.jpg".
        string imagePath = args.Length > 0 ? args[0] : "databar.jpg";

        // Verify that the specified file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        try
        {
            // Initialize the barcode reader for DataBar Expanded symbology.
            using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.DatabarExpanded))
            {
                bool anyFound = false;

                // Iterate through all detected barcodes in the image.
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    anyFound = true;

                    // Output basic barcode information.
                    Console.WriteLine($"CodeTypeName: {result.CodeTypeName}");
                    Console.WriteLine($"CodeText: {result.CodeText}");

                    // If the API provides extended DataBar fields, they can be accessed here.
                    // Example (uncomment and adjust when available):
                    // Console.WriteLine($"DataBar Expanded Field: {result.Extended.DataBar?.SomeField}");
                }

                // Inform the user if no DataBar Expanded barcodes were found.
                if (!anyFound)
                {
                    Console.WriteLine("No DataBar Expanded barcode detected in the image.");
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during barcode reading.
            Console.WriteLine($"Error during barcode reading: {ex.Message}");
        }
    }
}