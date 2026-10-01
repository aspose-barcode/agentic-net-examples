// Title: Read barcodes from each page of a multi‑page TIFF and capture orientation
// Description: Demonstrates loading a multi‑page TIFF, iterating through its pages, detecting all barcodes on each page, and retrieving the orientation angle of each detected barcode.
// Category-Description: This example belongs to the Aspose.BarCode reading category, showcasing how to use Aspose.BarCode.BarCodeRecognition.BarCodeReader together with Aspose.Drawing to process multi‑frame image formats (e.g., TIFF). Typical scenarios include inventory scanning, document processing, and quality control where each page may contain one or more barcodes and their orientation is required for downstream handling. Developers often need to extract barcode data and orientation from each frame of a multi‑page image using the BarCodeReader API.
// Prompt: Read barcodes from a multi‑page TIFF file and capture orientation for each page.
// Tags: barcode, reading, tiff, orientation, multiframe, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that reads barcodes from a multi‑page TIFF file and reports each barcode's orientation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Loads the TIFF, iterates pages, reads barcodes, and prints results.
    /// </summary>
    static void Main()
    {
        // Path to the multi‑page TIFF file (placed in the system temporary folder).
        string tiffPath = Path.Combine(Path.GetTempPath(), "sample_multi_page.tif");

        // Verify that the file exists before attempting to read it.
        if (!File.Exists(tiffPath))
        {
            Console.WriteLine($"TIFF file not found at: {tiffPath}");
            Console.WriteLine("Place a multi‑page TIFF containing barcodes at the above location and rerun.");
            return;
        }

        // Load the TIFF image using Aspose.Drawing.
        using (Image tiffImage = Image.FromFile(tiffPath))
        {
            // Determine the number of pages (frames) in the TIFF.
            int pageCount = tiffImage.GetFrameCount(FrameDimension.Page);
            Console.WriteLine($"TIFF contains {pageCount} page(s).");

            // Iterate through each page of the TIFF.
            for (int pageIndex = 0; pageIndex < pageCount; pageIndex++)
            {
                // Activate the current page so it can be processed.
                tiffImage.SelectActiveFrame(FrameDimension.Page, pageIndex);

                // Save the active frame to a memory stream in PNG format (BarCodeReader works with common image formats).
                using (MemoryStream pageStream = new MemoryStream())
                {
                    tiffImage.Save(pageStream, ImageFormat.Png);
                    pageStream.Position = 0; // Reset stream position for reading.

                    // Create a barcode reader that attempts to decode all supported barcode types.
                    using (BarCodeReader reader = new BarCodeReader(pageStream, DecodeType.AllSupportedTypes))
                    {
                        // Read all barcodes present on the current page.
                        BarCodeResult[] results = reader.ReadBarCodes();

                        if (results.Length == 0)
                        {
                            Console.WriteLine($"Page {pageIndex + 1}: No barcode detected.");
                        }
                        else
                        {
                            // Output each detected barcode's text, type, and orientation angle.
                            foreach (BarCodeResult result in results)
                            {
                                double orientation = result.Region.Angle; // Angle in degrees.
                                Console.WriteLine($"Page {pageIndex + 1}: Detected barcode '{result.CodeText}' " +
                                                  $"of type {result.CodeTypeName}, orientation {orientation} degrees.");
                            }
                        }
                    }
                }
            }
        }
    }
}