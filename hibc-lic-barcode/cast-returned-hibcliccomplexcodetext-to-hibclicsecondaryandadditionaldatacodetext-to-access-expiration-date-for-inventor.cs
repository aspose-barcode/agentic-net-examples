// Title: HIBCLIC QR Code Generation and Expiration Date Extraction
// Description: Demonstrates generating a HIBC QR LIC barcode with secondary and additional data, including an expiration date, and then decoding it to retrieve that date.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode operations collection. It showcases the use of ComplexBarcodeGenerator for creating HIBC QR LIC barcodes, BarCodeReader for decoding, and the HIBCLICSecondaryAndAdditionalDataCodetext class for handling secondary data. Typical scenarios include inventory management, product tracking, and regulatory compliance where expiration dates and lot information are embedded in barcodes.
// Prompt: Cast the returned HIBCLICComplexCodetext to HIBCLICSecondaryAndAdditionalDataCodetext to access expiration date for inventory processing.
// Tags: hibclic, qr, barcode, generation, recognition, expirationdate, inventory, aspose.barcode, complexbarcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates creating a HIBC QR LIC barcode with secondary data, saving it,
/// reading it back, and extracting the expiration date.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, decodes it, and prints the expiration date.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "HIBCLICDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "hibclic.png");

        // Build secondary and additional data codetext with an expiration date
        var secondaryCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCQRLIC,
            LinkCharacter = '+',
            Data = new SecondaryAndAdditionalData
            {
                ExpiryDate = DateTime.Today.AddDays(30),
                ExpiryDateFormat = HIBCLICDateFormat.MMDDYY,
                Quantity = 10,
                LotNumber = "LOT123",
                SerialNumber = "SERIAL123",
                DateOfManufacture = DateTime.Today
            }
        };

        // Generate the barcode image using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(secondaryCodetext))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 10;
            generator.Save(imagePath);
        }

        // Read and decode the barcode, then cast to access expiration date
        using (var reader = new BarCodeReader(imagePath, DecodeType.HIBCQRLIC))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Decode the complex codetext from the scanned result
                HIBCLICComplexCodetext complex = ComplexCodetextReader.TryDecodeHIBCLIC(result.CodeText);
                // Attempt to cast to the secondary data type
                var secondary = complex as HIBCLICSecondaryAndAdditionalDataCodetext;
                if (secondary != null)
                {
                    Console.WriteLine($"Expiration Date: {secondary.Data.ExpiryDate:yyyy-MM-dd}");
                }
                else
                {
                    Console.WriteLine("Decoded codetext is not secondary data type.");
                }
            }
        }

        // Clean up temporary files
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit
        }
    }
}