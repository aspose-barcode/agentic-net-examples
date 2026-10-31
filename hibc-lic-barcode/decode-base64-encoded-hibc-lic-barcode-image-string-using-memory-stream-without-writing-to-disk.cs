// Title: Decode HIBC LIC barcode from Base64 string using memory stream
// Description: Demonstrates how to generate a HIBC LIC barcode, convert it to a Base64 string, then decode it back from memory without writing files.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of ComplexBarcodeGenerator for creating HIBC LIC barcodes, BarCodeReader for decoding, and ComplexCodetextReader for parsing complex HIBC LIC codetext. Developers working with healthcare product labeling often need to generate and read HIBC LIC barcodes directly in memory for web services or automated pipelines.
// Prompt: Decode a base64‑encoded HIBC LIC barcode image string using a memory stream without writing to disk.
// Tags: hibc lic, barcode generation, barcode recognition, base64, memory stream, aspnet, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generating a HIBC LIC barcode, encoding it to Base64, and decoding it from a memory stream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a sample barcode, prints its Base64 representation, and decodes it back to display parsed data.
    /// </summary>
    static void Main()
    {
        // Generate a sample HIBC LIC barcode and obtain its Base64 representation
        string base64 = GenerateSampleBarcodeBase64();

        // Output the Base64 string to the console
        Console.WriteLine("Base64 barcode image:");
        Console.WriteLine(base64);
        Console.WriteLine();

        // Decode the Base64 string back to an image byte array
        byte[] imageBytes = Convert.FromBase64String(base64);

        // Use a memory stream to avoid writing the image to disk
        using (MemoryStream ms = new MemoryStream(imageBytes))
        {
            // Initialize the barcode reader to recognize all supported types
            using (BarCodeReader reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
            {
                // Iterate through all detected barcodes in the image
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // Attempt to parse the HIBC LIC complex codetext
                    HIBCLICComplexCodetext complex = ComplexCodetextReader.TryDecodeHIBCLIC(result.CodeText);
                    if (complex == null)
                    {
                        Console.WriteLine("Failed to decode HIBC LIC complex codetext.");
                        continue;
                    }

                    // Handle primary data only barcode
                    if (complex is HIBCLICPrimaryDataCodetext primary)
                    {
                        Console.WriteLine("Decoded HIBC LIC Primary Data:");
                        Console.WriteLine($"Product or catalog number: {primary.Data.ProductOrCatalogNumber}");
                        Console.WriteLine($"Labeler identification code: {primary.Data.LabelerIdentificationCode}");
                        Console.WriteLine($"Unit of measure ID: {primary.Data.UnitOfMeasureID}");
                    }
                    // Handle secondary and additional data barcode
                    else if (complex is HIBCLICSecondaryAndAdditionalDataCodetext secondary)
                    {
                        Console.WriteLine("Decoded HIBC LIC Secondary Data:");
                        Console.WriteLine($"Expiry date: {secondary.Data.ExpiryDate}");
                        Console.WriteLine($"Quantity: {secondary.Data.Quantity}");
                        Console.WriteLine($"Lot number: {secondary.Data.LotNumber}");
                        Console.WriteLine($"Serial number: {secondary.Data.SerialNumber}");
                        Console.WriteLine($"Date of manufacture: {secondary.Data.DateOfManufacture}");
                    }
                    // Handle combined primary and secondary data barcode
                    else if (complex is HIBCLICCombinedCodetext combined)
                    {
                        Console.WriteLine("Decoded HIBC LIC Combined Data:");
                        Console.WriteLine($"Product or catalog number: {combined.PrimaryData.ProductOrCatalogNumber}");
                        Console.WriteLine($"Labeler identification code: {combined.PrimaryData.LabelerIdentificationCode}");
                        Console.WriteLine($"Unit of measure ID: {combined.PrimaryData.UnitOfMeasureID}");
                        Console.WriteLine($"Expiry date: {combined.SecondaryAndAdditionalData.ExpiryDate}");
                        Console.WriteLine($"Quantity: {combined.SecondaryAndAdditionalData.Quantity}");
                        Console.WriteLine($"Lot number: {combined.SecondaryAndAdditionalData.LotNumber}");
                        Console.WriteLine($"Serial number: {combined.SecondaryAndAdditionalData.SerialNumber}");
                        Console.WriteLine($"Date of manufacture: {combined.SecondaryAndAdditionalData.DateOfManufacture}");
                    }
                    else
                    {
                        Console.WriteLine("Decoded HIBC LIC unknown type.");
                    }
                }
            }
        }
    }

    /// <summary>
    /// Generates a sample HIBC LIC barcode, saves it to a memory stream, and returns its Base64 string.
    /// </summary>
    /// <returns>Base64-encoded PNG image of the generated barcode.</returns>
    static string GenerateSampleBarcodeBase64()
    {
        // Create primary data codetext for HIBC LIC
        HIBCLICPrimaryDataCodetext codetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCQRLIC,
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",
                LabelerIdentificationCode = "A999",
                UnitOfMeasureID = 1
            }
        };

        // Render the barcode into a memory stream
        using (MemoryStream ms = new MemoryStream())
        {
            using (ComplexBarcodeGenerator gen = new ComplexBarcodeGenerator(codetext))
            {
                // Adjust image quality settings
                gen.Parameters.Barcode.XDimension.Pixels = 10;
                gen.Save(ms, BarCodeImageFormat.Png);
            }

            // Convert the image bytes to a Base64 string
            return Convert.ToBase64String(ms.ToArray());
        }
    }
}