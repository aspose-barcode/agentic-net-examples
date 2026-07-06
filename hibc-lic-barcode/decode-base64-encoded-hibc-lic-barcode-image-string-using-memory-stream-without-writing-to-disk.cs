// Title: Decode HIBC LIC barcode from Base64 string using memory stream
// Description: Demonstrates how to convert a Base64‑encoded image of a HIBC LIC barcode into a byte array, read it from a MemoryStream, and decode the barcode without writing any files to disk.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, focusing on reading and parsing complex HIBC LIC symbology. It showcases the BarCodeReader with DecodeType.AllSupportedTypes and the ComplexCodetextReader for extracting primary, secondary, and combined data. Developers working with healthcare or logistics barcodes can use these APIs to integrate barcode decoding directly from in‑memory image data.
// Prompt: Decode a base64‑encoded HIBC LIC barcode image string using a memory stream without writing to disk.
// Tags: hibc, lic, barcode, decoding, memorystream, base64, aspose.barcode, c#, .net

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that decodes a HIBC LIC barcode from a Base64‑encoded image using an in‑memory stream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Converts the Base64 string to a byte array, reads the barcode, and prints parsed HIBC LIC data.
    /// </summary>
    static void Main()
    {
        // Base64‑encoded PNG image containing a HIBC LIC barcode.
        // Replace this string with actual Base64‑encoded image data of a HIBC LIC barcode.
        string base64Image = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMCAO+XK6UAAAAASUVORK5CYII=";

        // Decode the Base64 string into a byte array.
        byte[] imageBytes = Convert.FromBase64String(base64Image);

        // Create a memory stream from the byte array so no file I/O is required.
        using (var memoryStream = new MemoryStream(imageBytes))
        // Initialise the barcode reader to recognise all supported types.
        using (var reader = new BarCodeReader(memoryStream, DecodeType.AllSupportedTypes))
        {
            // Read all barcodes found in the image.
            var results = reader.ReadBarCodes();

            // Process each recognised barcode.
            foreach (var result in results)
            {
                // Attempt to parse the codetext as HIBC LIC data.
                var hibc = ComplexCodetextReader.TryDecodeHIBCLIC(result.CodeText);
                if (hibc == null)
                {
                    Console.WriteLine("Unable to parse HIBC LIC codetext.");
                    continue;
                }

                // Determine which HIBC LIC data structure was returned and output its fields.
                switch (hibc)
                {
                    case HIBCLICPrimaryDataCodetext primary:
                        Console.WriteLine("Primary Data:");
                        Console.WriteLine($"Product or Catalog Number: {primary.Data.ProductOrCatalogNumber}");
                        Console.WriteLine($"Labeler Identification Code: {primary.Data.LabelerIdentificationCode}");
                        Console.WriteLine($"Unit of Measure ID: {primary.Data.UnitOfMeasureID}");
                        break;

                    case HIBCLICSecondaryAndAdditionalDataCodetext secondary:
                        Console.WriteLine("Secondary and Additional Data:");
                        Console.WriteLine($"Lot Number: {secondary.Data.LotNumber}");
                        Console.WriteLine($"Serial Number: {secondary.Data.SerialNumber}");
                        Console.WriteLine($"Quantity: {secondary.Data.Quantity}");
                        Console.WriteLine($"Expiry Date: {secondary.Data.ExpiryDate}");
                        Console.WriteLine($"Expiry Date Format: {secondary.Data.ExpiryDateFormat}");
                        Console.WriteLine($"Date of Manufacture: {secondary.Data.DateOfManufacture}");
                        break;

                    case HIBCLICCombinedCodetext combined:
                        Console.WriteLine("Combined Data:");
                        Console.WriteLine($"Product or Catalog Number: {combined.PrimaryData.ProductOrCatalogNumber}");
                        Console.WriteLine($"Labeler Identification Code: {combined.PrimaryData.LabelerIdentificationCode}");
                        Console.WriteLine($"Unit of Measure ID: {combined.PrimaryData.UnitOfMeasureID}");
                        Console.WriteLine($"Lot Number: {combined.SecondaryAndAdditionalData.LotNumber}");
                        Console.WriteLine($"Serial Number: {combined.SecondaryAndAdditionalData.SerialNumber}");
                        Console.WriteLine($"Quantity: {combined.SecondaryAndAdditionalData.Quantity}");
                        Console.WriteLine($"Expiry Date: {combined.SecondaryAndAdditionalData.ExpiryDate}");
                        break;

                    default:
                        Console.WriteLine("Decoded HIBC LIC type not recognized.");
                        break;
                }
            }
        }
    }
}