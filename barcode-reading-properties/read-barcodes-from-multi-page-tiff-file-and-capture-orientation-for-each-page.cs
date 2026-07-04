// Title: Read barcodes from each page of a multi‑page TIFF and report orientation
// Description: Demonstrates loading a multi‑page TIFF, iterating through its pages, reading any barcodes present, and outputting the barcode type, text, and rotation angle for each page.
// Prompt: Read barcodes from a multi‑page TIFF file and capture orientation for each page.
// Tags: barcode, tiff, orientation, multiframe, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Sample console application that reads barcodes from a multi‑page TIFF file
/// and prints each barcode's type, text, and orientation (angle) per page.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Loads the TIFF, iterates through its pages, and uses Aspose.BarCode to detect barcodes.
    /// </summary>
    static void Main()
    {
        // Path to the multi‑page TIFF file (adjust as needed)
        const string tiffPath = "sample.tif";

        // Verify that the file exists before attempting to load it
        if (!File.Exists(tiffPath))
        {
            Console.WriteLine($"File not found: {tiffPath}");
            return;
        }

        // Load the TIFF image using Aspose.Drawing
        using (Image tiffImage = Image.FromFile(tiffPath))
        {
            // Determine the number of pages/frames in the TIFF
            int pageCount = tiffImage.GetFrameCount(FrameDimension.Page);

            // Iterate over each page in the TIFF
            for (int pageIndex = 0; pageIndex < pageCount; pageIndex++)
            {
                // Select the current page as the active frame
                tiffImage.SelectActiveFrame(FrameDimension.Page, pageIndex);

                // Create a bitmap representation of the current page for barcode scanning
                using (Bitmap pageBitmap = new Bitmap(tiffImage))
                {
                    // Initialize the barcode reader to detect all supported barcode types
                    using (BarCodeReader reader = new BarCodeReader(pageBitmap, DecodeType.AllSupportedTypes))
                    {
                        // Optional: set quality settings (default is NormalQuality)
                        // reader.QualitySettings = QualitySettings.NormalQuality;

                        // Perform the barcode detection
                        BarCodeResult[] results = reader.ReadBarCodes();

                        // Output results for the current page
                        if (results.Length == 0)
                        {
                            Console.WriteLine($"Page {pageIndex + 1}: No barcodes detected.");
                        }
                        else
                        {
                            foreach (BarCodeResult result in results)
                            {
                                // Retrieve the orientation angle of the detected barcode region
                                double orientation = result.Region.Angle; // orientation in degrees

                                // Print barcode details including type, text, and orientation
                                Console.WriteLine($"Page {pageIndex + 1}: Type = {result.CodeTypeName}, Text = {result.CodeText}, Orientation = {orientation}°");
                            }
                        }
                    }
                }
            }
        }
    }
}