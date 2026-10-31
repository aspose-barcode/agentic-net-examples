// Title: Decode HIBC LIC Barcodes from a Directory and Log Product IDs
// Description: This example generates sample HIBC QR LIC barcodes, saves them to a temporary folder, then iterates over the images, decodes each barcode, and outputs the primary product or catalog number.
// Category-Description: Demonstrates Aspose.BarCode complex barcode generation and recognition for HIBC LIC symbology. It uses ComplexBarcodeGenerator to create HIBCLICPrimaryDataCodetext, BarCodeReader with DecodeType.HIBCQRLIC to read images, and ComplexCodetextReader to parse the complex codetext. Typical use cases include batch processing of medical device labels, inventory tracking, and validation of HIBC QR codes in healthcare applications.
// Prompt: Iterate over a directory of barcode images, decode each HIBC LIC, and log primary product IDs.
// Tags: barcode, hibc, lic, decoding, batch processing, aspose.barcode, complexbarcode, qr, productid

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Sample program that creates HIBC LIC barcodes, reads them back, and logs the primary product identifiers.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, decodes each file, and writes product numbers to the console.
    /// </summary>
    static void Main()
    {
        // ----------------------------------------------------------------------
        // Create a dedicated temporary folder for sample barcode images
        // ----------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "HIBCBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // ----------------------------------------------------------------------
        // Generate sample HIBC LIC primary data barcodes and collect file paths
        // ----------------------------------------------------------------------
        List<string> barcodeFiles = new List<string>();
        for (int i = 1; i <= 5; i++)
        {
            // Prepare primary data codetext
            HIBCLICPrimaryDataCodetext primaryCodetext = new HIBCLICPrimaryDataCodetext
            {
                BarcodeType = EncodeTypes.HIBCQRLIC,
                Data = new PrimaryData
                {
                    ProductOrCatalogNumber = $"PROD{i:D3}",
                    LabelerIdentificationCode = "A999",
                    UnitOfMeasureID = 1
                }
            };

            // Generate barcode image and save to temporary folder
            string filePath = Path.Combine(tempFolder, $"HIBCPrimary_{i}.png");
            using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(primaryCodetext))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 10f;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            barcodeFiles.Add(filePath);
        }

        // ----------------------------------------------------------------------
        // Decode each barcode file and log the primary product IDs
        // ----------------------------------------------------------------------
        BaseDecodeType decodeType = DecodeType.HIBCQRLIC;
        foreach (string file in barcodeFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                using (BarCodeReader reader = new BarCodeReader(file, decodeType))
                {
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        // Attempt to decode HIBC LIC complex codetext
                        HIBCLICComplexCodetext complex = ComplexCodetextReader.TryDecodeHIBCLIC(result.CodeText);
                        if (complex == null)
                        {
                            Console.WriteLine($"Unable to decode HIBC LIC in file: {file}");
                            continue;
                        }

                        // Handle primary data codetext (stand‑alone or within combined codetext)
                        if (complex is HIBCLICPrimaryDataCodetext primary)
                        {
                            string productId = primary.Data?.ProductOrCatalogNumber ?? "(null)";
                            Console.WriteLine($"File: {Path.GetFileName(file)} - ProductOrCatalogNumber: {productId}");
                        }
                        else if (complex is HIBCLICCombinedCodetext combined && combined.PrimaryData != null)
                        {
                            string productId = combined.PrimaryData.ProductOrCatalogNumber ?? "(null)";
                            Console.WriteLine($"File: {Path.GetFileName(file)} - ProductOrCatalogNumber: {productId}");
                        }
                        else
                        {
                            Console.WriteLine($"File: {Path.GetFileName(file)} - No primary data found.");
                        }
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error loading image '{file}': {ex.Message}");
            }
        }

        // ----------------------------------------------------------------------
        // Cleanup: delete temporary folder and its contents
        // ----------------------------------------------------------------------
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // If deletion fails, ignore – folder will be cleaned up by the OS eventually.
        }
    }
}