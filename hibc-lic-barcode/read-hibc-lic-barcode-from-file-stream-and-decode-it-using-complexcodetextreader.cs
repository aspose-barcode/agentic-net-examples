using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates reading and decoding HIBC LIC barcodes from an image file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Reads an image, detects barcodes, and attempts to decode HIBC LIC data.
    /// </summary>
    static void Main()
    {
        // Path to the image containing a HIBC LIC barcode.
        const string imagePath = "hibc.png";

        // Verify that the file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Open the image file as a stream and create a barcode reader that supports all barcode types.
        using (FileStream fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
        using (BarCodeReader reader = new BarCodeReader(fileStream, DecodeType.AllSupportedTypes))
        {
            // Read all barcodes found in the image.
            BarCodeResult[] results = reader.ReadBarCodes();

            // If no barcodes were detected, inform the user and exit.
            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes detected.");
                return;
            }

            // Process each detected barcode.
            foreach (BarCodeResult result in results)
            {
                // Attempt to decode the raw codetext as HIBC LIC using ComplexCodetextReader.
                HIBCLICComplexCodetext decoded = ComplexCodetextReader.TryDecodeHIBCLIC(result.CodeText);

                // If decoding fails or the barcode is not HIBC LIC, report and continue to the next result.
                if (decoded == null)
                {
                    Console.WriteLine($"Barcode detected but not HIBC LIC or decode failed. Type: {result.CodeTypeName}, Text: {result.CodeText}");
                    continue;
                }

                // Determine the concrete type of the decoded HIBC LIC codetext and display its fields.
                switch (decoded)
                {
                    case HIBCLICPrimaryDataCodetext primary:
                        Console.WriteLine("Decoded HIBC LIC Primary Data:");
                        Console.WriteLine($"  ProductOrCatalogNumber: {primary.Data.ProductOrCatalogNumber}");
                        Console.WriteLine($"  LabelerIdentificationCode: {primary.Data.LabelerIdentificationCode}");
                        Console.WriteLine($"  UnitOfMeasureID: {primary.Data.UnitOfMeasureID}");
                        break;

                    case HIBCLICSecondaryAndAdditionalDataCodetext secondary:
                        Console.WriteLine("Decoded HIBC LIC Secondary Data:");
                        Console.WriteLine($"  LotNumber: {secondary.Data.LotNumber}");
                        Console.WriteLine($"  SerialNumber: {secondary.Data.SerialNumber}");
                        Console.WriteLine($"  ExpiryDate: {secondary.Data.ExpiryDate}");
                        Console.WriteLine($"  ExpiryDateFormat: {secondary.Data.ExpiryDateFormat}");
                        Console.WriteLine($"  Quantity: {secondary.Data.Quantity}");
                        Console.WriteLine($"  DateOfManufacture: {secondary.Data.DateOfManufacture}");
                        break;

                    case HIBCLICCombinedCodetext combined:
                        Console.WriteLine("Decoded HIBC LIC Combined Data:");
                        Console.WriteLine($"  ProductOrCatalogNumber: {combined.PrimaryData.ProductOrCatalogNumber}");
                        Console.WriteLine($"  LabelerIdentificationCode: {combined.PrimaryData.LabelerIdentificationCode}");
                        Console.WriteLine($"  UnitOfMeasureID: {combined.PrimaryData.UnitOfMeasureID}");
                        Console.WriteLine($"  LotNumber: {combined.SecondaryAndAdditionalData.LotNumber}");
                        Console.WriteLine($"  SerialNumber: {combined.SecondaryAndAdditionalData.SerialNumber}");
                        Console.WriteLine($"  ExpiryDate: {combined.SecondaryAndAdditionalData.ExpiryDate}");
                        Console.WriteLine($"  ExpiryDateFormat: {combined.SecondaryAndAdditionalData.ExpiryDateFormat}");
                        Console.WriteLine($"  Quantity: {combined.SecondaryAndAdditionalData.Quantity}");
                        Console.WriteLine($"  DateOfManufacture: {combined.SecondaryAndAdditionalData.DateOfManufacture}");
                        break;

                    default:
                        // This should not occur, but handle any unexpected decoded types gracefully.
                        Console.WriteLine("Decoded HIBC LIC codetext of an unexpected type.");
                        break;
                }
            }
        }
    }
}