// Title: Read barcodes from a multi‑page TIFF and capture orientation per page
// Description: Demonstrates how to load a multi‑page TIFF, iterate through its pages, detect barcodes, and retrieve each barcode's orientation angle.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing the use of BarCodeReader, DecodeType, and image handling classes such as Image, Bitmap, and FrameDimension. Typical use cases include processing scanned documents, invoices, or multi‑page forms where barcodes may appear on any page and orientation information is required for downstream processing. Developers often need to extract barcode data and its rotation to correctly align or validate the content.
// Prompt: Read barcodes from a multi‑page TIFF file and capture orientation for each page.
// Tags: barcode, recognition, tiff, multiframe, orientation, aspose.barcode, decode type, image processing

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that reads barcodes from each page of a multi‑page TIFF file
/// and reports the barcode type, text, and orientation angle.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Loads the TIFF, iterates through its frames, and uses <see cref="BarCodeReader"/>
    /// to detect and report barcodes along with their orientation.
    /// </summary>
    static void Main()
    {
        // Path to the multi‑page TIFF file.
        string tiffPath = "sample.tiff";

        // Verify that the file exists before attempting to load it.
        if (!File.Exists(tiffPath))
        {
            Console.WriteLine($"File not found: {tiffPath}");
            return;
        }

        // Load the TIFF image from disk.
        using (Image tiffImage = Image.FromFile(tiffPath))
        {
            // Get the total number of pages (frames) in the TIFF.
            int pageCount = tiffImage.GetFrameCount(FrameDimension.Page);

            // Process each page sequentially.
            for (int pageIndex = 0; pageIndex < pageCount; pageIndex++)
            {
                // Activate the current page so it can be read.
                tiffImage.SelectActiveFrame(FrameDimension.Page, pageIndex);

                // Clone the active frame into a Bitmap, which BarCodeReader requires.
                using (Bitmap pageBitmap = new Bitmap(tiffImage))
                {
                    // Initialize the barcode reader.
                    using (BarCodeReader reader = new BarCodeReader())
                    {
                        // Configure the reader to attempt decoding all supported symbologies.
                        reader.BarCodeReadType = DecodeType.AllSupportedTypes;

                        // Provide the bitmap image to the reader.
                        reader.SetBarCodeImage(pageBitmap);

                        int barcodeCount = 0;

                        // Iterate over all detected barcodes on the current page.
                        foreach (var result in reader.ReadBarCodes())
                        {
                            barcodeCount++;

                            // Retrieve the orientation angle (in degrees) of the barcode region.
                            double orientation = result.Region.Angle;

                            Console.WriteLine(
                                $"Page {pageIndex + 1}, Barcode {barcodeCount}: " +
                                $"Type = {result.CodeTypeName}, " +
                                $"Text = {result.CodeText}, " +
                                $"Orientation = {orientation}°");
                        }

                        // If no barcodes were found, inform the user.
                        if (barcodeCount == 0)
                        {
                            Console.WriteLine($"Page {pageIndex + 1}: No barcodes detected.");
                        }
                    }
                }
            }
        }
    }
}