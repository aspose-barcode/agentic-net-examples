using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Path to the image containing barcodes
        string imagePath = "sample.png";

        // Verify that the file exists before attempting recognition
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Start timing the recognition process
        Stopwatch stopwatch = Stopwatch.StartNew();

        // Initialize the reader with desired decode types
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code128, DecodeType.QR))
        {
            // Perform barcode detection
            reader.ReadBarCodes();

            // Stop the timer after recognition completes
            stopwatch.Stop();

            // Log processing time and number of barcodes found
            Console.WriteLine($"Processing time: {stopwatch.ElapsedMilliseconds} ms");
            Console.WriteLine($"Barcodes found: {reader.FoundCount}");

            // Iterate through each detected barcode and log details
            for (int i = 0; i < reader.FoundCount; i++)
            {
                var result = reader.FoundBarCodes[i];
                Console.WriteLine($"[{i + 1}] Type: {result.CodeTypeName}, Text: {result.CodeText}, Confidence: {result.Confidence}");
            }
        }
    }
}