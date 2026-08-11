// Title: Decode Australia Post Barcodes from Multi‑Page TIFF
// Description: Demonstrates loading a multi‑page TIFF, iterating through each page, and decoding Australia Post barcodes using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category. It shows how to use Aspose.Drawing to handle multi‑frame TIFF images and Aspose.BarCode.BarCodeRecognition's BarCodeReader with DecodeType.AustraliaPost to extract barcode data. Typical use cases include processing scanned shipping documents, batch‑scanning postal forms, or automating data entry from multi‑page image files. Developers often need to read each frame, create a Bitmap, and invoke the reader to obtain barcode type and text.
// Prompt: Decode an Australia Post barcode from a multi‑page TIFF and process each page sequentially.
// Tags: australia post, barcode, decoding, tiff, multiframe, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that decodes Australia Post barcodes from each page of a multi‑page TIFF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Loads the TIFF, iterates pages, and prints decoded barcode information.
    /// </summary>
    static void Main()
    {
        // Path to the multi‑page TIFF containing Australia Post barcodes.
        string tiffPath = "AustraliaPost.tif";

        // Verify that the file exists before attempting to load it.
        if (!File.Exists(tiffPath))
        {
            Console.WriteLine($"File not found: {tiffPath}");
            return;
        }

        // Load the TIFF image. Aspose.Drawing.Image supports multi‑frame TIFFs.
        using (Image tiffImage = Image.FromFile(tiffPath))
        {
            // Determine how many pages (frames) the TIFF contains.
            int pageCount = tiffImage.GetFrameCount(FrameDimension.Page);
            Console.WriteLine($"TIFF contains {pageCount} page(s).");

            // Process each page sequentially.
            for (int pageIndex = 0; pageIndex < pageCount; pageIndex++)
            {
                // Activate the current frame.
                tiffImage.SelectActiveFrame(FrameDimension.Page, pageIndex);

                // Clone the active frame into a Bitmap for barcode reading.
                using (Bitmap frameBitmap = (Bitmap)tiffImage.Clone())
                {
                    // Create a BarCodeReader for Australia Post symbology.
                    using (BarCodeReader reader = new BarCodeReader(frameBitmap, DecodeType.AustraliaPost))
                    {
                        // Optional: set the interpreting type for customer information if needed.
                        // reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;

                        // Perform the recognition.
                        BarCodeResult[] results = reader.ReadBarCodes();

                        Console.WriteLine($"Page {pageIndex + 1}: Detected {results.Length} barcode(s).");
                        foreach (BarCodeResult result in results)
                        {
                            Console.WriteLine($"  Type    : {result.CodeType}");
                            Console.WriteLine($"  CodeText: {result.CodeText}");
                        }
                    }
                }
            }
        }

        Console.WriteLine("Processing completed.");
    }
}