// Title: Decode HIBC LIC Barcodes from Multiple Images and Log Product IDs
// Description: This example generates a series of HIBC QR LIC barcode images, then iterates over a directory, decodes each barcode, and logs the primary product or catalog number found in the codetext.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition of complex HIBC LIC barcodes. It uses ComplexBarcodeGenerator to create HIBC QR LIC images, BarCodeReader with DecodeType.HIBCQRLIC to read them, and ComplexCodetextReader to parse the resulting codetext. Typical scenarios include batch processing of medical or pharmaceutical labels where product identifiers must be extracted automatically.
// Prompt: Iterate over a directory of barcode images, decode each HIBC LIC, and log primary product IDs.
// Tags: barcode, hibc, lic, decoding, generation, complexbarcode, aspose.barcode, c#, example

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch generation and decoding of HIBC QR LIC barcodes,
/// extracting and logging the primary product or catalog number from each image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample HIBC QR LIC barcodes,
    /// saves them to a temporary folder, then reads each file, decodes the
    /// barcode, and writes the product identifier to the console.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a dedicated temporary folder for sample barcode images
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "HIBCBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // --------------------------------------------------------------------
        // Generate sample barcode images with primary data codetext
        // --------------------------------------------------------------------
        List<string> barcodeFiles = new List<string>();
        for (int i = 1; i <= 5; i++)
        {
            // Build primary data codetext for the current iteration
            HIBCLICPrimaryDataCodetext primaryCodetext = new HIBCLICPrimaryDataCodetext
            {
                BarcodeType = EncodeTypes.HIBCQRLIC,
                Data = new PrimaryData
                {
                    ProductOrCatalogNumber = $"PROD{i:D3}",
                    LabelerIdentificationCode = $"L{i:D3}",
                    UnitOfMeasureID = i
                }
            };

            // Define the output file path for the generated barcode image
            string filePath = Path.Combine(tempFolder, $"HIBCPrimary_{i}.png");

            // Generate and save the barcode image
            using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(primaryCodetext))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 5f;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            barcodeFiles.Add(filePath);
        }

        // --------------------------------------------------------------------
        // Decode each barcode image and log the primary product IDs
        // --------------------------------------------------------------------
        foreach (string file in barcodeFiles)
        {
            // Verify that the image file exists before attempting to read it
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                // Initialize the barcode reader for HIBC QR LIC symbology
                using (BarCodeReader reader = new BarCodeReader(file, DecodeType.HIBCQRLIC))
                {
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // Handle case where no barcode was detected in the image
                    if (results.Length == 0)
                    {
                        Console.WriteLine($"No barcode detected in {Path.GetFileName(file)}");
                        continue;
                    }

                    // Process each detected barcode result
                    foreach (BarCodeResult result in results)
                    {
                        // Attempt to decode the HIBC LIC codetext into a strongly‑typed object
                        HIBCLICComplexCodetext complex = ComplexCodetextReader.TryDecodeHIBCLIC(result.CodeText);

                        // Output the primary product or catalog number based on the decoded type
                        if (complex is HIBCLICPrimaryDataCodetext primary)
                        {
                            Console.WriteLine($"File: {Path.GetFileName(file)} - ProductOrCatalogNumber: {primary.Data.ProductOrCatalogNumber}");
                        }
                        else if (complex is HIBCLICCombinedCodetext combined)
                        {
                            Console.WriteLine($"File: {Path.GetFileName(file)} - ProductOrCatalogNumber: {combined.PrimaryData.ProductOrCatalogNumber}");
                        }
                        else
                        {
                            Console.WriteLine($"File: {Path.GetFileName(file)} - Unrecognized HIBC LIC codetext.");
                        }
                    }
                }
            }
            // Specific handling for image‑loading errors
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error loading image {Path.GetFileName(file)}: {ex.Message}");
            }
            // General exception handling to avoid termination of the batch process
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error processing {Path.GetFileName(file)}: {ex.Message}");
            }
        }
    }
}