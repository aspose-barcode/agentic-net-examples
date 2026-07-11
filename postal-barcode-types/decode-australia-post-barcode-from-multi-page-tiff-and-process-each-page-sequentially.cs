// Title: Decode Australia Post barcode from a multi‑page TIFF
// Description: Demonstrates how to read Australia Post barcodes from each page of a multi‑page TIFF image and output barcode type, text, and location.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, showcasing the use of BarCodeReader with DecodeType.AustraliaPost on multi‑frame images. It illustrates loading TIFF frames via Aspose.Drawing, converting frames to a supported format, and iterating through pages to extract barcode data—common tasks for developers handling batch scanning or document processing workflows.
// Prompt: Decode an Australia Post barcode from a multi‑page TIFF and process each page sequentially.
// Tags: australia post, barcode, decode, tiff, multiframe, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that decodes Australia Post barcodes from each page of a multi‑page TIFF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Loads the TIFF, iterates through its pages, and reads barcodes.
    /// </summary>
    static void Main()
    {
        // Path to the multi‑page TIFF file containing Australia Post barcodes
        const string tiffPath = "input.tif";

        // Verify that the file exists before attempting to process it
        if (!File.Exists(tiffPath))
        {
            Console.WriteLine($"File not found: {tiffPath}");
            return;
        }

        // Load the TIFF image using Aspose.Drawing
        using (var tiffImage = new Bitmap(tiffPath))
        {
            // Use the time dimension to iterate over pages (frames) of the TIFF
            var frameDimension = FrameDimension.Time;
            int pageCount = tiffImage.GetFrameCount(frameDimension);

            // Process each page sequentially
            for (int pageIndex = 0; pageIndex < pageCount; pageIndex++)
            {
                // Select the current frame (page) in the TIFF
                tiffImage.SelectActiveFrame(frameDimension, pageIndex);

                // Save the selected frame to a memory stream as PNG (BarCodeReader works with Bitmap)
                using (var ms = new MemoryStream())
                {
                    tiffImage.Save(ms, ImageFormat.Png);
                    ms.Position = 0;

                    // Load the frame as a Bitmap for barcode recognition
                    using (var frameBitmap = new Bitmap(ms))
                    {
                        // Create a reader configured for Australia Post barcodes
                        using (var reader = new BarCodeReader(frameBitmap, DecodeType.AustraliaPost))
                        {
                            // Read all barcodes on the current page
                            foreach (var result in reader.ReadBarCodes())
                            {
                                Console.WriteLine($"Page {pageIndex + 1}: Type = {result.CodeType}, Text = {result.CodeText}");

                                // Output the bounding rectangle of the detected barcode
                                var rect = result.Region.Rectangle;
                                Console.WriteLine($"  Region - X:{rect.X}, Y:{rect.Y}, Width:{rect.Width}, Height:{rect.Height}");
                            }
                        }
                    }
                }
            }
        }
    }
}