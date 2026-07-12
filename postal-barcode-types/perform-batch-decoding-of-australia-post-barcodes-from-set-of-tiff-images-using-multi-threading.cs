// Title: Batch decode Australia Post barcodes from TIFF images using multi‑threading
// Description: Demonstrates how to read Australia Post barcodes from multi‑frame TIFF files in parallel, improving throughput for large image sets.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing multi‑threaded processing of image collections. It uses BarCodeReader, QualitySettings, and DecodeType classes to efficiently decode barcodes. Developers often need to batch‑process scanned documents or shipping labels, and this pattern illustrates best practices for parallel decoding and handling multi‑frame TIFFs.
// Prompt: Perform batch decoding of Australia Post barcodes from a set of TIFF images using multi‑threading.
// Tags: australia post, barcode, decoding, multithreading, tiff, aspose.barcode, qualitysettings

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that decodes Australia Post barcodes from TIFF images using parallel processing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Scans a folder for TIFF files, extracts each frame, and decodes Australia Post barcodes in parallel.
    /// </summary>
    static void Main()
    {
        // Folder containing TIFF images
        string folderPath = "Barcodes";

        // Verify the folder exists
        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Retrieve up to 5 TIFF files for a safe sample size
        string[] tiffFiles = Directory.GetFiles(folderPath, "*.tif");
        int maxFiles = Math.Min(tiffFiles.Length, 5);
        if (maxFiles == 0)
        {
            Console.WriteLine("No TIFF files found.");
            return;
        }

        // Configure the barcode processor to use all available CPU cores
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;

        // Process each file in parallel, limiting degree of parallelism to the number of cores
        Parallel.ForEach(tiffFiles, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, (file, state, index) =>
        {
            // Enforce the sample size limit
            if (index >= maxFiles) return;

            try
            {
                // Load the TIFF image (supports multi‑frame TIFFs)
                using (Image tiffImage = Image.FromFile(file))
                {
                    // Identify the time dimension for frames (multi‑frame TIFF)
                    FrameDimension frameDimension = FrameDimension.Time;
                    int frameCount = tiffImage.GetFrameCount(frameDimension);

                    // Iterate through each frame in the TIFF
                    for (int frameIndex = 0; frameIndex < frameCount; frameIndex++)
                    {
                        tiffImage.SelectActiveFrame(frameDimension, frameIndex);

                        // Convert the current frame to a PNG stream (Aspose.Drawing works well with PNG)
                        using (MemoryStream ms = new MemoryStream())
                        {
                            tiffImage.Save(ms, ImageFormat.Png);
                            ms.Position = 0;

                            // Create a bitmap from the PNG stream for barcode reading
                            using (Bitmap bitmap = new Bitmap(ms))
                            {
                                // Initialize the barcode reader for Australia Post symbology
                                using (BarCodeReader reader = new BarCodeReader(bitmap, DecodeType.AustraliaPost))
                                {
                                    // Apply a high‑performance quality preset to speed up decoding
                                    reader.QualitySettings = QualitySettings.HighPerformance;

                                    // Optional: configure interpreting type if required
                                    // reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;

                                    // Perform the decoding
                                    BarCodeResult[] results = reader.ReadBarCodes();

                                    // Output each decoded result
                                    foreach (BarCodeResult result in results)
                                    {
                                        var rect = result.Region.Rectangle;
                                        Console.WriteLine($"File: {Path.GetFileName(file)}, Frame: {frameIndex}, Type: {result.CodeTypeName}, Text: {result.CodeText}, Region: {rect}");
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log any errors encountered while processing the file
                Console.WriteLine($"Error processing file '{Path.GetFileName(file)}': {ex.Message}");
            }
        });
    }
}