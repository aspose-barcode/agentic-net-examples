using System;
using System.IO;
using System.Linq;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates reading HIBC barcodes from image files in a specified directory.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Scans a folder for image files, reads HIBC barcodes, and outputs product identifiers.
    /// </summary>
    /// <param name="args">Optional command‑line argument specifying the folder path.</param>
    static void Main(string[] args)
    {
        // Determine the directory containing barcode images.
        // Use the first command‑line argument if provided; otherwise default to "Barcodes".
        string folderPath = args.Length > 0 ? args[0] : "Barcodes";

        // Verify that the directory exists before proceeding.
        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine($"Directory not found: {folderPath}");
            return;
        }

        // Define the set of supported image file extensions.
        string[] extensions = new[] { ".png", ".jpg", ".jpeg", ".bmp", ".gif", ".tif", ".tiff" };

        // Retrieve up to 10 image files that match the supported extensions.
        var imageFiles = Directory.GetFiles(folderPath)
                                  .Where(f => extensions.Contains(Path.GetExtension(f), StringComparer.OrdinalIgnoreCase))
                                  .Take(10) // safe sample size for the runner
                                  .ToArray();

        // If no matching files are found, inform the user and exit.
        if (imageFiles.Length == 0)
        {
            Console.WriteLine("No barcode image files found in the directory.");
            return;
        }

        // Process each image file individually.
        foreach (var filePath in imageFiles)
        {
            string fileName = Path.GetFileName(filePath);

            // Initialize a BarCodeReader configured for all HIBC LIC decode types.
            using (var reader = new BarCodeReader(
                filePath,
                DecodeType.HIBCCode128LIC,
                DecodeType.HIBCAztecLIC,
                DecodeType.HIBCDataMatrixLIC,
                DecodeType.HIBCQRLIC))
            {
                // Read all barcodes present in the image.
                var results = reader.ReadBarCodes();

                // If no HIBC barcodes were detected, report and continue to the next file.
                if (results.Length == 0)
                {
                    Console.WriteLine($"{fileName}: No HIBC barcode detected.");
                    continue;
                }

                // Iterate over each detected barcode.
                foreach (var result in results)
                {
                    // Attempt to parse the complex HIBC codetext into a strongly‑typed object.
                    var hibcObject = ComplexCodetextReader.TryDecodeHIBCLIC(result.CodeText);
                    if (hibcObject == null)
                    {
                        Console.WriteLine($"{fileName}: Unable to decode HIBC codetext.");
                        continue;
                    }

                    string productId = null;

                    // Extract the primary product or catalog number based on the specific HIBC type.
                    if (hibcObject is HIBCLICPrimaryDataCodetext primary)
                    {
                        // Primary data only.
                        productId = primary.Data?.ProductOrCatalogNumber;
                    }
                    else if (hibcObject is HIBCLICCombinedCodetext combined)
                    {
                        // Combined primary + secondary data.
                        productId = combined.PrimaryData?.ProductOrCatalogNumber;
                    }
                    else if (hibcObject is HIBCLICSecondaryAndAdditionalDataCodetext)
                    {
                        // Secondary‑only codetext does not contain a primary product ID.
                        productId = null;
                    }

                    // Output the extracted product identifier, or indicate its absence.
                    if (!string.IsNullOrEmpty(productId))
                    {
                        Console.WriteLine($"{fileName}: ProductOrCatalogNumber = {productId}");
                    }
                    else
                    {
                        Console.WriteLine($"{fileName}: No primary product ID found.");
                    }
                }
            }
        }
    }
}