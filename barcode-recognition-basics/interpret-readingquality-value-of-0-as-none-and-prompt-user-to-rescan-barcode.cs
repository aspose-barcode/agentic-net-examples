using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode, storing it in memory,
/// and then reading it back to evaluate the reading quality.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, saves it to a memory stream, reads it,
    /// and outputs the reading quality.
    /// </summary>
    static void Main()
    {
        // Create a memory stream to hold the generated barcode image
        using (var memoryStream = new MemoryStream())
        {
            // Generate a Code128 barcode with the value "123456"
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
            {
                // Save the barcode image as PNG into the memory stream
                generator.Save(memoryStream, BarCodeImageFormat.Png);
                // Reset stream position to the beginning for reading
                memoryStream.Position = 0;
            }

            // Load the barcode image from the memory stream into a bitmap
            using (var bitmap = new Bitmap(memoryStream))
            {
                // Initialize a barcode reader for Code128 type
                using (var reader = new BarCodeReader(bitmap, DecodeType.Code128))
                {
                    // Iterate through all detected barcodes
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Retrieve the reading quality metric
                        double quality = result.ReadingQuality;
                        // If quality is zero, indicate that no quality was detected
                        if (quality == 0.0)
                        {
                            Console.WriteLine("Reading quality is none. Please rescan the barcode.");
                        }
                        else
                        {
                            // Output the measured reading quality
                            Console.WriteLine($"Reading quality: {quality}");
                        }
                    }
                }
            }
        }
    }
}