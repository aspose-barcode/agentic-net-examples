using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to read Australia Post barcodes from each page of a multi‑page TIFF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Processes each frame of the TIFF, decodes barcodes, and prints results.
    /// </summary>
    static void Main()
    {
        // Path to the multi‑page TIFF file containing Australia Post barcodes
        const string tiffPath = "input.tif";

        // Verify that the input file exists before proceeding
        if (!File.Exists(tiffPath))
        {
            Console.WriteLine($"File not found: {tiffPath}");
            return;
        }

        // Load the TIFF image using Aspose.Drawing's Bitmap class
        using (var tiffImage = new Bitmap(tiffPath))
        {
            // FrameDimension.Time is used for multi‑frame (page) images such as TIFF
            var frameDimension = FrameDimension.Time;
            int frameCount = tiffImage.GetFrameCount(frameDimension);

            // Iterate through each frame (page) in the TIFF
            for (int i = 0; i < frameCount; i++)
            {
                // Activate the current frame so it can be processed
                tiffImage.SelectActiveFrame(frameDimension, i);

                // Convert the current frame to PNG and store it in a memory stream
                using (var ms = new MemoryStream())
                {
                    tiffImage.Save(ms, ImageFormat.Png);
                    ms.Position = 0; // Reset stream position for reading

                    // Initialize the barcode reader for Australia Post symbology
                    using (var reader = new BarCodeReader(ms, DecodeType.AustraliaPost))
                    {
                        // Optional: configure decoding parameters specific to Australia Post
                        reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;
                        reader.BarcodeSettings.AustraliaPost.IgnoreEndingFillingPatternsForCTable = true;

                        // Read all barcodes found in the current frame
                        foreach (var result in reader.ReadBarCodes())
                        {
                            // Output the decoded text along with the page number (1‑based)
                            Console.WriteLine($"Page {i + 1}: CodeText = {result.CodeText}");
                        }
                    }
                }
            }
        }
    }
}