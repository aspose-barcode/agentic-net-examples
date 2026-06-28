using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates reading Australia Post barcodes from image files located on a network share.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Scans a network directory for image files,
    /// limits processing to a maximum of five files, and reads Australia Post barcodes
    /// from each image using Aspose.BarCode.
    /// </summary>
    static void Main()
    {
        // Path to the network share containing barcode images
        string networkSharePath = @"\\Server\Share\Barcodes";

        // Verify that the directory exists before proceeding
        if (!Directory.Exists(networkSharePath))
        {
            Console.WriteLine($"Directory not found: {networkSharePath}");
            return;
        }

        // Retrieve all files from the share (any extension) – we'll filter later
        string[] allFiles = Directory.GetFiles(networkSharePath, "*.*", SearchOption.TopDirectoryOnly);
        var imageFiles = new List<string>();

        // Filter the list to include only common image formats
        foreach (string file in allFiles)
        {
            string ext = Path.GetExtension(file).ToLowerInvariant();
            if (ext == ".png" || ext == ".jpg" || ext == ".jpeg" || ext == ".bmp" || ext == ".tif" || ext == ".tiff")
            {
                imageFiles.Add(file);
            }
        }

        // Process up to a safe number of files (e.g., 5) to avoid excessive load
        int maxFiles = Math.Min(5, imageFiles.Count);
        for (int i = 0; i < maxFiles; i++)
        {
            string filePath = imageFiles[i];
            Console.WriteLine($"Processing file: {filePath}");

            // Ensure the file still exists before attempting to read it
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File does not exist.");
                continue;
            }

            // Load the image and create a barcode reader configured for Australia Post barcodes
            using (var bitmap = new Bitmap(filePath))
            using (var reader = new BarCodeReader(bitmap, DecodeType.AustraliaPost))
            {
                // Apply specific settings required for Australia Post barcode interpretation
                reader.BarcodeSettings.AustraliaPost.IgnoreEndingFillingPatternsForCTable = true;
                reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;

                // Iterate through all detected barcodes and output their details
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"BarCode Type: {result.CodeType}");
                    Console.WriteLine($"BarCode CodeText: {result.CodeText}");
                }
            }
        }
    }
}