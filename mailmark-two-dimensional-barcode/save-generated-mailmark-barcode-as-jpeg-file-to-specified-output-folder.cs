using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        string outputFile = Path.Combine(outputFolder, "mailmark.jpeg");

        var mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Set a reasonable positive bar height (in points)
            generator.Parameters.Barcode.BarHeight.Point = 10f;

            generator.Save(outputFile, BarCodeImageFormat.Jpeg);
        }

        Console.WriteLine($"Mailmark barcode saved to: {outputFile}");
    }
}