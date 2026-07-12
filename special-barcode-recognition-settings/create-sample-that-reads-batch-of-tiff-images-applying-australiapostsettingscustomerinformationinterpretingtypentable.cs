// Title: Read batch of TIFF images with Australia Post NTable interpretation
// Description: Demonstrates how to load multiple TIFF files and decode Australia Post barcodes using the NTable customer information interpreting type.
// Category-Description: This example belongs to the Aspose.BarCode barcode reading category, focusing on image batch processing and specific symbology settings. It showcases the BarCodeReader, DecodeType, and AustraliaPostSettings classes, which developers commonly use to extract barcode data from various image formats, apply custom decoding options, and handle batch operations efficiently.
// Prompt: Create a sample that reads a batch of TIFF images applying AustraliaPostSettings.CustomerInformationInterpretingType.NTable.
// Tags: barcode symbology, australia post, batch processing, tiff, barcodereader, decode type, customerinformationinterpretingtype

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Sample program that reads up to five TIFF images from a folder and decodes
/// Australia Post barcodes using the NTable customer information interpreting type.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the folder that contains the TIFF images.
        string folderPath = "tiff_images";

        // Verify that the folder exists before proceeding.
        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Retrieve all TIFF files in the folder (case‑insensitive pattern).
        string[] tiffFiles = Directory.GetFiles(folderPath, "*.tif");
        // Limit processing to a maximum of five files.
        int filesToProcess = Math.Min(tiffFiles.Length, 5);

        // Iterate over each selected TIFF file.
        for (int i = 0; i < filesToProcess; i++)
        {
            string file = tiffFiles[i];

            // Ensure the file still exists (it could have been removed externally).
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            // Load the TIFF image into a bitmap object.
            using (Bitmap bitmap = new Bitmap(file))
            {
                // Create a barcode reader configured for Australia Post symbology.
                using (BarCodeReader reader = new BarCodeReader(bitmap, DecodeType.AustraliaPost))
                {
                    // Set the customer information interpreting type to NTable.
                    reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.NTable;

                    // Read all barcodes found in the image.
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Output details of each detected barcode.
                        Console.WriteLine($"File: {Path.GetFileName(file)}");
                        Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                        Console.WriteLine($"CodeText: {result.CodeText}");
                        var rect = result.Region.Rectangle;
                        Console.WriteLine($"Region - X:{rect.X}, Y:{rect.Y}, Width:{rect.Width}, Height:{rect.Height}");
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}