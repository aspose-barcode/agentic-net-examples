// Title: Read DataMatrix Symbol Size and Encoding Mode from TIFF
// Description: Demonstrates how to load a TIFF image containing DataMatrix barcodes, iterate through detected symbols, and attempt to retrieve symbol size and encoding mode (which are not exposed by the API).
// Prompt: Read DataMatrix symbol size and encoding mode from a TIFF image with DataMatrix barcodes.
// Tags: datamatrix, barcode, recognition, tiff, aspose.barcode, aspnet

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that reads DataMatrix barcodes from a TIFF image
/// and displays available recognition information.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Path to the TIFF image containing DataMatrix barcodes
        string imagePath = "datamatrix.tiff";

        // Verify that the file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Create a BarCodeReader configured for DataMatrix symbology
        using (var reader = new BarCodeReader(imagePath, DecodeType.DataMatrix))
        {
            // Iterate through all detected barcodes in the image
            foreach (var result in reader.ReadBarCodes())
            {
                // Output basic barcode information
                Console.WriteLine($"Detected barcode type: {result.CodeTypeName}");
                Console.WriteLine($"Code text: {result.CodeText}");

                // Retrieve DataMatrix‑specific extended parameters
                var dmExt = result.Extended.DataMatrix;

                // Symbol size (version) and encoding mode are not exposed directly
                // via the public recognition API. We report their unavailability.
                Console.WriteLine("Symbol size (version): not available via recognition API");
                Console.WriteLine("Encoding mode: not available via recognition API");

                // Additional DataMatrix flags that are available
                Console.WriteLine($"Is Reader Programming: {dmExt.IsReaderProgramming}");
                Console.WriteLine($"Structured Append Barcode ID: {dmExt.StructuredAppendBarcodeId}");
                Console.WriteLine($"Structured Append Barcodes Count: {dmExt.StructuredAppendBarcodesCount}");
                Console.WriteLine($"Structured Append File ID: {dmExt.StructuredAppendFileId}");
                Console.WriteLine();
            }
        }
    }
}