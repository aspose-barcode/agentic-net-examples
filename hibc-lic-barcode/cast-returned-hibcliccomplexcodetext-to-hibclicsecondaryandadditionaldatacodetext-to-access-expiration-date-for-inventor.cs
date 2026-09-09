// Title: Decode HIBCLIC QR Code and Access Expiration Date
// Description: Demonstrates generating a HIBC QR LIC barcode with secondary data, saving it, then reading and casting the complex codetext to retrieve the expiration date for inventory processing.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode operations collection. It showcases the use of ComplexBarcodeGenerator to create HIBC QR LIC barcodes, BarCodeReader for decoding, and the HIBCLICSecondaryAndAdditionalDataCodetext class to embed and extract secondary data such as expiry dates, lot numbers, and serial numbers. Developers working on healthcare, pharmaceutical, or supply‑chain applications often need to embed detailed product information in barcodes and later retrieve it for inventory tracking and compliance.
// Prompt: Cast the returned HIBCLICComplexCodetext to HIBCLICSecondaryAndAdditionalDataCodetext to access expiration date for inventory processing.
// Tags: hibclic, qr, barcode, generation, recognition, complexbarcode, expirationdate, inventory, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that generates a HIBC QR LIC barcode with secondary data,
/// saves it to a temporary file, reads it back, and extracts the expiration date
/// by casting the complex codetext to <see cref="HIBCLICSecondaryAndAdditionalDataCodetext"/>.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, saving, reading,
    /// and decoding of secondary data.
    /// </summary>
    static void Main(string[] args)
    {
        // Create a temporary folder for the barcode image
        string tempDir = Path.Combine(Path.GetTempPath(), "HIBCLIC_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string imagePath = Path.Combine(tempDir, "hibclic.png");

        // Prepare secondary data codetext with expiration date and other details
        var secondaryCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCQRLIC,
            LinkCharacter = '+',
            Data = new SecondaryAndAdditionalData
            {
                ExpiryDate = DateTime.Now.AddDays(30),
                ExpiryDateFormat = HIBCLICDateFormat.MMDDYY,
                Quantity = 10,
                LotNumber = "LOT123",
                SerialNumber = "SER123",
                DateOfManufacture = DateTime.Now.AddDays(-10)
            }
        };

        // Generate the barcode image using the complex barcode generator
        using (var generator = new ComplexBarcodeGenerator(secondaryCodetext))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 10;
            generator.Save(imagePath);
        }

        // Verify the image was created before attempting to read it
        if (File.Exists(imagePath))
        {
            // Read and decode the barcode, then cast to secondary data codetext
            using (var reader = new BarCodeReader(imagePath, DecodeType.HIBCQRLIC))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    HIBCLICComplexCodetext complex = ComplexCodetextReader.TryDecodeHIBCLIC(result.CodeText);
                    var secondary = complex as HIBCLICSecondaryAndAdditionalDataCodetext;

                    if (secondary != null && secondary.Data != null)
                    {
                        Console.WriteLine($"Decoded Expiry Date: {secondary.Data.ExpiryDate:yyyy-MM-dd}");
                    }
                    else
                    {
                        Console.WriteLine("Failed to cast to HIBCLICSecondaryAndAdditionalDataCodetext.");
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("Barcode image not found.");
        }
    }
}