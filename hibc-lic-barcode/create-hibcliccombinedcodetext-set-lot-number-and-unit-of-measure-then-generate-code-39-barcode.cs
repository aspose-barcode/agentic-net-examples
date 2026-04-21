using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Create combined codetext for HIBC LIC
        var combinedCodetext = new HIBCLICCombinedCodetext();

        // Set barcode type to Code 39 (default, but set explicitly for clarity)
        combinedCodetext.BarcodeType = EncodeTypes.HIBCCode39LIC;

        // Populate primary data (required fields)
        combinedCodetext.PrimaryData = new PrimaryData
        {
            ProductOrCatalogNumber = "12345",
            LabelerIdentificationCode = "A999",
            UnitOfMeasureID = 1 // Unit of measure
        };

        // Populate secondary data with lot number
        combinedCodetext.SecondaryAndAdditionalData = new SecondaryAndAdditionalData
        {
            LotNumber = "LOT123"
        };

        // Generate and save the barcode image
        using (var generator = new ComplexBarcodeGenerator(combinedCodetext))
        {
            // Save directly to a PNG file
            generator.Save("hibc_lic_code39.png");
        }

        // Optional: decode the generated barcode to verify
        if (File.Exists("hibc_lic_code39.png"))
        {
            using (var reader = new BarCodeReader("hibc_lic_code39.png", DecodeType.HIBCCode39LIC))
            {
                var barCodes = reader.ReadBarCodes();
                if (barCodes.Length > 0)
                {
                    var decoded = ComplexCodetextReader.TryDecodeHIBCLIC(barCodes[0].CodeText) as HIBCLICCombinedCodetext;
                    if (decoded != null)
                    {
                        Console.WriteLine("Decoded Lot Number: " + decoded.SecondaryAndAdditionalData?.LotNumber);
                        Console.WriteLine("Decoded Unit of Measure ID: " + decoded.PrimaryData?.UnitOfMeasureID);
                    }
                }
            }
        }
    }
}