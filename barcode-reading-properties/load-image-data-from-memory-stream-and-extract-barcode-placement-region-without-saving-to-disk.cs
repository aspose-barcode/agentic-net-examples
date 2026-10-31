// Title: Load barcode from memory stream and retrieve placement region
// Description: Demonstrates generating a QR code into a memory stream, then reading it directly from that stream to obtain barcode region information without writing to disk.
// Category-Description: This example belongs to the Aspose.BarCode image processing and barcode recognition category. It shows how to use BarcodeGenerator to create barcodes in memory and BarCodeReader to decode them, focusing on extracting placement region data such as quadrangle, angle, rectangle, and corner points. Developers working with on‑the‑fly barcode generation and detection, especially in scenarios where file I/O is undesirable, can use these APIs for real‑time processing.
// Prompt: Load image data from a memory stream and extract barcode placement region without saving to disk.
// Tags: qr, barcode generation, barcode recognition, memory stream, region extraction, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR code into a memory stream,
/// then reads the barcode directly from that stream to obtain
/// placement region details without persisting the image to disk.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, reads it from memory,
    /// and prints barcode type, text, and region information.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the QR code.
        const string barcodeText = "Hello Aspose";

        // Create a BarcodeGenerator for QR encoding and write the image to a MemoryStream.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, barcodeText))
        {
            using (var barcodeStream = new MemoryStream())
            {
                // Save the generated barcode as PNG into the memory stream.
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                // Reset stream position to the beginning for reading.
                barcodeStream.Position = 0;

                // Initialize a BarCodeReader to decode QR codes from the memory stream.
                using (var reader = new BarCodeReader(barcodeStream, DecodeType.QR))
                {
                    // Perform the detection and retrieve all results.
                    BarCodeResult[] results = reader.ReadBarCodes();

                    if (results.Length == 0)
                    {
                        Console.WriteLine("No barcode detected.");
                    }
                    else
                    {
                        // Iterate through each detected barcode and output its details.
                        foreach (BarCodeResult result in results)
                        {
                            Console.WriteLine($"Code Type: {result.CodeTypeName}");
                            Console.WriteLine($"Code Text: {result.CodeText}");

                            // Output the quadrangle that defines the barcode region.
                            Console.WriteLine($"Quadrangle: {result.Region.Quadrangle}");

                            // Output the rotation angle of the barcode.
                            Console.WriteLine($"Angle: {result.Region.Angle}");

                            // Output the bounding rectangle of the barcode.
                            Console.WriteLine($"Rectangle: {result.Region.Rectangle}");

                            // Output the corner points of the barcode region.
                            Console.Write("Points: ");
                            foreach (Point pt in result.Region.Points)
                            {
                                Console.Write($"{pt} ");
                            }
                            Console.WriteLine();
                        }
                    }
                }
            }
        }
    }
}