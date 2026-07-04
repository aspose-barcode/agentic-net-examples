// Title: Barcode detection demo for image stream processing
// Description: Shows how to generate a sample barcode image and detect barcodes from a stream, mimicking a web API endpoint.
// Prompt: Integrate barcode detection into a web API endpoint that accepts uploaded image streams for instant processing.
// Tags: barcode symbology, detection, png, aspose.barcode, console demo

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation and detection using Aspose.BarCode.
/// Intended as core logic for a web API endpoint that processes uploaded image streams.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a sample barcode if missing, then reads barcodes from the image stream.
    /// </summary>
    static void Main()
    {
        // NOTE: The original task describes a web API endpoint.
        // The snippet runner does not support hosting an HTTP server,
        // so this console program demonstrates the core barcode detection logic
        // that would be used inside such an endpoint.

        const string sampleImagePath = "sample.png";

        // Ensure a sample barcode image exists.
        if (!File.Exists(sampleImagePath))
        {
            // Create a simple Code128 barcode and save it as a PNG file.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Optional: configure image size.
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 150f;
                generator.Save(sampleImagePath);
            }
        }

        // Open the image file as a stream for recognition.
        using (var imageStream = new FileStream(sampleImagePath, FileMode.Open, FileAccess.Read))
        {
            // Initialize the reader and configure it to scan all supported symbologies.
            using (var reader = new BarCodeReader())
            {
                reader.BarCodeReadType = DecodeType.AllSupportedTypes;
                reader.SetBarCodeImage(imageStream);

                // Perform barcode detection.
                foreach (var result in reader.ReadBarCodes())
                {
                    // Output detected barcode type.
                    Console.WriteLine($"Detected Barcode Type: {result.CodeTypeName}");
                    // Output decoded text.
                    Console.WriteLine($"Decoded Text: {result.CodeText}");

                    // Output the location of the barcode within the image.
                    var rect = result.Region.Rectangle;
                    Console.WriteLine($"Region - X:{rect.X}, Y:{rect.Y}, Width:{rect.Width}, Height:{rect.Height}");
                    Console.WriteLine();
                }
            }
        }
    }
}