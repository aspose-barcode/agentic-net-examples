// Title: Batch barcode reading from multiple image files
// Description: Demonstrates generating several barcode images, storing their file paths, and reading each barcode using BarCodeReader in a loop.
// Category-Description: This example belongs to the Aspose.BarCode reading and generation category. It showcases the use of BarcodeGenerator to create barcodes and BarCodeReader to decode them from image files. Typical scenarios include batch processing of scanned documents, automated inventory checks, and bulk verification of encoded data. Developers often need to iterate over file collections, handle unsupported formats, and extract barcode metadata using these core API classes.
// Prompt: Process a batch of image files in a directory by looping BarCodeReader construction for each file path.
// Tags: barcode, batch, reading, image, aspose.barcode, generation, recognition, qr, code128, datamatrix

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that generates a set of barcode images and then reads each one
/// using <see cref="BarCodeReader"/>. The workflow demonstrates batch processing of
/// image files in a directory.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates temporary barcode images,
    /// iterates over them with <see cref="BarCodeReader"/>, and outputs
    /// detection results to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample barcodes
        string tempFolder = Path.Combine(Path.GetTempPath(), "BatchBarcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample barcode images and keep their paths
        List<string> barcodeFiles = new List<string>();
        GenerateSampleBarcode(Path.Combine(tempFolder, "qr.png"), EncodeTypes.QR, "Sample QR");
        barcodeFiles.Add(Path.Combine(tempFolder, "qr.png"));
        GenerateSampleBarcode(Path.Combine(tempFolder, "code128.png"), EncodeTypes.Code128, "1234567890");
        barcodeFiles.Add(Path.Combine(tempFolder, "code128.png"));
        GenerateSampleBarcode(Path.Combine(tempFolder, "datamatrix.png"), EncodeTypes.DataMatrix, "DM");
        barcodeFiles.Add(Path.Combine(tempFolder, "datamatrix.png"));

        // Process each file with BarCodeReader
        foreach (string filePath in barcodeFiles)
        {
            // Verify that the file exists before attempting to read it
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Construct a reader for the current image file, supporting all barcode types
                using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
                {
                    // Read all barcodes present in the image
                    BarCodeResult[] results = reader.ReadBarCodes();

                    if (results.Length == 0)
                    {
                        Console.WriteLine($"No barcode detected in file: {Path.GetFileName(filePath)}");
                    }
                    else
                    {
                        // Output details for each detected barcode
                        foreach (var result in results)
                        {
                            Console.WriteLine($"File: {Path.GetFileName(filePath)}");
                            Console.WriteLine($"  Code Text : {result.CodeText}");
                            Console.WriteLine($"  Code Type : {result.CodeTypeName}");
                            Console.WriteLine($"  Quality   : {result.ReadingQuality}");
                            var bounds = result.Region.Rectangle;
                            Console.WriteLine($"  Region    : X={bounds.X}, Y={bounds.Y}, W={bounds.Width}, H={bounds.Height}");
                            Console.WriteLine($"  Angle     : {result.Region.Angle}");
                        }
                    }
                }
            }
            // Handle cases where the image cannot be loaded (e.g., unsupported format)
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                Console.WriteLine($"Skipping unsupported or corrupted file: {Path.GetFileName(filePath)}");
            }
            // Catch any other unexpected errors during processing
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing file {Path.GetFileName(filePath)}: {ex.Message}");
            }
        }

        // Clean up temporary files (optional)
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }

    /// <summary>
    /// Generates a barcode image using <see cref="BarcodeGenerator"/> and saves it to the specified path.
    /// </summary>
    /// <param name="filePath">Full file path where the image will be saved.</param>
    /// <param name="encodeType">The barcode symbology to encode.</param>
    /// <param name="codeText">The text/value to encode in the barcode.</param>
    static void GenerateSampleBarcode(string filePath, BaseEncodeType encodeType, string codeText)
    {
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // No additional parameters needed for this simple example
            generator.Save(filePath, BarCodeImageFormat.Png);
        }
    }
}