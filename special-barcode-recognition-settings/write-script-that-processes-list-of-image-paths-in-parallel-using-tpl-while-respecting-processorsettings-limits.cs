using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Entry point for the barcode processing application.
/// </summary>
class Program
{
    /// <summary>
    /// Main method that processes image files to read barcodes in parallel.
    /// </summary>
    /// <param name="args">Optional list of image file paths to process.</param>
    static void Main(string[] args)
    {
        // Determine the list of image paths: use command‑line arguments if provided,
        // otherwise fall back to a predefined set of sample images.
        List<string> imagePaths = args.Length > 0
            ? new List<string>(args)
            : new List<string>
            {
                "image1.png",
                "image2.png",
                "image3.png",
                "image4.png",
                "image5.png"
            };

        // Set the maximum degree of parallelism to the number of logical processors.
        int maxDegree = Environment.ProcessorCount;
        var parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = maxDegree };

        // Process each image file in parallel.
        Parallel.ForEach(imagePaths, parallelOptions, path =>
        {
            // Verify that the file exists before attempting to read it.
            if (!File.Exists(path))
            {
                Console.WriteLine($"File not found: {path}");
                return;
            }

            // Create a barcode reader for the current image, supporting all barcode types.
            using (BarCodeReader reader = new BarCodeReader(path, DecodeType.AllSupportedTypes))
            {
                try
                {
                    // Iterate through all detected barcodes in the image.
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"File: {path}");
                        Console.WriteLine($"  Barcode Type : {result.CodeTypeName}");
                        Console.WriteLine($"  Code Text    : {result.CodeText}");
                    }
                }
                catch (Exception ex)
                {
                    // Log any errors that occur during barcode processing.
                    Console.WriteLine($"Error processing {path}: {ex.Message}");
                }
            }
        });
    }
}