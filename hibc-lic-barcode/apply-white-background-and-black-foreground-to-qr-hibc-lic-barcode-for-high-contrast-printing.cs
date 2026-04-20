using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Prepare complex codetext for HIBC QR LIC barcode (secondary data example)
        var complexCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext();
        complexCodetext.BarcodeType = EncodeTypes.HIBCQRLIC;
        complexCodetext.LinkCharacter = 'L';
        complexCodetext.Data = new SecondaryAndAdditionalData
        {
            LotNumber = "LOT123",
            SerialNumber = "SERIAL123",
            ExpiryDate = DateTime.Now,
            ExpiryDateFormat = HIBCLICDateFormat.MMDDYY,
            Quantity = 30,
            DateOfManufacture = DateTime.Now
        };

        // Generate the barcode with high‑contrast colors
        using (var generator = new ComplexBarcodeGenerator(complexCodetext))
        {
            // Set foreground (bars) to black and background to white
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the barcode image as PNG
            generator.Save("hibcqrlic.png");
        }

        Console.WriteLine("HIBC QR LIC barcode generated: hibcqrlic.png");
    }
}