// Title: Read all barcodes from a TIFF image using a memory stream
// Description: Demonstrates loading a TIFF file into a MemoryStream and using Aspose.BarCode's BarCodeReader to detect and list every barcode found in the image.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, showcasing how to work with the BarCodeReader class together with DecodeType.AllSupportedTypes to process image data supplied via streams. Typical scenarios include batch processing of scanned documents, automated inventory checks, and any situation where barcode data must be extracted from TIFF files without writing temporary files to disk. Developers often need to read image bytes, create a stream, and iterate over BarCodeResult objects to obtain barcode type and text.
// Prompt: Pass a memory stream containing TIFF data to BarCodeReader and extract all detected barcode values.
// Tags: barcode, tiff, memorystream, barcodereader, decode, aspose.barcode, recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates reading a TIFF file into a memory stream and extracting all barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Accepts an optional file path argument; otherwise uses "sample.tiff".
    /// </summary>
    /// <param name="args">Command‑line arguments where the first argument can be a TIFF file path.</param>
    static void Main(string[] args)
    {
        // Determine the TIFF file path: use argument if provided, otherwise default to "sample.tiff".
        string tiffPath = args.Length > 0 ? args[0] : "sample.tiff";

        // Verify that the file exists before attempting to read it.
        if (!File.Exists(tiffPath))
        {
            Console.WriteLine($"File not found: {tiffPath}");
            return;
        }

        // Load the entire TIFF file into a byte array.
        byte[] tiffBytes = File.ReadAllBytes(tiffPath);

        // Wrap the byte array in a MemoryStream so BarCodeReader can consume it.
        using (MemoryStream ms = new MemoryStream(tiffBytes))
        {
            // Ensure the stream position is at the beginning.
            ms.Position = 0;

            // Create a BarCodeReader that scans for all supported barcode types.
            using (BarCodeReader reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
            {
                // Read all barcodes present in the image.
                BarCodeResult[] results = reader.ReadBarCodes();

                // Output the results or indicate that none were found.
                if (results.Length == 0)
                {
                    Console.WriteLine("No barcodes detected.");
                }
                else
                {
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
                    }
                }
            }
        }
    }
}