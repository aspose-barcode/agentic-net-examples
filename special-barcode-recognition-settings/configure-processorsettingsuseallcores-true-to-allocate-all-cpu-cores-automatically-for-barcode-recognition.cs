using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Allocate all CPU cores for barcode recognition
        BarCodeReader.ProcessorSettings.UseAllCores = true;

        // Path to the image containing barcodes
        string imagePath = "sample.png";

        // Verify that the image file exists
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Initialize the reader with the image file
        using (BarCodeReader reader = new BarCodeReader(imagePath))
        {
            // Perform barcode detection
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"Barcode Text: {result.CodeText}");
            }
        }
    }
}