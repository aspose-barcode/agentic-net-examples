// Title: Mailmark barcode extraction from multiple TIFF images
// Description: Demonstrates reading up to five TIFF files, detecting Mailmark barcodes, and outputting decoded fields to the console.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, focusing on complex barcode types such as Mailmark. It showcases the BarCodeReader and ComplexCodetextReader classes for detecting and decoding Mailmark symbology, a common requirement in postal and logistics applications where developers need to extract routing and item information from scanned documents.
// Prompt: Develop a console app that reads multiple TIFF files, extracts Mailmark barcodes, and logs decoded fields.
// Tags: mailmark, barcode, recognition, tiff, console, aspose.barcode, complexbarcode

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

namespace MailmarkReaderApp
{
    /// <summary>
    /// Entry point for the MailmarkReader console application.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Reads up to five TIFF files from a specified directory, detects Mailmark barcodes,
        /// decodes their fields, and writes the information to the console.
        /// </summary>
        static void Main()
        {
            // Directory containing TIFF files (adjust as needed)
            string imagesDirectory = "SampleTiffImages";

            // Ensure the directory exists before proceeding
            if (!Directory.Exists(imagesDirectory))
            {
                Console.WriteLine($"Directory not found: {imagesDirectory}");
                return;
            }

            // Retrieve all TIFF files in the directory
            string[] tiffFiles = Directory.GetFiles(imagesDirectory, "*.tif");
            // Limit processing to a maximum of five files
            int maxFiles = Math.Min(tiffFiles.Length, 5);
            if (maxFiles == 0)
            {
                Console.WriteLine("No TIFF files found.");
                return;
            }

            // Process each selected TIFF file
            for (int i = 0; i < maxFiles; i++)
            {
                string filePath = tiffFiles[i];
                Console.WriteLine($"Processing file: {Path.GetFileName(filePath)}");

                // Verify the file still exists (it may have been moved or deleted)
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("File does not exist, skipping.");
                    continue;
                }

                // Initialize the barcode reader for Mailmark symbology
                using (var reader = new BarCodeReader(filePath, DecodeType.Mailmark))
                {
                    // Optional: improve detection speed/quality
                    // reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;

                    try
                    {
                        // Iterate through all detected barcodes in the image
                        foreach (var result in reader.ReadBarCodes())
                        {
                            // Output basic barcode information
                            Console.WriteLine($"  Detected Barcode Type: {result.CodeTypeName}");
                            Console.WriteLine($"  Raw CodeText: {result.CodeText}");

                            // Attempt to decode Mailmark-specific fields
                            MailmarkCodetext mailmark = ComplexCodetextReader.TryDecodeMailmark(result.CodeText);
                            if (mailmark != null)
                            {
                                Console.WriteLine("  Decoded Mailmark fields:");
                                Console.WriteLine($"    Format: {mailmark.Format}");
                                Console.WriteLine($"    VersionID: {mailmark.VersionID}");
                                Console.WriteLine($"    Class: {mailmark.Class}");
                                Console.WriteLine($"    SupplychainID: {mailmark.SupplychainID}");
                                Console.WriteLine($"    ItemID: {mailmark.ItemID}");
                                Console.WriteLine($"    DestinationPostCodePlusDPS: \"{mailmark.DestinationPostCodePlusDPS}\"");
                            }
                            else
                            {
                                Console.WriteLine("  Failed to decode Mailmark codetext.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log any errors encountered while reading barcodes
                        Console.WriteLine($"  Error reading barcode: {ex.Message}");
                    }
                }

                Console.WriteLine(); // Blank line between files for readability
            }

            Console.WriteLine("Processing completed.");
        }
    }
}