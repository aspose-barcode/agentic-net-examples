// Title: Read and Decode HIBC LIC Barcode Using ComplexCodetextReader
// Description: Demonstrates generating a HIBC LIC QR barcode, saving it to a temporary file, and decoding it with ComplexCodetextReader to extract primary, secondary, or combined data.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on HIBC (Health Industry Bar Code) LIC symbology. It showcases the use of ComplexBarcodeGenerator, BarCodeReader, and ComplexCodetextReader classes to create, read, and parse complex HIBC LIC code texts—common tasks for healthcare and pharmaceutical applications that need to encode product, lot, and expiry information.
// Prompt: Read a HIBC LIC barcode from a file stream and decode it using ComplexCodetextReader.
// Tags: hibc, lic, barcode, decoding, complexcodetextreader, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Sample program that generates a HIBC LIC QR barcode, saves it to a temporary file,
/// reads it back, and decodes the complex code text using <see cref="ComplexCodetextReader"/>.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the sample. Performs barcode generation, reading, decoding, and cleanup.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the sample barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "HIBCLIC_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "hibclic.png");

        // Generate a sample HIBC LIC Primary Data barcode
        try
        {
            HIBCLICPrimaryDataCodetext primaryCodetext = new HIBCLICPrimaryDataCodetext();
            primaryCodetext.BarcodeType = EncodeTypes.HIBCQRLIC;
            primaryCodetext.Data = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",
                LabelerIdentificationCode = "A999",
                UnitOfMeasureID = 1
            };

            using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(primaryCodetext))
            {
                // Set barcode visual parameters
                generator.Parameters.Barcode.XDimension.Pixels = 5f;
                generator.Save(barcodePath);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error generating barcode: " + ex.Message);
            return;
        }

        // Verify the file exists before attempting to read
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Barcode file not found: " + barcodePath);
            return;
        }

        // Read and decode the HIBC LIC barcode from a file stream
        try
        {
            using (FileStream stream = new FileStream(barcodePath, FileMode.Open, FileAccess.Read))
            {
                using (BarCodeReader reader = new BarCodeReader(stream, DecodeType.HIBCQRLIC))
                {
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine("Raw CodeText: " + result.CodeText);
                        HIBCLICComplexCodetext complex = ComplexCodetextReader.TryDecodeHIBCLIC(result.CodeText);
                        if (complex is HIBCLICPrimaryDataCodetext primary)
                        {
                            Console.WriteLine("Decoded as Primary Data:");
                            Console.WriteLine("  Product or Catalog Number: " + primary.Data.ProductOrCatalogNumber);
                            Console.WriteLine("  Labeler Identification Code: " + primary.Data.LabelerIdentificationCode);
                            Console.WriteLine("  Unit of Measure ID: " + primary.Data.UnitOfMeasureID);
                        }
                        else if (complex is HIBCLICSecondaryAndAdditionalDataCodetext secondary)
                        {
                            Console.WriteLine("Decoded as Secondary Data:");
                            Console.WriteLine("  Expiry Date: " + secondary.Data.ExpiryDate);
                            Console.WriteLine("  Quantity: " + secondary.Data.Quantity);
                            Console.WriteLine("  Lot Number: " + secondary.Data.LotNumber);
                            Console.WriteLine("  Serial Number: " + secondary.Data.SerialNumber);
                            Console.WriteLine("  Date of Manufacture: " + secondary.Data.DateOfManufacture);
                            Console.WriteLine("  Link Character: " + secondary.LinkCharacter);
                        }
                        else if (complex is HIBCLICCombinedCodetext combined)
                        {
                            Console.WriteLine("Decoded as Combined Data:");
                            Console.WriteLine("  Primary - Product or Catalog Number: " + combined.PrimaryData.ProductOrCatalogNumber);
                            Console.WriteLine("  Primary - Labeler Identification Code: " + combined.PrimaryData.LabelerIdentificationCode);
                            Console.WriteLine("  Primary - Unit of Measure ID: " + combined.PrimaryData.UnitOfMeasureID);
                            Console.WriteLine("  Secondary - Expiry Date: " + combined.SecondaryAndAdditionalData.ExpiryDate);
                            Console.WriteLine("  Secondary - Quantity: " + combined.SecondaryAndAdditionalData.Quantity);
                            Console.WriteLine("  Secondary - Lot Number: " + combined.SecondaryAndAdditionalData.LotNumber);
                            Console.WriteLine("  Secondary - Serial Number: " + combined.SecondaryAndAdditionalData.SerialNumber);
                            Console.WriteLine("  Secondary - Date of Manufacture: " + combined.SecondaryAndAdditionalData.DateOfManufacture);
                        }
                        else
                        {
                            Console.WriteLine("Decoded to an unknown HIBC LIC type.");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error reading barcode: " + ex.Message);
        }
        finally
        {
            // Clean up temporary files
            try
            {
                if (File.Exists(barcodePath))
                    File.Delete(barcodePath);
                if (Directory.Exists(tempFolder))
                    Directory.Delete(tempFolder, true);
            }
            catch
            {
                // Suppress any cleanup errors
            }
        }
    }
}