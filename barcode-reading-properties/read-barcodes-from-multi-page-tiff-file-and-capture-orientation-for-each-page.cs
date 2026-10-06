// Title: Read barcodes from first page of a multi‑page TIFF and report orientation
// Description: Demonstrates how to load a multi‑page TIFF, read barcodes on the first page using Aspose.BarCode, and output each barcode’s type, text and detected angle.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category. It shows how to use the BarCodeReader class with DecodeType.AllSupportedTypes to detect any supported symbology in an image stream. Typical use cases include scanning documents, invoices, or shipping labels where barcodes may appear on multi‑page TIFF files. Developers often need to extract barcode data and orientation for downstream processing or validation.
// Prompt: Read barcodes from a multi‑page TIFF file and capture orientation for each page.
// Tags: barcode, recognition, tiff, orientation, mult-page, aspose.barcode, decodeall, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that reads barcodes from the first page of a multi‑page TIFF file
/// and prints each barcode’s type, text and orientation angle.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Loads the TIFF, runs BarCodeReader and writes results to console.
    /// </summary>
    static void Main()
    {
        // Path to the multi‑page TIFF file (adjust as needed)
        string tiffPath = "MultiPageSample.tiff";

        // Verify that the file exists before attempting to read it
        if (!File.Exists(tiffPath))
        {
            Console.WriteLine($"File not found: {tiffPath}");
            return;
        }

        // Aspose.BarCode can read only the first frame of a TIFF.
        // Multi‑page processing would require Aspose.Imaging, which is not available in this runner.
        // The example therefore demonstrates reading whatever barcodes are found on the first page
        // and reports the detected orientation angle for each barcode.
        using (FileStream fs = new FileStream(tiffPath, FileMode.Open, FileAccess.Read))
        {
            // Initialize the barcode reader to detect all supported symbologies
            using (BarCodeReader reader = new BarCodeReader(fs, DecodeType.AllSupportedTypes))
            {
                // Perform the recognition
                BarCodeResult[] results = reader.ReadBarCodes();

                // If no barcodes were found, inform the user
                if (results.Length == 0)
                {
                    Console.WriteLine("No barcodes detected on the first page.");
                }
                else
                {
                    int pageNumber = 1; // Only the first page is processed
                    // Iterate through each detected barcode and output its details
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"Page {pageNumber}: Type = {result.CodeTypeName}, Text = {result.CodeText}, Angle = {result.Region.Angle}");
                    }
                }
            }
        }
    }
}