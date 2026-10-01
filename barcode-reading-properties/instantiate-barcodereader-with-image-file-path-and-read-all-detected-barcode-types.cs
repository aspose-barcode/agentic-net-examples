// Title: Read All Barcode Types from an Image Using BarCodeReader
// Description: Demonstrates how to instantiate Aspose.BarCode.BarCodeReader with an image file path and read every supported barcode symbology present in the image.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing the use of BarCodeReader, BarCodeResult, and DecodeType to detect and decode barcodes in images. Typical scenarios include scanning documents, receipts, or product labels where multiple barcode types may appear. Developers often need a quick way to extract all barcode data without specifying individual symbologies, and this snippet provides a ready‑to‑run pattern for such use cases.
// Prompt: Instantiate BarCodeReader with an image file path and read all detected barcode types.
// Tags: barcode symbology, read, all types, aspose.barcode, barcodereader, decode, image, console

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that reads all supported barcode types from an image file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Loads an image, checks existence, reads all barcodes, and prints results to console.
    /// </summary>
    static void Main()
    {
        // Path to the image containing barcodes – replace with a real file path as needed.
        string imagePath = "sample_barcode.png";

        // Ensure the file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Use DecodeType.AllSupportedTypes to detect any barcode symbology.
        BaseDecodeType decodeType = DecodeType.AllSupportedTypes;

        // BarCodeReader implements IDisposable – use a using block to guarantee proper resource cleanup.
        using (BarCodeReader reader = new BarCodeReader(imagePath, decodeType))
        {
            // Read all barcodes from the image.
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes detected.");
            }
            else
            {
                // Iterate through each detected barcode and output its details.
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"Code Text   : {result.CodeText}");
                    Console.WriteLine($"Symbology   : {result.CodeTypeName}");
                    Console.WriteLine($"Quality     : {result.ReadingQuality}");
                    Console.WriteLine(new string('-', 30));
                }
            }
        }
    }
}