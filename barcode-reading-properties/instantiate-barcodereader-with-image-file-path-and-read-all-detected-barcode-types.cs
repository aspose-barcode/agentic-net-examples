using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main(string[] args)
    {
        // Use a default image path if none is provided via command‑line arguments
        string imagePath = args.Length > 0 ? args[0] : "sample.png";

        // Verify that the image file exists before attempting to read barcodes
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a BarCodeReader that scans the image for all supported barcode types
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Read all barcodes found in the image
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes were detected.");
            }
            else
            {
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                    Console.WriteLine($"BarCode Text: {result.CodeText}");
                    Console.WriteLine();
                }
            }
        }
    }
}