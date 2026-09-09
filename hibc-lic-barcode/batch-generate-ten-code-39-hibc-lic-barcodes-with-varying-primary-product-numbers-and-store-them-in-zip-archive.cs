// Title: Batch generation of HIBC Code 39 LIC barcodes into a ZIP archive
// Description: Demonstrates how to create ten HIBC Code 39 LIC barcodes with different product numbers using Aspose.BarCode and store them as PNG files in a zip file.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator, HIBCLICPrimaryDataCodetext, and related classes to produce HIBC Code 39 LIC symbology. Typical use cases include batch creation of product identification labels for healthcare or logistics, where each barcode encodes unique primary product data. Developers often need to generate multiple barcodes programmatically and package them for distribution or archival.
// Prompt: Batch generate ten Code 39 HIBC LIC barcodes with varying primary product numbers and store them in a zip archive.
// Tags: barcode, code39, hibc, lic, batch, zip, png, aspose.barcode, complexbarcode

using System;
using System.IO;
using System.IO.Compression;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Generates a set of HIBC Code 39 LIC barcodes with unique product numbers
/// and saves them as PNG images inside a ZIP archive.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates ten barcodes, writes them to a zip file,
    /// and outputs the location of the generated archive.
    /// </summary>
    static void Main()
    {
        // Define the output ZIP file path in the current working directory
        string zipPath = Path.Combine(Directory.GetCurrentDirectory(), "HIBCLIC_Code39.zip");

        // Create a FileStream for the ZIP archive
        using (FileStream zipFileStream = new FileStream(zipPath, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            // Initialize the ZipArchive for adding entries
            using (ZipArchive zipArchive = new ZipArchive(zipFileStream, ZipArchiveMode.Create, leaveOpen: false))
            {
                // Loop to generate ten barcodes with sequential product numbers
                for (int i = 1; i <= 10; i++)
                {
                    // Prepare primary data codetext for the current barcode
                    HIBCLICPrimaryDataCodetext complexCodetext = new HIBCLICPrimaryDataCodetext
                    {
                        BarcodeType = EncodeTypes.HIBCCode39LIC,
                        Data = new PrimaryData
                        {
                            ProductOrCatalogNumber = $"PN{i:D4}",
                            LabelerIdentificationCode = "A999",
                            UnitOfMeasureID = 1
                        }
                    };

                    // Generate the barcode image in memory
                    using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(complexCodetext))
                    {
                        // Set image resolution (pixel size of X-dimension)
                        generator.Parameters.Barcode.XDimension.Pixels = 10;

                        // Save the generated barcode to a memory stream as PNG
                        using (MemoryStream imageStream = new MemoryStream())
                        {
                            generator.Save(imageStream, BarCodeImageFormat.Png);
                            imageStream.Position = 0; // Reset stream position for reading

                            // Create a new entry in the ZIP archive for this barcode image
                            ZipArchiveEntry entry = zipArchive.CreateEntry($"barcode_{i:D2}.png");
                            using (Stream entryStream = entry.Open())
                            {
                                // Copy the PNG data into the ZIP entry
                                imageStream.CopyTo(entryStream);
                            }
                        }
                    }
                }
            }
        }

        // Inform the user where the ZIP file has been saved
        Console.WriteLine($"Generated 10 HIBC Code 39 LIC barcodes and saved to: {zipPath}");
    }
}