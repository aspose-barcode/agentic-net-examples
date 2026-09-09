// Title: Decode HIBC LIC barcode from image using ComplexCodetextReader
// Description: Demonstrates reading a HIBC LIC barcode from a file stream and decoding its complex codetext into primary and secondary data components.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, focusing on HIBC symbologies. It showcases the BarCodeReader with DecodeType.HIBCQRLIC and the ComplexCodetextReader for parsing HIBC LIC codetext into structured objects such as HIBCLICCombinedCodetext, HIBCLICPrimaryDataCodetext, and HIBCLICSecondaryAndAdditionalDataCodetext. Developers working with healthcare or logistics barcodes can use this pattern to extract detailed product information from scanned images.
// Prompt: Read a HIBC LIC barcode from a file stream and decode it using ComplexCodetextReader.
// Tags: hibc, lic, barcode, decoding, complexcodetextreader, barcodereader, aspnet, csharp

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that reads a HIBC LIC barcode from an image file and decodes its complex codetext.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts an optional file path argument; defaults to "hibc_lic.png".
    /// </summary>
    /// <param name="args">Command‑line arguments.</param>
    static void Main(string[] args)
    {
        // Determine the image file to process: use first argument or fallback to default name.
        string filePath = args.Length > 0 ? args[0] : "hibc_lic.png";

        // Verify that the file exists before attempting to read it.
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"File not found: {filePath}");
            return;
        }

        // Open the image file as a read‑only stream.
        using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            // Initialize the barcode reader for HIBC QR/LIC symbology.
            using (BarCodeReader reader = new BarCodeReader(stream, DecodeType.HIBCQRLIC))
            {
                bool anyBarcode = false;

                // Iterate through all detected barcodes in the image.
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    anyBarcode = true;

                    // Attempt to decode the complex HIBC LIC codetext.
                    var complex = ComplexCodetextReader.TryDecodeHIBCLIC(result.CodeText);
                    if (complex == null)
                    {
                        Console.WriteLine("Failed to decode HIBC LIC complex codetext.");
                        continue;
                    }

                    // Handle the different possible complex codetext types.
                    if (complex is HIBCLICCombinedCodetext combined)
                    {
                        Console.WriteLine("Combined HIBC LIC barcode:");
                        PrintPrimary(combined.PrimaryData);
                        PrintSecondary(combined.SecondaryAndAdditionalData);
                    }
                    else if (complex is HIBCLICPrimaryDataCodetext primary)
                    {
                        Console.WriteLine("Primary HIBC LIC barcode:");
                        PrintPrimary(primary.Data);
                    }
                    else if (complex is HIBCLICSecondaryAndAdditionalDataCodetext secondary)
                    {
                        Console.WriteLine("Secondary HIBC LIC barcode:");
                        PrintSecondary(secondary.Data);
                        Console.WriteLine($"LinkCharacter: {secondary.LinkCharacter}");
                    }
                    else
                    {
                        Console.WriteLine("Unknown HIBC LIC codetext type.");
                    }
                }

                // Inform the user if no barcodes were found in the image.
                if (!anyBarcode)
                {
                    Console.WriteLine("No barcodes detected.");
                }
            }
        }
    }

    /// <summary>
    /// Prints primary data fields of a HIBC LIC barcode.
    /// </summary>
    /// <param name="data">Primary data object.</param>
    static void PrintPrimary(PrimaryData data)
    {
        if (data == null) return;
        Console.WriteLine($"Product or catalog number: {data.ProductOrCatalogNumber}");
        Console.WriteLine($"Labeler identification code: {data.LabelerIdentificationCode}");
        Console.WriteLine($"Unit of measure ID: {data.UnitOfMeasureID}");
    }

    /// <summary>
    /// Prints secondary and additional data fields of a HIBC LIC barcode.
    /// </summary>
    /// <param name="data">Secondary and additional data object.</param>
    static void PrintSecondary(SecondaryAndAdditionalData data)
    {
        if (data == null) return;
        Console.WriteLine($"Expiry date: {data.ExpiryDate}");
        Console.WriteLine($"Quantity: {data.Quantity}");
        Console.WriteLine($"Lot number: {data.LotNumber}");
        Console.WriteLine($"Serial number: {data.SerialNumber}");
        Console.WriteLine($"Date of manufacture: {data.DateOfManufacture}");
    }
}