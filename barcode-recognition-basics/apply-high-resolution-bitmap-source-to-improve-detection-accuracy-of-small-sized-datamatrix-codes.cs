// Title: High‑Resolution Bitmap Improves Small DataMatrix Detection
// Description: Demonstrates generating a small DataMatrix barcode, increasing its bitmap resolution to 300 DPI, and reading it back to show improved detection accuracy.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator, BarCodeReader, and image manipulation classes (Bitmap, ImageFormat) to enhance detection of tiny DataMatrix symbols. Developers often need to adjust image resolution when scanning low‑size barcodes to achieve reliable decoding in real‑world applications.
// Prompt: Apply a high‑resolution bitmap source to improve detection accuracy of small‑sized DataMatrix codes.
// Tags: datamatrix, high resolution, barcode generation, barcode recognition, aspose.barcode, bitmap, dpi

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a small DataMatrix barcode, upsamples its bitmap resolution,
/// and reads it back to demonstrate improved detection accuracy.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes barcode generation, resolution enhancement,
    /// and recognition steps.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the DataMatrix barcode (small size)
        const string codeText = "ABC123";

        // Create a memory stream to hold the generated barcode image
        using (var generationStream = new MemoryStream())
        {
            // Generate the DataMatrix barcode and write it to the stream as PNG
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
            {
                // Set a small XDimension to keep the barcode compact
                generator.Parameters.Barcode.XDimension.Point = 1f;
                generator.Save(generationStream, BarCodeImageFormat.Png);
            }

            // Reset the stream position so it can be read from the beginning
            generationStream.Position = 0;

            // Load the generated PNG into a Bitmap for resolution manipulation
            using (var bitmap = new Bitmap(generationStream))
            {
                // Apply a high resolution (e.g., 300 DPI) to improve detection of small barcodes
                bitmap.SetResolution(300f, 300f);

                // Save the high‑resolution bitmap into a second memory stream
                using (var highResStream = new MemoryStream())
                {
                    bitmap.Save(highResStream, ImageFormat.Png);
                    highResStream.Position = 0;

                    // Initialize a barcode reader for DataMatrix on the high‑resolution image
                    using (var reader = new BarCodeReader(highResStream, DecodeType.DataMatrix))
                    {
                        bool found = false;

                        // Iterate through all detected barcodes (should be one)
                        foreach (BarCodeResult result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"Detected CodeText: {result.CodeText}");
                            found = true;
                        }

                        // Inform the user if no barcode was detected
                        if (!found)
                        {
                            Console.WriteLine("No DataMatrix barcode was detected.");
                        }
                    }
                }
            }
        }
    }
}