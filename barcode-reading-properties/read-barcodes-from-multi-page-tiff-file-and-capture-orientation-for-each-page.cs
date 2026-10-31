// Title: Read barcodes from each page of a multi‑page TIFF and report orientation
// Description: Demonstrates how to load a multi‑page TIFF, extract each page as an image, decode any barcodes present, and capture the rotation angle of each detected barcode.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating the use of Image, FrameDimension, BarCodeReader, and DecodeType classes to process multi‑page image files. Typical use cases include inventory scanning of scanned documents, batch processing of scanned forms, and extracting barcode data from multi‑page PDFs or TIFFs. Developers often need to iterate through pages, decode barcodes, and obtain positional information such as orientation for downstream processing.
// Prompt: Read barcodes from a multi‑page TIFF file and capture orientation for each page.
// Tags: barcode, tiff, multi-page, orientation, decode, aspose.barcode, barcodereader, imageprocessing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates reading barcodes from each page of a multi‑page TIFF file and reporting their orientation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts an optional file path argument, processes each TIFF page, and writes barcode details to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument may specify the TIFF file path.</param>
    static void Main(string[] args)
    {
        // Determine the TIFF file path: use the first argument if supplied, otherwise default to a sample file.
        string tiffPath = args.Length > 0 ? args[0] : "MultiPageBarcodes.tiff";

        // Verify that the file exists before attempting to load it.
        if (!File.Exists(tiffPath))
        {
            Console.WriteLine($"File not found: {tiffPath}");
            return;
        }

        // Load the multi‑page TIFF image.
        using (Image tiffImage = Image.FromFile(tiffPath))
        {
            // Get the total number of pages (frames) in the TIFF.
            int frameCount = tiffImage.GetFrameCount(FrameDimension.Page);
            Console.WriteLine($"Total pages: {frameCount}");

            // Configure the reader to detect all supported barcode types.
            BaseDecodeType decodeType = DecodeType.AllSupportedTypes;

            // Iterate through each page of the TIFF.
            for (int i = 0; i < frameCount; i++)
            {
                // Activate the current page/frame.
                tiffImage.SelectActiveFrame(FrameDimension.Page, i);

                // Convert the active frame to a PNG stream for barcode recognition.
                using (MemoryStream ms = new MemoryStream())
                {
                    tiffImage.Save(ms, ImageFormat.Png);
                    ms.Position = 0; // Reset stream position before reading.

                    // Initialize the barcode reader with the PNG stream and decode settings.
                    using (BarCodeReader reader = new BarCodeReader(ms, decodeType))
                    {
                        bool anyFound = false;

                        // Read all barcodes on the current page.
                        foreach (BarCodeResult result in reader.ReadBarCodes())
                        {
                            anyFound = true;
                            double angle = result.Region.Angle; // Capture the orientation angle.
                            Console.WriteLine($"Page {i + 1}: Type={result.CodeTypeName}, Text={result.CodeText}, Angle={angle}");
                        }

                        // If no barcodes were detected, inform the user.
                        if (!anyFound)
                        {
                            Console.WriteLine($"Page {i + 1}: No barcode detected.");
                        }
                    }
                }
            }
        }
    }
}