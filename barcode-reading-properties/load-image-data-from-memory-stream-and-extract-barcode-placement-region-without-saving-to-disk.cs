// Title: Load barcode from memory stream and retrieve placement region
// Description: Demonstrates generating a barcode image in memory, reading it directly from a stream, and extracting the barcode's placement region without writing to disk.
// Category-Description: This example belongs to the Aspose.BarCode image processing and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and the Region property to obtain location and orientation data. Developers often need to process barcodes in-memory for web services, document workflows, or real‑time scanning scenarios where disk I/O is undesirable.
// Prompt: Load image data from a memory stream and extract barcode placement region without saving to disk.
// Tags: barcode, code128, memory stream, region extraction, in-memory processing, aspose.barcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a barcode in memory, reading it from a stream, and extracting its placement region.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode, reads it from a MemoryStream, and prints barcode details including region rectangle and angle.
    /// </summary>
    static void Main()
    {
        // Generate a sample barcode and keep it in a memory stream
        using (var barcodeStream = new MemoryStream())
        {
            // Create a BarcodeGenerator for Code128 with sample text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Save the barcode image to the memory stream in PNG format
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
            }

            // Reset stream position to the beginning for reading
            barcodeStream.Position = 0;

            // Initialize a BarCodeReader to decode all supported barcode types from the stream
            using (var reader = new BarCodeReader(barcodeStream, DecodeType.AllSupportedTypes))
            {
                // Read all barcodes found in the image
                var results = reader.ReadBarCodes();

                // Iterate through each detected barcode result
                foreach (var result in results)
                {
                    // Extract the placement region rectangle
                    var rect = result.Region.Rectangle;

                    // Output barcode details and region information
                    Console.WriteLine($"Code Text: {result.CodeText}");
                    Console.WriteLine($"Code Type: {result.CodeTypeName}");
                    Console.WriteLine($"Region - X: {rect.X}, Y: {rect.Y}, Width: {rect.Width}, Height: {rect.Height}");
                    Console.WriteLine($"Angle: {result.Region.Angle}");
                    Console.WriteLine();
                }

                // Inform the user if no barcodes were detected
                if (results.Length == 0)
                {
                    Console.WriteLine("No barcode detected.");
                }
            }
        }
    }
}