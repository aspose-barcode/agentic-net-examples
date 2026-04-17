using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path to the image containing barcodes.
        // Adjust the file name as needed; the program will exit gracefully if the file is missing.
        string imagePath = "sample.png";

        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Initialize the reader to scan all supported barcode types.
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Perform the recognition.
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes detected.");
                return;
            }

            // Log each barcode's type and decoded text.
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"BarCode Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }
    }
}