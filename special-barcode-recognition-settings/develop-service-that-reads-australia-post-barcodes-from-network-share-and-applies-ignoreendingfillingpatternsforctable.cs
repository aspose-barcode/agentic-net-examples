// Title: Read Australia Post barcodes from network share with CTable settings
// Description: Demonstrates how to scan images on a network share for Australia Post barcodes, applying the CTable interpreting type and ignoring ending filling patterns.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, focusing on Australia Post symbology. It shows usage of BarCodeReader, DecodeType.AustraliaPost, and BarcodeSettings to configure CustomerInformationInterpretingType and IgnoreEndingFillingPatternsForCTable. Developers often need to process batches of images from shared locations and customize interpretation settings for accurate data extraction.
// Prompt: Develop a service that reads Australia Post barcodes from a network share and applies IgnoreEndingFillingPatternsForCTable.
// Tags: australia post, barcode recognition, ctable, ignoreendingfillingpatterns, network share, aspnet, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example service that scans image files on a network share for Australia Post barcodes,
/// configuring the reader to use CTable interpretation and to ignore ending filling patterns.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Iterates through supported image files in a network folder,
    /// reads Australia Post barcodes, and outputs detection results to the console.
    /// </summary>
    static void Main()
    {
        // Network share path containing barcode images.
        string networkFolder = @"\\server\share\barcodes";

        // Verify the folder exists before proceeding.
        if (!Directory.Exists(networkFolder))
        {
            Console.WriteLine($"Folder not found: {networkFolder}");
            return;
        }

        // Define supported image extensions for barcode scanning.
        string[] extensions = new[] { ".png", ".jpg", ".jpeg", ".bmp", ".tif", ".tiff" };
        string[] files = Directory.GetFiles(networkFolder);

        bool anyFileProcessed = false;

        // Process each file found in the network folder.
        foreach (string filePath in files)
        {
            // Skip files that do not have a supported image extension.
            if (Array.IndexOf(extensions, Path.GetExtension(filePath).ToLowerInvariant()) < 0)
                continue;

            anyFileProcessed = true;

            // Ensure the file still exists (it could have been removed after enumeration).
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the image into a bitmap and create a barcode reader for Australia Post symbology.
                using (Bitmap bitmap = new Bitmap(filePath))
                using (BarCodeReader reader = new BarCodeReader(bitmap, DecodeType.AustraliaPost))
                {
                    // Configure the reader to interpret customer information as CTable
                    // and to ignore any ending filling patterns that may be present.
                    reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;
                    reader.BarcodeSettings.AustraliaPost.IgnoreEndingFillingPatternsForCTable = true;

                    bool found = false;

                    // Iterate through all detected barcodes in the current image.
                    foreach (var result in reader.ReadBarCodes())
                    {
                        found = true;
                        Console.WriteLine($"File: {Path.GetFileName(filePath)}");
                        Console.WriteLine($"  Barcode Type : {result.CodeType}");
                        Console.WriteLine($"  Code Text    : {result.CodeText}");

                        // Output the region of the barcode within the image.
                        var rect = result.Region.Rectangle;
                        Console.WriteLine($"  Region       : X={rect.X}, Y={rect.Y}, Width={rect.Width}, Height={rect.Height}");
                    }

                    // If no barcodes were detected, inform the user.
                    if (!found)
                    {
                        Console.WriteLine($"File: {Path.GetFileName(filePath)} - No AustraliaPost barcode detected.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Report any errors that occur while processing the file.
                Console.WriteLine($"Error processing file '{Path.GetFileName(filePath)}': {ex.Message}");
            }
        }

        // If no supported image files were found, notify the user.
        if (!anyFileProcessed)
        {
            Console.WriteLine("No image files found in the specified folder.");
        }
    }
}