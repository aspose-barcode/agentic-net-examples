// Title: Read DataMatrix Symbol Size and Encoding Mode from a TIFF Image
// Description: Demonstrates how to load a TIFF file containing DataMatrix barcodes and attempt to retrieve barcode details using Aspose.BarCode's recognition API.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating the use of BarCodeReader with DecodeType.DataMatrix to extract information from images. Developers often need to read barcode data from scanned documents or multi-page TIFFs, and typical use cases involve retrieving the encoded text and barcode type. While the API provides basic recognition results, advanced properties such as symbol size (version) and encoding mode are only available during generation.
// Prompt: Read DataMatrix symbol size and encoding mode from a TIFF image with DataMatrix barcodes.
// Tags: datamatrix, barcode, recognition, tiff, aspnet, aspnetcore, aspose.barcode, symbolsize, encodingmode

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that reads DataMatrix barcodes from a TIFF file and displays basic recognition results.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Loads the TIFF, checks existence, reads DataMatrix barcodes, and prints code text and type.
    /// </summary>
    static void Main()
    {
        // Build the full path to the sample TIFF file located in the current directory
        string tiffPath = Path.Combine(Directory.GetCurrentDirectory(), "sample.tif");

        // Verify that the file exists before attempting to read it
        if (!File.Exists(tiffPath))
        {
            Console.WriteLine($"File not found: {tiffPath}");
            return;
        }

        // Initialize a BarCodeReader configured to detect only DataMatrix barcodes
        using (BarCodeReader reader = new BarCodeReader(tiffPath, DecodeType.DataMatrix))
        {
            // Perform the recognition and retrieve all detected barcodes
            BarCodeResult[] results = reader.ReadBarCodes();

            // If no barcodes were found, inform the user and exit
            if (results.Length == 0)
            {
                Console.WriteLine("No DataMatrix barcode detected.");
                return;
            }

            // Iterate through each recognized barcode and display its details
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"CodeType: {result.CodeTypeName}");

                // Note: The Aspose.BarCode API does not expose DataMatrix symbol size (version) or
                // encoding mode in the recognition results. These details are only available
                // during generation via generator.Parameters.Barcode.DataMatrix.Version and
                // generator.Parameters.Barcode.DataMatrix.EncodeMode.
                Console.WriteLine("Symbol size and encoding mode are not available via the reader API.");
                Console.WriteLine();
            }
        }
    }
}