// Title: Read barcode text and symbology from JPEG using BarCodeReader
// Description: Demonstrates how to load a JPEG image, detect all supported barcodes, and output their symbology type and decoded text.
// Prompt: Read barcode code text and symbology type from a JPEG image using BarCodeReader.
// Tags: barcode, symbology, read, jpeg, aspose, barcodereader

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that reads barcode information from a JPEG image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Scans the specified image (or a default one) for barcodes
    /// and prints each barcode's symbology type and decoded text to the console.
    /// </summary>
    /// <param name="args">Optional command‑line arguments; the first argument can specify the image path.</param>
    static void Main(string[] args)
    {
        // Determine the image file to process; allow override via first command‑line argument.
        string imagePath = args.Length > 0 ? args[0] : "barcode.jpg";

        // Verify that the image file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Initialize a BarCodeReader that will attempt to decode all supported barcode types.
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Loop through each detected barcode and output its details.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Symbology: {result.CodeTypeName}");
                Console.WriteLine($"Code Text : {result.CodeText}");
                Console.WriteLine();
            }
        }
    }
}