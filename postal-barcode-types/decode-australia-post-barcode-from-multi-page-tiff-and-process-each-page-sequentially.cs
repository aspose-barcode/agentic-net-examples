// Title: Decode Australia Post barcodes from a multi‑page TIFF
// Description: Demonstrates how to read and decode Australia Post barcodes embedded in each page of a multi‑page TIFF image.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating the use of BarCodeReader with DecodeType.AustraliaPost. It shows configuring Australia Post specific settings, processing multi‑page image files, and extracting barcode data per page—common tasks for developers handling postal automation and document scanning workflows.
// Prompt: Decode an Australia Post barcode from a multi‑page TIFF and process each page sequentially.
// Tags: australia post, barcode decoding, multipage tiff, barcodereader, aspnet.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that decodes Australia Post barcodes from each page of a multi‑page TIFF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts an optional file path argument, reads the TIFF, and outputs barcode information per page.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument may specify the TIFF file path.</param>
    static void Main(string[] args)
    {
        // Determine the TIFF file path: use first argument if provided, otherwise default name.
        string tiffPath = args.Length > 0 ? args[0] : "MultiPageAustraliaPost.tiff";

        // Verify that the specified file exists before attempting to read.
        if (!File.Exists(tiffPath))
        {
            Console.WriteLine($"File not found: {tiffPath}");
            return;
        }

        // Initialize the barcode reader for Australia Post symbology on the multi‑page TIFF.
        using (BarCodeReader reader = new BarCodeReader(tiffPath, DecodeType.AustraliaPost))
        {
            // Configure Australia Post specific recognition settings.
            reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;
            reader.BarcodeSettings.AustraliaPost.IgnoreEndingFillingPatternsForCTable = true;

            // Read all barcodes from the document; each result corresponds to a page.
            BarCodeResult[] results = reader.ReadBarCodes();

            // If no barcodes were detected, inform the user.
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
            }
            else
            {
                // Iterate through each result, outputting page number and barcode details.
                int page = 1;
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"Page {page}:");
                    Console.WriteLine($"  Barcode type: {result.CodeTypeName}");
                    Console.WriteLine($"  Barcode data: {result.CodeText}");
                    page++;
                }
            }
        }
    }
}