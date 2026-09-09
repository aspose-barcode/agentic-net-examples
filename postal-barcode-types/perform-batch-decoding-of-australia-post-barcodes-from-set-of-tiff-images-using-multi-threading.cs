// Title: Batch decode Australia Post barcodes from TIFF images using multi‑threading
// Description: Demonstrates generating a set of TIFF images containing Australia Post barcodes, then decoding them concurrently with Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode batch processing and multi‑core decoding category. It shows how to use BarcodeGenerator to create barcodes, BarCodeReader with ProcessorSettings for parallel execution, and typical patterns for handling multiple image files. Developers working with high‑volume barcode scanning, especially Australia Post symbology, can use these APIs to improve performance in server or desktop applications.
// Prompt: Perform batch decoding of Australia Post barcodes from a set of TIFF images using multi‑threading.
// Tags: australia post, barcode, batch decoding, multithreading, tiff, aspnet.barcode, barcodegenerator, barcodereader

using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates TIFF images with Australia Post barcodes
/// and decodes them in parallel using all available CPU cores.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates temporary barcode images, decodes them concurrently,
    /// and cleans up all generated files.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for generated TIFF files
        string tempFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Sample Australia Post barcode texts to encode
        List<string> codeTexts = new List<string>
        {
            "6201234567ASPOSE",
            "620123456701234",
            "6201234567END",
            "6201234567CTAB",
            "6201234567NABC"
        };

        // Generate TIFF images containing Australia Post barcodes
        List<string> tiffFiles = new List<string>();
        foreach (string text in codeTexts)
        {
            string filePath = Path.Combine(tempFolder, $"AUPost_{Guid.NewGuid().ToString("N")}.tiff");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, text))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                generator.Parameters.Barcode.BarHeight.Pixels = 50f;
                generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;
                generator.Save(filePath, BarCodeImageFormat.Tiff);
            }
            tiffFiles.Add(filePath);
        }

        // Enable multi‑core processing for the barcode reader
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;

        // Configure parallel execution options
        ParallelOptions po = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };

        // Batch decode each TIFF file in parallel
        Parallel.ForEach(tiffFiles, po, file =>
        {
            try
            {
                using (BarCodeReader reader = new BarCodeReader(file, DecodeType.AustraliaPost))
                {
                    reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        lock (Console.Out)
                        {
                            Console.WriteLine($"File: {Path.GetFileName(file)}");
                            Console.WriteLine($"  CodeType: {result.CodeTypeName}");
                            Console.WriteLine($"  CodeText: {result.CodeText}");
                        }
                    }
                }
            }
            catch (ArgumentException ex)
            {
                lock (Console.Out)
                {
                    Console.WriteLine($"Failed to process {Path.GetFileName(file)}: {ex.Message}");
                }
            }
        });

        // Cleanup generated TIFF files
        foreach (string file in tiffFiles)
        {
            try { File.Delete(file); } catch { }
        }

        // Remove the temporary folder
        try { Directory.Delete(tempFolder, true); } catch { }
    }
}