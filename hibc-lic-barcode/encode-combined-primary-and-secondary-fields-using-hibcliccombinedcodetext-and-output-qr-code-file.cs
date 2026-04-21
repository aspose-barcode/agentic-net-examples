using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Create combined primary and secondary codetext for HIBC LIC QR
        var combinedCodetext = new HIBCLICCombinedCodetext
        {
            BarcodeType = EncodeTypes.HIBCQRLIC,
            PrimaryData = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",
                LabelerIdentificationCode = "A999",
                UnitOfMeasureID = 1
            },
            SecondaryAndAdditionalData = new SecondaryAndAdditionalData
            {
                ExpiryDate = DateTime.Now,
                ExpiryDateFormat = HIBCLICDateFormat.MMDDYY,
                Quantity = 30,
                LotNumber = "LOT123",
                SerialNumber = "SERIAL123",
                DateOfManufacture = DateTime.Now
            }
        };

        // Generate the QR code image and save it to a file
        using (var generator = new ComplexBarcodeGenerator(combinedCodetext))
        {
            // Set high error correction level (optional)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the barcode image as PNG
            generator.Save("hibc_combined_qr.png");
        }
    }
}