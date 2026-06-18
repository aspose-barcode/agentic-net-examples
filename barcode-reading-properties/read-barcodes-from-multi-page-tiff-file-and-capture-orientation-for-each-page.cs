using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to read barcodes from each page of a multi‑page TIFF file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Loads a TIFF file, iterates through its pages, and prints detected barcode information.
    /// </summary>
    static void Main()
    {
        // Path to the multi‑page TIFF file
        string tiffPath = "sample.tiff";

        // Verify that the file exists before attempting to load it
        if (!File.Exists(tiffPath))
        {
            Console.WriteLine($"File not found: {tiffPath}");
            return;
        }

        // Load the TIFF image from file; the using statement ensures proper disposal
        using (Image multiPageImage = Image.FromFile(tiffPath))
        {
            // Determine how many pages (frames) the TIFF contains
            int pageCount = multiPageImage.GetFrameCount(FrameDimension.Page);

            // Loop through each page in the TIFF
            for (int pageIndex = 0; pageIndex < pageCount; pageIndex++)
            {
                // Select the current page as the active frame for processing
                multiPageImage.SelectActiveFrame(FrameDimension.Page, pageIndex);

                // Create a bitmap representation of the active page
                using (Bitmap pageBitmap = new Bitmap(multiPageImage))
                {
                    // Initialize the barcode reader to detect all supported barcode types
                    using (BarCodeReader reader = new BarCodeReader(pageBitmap, DecodeType.AllSupportedTypes))
                    {
                        bool barcodeFound = false;

                        // Iterate over all barcodes detected on the current page
                        foreach (var result in reader.ReadBarCodes())
                        {
                            barcodeFound = true;
                            double orientation = result.Region.Angle; // Orientation angle in degrees
                            Console.WriteLine($"Page {pageIndex + 1}: Type = {result.CodeTypeName}, Text = {result.CodeText}, Orientation = {orientation}°");
                        }

                        // If no barcodes were found, inform the user
                        if (!barcodeFound)
                        {
                            Console.WriteLine($"Page {pageIndex + 1}: No barcode detected.");
                        }
                    }
                }
            }
        }
    }
}