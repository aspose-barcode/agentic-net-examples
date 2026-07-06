// Title: Batch decode HIBC LIC barcodes from TIFF images and export to CSV
// Description: Demonstrates how to read multiple TIFF files, iterate through each frame, decode HIBC LIC barcodes, and write the results to a CSV file.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating the use of BarCodeReader with specific DecodeType values (HIBCAztecLIC, HIBCCode128LIC, HIBCDataMatrixLIC, HIBCQRLIC). It shows typical workflows for batch processing image files, handling multi‑page TIFFs, and exporting decoded data, which developers often need when integrating barcode scanning into document processing pipelines.
// Prompt: Batch decode a folder of TIFF images containing HIBC LIC barcodes and export results to a CSV file.
// Tags: hibc, lic, barcode, decoding, tiff, csv, batch-processing, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Linq;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates batch decoding of HIBC LIC barcodes from TIFF images and exporting results to a CSV file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Processes up to 10 TIFF files in the specified input folder,
    /// decodes HIBC LIC barcodes from each frame, and writes the findings to a CSV file.
    /// </summary>
    static void Main()
    {
        // Input folder containing TIFF images
        string inputFolder = "InputTiffImages";

        // Output CSV file path
        string outputCsv = "DecodedBarcodes.csv";

        // Ensure input folder exists; if not, create it and exit because there are no files to process
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder \"{inputFolder}\" does not exist. Creating it.");
            Directory.CreateDirectory(inputFolder);
            return;
        }

        // Retrieve up to 10 TIFF files (both .tif and .tiff extensions) from the folder
        var tiffFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly)
                                 .Where(f => f.EndsWith(".tif", StringComparison.OrdinalIgnoreCase) ||
                                             f.EndsWith(".tiff", StringComparison.OrdinalIgnoreCase))
                                 .Take(10)
                                 .ToArray();

        // Open a StreamWriter for the CSV output; overwrite any existing file
        using (var writer = new StreamWriter(outputCsv, false))
        {
            // Write CSV header line
            writer.WriteLine("FileName,FrameIndex,BarcodeType,CodeText");

            // Process each TIFF file found
            foreach (var filePath in tiffFiles)
            {
                // Extract just the file name for CSV reporting
                string fileName = Path.GetFileName(filePath);

                // Load the TIFF image using Aspose.Drawing
                using (var image = Image.FromFile(filePath))
                {
                    // Determine the number of frames (pages) in the multi‑page TIFF
                    var frameDimension = FrameDimension.Time;
                    int frameCount = image.GetFrameCount(frameDimension);

                    // Iterate through each frame in the TIFF
                    for (int frameIndex = 0; frameIndex < frameCount; frameIndex++)
                    {
                        // Select the current frame as the active image
                        image.SelectActiveFrame(frameDimension, frameIndex);

                        // Save the active frame to a memory stream in PNG format for barcode reading
                        using (var ms = new MemoryStream())
                        {
                            image.Save(ms, ImageFormat.Png);
                            ms.Position = 0; // Reset stream position before reading

                            // Initialize BarCodeReader to look for HIBC LIC barcode types
                            using (var reader = new BarCodeReader(
                                ms,
                                DecodeType.HIBCAztecLIC,
                                DecodeType.HIBCCode128LIC,
                                DecodeType.HIBCDataMatrixLIC,
                                DecodeType.HIBCQRLIC))
                            {
                                // Perform barcode detection on the current frame
                                var results = reader.ReadBarCodes();

                                // If no barcodes were found, write an empty entry for this frame
                                if (results.Length == 0)
                                {
                                    writer.WriteLine($"{fileName},{frameIndex},,");
                                }
                                else
                                {
                                    // Write a CSV line for each detected barcode
                                    foreach (var result in results)
                                    {
                                        writer.WriteLine($"{fileName},{frameIndex},{result.CodeTypeName},{result.CodeText}");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        // Inform the user that processing is complete
        Console.WriteLine($"Decoding completed. Results saved to \"{outputCsv}\".");
    }
}