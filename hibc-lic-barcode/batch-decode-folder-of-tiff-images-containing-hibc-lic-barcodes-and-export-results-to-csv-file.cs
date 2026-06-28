using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Program that scans multi‑frame TIFF files for HIBC LIC barcodes and writes results to a CSV file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Accepts optional command‑line arguments: input folder path and output CSV file path.
    /// </summary>
    /// <param name="args">Command‑line arguments.</param>
    static void Main(string[] args)
    {
        // Determine input folder and output CSV file from arguments or use defaults.
        string inputFolder = args.Length > 0 ? args[0] : "InputTiff";
        string outputCsv   = args.Length > 1 ? args[1] : "Barcodes.csv";

        // Verify that the input folder exists before proceeding.
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Open the CSV writer; overwrite any existing file.
        using (var writer = new StreamWriter(outputCsv, false))
        {
            // Write CSV header.
            writer.WriteLine("FileName,FrameIndex,BarcodeType,CodeText");

            // Collect all TIFF files (both .tif and .tiff) from the input folder.
            var tiffFiles = new List<string>();
            tiffFiles.AddRange(Directory.GetFiles(inputFolder, "*.tif"));
            tiffFiles.AddRange(Directory.GetFiles(inputFolder, "*.tiff"));

            // Process each TIFF file individually.
            foreach (var filePath in tiffFiles)
            {
                try
                {
                    // Load the multi‑frame TIFF using Aspose.Drawing.
                    using (var image = Image.FromFile(filePath))
                    {
                        // Use the Time dimension to access frames.
                        var frameDimension = FrameDimension.Time;
                        int frameCount = image.GetFrameCount(frameDimension);

                        // Iterate through each frame in the TIFF.
                        for (int frameIndex = 0; frameIndex < frameCount; frameIndex++)
                        {
                            // Select the current frame for processing.
                            image.SelectActiveFrame(frameDimension, frameIndex);

                            // Export the selected frame to a memory stream in PNG format (compatible with the barcode reader).
                            using (var ms = new MemoryStream())
                            {
                                image.Save(ms, ImageFormat.Png);
                                ms.Position = 0; // Reset stream position for reading.

                                // Initialize the barcode reader to decode only HIBC LIC symbologies.
                                using (var reader = new BarCodeReader(
                                    ms,
                                    DecodeType.HIBCAztecLIC,
                                    DecodeType.HIBCCode128LIC,
                                    DecodeType.HIBCDataMatrixLIC,
                                    DecodeType.HIBCQRLIC))
                                {
                                    // Read all barcodes found in the current frame.
                                    foreach (var result in reader.ReadBarCodes())
                                    {
                                        // Build a CSV line with file name, frame index, barcode type, and decoded text.
                                        string line = $"{Path.GetFileName(filePath)},{frameIndex},{result.CodeTypeName},{result.CodeText}";
                                        writer.WriteLine(line);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log any errors and continue processing remaining files.
                    Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                }
            }
        }

        // Inform the user that processing is complete.
        Console.WriteLine($"Decoding completed. Results saved to '{outputCsv}'.");
    }
}