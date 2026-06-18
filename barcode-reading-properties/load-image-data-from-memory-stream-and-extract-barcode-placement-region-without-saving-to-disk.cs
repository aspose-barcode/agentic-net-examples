using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode, storing it in memory,
/// and then reading it back to obtain barcode information and region data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, saves it to a memory stream, and reads it back.
    /// </summary>
    static void Main()
    {
        // Sample barcode text to encode
        const string barcodeText = "1234567890";

        // Create a memory stream to hold the generated barcode image
        using (var memoryStream = new MemoryStream())
        {
            // Initialize the barcode generator with Code128 symbology and the sample text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, barcodeText))
            {
                // Save the generated barcode as a PNG image into the memory stream
                generator.Save(memoryStream, BarCodeImageFormat.Png);
            }

            // Reset the stream position to the beginning before reading
            memoryStream.Position = 0;

            // Initialize a barcode reader for Code128 from the memory stream
            using (var reader = new BarCodeReader(memoryStream, DecodeType.Code128))
            {
                // Iterate through all detected barcodes in the image
                foreach (var result in reader.ReadBarCodes())
                {
                    // Retrieve the region rectangle of the detected barcode (pixel coordinates)
                    var region = result.Region.Rectangle;

                    // Convert floating-point rectangle values to integer pixel values
                    int x = (int)Math.Round((double)region.X);
                    int y = (int)Math.Round((double)region.Y);
                    int width = (int)Math.Round((double)region.Width);
                    int height = (int)Math.Round((double)region.Height);

                    // Output barcode type, decoded text, and region details to the console
                    Console.WriteLine($"Detected barcode: {result.CodeTypeName}");
                    Console.WriteLine($"Code text: {result.CodeText}");
                    Console.WriteLine($"Region - X:{x}, Y:{y}, Width:{width}, Height:{height}");
                }
            }
        }
    }
}