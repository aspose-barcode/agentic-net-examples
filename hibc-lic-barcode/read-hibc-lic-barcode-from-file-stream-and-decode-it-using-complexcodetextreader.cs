// Title: Read and Decode HIBC LIC Barcode Using ComplexCodetextReader
// Description: Demonstrates generating a HIBC QR LIC barcode, saving it to a temporary file, and decoding its complex codetext from a file stream.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation and recognition category. It showcases the use of ComplexBarcodeGenerator to create HIBC LIC barcodes, BarCodeReader for image decoding, and ComplexCodetextReader for parsing the complex codetext. Typical scenarios include healthcare product labeling and inventory tracking where HIBC standards are required. Developers often need to generate, read, and interpret primary, secondary, or combined HIBC data using these APIs.
// Prompt: Read a HIBC LIC barcode from a file stream and decode it using ComplexCodetextReader.
// Tags: hibc, lic, barcode, generation, recognition, complexcodetextreader, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates creating a HIBC QR LIC barcode, saving it to a temporary file,
/// and decoding its complex codetext using Aspose.BarCode APIs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, reads it from a file stream,
    /// and outputs decoded primary, secondary, or combined data.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a temporary folder for the barcode image
        string tempDir = Path.Combine(Path.GetTempPath(), "HIBCLICDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "hibclic.png");

        // Build primary data codetext for a HIBC QR LIC barcode
        HIBCLICPrimaryDataCodetext primaryCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCQRLIC,
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",
                LabelerIdentificationCode = "A999",
                UnitOfMeasureID = 1
            }
        };

        // Generate the barcode image and save it to the temporary file
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(primaryCodetext))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 10;
            generator.Save(barcodePath);
        }

        // Verify the file was created before attempting to read it
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read and decode the barcode from a file stream
        using (FileStream fs = new FileStream(barcodePath, FileMode.Open, FileAccess.Read))
        using (BarCodeReader reader = new BarCodeReader(fs, DecodeType.HIBCQRLIC))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Decode the complex HIBC LIC codetext
                var complex = ComplexCodetextReader.TryDecodeHIBCLIC(result.CodeText);
                if (complex == null)
                {
                    Console.WriteLine("Unable to decode complex codetext.");
                    continue;
                }

                // Primary data only
                if (complex is HIBCLICPrimaryDataCodetext primary)
                {
                    Console.WriteLine("=== Primary Data ===");
                    Console.WriteLine($"Product or catalog number: {primary.Data.ProductOrCatalogNumber}");
                    Console.WriteLine($"Labeler identification code: {primary.Data.LabelerIdentificationCode}");
                    Console.WriteLine($"Unit of measure ID: {primary.Data.UnitOfMeasureID}");
                }
                // Secondary data only
                else if (complex is HIBCLICSecondaryAndAdditionalDataCodetext secondary)
                {
                    Console.WriteLine("=== Secondary Data ===");
                    var d = secondary.Data;
                    Console.WriteLine($"Expiry date: {d.ExpiryDate}");
                    Console.WriteLine($"Quantity: {d.Quantity}");
                    Console.WriteLine($"Lot number: {d.LotNumber}");
                    Console.WriteLine($"Serial number: {d.SerialNumber}");
                    Console.WriteLine($"Date of manufacture: {d.DateOfManufacture}");
                    Console.WriteLine($"Link character: {secondary.LinkCharacter}");
                }
                // Combined primary and secondary data
                else if (complex is HIBCLICCombinedCodetext combined)
                {
                    Console.WriteLine("=== Combined Data ===");
                    var p = combined.PrimaryData;
                    var s = combined.SecondaryAndAdditionalData;
                    Console.WriteLine($"Product or catalog number: {p.ProductOrCatalogNumber}");
                    Console.WriteLine($"Labeler identification code: {p.LabelerIdentificationCode}");
                    Console.WriteLine($"Unit of measure ID: {p.UnitOfMeasureID}");
                    Console.WriteLine($"Expiry date: {s.ExpiryDate}");
                    Console.WriteLine($"Quantity: {s.Quantity}");
                    Console.WriteLine($"Lot number: {s.LotNumber}");
                    Console.WriteLine($"Serial number: {s.SerialNumber}");
                    Console.WriteLine($"Date of manufacture: {s.DateOfManufacture}");
                }
                else
                {
                    Console.WriteLine("Decoded codetext type is unrecognized.");
                }
            }
        }

        // Cleanup temporary files (optional)
        // Directory.Delete(tempDir, true);
    }
}