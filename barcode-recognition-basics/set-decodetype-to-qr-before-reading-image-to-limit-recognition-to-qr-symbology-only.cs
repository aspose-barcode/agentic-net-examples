// Title: Decode QR Code from Image Using Aspose.BarCode
// Description: Demonstrates how to configure Aspose.BarCode to read only QR symbology from an image file.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category. It showcases the use of BarCodeReader with a specific BaseDecodeType (QR) to limit scanning to QR codes. Developers commonly use these APIs to extract data from images containing barcodes, selecting appropriate symbologies for performance or accuracy reasons. Typical use cases include scanning product labels, tickets, or QR-based authentication tokens.
// Prompt: Set DecodeType to QR before reading an image to limit recognition to QR symbology only.
// Tags: qr,barcode,recognition,decode,aspose.barcode,csharp,console

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that reads QR codes from an image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts an optional image path argument, validates the file,
    /// configures the reader for QR decoding only, and prints detected code information.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument may be the image file path.</param>
    static void Main(string[] args)
    {
        // Determine the image file path: use argument if provided, otherwise default.
        string imagePath = args.Length > 0 ? args[0] : "sample_qr.png";

        // Verify that the specified file exists before attempting to read.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Restrict decoding to QR symbology only.
        BaseDecodeType decodeType = DecodeType.QR;

        // Initialize the barcode reader with the image path and the QR decode type.
        using (BarCodeReader reader = new BarCodeReader(imagePath, decodeType))
        {
            // Iterate through all detected barcodes (should be QR codes only).
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Output the type and text of each detected QR code.
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
            }
        }
    }
}