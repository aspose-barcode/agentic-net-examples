// Title: Batch decode Australia Post barcodes from TIFF images using multithreading
// Description: Demonstrates generating sample Australia Post barcodes, storing them as TIFF files, and decoding them concurrently with Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category, showcasing how to work with the BarCodeGenerator and BarCodeReader classes for high‑throughput barcode operations. Typical use cases include bulk scanning of shipping labels, automated inventory checks, and large‑scale document processing where multi‑core decoding improves performance. Developers often need to generate test barcodes, configure processor settings, and read results in parallel.
// Prompt: Perform batch decoding of Australia Post barcodes from a set of TIFF images using multi‑threading.
// Tags: barcode, decoding, australia post, multithreading, tiff, aspose.barcode, batch processing

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Sample program that creates a set of Australia Post barcode images and decodes them in parallel.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates sample TIFF images containing Australia Post barcodes
    /// and then decodes all images concurrently, writing results to the console.
    /// </summary>
    static void Main()
    {
        // Define the folder that will hold the sample TIFF images.
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }

        // Sample Australia Post barcode texts (valid formats).
        string[] sampleCodes = new string[]
        {
            "1100000000",          // FCC=11, no customer info
            "4580123456",          // FCC=45, no customer info
            "5980123456AB",        // FCC=59, 2 CTable chars
            "6280123456ABCDE",     // FCC=62, 5 CTable chars (max)
            "9280123456AB"         // FCC=92, 2 CTable chars
        };

        // Generate a TIFF file for each sample code if it does not already exist.
        for (int i = 0; i < sampleCodes.Length; i++)
        {
            string code = sampleCodes[i];
            string filePath = Path.Combine(inputFolder, $"barcode_{i + 1}.tif");

            if (File.Exists(filePath))
                continue; // Skip existing files to avoid unnecessary regeneration.

            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, code))
            {
                // Use CTable for customer information interpretation (alternatively NTable may be used).
                generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;
                // Save the generated barcode as a TIFF image.
                generator.Save(filePath, BarCodeImageFormat.Tiff);
            }
        }

        // Configure the barcode reader to utilize all available CPU cores.
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;

        // Retrieve all TIFF files from the input folder.
        string[] tiffFiles = Directory.GetFiles(inputFolder, "*.tif");

        if (tiffFiles.Length == 0)
        {
            Console.WriteLine("No TIFF images found for decoding.");
            return;
        }

        // Decode each TIFF image in parallel to maximize throughput.
        Parallel.ForEach(tiffFiles, filePath =>
        {
            try
            {
                BaseDecodeType decodeType = DecodeType.AustraliaPost;
                using (BarCodeReader reader = new BarCodeReader(filePath, decodeType))
                {
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // Output each detected barcode.
                    foreach (var result in results)
                    {
                        Console.WriteLine($"File: {Path.GetFileName(filePath)} | Detected: {result.CodeType} | CodeText: {result.CodeText}");
                    }

                    // Inform if no barcode was found in the image.
                    if (results.Length == 0)
                    {
                        Console.WriteLine($"File: {Path.GetFileName(filePath)} | No barcode detected.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing of a specific file.
                Console.WriteLine($"Error processing file '{Path.GetFileName(filePath)}': {ex.Message}");
            }
        });
    }
}