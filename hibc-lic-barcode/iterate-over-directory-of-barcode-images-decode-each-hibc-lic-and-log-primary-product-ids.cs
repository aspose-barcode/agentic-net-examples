// Title: Decode HIBC LIC barcodes from image files
// Description: Demonstrates how to iterate through a folder of images, decode HIBC LIC barcodes, and output primary product identifiers.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, focusing on HIBC (Health Industry Bar Code) LIC symbologies. It showcases the BarCodeReader and ComplexCodetextReader classes to extract primary product data, a common requirement for healthcare and pharmaceutical inventory systems. Developers can use this pattern to batch‑process barcode images and integrate product identification into their applications.
// Prompt: Iterate over a directory of barcode images, decode each HIBC LIC, and log primary product IDs.
// Tags: hibc, lic, barcode, decode, console, aspose.barcode, barcodereader, complexcodetextreader

using System;
using System.IO;
using System.Linq;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Program that scans a directory for image files, decodes any HIBC LIC barcodes,
/// and writes the primary product information to the console.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts an optional folder path argument; defaults to "Barcodes".
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument is the folder path.</param>
    static void Main(string[] args)
    {
        // Determine the folder containing barcode images (use argument if provided).
        string folderPath = args.Length > 0 ? args[0] : "Barcodes";

        // Verify that the folder exists before proceeding.
        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Retrieve up to 10 image files with supported extensions.
        var imageFiles = Directory.GetFiles(folderPath)
            .Where(f => f.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                        f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                        f.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                        f.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase) ||
                        f.EndsWith(".tif", StringComparison.OrdinalIgnoreCase) ||
                        f.EndsWith(".tiff", StringComparison.OrdinalIgnoreCase))
            .Take(10)
            .ToArray();

        // If no images were found, inform the user and exit.
        if (imageFiles.Length == 0)
        {
            Console.WriteLine("No image files found in the specified folder.");
            return;
        }

        // Process each image file individually.
        foreach (string filePath in imageFiles)
        {
            // Initialize BarCodeReader to detect all HIBC LIC symbologies.
            using (var reader = new BarCodeReader(
                filePath,
                DecodeType.HIBCCode128LIC,
                DecodeType.HIBCAztecLIC,
                DecodeType.HIBCDataMatrixLIC,
                DecodeType.HIBCQRLIC))
            {
                // Read all barcodes present in the image.
                var results = reader.ReadBarCodes();

                // If no HIBC LIC barcode is detected, report and continue to next file.
                if (results.Length == 0)
                {
                    Console.WriteLine($"{Path.GetFileName(filePath)}: No HIBC LIC barcode detected.");
                    continue;
                }

                // Iterate through each detected barcode result.
                foreach (var result in results)
                {
                    // Attempt to parse the HIBC LIC codetext using the complex reader.
                    var hibcCodetext = ComplexCodetextReader.TryDecodeHIBCLIC(result.CodeText);
                    if (hibcCodetext == null)
                    {
                        Console.WriteLine($"{Path.GetFileName(filePath)}: Unable to decode HIBC LIC codetext.");
                        continue;
                    }

                    // Handle primary data (primary only or combined with secondary data).
                    if (hibcCodetext is HIBCLICPrimaryDataCodetext primary)
                    {
                        Console.WriteLine($"{Path.GetFileName(filePath)}: ProductOrCatalogNumber={primary.Data.ProductOrCatalogNumber}, " +
                                          $"LabelerIdentificationCode={primary.Data.LabelerIdentificationCode}, " +
                                          $"UnitOfMeasureID={primary.Data.UnitOfMeasureID}");
                    }
                    else if (hibcCodetext is HIBCLICCombinedCodetext combined)
                    {
                        var pd = combined.PrimaryData;
                        Console.WriteLine($"{Path.GetFileName(filePath)}: ProductOrCatalogNumber={pd.ProductOrCatalogNumber}, " +
                                          $"LabelerIdentificationCode={pd.LabelerIdentificationCode}, " +
                                          $"UnitOfMeasureID={pd.UnitOfMeasureID}");
                    }
                    else if (hibcCodetext is HIBCLICSecondaryAndAdditionalDataCodetext)
                    {
                        // No primary data present; only secondary or additional data.
                        Console.WriteLine($"{Path.GetFileName(filePath)}: Barcode contains only secondary data.");
                    }
                    else
                    {
                        // Unexpected codetext type.
                        Console.WriteLine($"{Path.GetFileName(filePath)}: Unrecognized HIBC LIC codetext type.");
                    }
                }
            }
        }
    }
}