// Title: Batch decode Australia Post barcodes from TIFF images using multi‑threading
// Description: Demonstrates generating sample Australia Post barcodes, saving them as TIFF files, and decoding them in parallel using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and ProcessorSettings for enabling multi‑core processing. Typical scenarios include high‑throughput barcode scanning of image batches, such as mail sorting or inventory audits, where developers need efficient, thread‑safe barcode operations.
// Prompt: Perform batch decoding of Australia Post barcodes from a set of TIFF images using multi‑threading.
// Tags: australia post, barcode, batch decoding, multithreading, tiff, aspose.barcode, generation, recognition

using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch generation and multi‑threaded decoding of Australia Post barcodes stored in TIFF images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample barcodes, saves them as TIFF files, and decodes them in parallel.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a unique temporary folder for sample TIFF images
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // --------------------------------------------------------------------
        // Define sample Australia Post barcode texts (FCC 11 + 8‑digit DPID)
        // --------------------------------------------------------------------
        List<string> sampleTexts = new List<string>
        {
            "1100000000",
            "4501234567",
            "5909876543",
            "6201112222",
            "8705556666"
        };

        // --------------------------------------------------------------------
        // Generate a TIFF image for each sample text
        // --------------------------------------------------------------------
        List<string> imageFiles = new List<string>();
        int index = 0;
        foreach (string codeText in sampleTexts)
        {
            string filePath = Path.Combine(tempFolder, $"barcode_{index}.tiff");
            using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                generator.Parameters.Barcode.BarHeight.Pixels = 50f;
                generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;
                generator.Save(filePath, BarCodeImageFormat.Tiff);
            }
            imageFiles.Add(filePath);
            index++;
        }

        // --------------------------------------------------------------------
        // Enable multi‑core processing for the barcode reader
        // --------------------------------------------------------------------
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;

        // --------------------------------------------------------------------
        // Batch decode the generated images using Parallel.ForEach
        // --------------------------------------------------------------------
        Parallel.ForEach(imageFiles, file =>
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                return;
            }

            try
            {
                using (var reader = new BarCodeReader(file, DecodeType.AustraliaPost))
                {
                    // Configure Australia Post specific recognition settings
                    reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;
                    reader.BarcodeSettings.AustraliaPost.IgnoreEndingFillingPatternsForCTable = true;

                    // Optional: set a quality preset for faster processing
                    reader.QualitySettings = QualitySettings.HighPerformance;

                    BarCodeResult[] results = reader.ReadBarCodes();
                    if (results.Length == 0)
                    {
                        Console.WriteLine($"No barcode detected in {Path.GetFileName(file)}");
                    }
                    else
                    {
                        foreach (var result in results)
                        {
                            Console.WriteLine($"File: {Path.GetFileName(file)}");
                            Console.WriteLine($"  Type: {result.CodeTypeName}");
                            Console.WriteLine($"  Text: {result.CodeText}");
                        }
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Failed to load image {Path.GetFileName(file)}: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing {Path.GetFileName(file)}: {ex.Message}");
            }
        });

        // --------------------------------------------------------------------
        // Cleanup temporary folder
        // --------------------------------------------------------------------
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}