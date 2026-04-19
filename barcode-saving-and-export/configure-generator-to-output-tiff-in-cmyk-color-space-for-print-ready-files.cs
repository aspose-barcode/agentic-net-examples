using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Define output file path
        string outputPath = "barcode_cmyk.tif";

        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Save the barcode image as TIFF in CMYK color space
            generator.Save(outputPath, BarCodeImageFormat.TiffInCmyk);
        }

        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}