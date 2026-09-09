// Title: Read barcodes from each page of a multi‑page TIFF and capture orientation
// Description: Demonstrates how to open a multi‑page TIFF, extract each page as PNG, read all supported barcodes, and obtain the rotation angle of each barcode region.
// Category-Description: This example belongs to the Aspose.BarCode barcode‑recognition category. It shows how to use Aspose.Drawing to work with multi‑frame images and Aspose.BarCode.BarCodeRecognition's BarCodeReader to detect barcodes of any supported symbology. Typical use cases include processing scanned documents, invoices, or shipping labels stored as multi‑page TIFFs where each page may contain barcodes at arbitrary orientations.
// Prompt: Read barcodes from a multi‑page TIFF file and capture orientation for each page.
// Tags: barcode, read, tiff, orientation, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that reads barcodes from each page of a multi‑page TIFF file
/// and outputs the barcode type, text, and orientation angle.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Build the full path to the multi‑page TIFF file located in the current directory.
        string tiffPath = Path.Combine(Directory.GetCurrentDirectory(), "MultiPageTiffWithBarcodes.tiff");

        // Verify that the file exists before attempting to process it.
        if (!File.Exists(tiffPath))
        {
            Console.WriteLine($"File not found: {tiffPath}");
            return;
        }

        // Load the TIFF image using Aspose.Drawing.
        using (Image tiffImage = Image.FromFile(tiffPath))
        {
            // Determine how many pages (frames) the TIFF contains.
            int pageCount = tiffImage.GetFrameCount(FrameDimension.Page);

            // Iterate through each page of the TIFF.
            for (int pageIndex = 0; pageIndex < pageCount; pageIndex++)
            {
                Console.WriteLine($"--- Page {pageIndex + 1} ---");

                // Activate the current page so it can be processed.
                tiffImage.SelectActiveFrame(FrameDimension.Page, pageIndex);

                // Convert the active page to PNG format and store it in a memory stream.
                using (var ms = new MemoryStream())
                {
                    tiffImage.Save(ms, ImageFormat.Png);
                    ms.Position = 0; // Reset stream position for reading.

                    // Initialize the barcode reader to detect all supported barcode types.
                    using (var reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
                    {
                        // Read all barcodes found on the current page.
                        foreach (var result in reader.ReadBarCodes())
                        {
                            // Output barcode details, including orientation angle.
                            Console.WriteLine($"Type: {result.CodeTypeName}");
                            Console.WriteLine($"Text: {result.CodeText}");
                            Console.WriteLine($"Orientation (degrees): {result.Region.Angle}");
                            Console.WriteLine();
                        }
                    }
                }
            }
        }
    }
}