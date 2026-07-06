// Title: Read and Decode HIBC LIC Barcode Using ComplexCodetextReader
// Description: Demonstrates reading a HIBC LIC barcode from an image file stream and decoding its complex data fields.
// Category-Description: This example belongs to the Aspose.BarCode recognition and complex barcode decoding category. It showcases the BarCodeReader (for image scanning) together with ComplexCodetextReader (for parsing HIBC LIC codetext). Typical use cases include healthcare and pharmaceutical labeling where detailed product, lot, and expiry information must be extracted from HIBC LIC barcodes. Developers often need to read barcode images, identify the symbology, and map the raw codetext to strongly‑typed objects for further processing.
// Prompt: Read a HIBC LIC barcode from a file stream and decode it using ComplexCodetextReader.
// Tags: hibc, lic, barcode, read, complexcodetextreader, aspnet, aspnetcore, csharp

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that reads a HIBC LIC barcode from an image file,
/// decodes the raw codetext using <see cref="ComplexCodetextReader"/>,
/// and prints the extracted fields to the console.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Path to the image file containing the HIBC LIC barcode.
        const string imagePath = "hibc_lic.png";

        // Verify that the file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Open the file as a read‑only stream.
        using (FileStream fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
        // Create a BarCodeReader for the HIBC Code128 LIC symbology.
        using (BarCodeReader reader = new BarCodeReader(fileStream, DecodeType.HIBCCode128LIC))
        {
            // Perform the recognition and obtain all detected barcodes.
            var results = reader.ReadBarCodes();

            // If no barcodes were found, inform the user and exit.
            if (results == null || results.Length == 0)
            {
                Console.WriteLine("No barcode detected in the image.");
                return;
            }

            // Iterate over all detected barcodes.
            foreach (var result in results)
            {
                // Decode the raw codetext into a complex HIBC LIC object.
                var complex = ComplexCodetextReader.TryDecodeHIBCLIC(result.CodeText);

                // If decoding fails, report and continue with the next result.
                if (complex == null)
                {
                    Console.WriteLine("Failed to decode complex HIBC LIC codetext.");
                    continue;
                }

                // Determine the concrete type of the decoded object and output its fields.
                switch (complex)
                {
                    case HIBCLICPrimaryDataCodetext primary:
                        Console.WriteLine("=== Primary Data ===");
                        Console.WriteLine($"Product or Catalog Number: {primary.Data.ProductOrCatalogNumber}");
                        Console.WriteLine($"Labeler Identification Code: {primary.Data.LabelerIdentificationCode}");
                        Console.WriteLine($"Unit of Measure ID: {primary.Data.UnitOfMeasureID}");
                        break;

                    case HIBCLICSecondaryAndAdditionalDataCodetext secondary:
                        Console.WriteLine("=== Secondary and Additional Data ===");
                        Console.WriteLine($"Lot Number: {secondary.Data.LotNumber}");
                        Console.WriteLine($"Serial Number: {secondary.Data.SerialNumber}");
                        Console.WriteLine($"Expiry Date: {secondary.Data.ExpiryDate}");
                        Console.WriteLine($"Expiry Date Format: {secondary.Data.ExpiryDateFormat}");
                        Console.WriteLine($"Quantity: {secondary.Data.Quantity}");
                        Console.WriteLine($"Date of Manufacture: {secondary.Data.DateOfManufacture}");
                        break;

                    case HIBCLICCombinedCodetext combined:
                        Console.WriteLine("=== Combined Data ===");
                        Console.WriteLine($"Product or Catalog Number: {combined.PrimaryData.ProductOrCatalogNumber}");
                        Console.WriteLine($"Labeler Identification Code: {combined.PrimaryData.LabelerIdentificationCode}");
                        Console.WriteLine($"Unit of Measure ID: {combined.PrimaryData.UnitOfMeasureID}");
                        Console.WriteLine($"Lot Number: {combined.SecondaryAndAdditionalData.LotNumber}");
                        Console.WriteLine($"Serial Number: {combined.SecondaryAndAdditionalData.SerialNumber}");
                        Console.WriteLine($"Expiry Date: {combined.SecondaryAndAdditionalData.ExpiryDate}");
                        Console.WriteLine($"Expiry Date Format: {combined.SecondaryAndAdditionalData.ExpiryDateFormat}");
                        Console.WriteLine($"Quantity: {combined.SecondaryAndAdditionalData.Quantity}");
                        Console.WriteLine($"Date of Manufacture: {combined.SecondaryAndAdditionalData.DateOfManufacture}");
                        break;

                    default:
                        Console.WriteLine("Detected HIBC LIC barcode, but type is unrecognized.");
                        break;
                }
            }
        }
    }
}