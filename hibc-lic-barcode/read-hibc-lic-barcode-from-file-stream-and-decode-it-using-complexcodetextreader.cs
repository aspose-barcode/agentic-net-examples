using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

class Program
{
    static void Main()
    {
        // Path to the image file containing the HIBC LIC barcode
        string imagePath = "hibc_lic.png";

        // Verify that the file exists
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Open the image file as a read‑only stream
        using (FileStream stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
        {
            // Create a barcode reader that supports all barcode types
            using (BarCodeReader reader = new BarCodeReader(stream, DecodeType.AllSupportedTypes))
            {
                // Read all barcodes from the image
                BarCodeResult[] results = reader.ReadBarCodes();

                if (results == null || results.Length == 0)
                {
                    Console.WriteLine("No barcodes were detected in the image.");
                    return;
                }

                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"Detected barcode symbology: {result.CodeTypeName}");
                    Console.WriteLine($"Raw CodeText: {result.CodeText}");

                    // Decode the HIBC LIC codetext using ComplexCodetextReader
                    HIBCLICComplexCodetext decoded = ComplexCodetextReader.TryDecodeHIBCLIC(result.CodeText);

                    if (decoded == null)
                    {
                        Console.WriteLine("Failed to decode HIBC LIC complex codetext.");
                        continue;
                    }

                    // Output basic decoded information
                    Console.WriteLine($"Decoded Barcode Type: {decoded.GetBarcodeType()}");
                    Console.WriteLine($"Decoded object type: {decoded.GetType().Name}");

                    // If the decoded object contains secondary data, display it
                    if (decoded is HIBCLICSecondaryAndAdditionalDataCodetext secondary)
                    {
                        if (secondary.Data != null)
                        {
                            Console.WriteLine($"Lot Number: {secondary.Data.LotNumber}");
                            Console.WriteLine($"Serial Number: {secondary.Data.SerialNumber}");
                            Console.WriteLine($"Quantity: {secondary.Data.Quantity}");
                            Console.WriteLine($"Expiry Date: {secondary.Data.ExpiryDate}");
                        }
                    }

                    // If the decoded object contains primary data, display it
                    if (decoded is HIBCLICPrimaryDataCodetext primary)
                    {
                        if (primary.Data != null)
                        {
                            Console.WriteLine($"Product or Catalog Number: {primary.Data.ProductOrCatalogNumber}");
                            Console.WriteLine($"Labeler Identification Code: {primary.Data.LabelerIdentificationCode}");
                            Console.WriteLine($"Unit of Measure ID: {primary.Data.UnitOfMeasureID}");
                        }
                    }

                    // If the decoded object contains combined data, display both parts
                    if (decoded is HIBCLICCombinedCodetext combined)
                    {
                        if (combined.PrimaryData != null)
                        {
                            Console.WriteLine($"Product or Catalog Number: {combined.PrimaryData.ProductOrCatalogNumber}");
                            Console.WriteLine($"Labeler Identification Code: {combined.PrimaryData.LabelerIdentificationCode}");
                            Console.WriteLine($"Unit of Measure ID: {combined.PrimaryData.UnitOfMeasureID}");
                        }
                        if (combined.SecondaryAndAdditionalData != null)
                        {
                            Console.WriteLine($"Lot Number: {combined.SecondaryAndAdditionalData.LotNumber}");
                            Console.WriteLine($"Serial Number: {combined.SecondaryAndAdditionalData.SerialNumber}");
                            Console.WriteLine($"Quantity: {combined.SecondaryAndAdditionalData.Quantity}");
                            Console.WriteLine($"Expiry Date: {combined.SecondaryAndAdditionalData.ExpiryDate}");
                        }
                    }

                    Console.WriteLine(new string('-', 40));
                }
            }
        }
    }
}