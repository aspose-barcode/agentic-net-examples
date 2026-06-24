using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates reading a barcode from a Base64‑encoded image,
/// decoding it, and extracting HIBC LIC information if present.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Base64‑encoded PNG image containing a barcode.
        // Replace this string with the actual image data as needed.
        string base64Image = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMCAO+XK6UAAAAASUVORK5CYII=";

        byte[] imageBytes;

        // Convert the Base64 string to a byte array; handle invalid format.
        try
        {
            imageBytes = Convert.FromBase64String(base64Image);
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid Base64 string.");
            return;
        }

        // Create a memory stream from the image bytes and initialize the barcode reader.
        using (var memoryStream = new MemoryStream(imageBytes))
        using (var reader = new BarCodeReader(memoryStream, DecodeType.AllSupportedTypes))
        {
            // Read all barcodes found in the image.
            var results = reader.ReadBarCodes();

            // If no barcodes were detected, inform the user and exit.
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
                return;
            }

            // Process each detected barcode.
            foreach (var result in results)
            {
                Console.WriteLine($"Detected barcode type: {result.CodeTypeName}");
                Console.WriteLine($"Raw code text: {result.CodeText}");

                // Attempt to decode the barcode text as HIBC LIC codetext.
                var hibc = ComplexCodetextReader.TryDecodeHIBCLIC(result.CodeText);
                if (hibc == null)
                {
                    Console.WriteLine("The decoded text is not a HIBC LIC codetext.");
                    continue;
                }

                // Handle the specific HIBC LIC codetext type.
                switch (hibc)
                {
                    case HIBCLICPrimaryDataCodetext primary:
                        Console.WriteLine("Primary Data:");
                        Console.WriteLine($"  ProductOrCatalogNumber: {primary.Data.ProductOrCatalogNumber}");
                        Console.WriteLine($"  LabelerIdentificationCode: {primary.Data.LabelerIdentificationCode}");
                        Console.WriteLine($"  UnitOfMeasureID: {primary.Data.UnitOfMeasureID}");
                        break;

                    case HIBCLICSecondaryAndAdditionalDataCodetext secondary:
                        Console.WriteLine("Secondary Data:");
                        Console.WriteLine($"  LotNumber: {secondary.Data.LotNumber}");
                        Console.WriteLine($"  SerialNumber: {secondary.Data.SerialNumber}");
                        Console.WriteLine($"  Quantity: {secondary.Data.Quantity}");
                        Console.WriteLine($"  ExpiryDate: {secondary.Data.ExpiryDate}");
                        Console.WriteLine($"  ExpiryDateFormat: {secondary.Data.ExpiryDateFormat}");
                        Console.WriteLine($"  DateOfManufacture: {secondary.Data.DateOfManufacture}");
                        break;

                    case HIBCLICCombinedCodetext combined:
                        Console.WriteLine("Combined Data:");
                        Console.WriteLine($"  ProductOrCatalogNumber: {combined.PrimaryData.ProductOrCatalogNumber}");
                        Console.WriteLine($"  LabelerIdentificationCode: {combined.PrimaryData.LabelerIdentificationCode}");
                        Console.WriteLine($"  UnitOfMeasureID: {combined.PrimaryData.UnitOfMeasureID}");
                        Console.WriteLine($"  LotNumber: {combined.SecondaryAndAdditionalData.LotNumber}");
                        Console.WriteLine($"  SerialNumber: {combined.SecondaryAndAdditionalData.SerialNumber}");
                        break;

                    default:
                        Console.WriteLine("Unrecognized HIBC LIC codetext type.");
                        break;
                }
            }
        }
    }
}