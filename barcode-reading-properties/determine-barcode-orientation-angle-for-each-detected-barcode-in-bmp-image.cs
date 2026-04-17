using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path to the BMP image containing barcodes.
        string imagePath = "sample.bmp";

        // Verify that the file exists before attempting recognition.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Initialize the barcode reader for the specified image.
        // The constructor without explicit decode types attempts to recognize all supported barcodes.
        using (BarCodeReader reader = new BarCodeReader(imagePath))
        {
            // Perform barcode detection.
            BarCodeResult[] results = reader.ReadBarCodes();

            // If no barcodes were found, inform the user.
            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes detected in the image.");
                return;
            }

            // Iterate through each detected barcode and output its angle.
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text   : {result.CodeText}");
                Console.WriteLine($"Angle (deg) : {result.Region.Angle}");
                Console.WriteLine(new string('-', 40));
            }
        }
    }
}