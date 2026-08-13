// Title: Read and Decode HIBC LIC Barcode Using ComplexCodetextReader
// Description: Demonstrates how to read a HIBC LIC barcode from an image file stream and decode its complex codetext into primary or secondary data fields.
// Category-Description: This example belongs to the Aspose.BarCode barcode reading and complex codetext decoding category. It showcases the use of BarCodeReader to detect HIBC Code128 LIC barcodes and ComplexCodetextReader to parse the structured information contained in the codetext. Developers working with healthcare or logistics barcodes often need to extract product, lot, serial, and expiry details, making this pattern a common requirement in inventory and compliance applications.
// Prompt: Read a HIBC LIC barcode from a file stream and decode it using ComplexCodetextReader.
// Tags: hibc, lic, barcode, reading, decoding, complexcodetextreader, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that reads a HIBC LIC barcode from an image file and decodes its complex codetext.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Opens the image, reads HIBC LIC barcodes, and prints decoded data.
    /// </summary>
    static void Main()
    {
        // Path to the barcode image file
        string imagePath = "hibc_lic.png";

        // Verify that the file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Open the image file as a read‑only stream
        using (FileStream stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
        {
            // Initialize a BarCodeReader for HIBC Code128 LIC symbology
            using (BarCodeReader reader = new BarCodeReader(stream, DecodeType.HIBCCode128LIC))
            {
                bool anyFound = false;

                // Iterate through all detected barcodes in the image
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    anyFound = true;
                    Console.WriteLine($"Raw CodeText: {result.CodeText}");

                    // Attempt to decode the complex HIBC LIC codetext
                    var complex = ComplexCodetextReader.TryDecodeHIBCLIC(result.CodeText);
                    if (complex == null)
                    {
                        Console.WriteLine("Failed to decode complex HIBC LIC codetext.");
                        continue;
                    }

                    // Process the decoded result based on its concrete type
                    if (complex is HIBCLICPrimaryDataCodetext primary)
                    {
                        Console.WriteLine("Decoded as Primary Data:");
                        Console.WriteLine($"Product or Catalog Number: {primary.Data?.ProductOrCatalogNumber}");
                        Console.WriteLine($"Labeler Identification Code: {primary.Data?.LabelerIdentificationCode}");
                        Console.WriteLine($"Unit of Measure ID: {primary.Data?.UnitOfMeasureID}");
                    }
                    else if (complex is HIBCLICSecondaryAndAdditionalDataCodetext secondary)
                    {
                        Console.WriteLine("Decoded as Secondary and Additional Data:");
                        Console.WriteLine($"Lot Number: {secondary.Data?.LotNumber}");
                        Console.WriteLine($"Serial Number: {secondary.Data?.SerialNumber}");
                        Console.WriteLine($"Quantity: {secondary.Data?.Quantity}");
                        Console.WriteLine($"Expiry Date: {secondary.Data?.ExpiryDate}");
                    }
                    else
                    {
                        // Fallback for any other complex codetext types
                        Console.WriteLine($"Decoded complex type: {complex.GetType().Name}");
                    }
                }

                // Inform the user if no barcodes were detected
                if (!anyFound)
                {
                    Console.WriteLine("No barcodes detected in the image.");
                }
            }
        }
    }
}