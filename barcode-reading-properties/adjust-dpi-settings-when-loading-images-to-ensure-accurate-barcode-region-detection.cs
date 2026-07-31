// Title: Adjust DPI Settings for Accurate Barcode Detection
// Description: Demonstrates how to set and adjust DPI when generating and loading a barcode image to ensure correct region detection.
// Category-Description: This example belongs to the Aspose.BarCode image processing category, illustrating the use of BarcodeGenerator, Bitmap, and BarCodeReader classes. It shows typical scenarios where developers need to control image resolution for reliable barcode recognition, such as scanning high‑resolution documents or preparing images for OCR pipelines.
// Prompt: Adjust DPI settings when loading images to ensure accurate barcode region detection.
// Tags: barcode, dpi, resolution, cod128, generation, recognition, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates adjusting DPI settings when loading a barcode image to ensure accurate detection of barcode regions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a high‑resolution barcode, adjusts DPI on load, and reads the barcode.
    /// </summary>
    static void Main()
    {
        // Generate a sample barcode image with a high resolution (300 DPI)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set the generation resolution (DPI)
            generator.Parameters.Resolution = 300;

            // Save the generated barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // Load the image from the memory stream into a Bitmap
                using (var bitmap = new Bitmap(ms))
                {
                    // Adjust DPI after loading to match the generation DPI
                    bitmap.SetResolution(300f, 300f);

                    // Initialize the barcode reader
                    using (var reader = new BarCodeReader())
                    {
                        // Provide the bitmap to the reader
                        reader.SetBarCodeImage(bitmap);

                        // Iterate through all detected barcodes
                        foreach (var result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                            Console.WriteLine($"Code Text: {result.CodeText}");

                            // Output the location and size of the detected barcode region
                            var rect = result.Region.Rectangle;
                            Console.WriteLine($"Region - X:{rect.X}, Y:{rect.Y}, Width:{rect.Width}, Height:{rect.Height}");
                        }
                    }
                }
            }
        }
    }
}