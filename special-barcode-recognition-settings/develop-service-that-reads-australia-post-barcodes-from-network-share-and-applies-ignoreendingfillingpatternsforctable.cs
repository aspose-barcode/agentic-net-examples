using System;
using System.IO;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Path to the network share containing barcode images.
        // Use a default sample path if no argument is provided.
        string folderPath = args.Length > 0 ? args[0] : @"\\networkshare\Barcodes";

        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Get a limited set of image files (png, jpg, bmp) to keep the demo short.
        var imageFiles = Directory.GetFiles(folderPath)
            .Where(f => f.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                        f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                        f.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                        f.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase))
            .Take(5) // limit to a few files for the example
            .ToArray();

        if (imageFiles.Length == 0)
        {
            Console.WriteLine("No image files found in the specified folder.");
            return;
        }

        foreach (string filePath in imageFiles)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            // Load the image using Aspose.Drawing.Bitmap (IDisposable).
            using (Bitmap bitmap = new Bitmap(filePath))
            // Create a reader for Australia Post barcodes.
            using (BarCodeReader reader = new BarCodeReader(bitmap, DecodeType.AustraliaPost))
            {
                // Configure decoding to use CTable interpreting type and ignore ending filling patterns.
                reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;
                reader.BarcodeSettings.AustraliaPost.IgnoreEndingFillingPatternsForCTable = true;

                // Read all barcodes found in the image.
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(filePath)}");
                    Console.WriteLine($"  Type: {result.CodeType}");
                    Console.WriteLine($"  Text: {result.CodeText}");
                }
            }
        }
    }
}