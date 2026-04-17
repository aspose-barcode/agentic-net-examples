using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Path to the JPEG image containing barcodes.
        string imagePath = "sample.jpg";

        // Verify that the file exists before attempting to read.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Initialize the reader for all supported barcode types.
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Read all barcodes from the image.
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes were detected.");
                return;
            }

            // Output detection details for each barcode.
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"Barcode Type : {result.CodeTypeName}");
                Console.WriteLine($"Code Text    : {result.CodeText}");

                // Region rectangle provides the location of the barcode in the image.
                Rectangle bounds = result.Region.Rectangle;
                Console.WriteLine($"Region       : X={bounds.X}, Y={bounds.Y}, Width={bounds.Width}, Height={bounds.Height}");
                Console.WriteLine(new string('-', 40));
            }
        }
    }
}