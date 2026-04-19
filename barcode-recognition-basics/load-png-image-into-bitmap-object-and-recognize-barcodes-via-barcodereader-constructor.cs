using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Path to the PNG image file
        string imagePath = "sample.png";

        // Verify that the image file exists
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the PNG image into a Bitmap
        using (Bitmap bitmap = new Bitmap(imagePath))
        {
            // Initialize the barcode reader with the bitmap and request all supported types
            using (BarCodeReader reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
            {
                // Read all barcodes from the image
                BarCodeResult[] results = reader.ReadBarCodes();

                // If no barcodes were found, inform the user
                if (results.Length == 0)
                {
                    Console.WriteLine("No barcodes detected in the image.");
                }
                else
                {
                    // Output each detected barcode's type and text
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
}