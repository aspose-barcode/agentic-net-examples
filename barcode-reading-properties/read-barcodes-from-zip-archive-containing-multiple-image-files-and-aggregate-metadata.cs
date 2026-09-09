// Title: Read barcodes from a zip archive and aggregate metadata
// Description: Demonstrates extracting image files from a zip, scanning each for barcodes using Aspose.BarCode, and outputting the collected barcode data as JSON.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing how to work with the BarCodeReader class to process multiple images. Typical use cases include batch processing of scanned documents, inventory images, or any collection of pictures stored in archives. Developers often need to extract barcode information from bulk files and aggregate results for reporting or further processing.
// Prompt: Read barcodes from a zip archive containing multiple image files and aggregate metadata.
// Tags: barcode, recognition, zip, batch processing, json, aspose.barcode, metadata

using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Program that extracts images from a zip archive, reads barcodes from each image, and outputs aggregated metadata as JSON.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Performs extraction, barcode reading, and JSON aggregation.
    /// </summary>
    static void Main()
    {
        // Path to the zip file containing barcode images
        string zipPath = "barcodes.zip";

        // Verify the zip file exists before proceeding
        if (!File.Exists(zipPath))
        {
            Console.WriteLine($"Zip file not found: {zipPath}");
            return;
        }

        // Create a temporary folder to extract images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Collect full paths of extracted image files
        var imageFiles = new List<string>();

        // Open the zip archive for reading
        using (ZipArchive archive = ZipFile.OpenRead(zipPath))
        {
            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                // Skip directory entries
                if (string.IsNullOrEmpty(entry.Name))
                    continue;

                // Determine destination path for the extracted file
                string destinationPath = Path.Combine(tempFolder, entry.FullName);
                string destinationDir = Path.GetDirectoryName(destinationPath);

                // Ensure the destination directory exists
                if (!Directory.Exists(destinationDir))
                {
                    Directory.CreateDirectory(destinationDir);
                }

                // Extract the file, overwriting if it already exists
                entry.ExtractToFile(destinationPath, overwrite: true);
                imageFiles.Add(destinationPath);
            }
        }

        // Prepare a list to hold barcode metadata records
        var records = new List<MetadataRecord>();

        // Use all supported barcode types for decoding
        BaseDecodeType decodeType = DecodeType.AllSupportedTypes;

        // Process each extracted image file
        foreach (string filePath in imageFiles)
        {
            // Verify the image file still exists
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found (skipped): {filePath}");
                continue;
            }

            try
            {
                // Initialize the barcode reader for the current image
                using (BarCodeReader reader = new BarCodeReader(filePath, decodeType))
                {
                    // Iterate over all detected barcodes in the image
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        // Create a metadata record for each barcode
                        var record = new MetadataRecord
                        {
                            FileName = Path.GetFileName(filePath),
                            CodeTypeName = result.CodeTypeName,
                            CodeText = result.CodeText
                        };
                        records.Add(record);
                    }
                }
            }
            // Handle cases where the image cannot be loaded as a barcode source
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                Console.WriteLine($"Unable to load image (skipped): {filePath}");
            }
            // Catch any other unexpected errors during processing
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing {filePath}: {ex.Message}");
            }
        }

        // Serialize the collected metadata to formatted JSON
        string json = JsonSerializer.Serialize(records, new JsonSerializerOptions { WriteIndented = true });

        // Output the aggregated barcode metadata
        Console.WriteLine("Aggregated Barcode Metadata:");
        Console.WriteLine(json);
    }

    /// <summary>
    /// Simple DTO representing barcode metadata extracted from an image file.
    /// </summary>
    class MetadataRecord
    {
        public string FileName { get; set; }
        public string CodeTypeName { get; set; }
        public string CodeText { get; set; }
    }
}