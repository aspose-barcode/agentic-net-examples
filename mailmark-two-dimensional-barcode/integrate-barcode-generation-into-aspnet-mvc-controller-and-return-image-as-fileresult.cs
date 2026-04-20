using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Set barcode bar color
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;

            // Save the barcode image to a memory stream in PNG format
            using (var stream = new MemoryStream())
            {
                generator.Save(stream, BarCodeImageFormat.Png);
                // Write the image bytes to a file (simulating a FileResult)
                File.WriteAllBytes("barcode.png", stream.ToArray());
            }
        }
    }
}