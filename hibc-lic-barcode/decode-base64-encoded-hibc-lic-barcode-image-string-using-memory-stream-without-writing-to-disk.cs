// Title: Decode HIBC LIC barcode from Base64 string using memory streams
// Description: Demonstrates generating a HIBC LIC QR barcode, encoding it to a Base64 string, then decoding it back from memory without writing files.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use ComplexBarcodeGenerator to create HIBC LIC barcodes, BarCodeReader for decoding, and memory streams for in‑memory processing. Developers working with healthcare barcodes often need to generate, transmit, and read HIBC codes without persisting images to disk, making this pattern useful for web services and automated pipelines.
// Prompt: Decode a base64‑encoded HIBC LIC barcode image string using a memory stream without writing to disk.
// Tags: hibc, lic, barcode, generation, recognition, base64, memorystream, aspnet, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that generates a HIBC LIC QR barcode, converts it to a Base64 string,
/// then decodes the string back to an image and reads the barcode data—all using memory streams.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, Base64 encoding, decoding, and reading.
    /// </summary>
    static void Main()
    {
        // Prepare primary data for the HIBC LIC barcode
        HIBCLICPrimaryDataCodetext primaryData = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCQRLIC,
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",
                LabelerIdentificationCode = "A999",
                UnitOfMeasureID = 1
            }
        };

        // Generate the barcode image directly into a memory stream
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(primaryData))
        {
            // Set barcode visual parameters (e.g., module size)
            generator.Parameters.Barcode.XDimension.Pixels = 10;

            using (MemoryStream generationStream = new MemoryStream())
            {
                // Save the generated barcode as PNG into the stream
                generator.Save(generationStream, BarCodeImageFormat.Png);

                // Convert the image bytes to a Base64 string for transport or storage
                string base64Image = Convert.ToBase64String(generationStream.ToArray());
                Console.WriteLine("Base64-encoded barcode image:");
                Console.WriteLine(base64Image);
                Console.WriteLine();

                // Decode the Base64 string back to raw image bytes
                byte[] imageBytes = Convert.FromBase64String(base64Image);
                using (MemoryStream decodeStream = new MemoryStream(imageBytes))
                {
                    // Initialize a barcode reader for the HIBC LIC QR symbology
                    using (BarCodeReader reader = new BarCodeReader(decodeStream, DecodeType.HIBCQRLIC))
                    {
                        // Iterate through all detected barcodes (should be one in this case)
                        foreach (BarCodeResult result in reader.ReadBarCodes())
                        {
                            // Attempt to parse the complex HIBC LIC codetext
                            HIBCLICComplexCodetext complex = ComplexCodetextReader.TryDecodeHIBCLIC(result.CodeText);
                            if (complex is HIBCLICPrimaryDataCodetext primaryResult)
                            {
                                // Output primary data fields
                                Console.WriteLine("Decoded Primary Data:");
                                Console.WriteLine($"Product or Catalog Number: {primaryResult.Data.ProductOrCatalogNumber}");
                                Console.WriteLine($"Labeler Identification Code: {primaryResult.Data.LabelerIdentificationCode}");
                                Console.WriteLine($"Unit of Measure ID: {primaryResult.Data.UnitOfMeasureID}");
                            }
                            else if (complex is HIBCLICCombinedCodetext combinedResult)
                            {
                                // Output combined data fields (primary + secondary/additional)
                                Console.WriteLine("Decoded Combined Data:");
                                Console.WriteLine($"Product or Catalog Number: {combinedResult.PrimaryData.ProductOrCatalogNumber}");
                                Console.WriteLine($"Labeler Identification Code: {combinedResult.PrimaryData.LabelerIdentificationCode}");
                                Console.WriteLine($"Unit of Measure ID: {combinedResult.PrimaryData.UnitOfMeasureID}");
                                Console.WriteLine($"Expiry Date: {combinedResult.SecondaryAndAdditionalData.ExpiryDate}");
                                Console.WriteLine($"Quantity: {combinedResult.SecondaryAndAdditionalData.Quantity}");
                                Console.WriteLine($"Lot Number: {combinedResult.SecondaryAndAdditionalData.LotNumber}");
                                Console.WriteLine($"Serial Number: {combinedResult.SecondaryAndAdditionalData.SerialNumber}");
                                Console.WriteLine($"Date of Manufacture: {combinedResult.SecondaryAndAdditionalData.DateOfManufacture}");
                            }
                            else
                            {
                                // Handle unexpected codetext formats
                                Console.WriteLine("Decoded codetext type not recognized.");
                            }
                        }
                    }
                }
            }
        }
    }
}