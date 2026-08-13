// Title: Batch decode Australia Post barcodes from TIFF images using multithreading
// Description: Demonstrates generating a set of Australia Post barcodes, saving them as TIFF files, and decoding them in parallel across all CPU cores.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator, BarCodeReader, and ProcessorSettings classes for high‑throughput batch processing, a common requirement when handling large volumes of shipping labels or postal data. Developers often need to generate barcodes, store them as images, and later decode them efficiently using multi‑threading.
// Prompt: Perform batch decoding of Australia Post barcodes from a set of TIFF images using multi‑threading.
// Tags: australia post, barcode, batch, decoding, multithreading, tiff, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates batch generation and multi‑threaded decoding of Australia Post barcodes stored as TIFF images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, saves them as TIFF files, then decodes them in parallel, finally cleaning up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BatchAustraliaPost_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Sample Australia Post barcode texts
        var sampleTexts = new List<string>
        {
            "5912345678AB",
            "5912345678CD",
            "5912345678EF",
            "5912345678GH",
            "5912345678IJ"
        };

        // Generate barcode images (TIFF) and keep the file list
        var barcodeFiles = new List<string>();
        foreach (var text in sampleTexts)
        {
            string filePath = Path.Combine(tempFolder, $"{text}.tif");
            using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, text))
            {
                // Use CTable interpreting type for customer information
                generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;
                // Save as TIFF
                generator.Save(filePath, BarCodeImageFormat.Tiff);
            }
            barcodeFiles.Add(filePath);
        }

        // Configure processor settings to use all available cores
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;

        // Decode the generated barcodes using parallel processing
        var parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };
        Parallel.ForEach(barcodeFiles, parallelOptions, file =>
        {
            try
            {
                using (var reader = new BarCodeReader(file, DecodeType.AustraliaPost))
                {
                    // Set decoding parameters matching the generation settings
                    reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;
                    reader.BarcodeSettings.AustraliaPost.IgnoreEndingFillingPatternsForCTable = true;

                    var results = reader.ReadBarCodes();
                    foreach (var result in results)
                    {
                        // Output the decoded information
                        Console.WriteLine($"File: {Path.GetFileName(file)} | Type: {result.CodeType} | Text: {result.CodeText}");
                    }
                }
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                // Image could not be loaded – log a warning and continue
                Console.WriteLine($"Warning: Unable to load image '{Path.GetFileName(file)}'. Skipping.");
            }
            catch (Exception ex)
            {
                // Unexpected error – log details for troubleshooting
                Console.WriteLine($"Error processing '{Path.GetFileName(file)}': {ex.Message}");
            }
        });

        // Cleanup: delete the temporary folder and its contents
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // If deletion fails (e.g., files still in use), ignore – the OS will clean up temp files later.
        }
    }
}