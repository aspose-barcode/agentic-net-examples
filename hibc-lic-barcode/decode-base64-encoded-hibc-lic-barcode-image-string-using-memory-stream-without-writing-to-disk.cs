using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

class Program
{
    static void Main()
    {
        // Base64‑encoded PNG image containing a HIBC LIC barcode.
        // Replace this string with the actual image data when available.
        string base64Image = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMCAO+XK6UAAAAASUVORK5CYII=";

        if (string.IsNullOrWhiteSpace(base64Image))
        {
            Console.WriteLine("No image data provided.");
            return;
        }

        byte[] imageBytes;
        try
        {
            imageBytes = Convert.FromBase64String(base64Image);
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid Base64 string.");
            return;
        }

        using (var memoryStream = new MemoryStream(imageBytes))
        {
            // Initialize the reader for all supported barcode types.
            using (var reader = new BarCodeReader(memoryStream, DecodeType.AllSupportedTypes))
            {
                var results = reader.ReadBarCodes();
                if (results == null || results.Length == 0)
                {
                    Console.WriteLine("No barcode detected in the image.");
                    return;
                }

                foreach (var result in results)
                {
                    Console.WriteLine($"Detected Barcode Type: {result.CodeTypeName}");
                    Console.WriteLine($"Raw CodeText: {result.CodeText}");

                    // Attempt to decode HIBC LIC complex codetext.
                    var hibcResult = ComplexCodetextReader.TryDecodeHIBCLIC(result.CodeText);
                    if (hibcResult == null)
                    {
                        Console.WriteLine("CodeText is not a valid HIBC LIC codetext.");
                        continue;
                    }

                    // Determine the concrete type and display its data.
                    switch (hibcResult)
                    {
                        case HIBCLICPrimaryDataCodetext primary:
                            Console.WriteLine("Decoded as Primary Data:");
                            Console.WriteLine($"  Product or Catalog Number: {primary.Data.ProductOrCatalogNumber}");
                            Console.WriteLine($"  Labeler Identification Code: {primary.Data.LabelerIdentificationCode}");
                            Console.WriteLine($"  Unit of Measure ID: {primary.Data.UnitOfMeasureID}");
                            break;

                        case HIBCLICSecondaryAndAdditionalDataCodetext secondary:
                            Console.WriteLine("Decoded as Secondary and Additional Data:");
                            Console.WriteLine($"  Lot Number: {secondary.Data.LotNumber}");
                            Console.WriteLine($"  Serial Number: {secondary.Data.SerialNumber}");
                            Console.WriteLine($"  Quantity: {secondary.Data.Quantity}");
                            Console.WriteLine($"  Expiry Date: {secondary.Data.ExpiryDate}");
                            Console.WriteLine($"  Expiry Date Format: {secondary.Data.ExpiryDateFormat}");
                            Console.WriteLine($"  Date of Manufacture: {secondary.Data.DateOfManufacture}");
                            break;

                        case HIBCLICCombinedCodetext combined:
                            Console.WriteLine("Decoded as Combined Data:");
                            Console.WriteLine($"  Product or Catalog Number: {combined.PrimaryData.ProductOrCatalogNumber}");
                            Console.WriteLine($"  Labeler Identification Code: {combined.PrimaryData.LabelerIdentificationCode}");
                            Console.WriteLine($"  Unit of Measure ID: {combined.PrimaryData.UnitOfMeasureID}");
                            Console.WriteLine($"  Lot Number: {combined.SecondaryAndAdditionalData.LotNumber}");
                            Console.WriteLine($"  Serial Number: {combined.SecondaryAndAdditionalData.SerialNumber}");
                            Console.WriteLine($"  Quantity: {combined.SecondaryAndAdditionalData.Quantity}");
                            Console.WriteLine($"  Expiry Date: {combined.SecondaryAndAdditionalData.ExpiryDate}");
                            Console.WriteLine($"  Expiry Date Format: {combined.SecondaryAndAdditionalData.ExpiryDateFormat}");
                            Console.WriteLine($"  Date of Manufacture: {combined.SecondaryAndAdditionalData.DateOfManufacture}");
                            break;

                        default:
                            Console.WriteLine("Decoded HIBC LIC codetext, but type is unrecognized.");
                            break;
                    }
                }
            }
        }
    }
}