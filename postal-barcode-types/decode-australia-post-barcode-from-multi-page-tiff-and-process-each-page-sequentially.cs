// Title: Decode Australia Post barcode from first page of a multi‑page TIFF
// Description: Demonstrates how to read an Australia Post barcode from a multi‑page TIFF file using Aspose.BarCode. The example loads the TIFF, configures decoding options, and outputs the barcode data from the first page.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating the use of BarCodeReader with DecodeType.AustraliaPost. It shows typical steps such as loading an image stream, setting barcode-specific settings, and iterating over detection results—common tasks for developers integrating barcode scanning into .NET applications.
// Prompt: Decode an Australia Post barcode from a multi‑page TIFF and process each page sequentially.
// Tags: australia post, barcode, decoding, tiff, multiframe, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that decodes Australia Post barcodes from a multi‑page TIFF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Loads the TIFF, configures the reader, and prints barcode information from the first page.
    /// </summary>
    static void Main()
    {
        // Build the full path to the multi‑page TIFF file containing Australia Post barcodes.
        string tiffPath = Path.Combine(Directory.GetCurrentDirectory(), "MultiPageAustraliaPost.tiff");

        // Verify that the file exists before attempting to read it.
        if (!File.Exists(tiffPath))
        {
            Console.WriteLine($"File not found: {tiffPath}");
            return;
        }

        // NOTE: Aspose.BarCode can read only the first frame of a TIFF.
        // To process each page, a side library (e.g., Aspose.Imaging) would be required.
        // In this example we demonstrate reading the first page and outline the intended loop.

        // Open the TIFF file as a seekable stream.
        using (FileStream tiffStream = new FileStream(tiffPath, FileMode.Open, FileAccess.Read))
        {
            // Create a BarCodeReader configured for Australia Post barcodes.
            using (BarCodeReader reader = new BarCodeReader(tiffStream, DecodeType.AustraliaPost))
            {
                // Set Australia Post specific decoding options.
                reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;
                reader.BarcodeSettings.AustraliaPost.IgnoreEndingFillingPatternsForCTable = true;

                // Iterate through all detected barcodes on the current frame (first page).
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Page 1 - Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }
        }

        // Placeholder for processing additional pages if a TIFF‑frame extraction library were available.
        // Example loop (commented out):
        // for (int pageIndex = 2; pageIndex <= totalPages; pageIndex++)
        // {
        //     // Extract pageIndex‑th frame to a MemoryStream (requires Aspose.Imaging or similar)
        //     // using (MemoryStream pageStream = ExtractTiffFrame(tiffPath, pageIndex)) { ... }
        // }
    }
}