// Title: Read Mailmark Barcode from JPEG Stream
// Description: Demonstrates how to read a Mailmark barcode from a JPEG image using Aspose.BarCode's BarCodeReader with DecodeType.Mailmark.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing the use of BarCodeReader and DecodeType to detect and decode specific symbologies. Typical use cases include processing scanned documents, images, or streams to extract Mailmark data for postal automation or tracking. Developers often need to read barcodes from various image formats, handle missing files, and retrieve detailed decoding information such as confidence and region.
// Prompt: Read a Mailmark barcode from a JPEG stream using BarCodeReader with DecodeType.Mailmark.
// Tags: mailmark, barcode, read, jpeg, stream, aspose.barcode, barcodereader

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that reads a Mailmark barcode from a JPEG image stream using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Opens a JPEG file, decodes any Mailmark barcodes, and prints their details.
    /// </summary>
    static void Main()
    {
        // Path to the JPEG image containing the Mailmark barcode
        const string imagePath = "mailmark.jpg";

        // Verify that the image file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Open the image file as a read‑only stream and create a BarCodeReader configured for Mailmark decoding
        using (FileStream imageStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
        using (BarCodeReader reader = new BarCodeReader(imageStream, DecodeType.Mailmark))
        {
            // Iterate through all detected barcodes in the image
            foreach (var result in reader.ReadBarCodes())
            {
                // Output basic information about each detected Mailmark barcode
                Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");
                Console.WriteLine($"Confidence: {result.Confidence}");
                Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                Console.WriteLine($"Region: {result.Region.Rectangle}");
            }

            // If no barcodes were found, inform the user
            if (reader.FoundCount == 0)
            {
                Console.WriteLine("No Mailmark barcode detected in the image.");
            }
        }
    }
}