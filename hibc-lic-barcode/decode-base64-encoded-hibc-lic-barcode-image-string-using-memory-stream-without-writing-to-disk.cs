// Title: Decode HIBC LIC barcode from a Base64 image using a memory stream
// Description: Demonstrates how to convert a Base64‑encoded PNG containing a HIBC LIC barcode into a byte array, read it from a MemoryStream, and decode the barcode without writing any files to disk.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing the BarCodeReader class with DecodeType.AllSupportedTypes and the ComplexCodetextReader for parsing HIBC LIC complex codetext. Typical use cases include processing barcode images received over network APIs or embedded in JSON payloads where disk I/O is undesirable. Developers often need to decode barcodes directly from streams and extract structured data such as product numbers, expiry dates, and lot information.
// Prompt: Decode a base64‑encoded HIBC LIC barcode image string using a memory stream without writing to disk.
// Tags: hibc lic, barcode decoding, base64, memorystream, aspose.barcode, complexcodetext, csharp

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that decodes a HIBC LIC barcode from a Base64‑encoded image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Converts the Base64 string to a byte array, reads the image from a MemoryStream,
    /// detects barcodes, and parses HIBC LIC complex codetext.
    /// </summary>
    static void Main()
    {
        // Base64‑encoded PNG image of a HIBC LIC barcode.
        // Replace this string with an actual base64 image when available.
        string base64Image = "";

        // Validate that the input string contains data.
        if (string.IsNullOrWhiteSpace(base64Image))
        {
            Console.WriteLine("No barcode image data provided.");
            return;
        }

        byte[] imageBytes;
        try
        {
            // Convert the Base64 string to a byte array.
            imageBytes = Convert.FromBase64String(base64Image);
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid Base64 string.");
            return;
        }

        // Load the image bytes into a memory stream (no file I/O required).
        using (MemoryStream ms = new MemoryStream(imageBytes))
        {
            // Initialize the barcode reader to detect any supported barcode type.
            using (BarCodeReader reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
            {
                bool anyFound = false;

                // Iterate through all detected barcodes in the image.
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    anyFound = true;
                    Console.WriteLine($"Detected barcode type: {result.CodeTypeName}");
                    Console.WriteLine($"Raw CodeText: {result.CodeText}");

                    // Attempt to parse the HIBC LIC complex codetext.
                    HIBCLICComplexCodetext complex = ComplexCodetextReader.TryDecodeHIBCLIC(result.CodeText);
                    if (complex == null)
                    {
                        Console.WriteLine("Failed to parse HIBC LIC codetext.");
                        continue;
                    }

                    // Handle combined primary and secondary data.
                    if (complex is HIBCLICCombinedCodetext combined)
                    {
                        Console.WriteLine("=== Combined Data ===");
                        if (combined.PrimaryData != null)
                        {
                            Console.WriteLine($"Product or catalog number: {combined.PrimaryData.ProductOrCatalogNumber}");
                            Console.WriteLine($"Labeler identification code: {combined.PrimaryData.LabelerIdentificationCode}");
                            Console.WriteLine($"Unit of measure ID: {combined.PrimaryData.UnitOfMeasureID}");
                        }
                        if (combined.SecondaryAndAdditionalData != null)
                        {
                            Console.WriteLine($"Expiry date: {combined.SecondaryAndAdditionalData.ExpiryDate}");
                            Console.WriteLine($"Quantity: {combined.SecondaryAndAdditionalData.Quantity}");
                            Console.WriteLine($"Lot number: {combined.SecondaryAndAdditionalData.LotNumber}");
                            Console.WriteLine($"Serial number: {combined.SecondaryAndAdditionalData.SerialNumber}");
                            Console.WriteLine($"Date of manufacture: {combined.SecondaryAndAdditionalData.DateOfManufacture}");
                        }
                    }
                    // Handle primary data only.
                    else if (complex is HIBCLICPrimaryDataCodetext primary)
                    {
                        Console.WriteLine("=== Primary Data ===");
                        if (primary.Data != null)
                        {
                            Console.WriteLine($"Product or catalog number: {primary.Data.ProductOrCatalogNumber}");
                            Console.WriteLine($"Labeler identification code: {primary.Data.LabelerIdentificationCode}");
                            Console.WriteLine($"Unit of measure ID: {primary.Data.UnitOfMeasureID}");
                        }
                    }
                    // Handle secondary and additional data only.
                    else if (complex is HIBCLICSecondaryAndAdditionalDataCodetext secondary)
                    {
                        Console.WriteLine("=== Secondary and Additional Data ===");
                        if (secondary.Data != null)
                        {
                            Console.WriteLine($"Expiry date: {secondary.Data.ExpiryDate}");
                            Console.WriteLine($"Quantity: {secondary.Data.Quantity}");
                            Console.WriteLine($"Lot number: {secondary.Data.LotNumber}");
                            Console.WriteLine($"Serial number: {secondary.Data.SerialNumber}");
                            Console.WriteLine($"Date of manufacture: {secondary.Data.DateOfManufacture}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Unrecognized HIBC LIC codetext type.");
                    }
                }

                // Inform the user if no barcodes were detected.
                if (!anyFound)
                {
                    Console.WriteLine("No barcodes were detected in the image.");
                }
            }
        }
    }
}