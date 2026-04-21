using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Sample HIBC LIC DataMatrix code text
        string codeText = "A12345";

        // Create a barcode generator for HIBC DataMatrix LIC
        using (var generator = new BarcodeGenerator(EncodeTypes.HIBCDataMatrixLIC, codeText))
        {
            // Configure image size to 300 × 150 pixels
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Ensure size is taken as specified
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Save the barcode image to a PNG file via a memory stream
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                File.WriteAllBytes("hibc_datamatrix_lic.png", ms.ToArray());
            }
        }
    }
}