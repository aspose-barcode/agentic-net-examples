using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Create HIBCLIC secondary and additional data codetext
        var complexCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext();
        complexCodetext.BarcodeType = EncodeTypes.HIBCDataMatrixLIC; // Use DataMatrix LIC
        complexCodetext.LinkCharacter = 'L'; // Optional link character

        // Populate secondary data (expiry date, lot number, serial number)
        var secondaryData = new SecondaryAndAdditionalData
        {
            ExpiryDate = DateTime.Today.AddMonths(6),
            ExpiryDateFormat = HIBCLICDateFormat.MMDDYY,
            LotNumber = "LOT12345",
            SerialNumber = "SN98765"
        };
        complexCodetext.Data = secondaryData;

        // Generate and save the barcode image
        using (var generator = new ComplexBarcodeGenerator(complexCodetext))
        {
            // Set image size (optional)
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 300f;

            string outputFile = "hibc_lic_datamatrix.png";
            generator.Save(outputFile, BarCodeImageFormat.Png);
            Console.WriteLine($"Barcode saved to {Path.GetFullPath(outputFile)}");
        }
    }
}