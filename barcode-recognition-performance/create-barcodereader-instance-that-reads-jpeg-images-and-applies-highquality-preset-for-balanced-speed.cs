using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path to the JPEG image. Change as needed or keep the default.
        string imagePath = "sample.jpg";

        // Verify that the file exists before attempting to read.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a BarCodeReader for the image, requesting all supported barcode types.
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Apply the HighQuality preset for a balanced speed/quality configuration.
            reader.QualitySettings = QualitySettings.HighQuality;

            // Read all barcodes from the image.
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes were detected.");
            }
            else
            {
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"Detected Barcode Type: {result.CodeTypeName}");
                    Console.WriteLine($"Code Text: {result.CodeText}");
                    Console.WriteLine();
                }
            }
        }
    }
}