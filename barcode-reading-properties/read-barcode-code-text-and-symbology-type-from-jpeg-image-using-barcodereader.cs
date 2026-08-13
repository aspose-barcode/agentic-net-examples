// Title: Read barcode text and symbology from a JPEG image using BarCodeReader
// Description: Demonstrates how to load a JPEG file, detect barcodes, and output their symbology type and decoded text.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing the BarCodeReader class for scanning images. It covers typical use cases such as extracting information from product labels, documents, or inventory images. Developers often need to read multiple symbologies from various image formats, and this snippet illustrates the straightforward approach using Aspose.BarCode APIs.
// Prompt: Read barcode code text and symbology type from a JPEG image using BarCodeReader.
// Tags: barcode, symbology, read, jpeg, aspose.barcode, barcodereader

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates reading barcode text and symbology type from a JPEG image using Aspose.BarCode's BarCodeReader.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Scans the specified JPEG image for all supported barcode types and prints each result.
    /// </summary>
    static void Main()
    {
        // Path to the JPEG image containing the barcode.
        string imagePath = "barcode.jpg";

        // Verify that the image file exists before attempting to read.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Initialize BarCodeReader to scan the image for every supported symbology.
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Iterate through all detected barcodes in the image.
            foreach (var result in reader.ReadBarCodes())
            {
                // Output the detected symbology name and its decoded text.
                Console.WriteLine($"Symbology: {result.CodeTypeName}, CodeText: {result.CodeText}");
            }
        }
    }
}