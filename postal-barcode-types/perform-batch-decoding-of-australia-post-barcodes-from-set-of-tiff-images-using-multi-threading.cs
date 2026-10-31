// Title: Batch decode Australia Post barcodes from TIFF images using multithreading
// Description: Demonstrates generating a set of Australia Post barcodes, saving them as TIFF files, and decoding them concurrently with multiple threads.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and ProcessorSettings for parallel processing. Typical scenarios include high‑volume barcode scanning, automated document processing, and performance‑critical applications where developers need to leverage multi‑core CPUs to speed up barcode recognition.
// Prompt: Perform batch decoding of Australia Post barcodes from a set of TIFF images using multi‑threading.
// Tags: australia post, barcode, batch, decoding, multithreading, tiff, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a collection of Australia Post barcode images and decodes them in parallel.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the sample. Creates temporary TIFF files, configures multithreaded decoding,
    /// processes each image concurrently, and cleans up the temporary resources.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a unique temporary folder for the sample images
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // --------------------------------------------------------------------
        // Define sample Australia Post codetexts (valid FCC 62 examples)
        // --------------------------------------------------------------------
        List<string> codeTexts = new List<string>
        {
            "6201234567ASPOSE",
            "6209876543TEST1",
            "6205555555DEMO2",
            "6201111111SAMPLE",
            "6202222222EXAMP"
        };

        // --------------------------------------------------------------------
        // Generate barcode images (TIFF) and collect file paths
        // --------------------------------------------------------------------
        List<string> imageFiles = new List<string>();
        foreach (string text in codeTexts)
        {
            string filePath = Path.Combine(tempFolder, $"{text}.tiff");
            using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, text))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                generator.Parameters.Barcode.BarHeight.Pixels = 50f;
                generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;
                generator.Save(filePath, BarCodeImageFormat.Tiff);
            }
            imageFiles.Add(filePath);
        }

        // --------------------------------------------------------------------
        // Enable multithreading for the barcode reader
        // --------------------------------------------------------------------
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;

        // --------------------------------------------------------------------
        // Batch decode using Parallel.ForEach
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
                    reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;
                    BarCodeResult[] results = reader.ReadBarCodes();

                    if (results.Length == 0)
                    {
                        Console.WriteLine($"No barcode detected in {Path.GetFileName(file)}");
                    }
                    else
                    {
                        foreach (var result in results)
                        {
                            Console.WriteLine($"File: {Path.GetFileName(file)} | CodeText: {result.CodeText}");
                        }
                    }
                }
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                Console.WriteLine($"Skipping unsupported file {Path.GetFileName(file)}: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing {Path.GetFileName(file)}: {ex.Message}");
            }
        });

        // --------------------------------------------------------------------
        // Cleanup: delete temporary files and folder
        // --------------------------------------------------------------------
        foreach (string file in imageFiles)
        {
            try { File.Delete(file); } catch { }
        }
        try { Directory.Delete(tempFolder, true); } catch { }
    }
}