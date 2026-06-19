using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode detection using Aspose.BarCode library on a local image file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the console application.
    /// Detects all supported barcodes in the specified image and prints their type and text.
    /// </summary>
    static void Main()
    {
        // NOTE: Full web API integration cannot be demonstrated in a console app.
        // The core barcode detection logic is shown below using a local image file.

        // Path to the image that contains barcodes.
        string imagePath = "sample.png";

        // Verify that the image file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Open the image file as a read‑only stream and create a BarCodeReader
        // configured to detect all supported barcode types.
        using (FileStream stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
        using (BarCodeReader reader = new BarCodeReader(stream, DecodeType.AllSupportedTypes))
        {
            // Perform the barcode detection.
            BarCodeResult[] results = reader.ReadBarCodes();

            // If no barcodes were found, inform the user.
            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes detected.");
            }
            else
            {
                // Iterate through each detected barcode and display its details.
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                    Console.WriteLine($"BarCode CodeText: {result.CodeText}");
                    Console.WriteLine();
                }
            }
        }
    }
}