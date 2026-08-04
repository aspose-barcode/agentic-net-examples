// Title: Batch decode HIBC LIC barcodes from TIFF files and export to CSV
// Description: Demonstrates how to read HIBC LIC barcodes from a folder of TIFF images using Aspose.BarCode and write the results to a CSV file.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition and generation category. It showcases the use of BarCodeReader for batch decoding, DecodeType for specifying symbology, and ComplexBarcodeGenerator for creating sample barcodes. Typical scenarios include processing large sets of medical or pharmaceutical labels, extracting product information, and exporting data for downstream systems. Developers often need to automate bulk barcode extraction and generate test images, making this pattern a common reference.
// Prompt: Batch decode a folder of TIFF images containing HIBC LIC barcodes and export results to a CSV file.
// Tags: barcode, hibc, lic, tiff, csv, batch, decoding, generation, aspose.barcode, recognition, generation

using System;
using System.IO;
using System.Text;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates batch decoding of HIBC LIC barcodes from TIFF images and exporting the results to a CSV file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Scans a folder for TIFF images, decodes HIBC LIC barcodes,
    /// and writes the filename, barcode type, and decoded text to a CSV file.
    /// </summary>
    static void Main()
    {
        // Define input folder (Barcodes) and output CSV file paths relative to the current directory.
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        string csvPath = Path.Combine(Directory.GetCurrentDirectory(), "results.csv");

        // Ensure the input folder exists; create it if it does not.
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }

        // If the folder is empty, generate a few sample HIBC LIC barcode TIFF images for demonstration.
        string[] existingTiffFiles = Directory.GetFiles(inputFolder, "*.tif");
        if (existingTiffFiles.Length == 0)
        {
            GenerateSampleBarcodes(inputFolder);
        }

        // Open a StreamWriter for the CSV output (UTF‑8 encoding, overwrite existing file).
        using (var csvWriter = new StreamWriter(csvPath, false, Encoding.UTF8))
        {
            // Write CSV header.
            csvWriter.WriteLine("FileName,BarcodeType,CodeText");

            // Iterate over each TIFF file in the input folder.
            foreach (string filePath in Directory.GetFiles(inputFolder, "*.tif"))
            {
                // Verify the file still exists before processing.
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"File not found: {filePath}");
                    continue;
                }

                // Decode using the HIBC LIC Code128 symbology.
                using (var reader = new BarCodeReader(filePath, DecodeType.HIBCCode128LIC))
                {
                    // Read all barcodes found in the image.
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Compose a CSV line with the file name, barcode type, and decoded text.
                        string line = $"{Path.GetFileName(filePath)},{result.CodeTypeName},{result.CodeText}";
                        csvWriter.WriteLine(line);
                        Console.WriteLine(line);
                    }
                }
            }
        }

        Console.WriteLine($"Decoding completed. Results saved to: {csvPath}");
    }

    // Generates a few sample HIBC LIC barcode images (TIFF) for demonstration purposes.
    private static void GenerateSampleBarcodes(string folderPath)
    {
        // Sample data for primary HIBC LIC barcode.
        var primaryData = new PrimaryData
        {
            ProductOrCatalogNumber = "12345",
            LabelerIdentificationCode = "A999",
            UnitOfMeasureID = 1
        };

        // Wrap the primary data in a complex codetext object specifying the barcode type.
        var complexCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC,
            Data = primaryData
        };

        // Create three sample images with slightly different product numbers.
        for (int i = 1; i <= 3; i++)
        {
            // Modify the product number to make each barcode unique.
            primaryData.ProductOrCatalogNumber = $"1234{i}";
            string fileName = Path.Combine(folderPath, $"Sample{i}.tif");

            // Generate the barcode image and save it as a TIFF file.
            using (var generator = new ComplexBarcodeGenerator(complexCodetext))
            {
                generator.Save(fileName, BarCodeImageFormat.Tiff);
            }
        }

        Console.WriteLine($"Generated {3} sample HIBC LIC barcode images in '{folderPath}'.");
    }
}