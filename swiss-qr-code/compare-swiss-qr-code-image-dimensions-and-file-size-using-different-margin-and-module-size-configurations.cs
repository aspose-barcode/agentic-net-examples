using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Sample Swiss QR bill data (valid IBAN, amount, version)
        var swissCodetext = new SwissQRCodetext();
        swissCodetext.Bill.Account = "CH9300762011623852957";
        swissCodetext.Bill.Amount = 199.95m;
        swissCodetext.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swissCodetext.Bill.Creditor.CountryCode = "CH";
        swissCodetext.Bill.Creditor.Name = "John Doe";

        // Configurations to test: module size (XDimension) and margin (padding)
        float[] moduleSizes = { 2f, 4f };
        float[] margins = { 0f, 5f, 10f };

        foreach (float moduleSize in moduleSizes)
        {
            foreach (float margin in margins)
            {
                using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(swissCodetext))
                {
                    // Set module size (XDimension) in points
                    generator.Parameters.Barcode.XDimension.Point = moduleSize;

                    // Set uniform padding on all sides
                    generator.Parameters.Barcode.Padding.Left.Point = margin;
                    generator.Parameters.Barcode.Padding.Top.Point = margin;
                    generator.Parameters.Barcode.Padding.Right.Point = margin;
                    generator.Parameters.Barcode.Padding.Bottom.Point = margin;

                    // Save barcode to a memory stream in PNG format
                    using (MemoryStream ms = new MemoryStream())
                    {
                        generator.Save(ms, BarCodeImageFormat.Png);
                        long fileSize = ms.Length;

                        // Load the image to obtain dimensions
                        ms.Position = 0;
                        using (Bitmap bitmap = new Bitmap(ms))
                        {
                            int width = bitmap.Width;
                            int height = bitmap.Height;

                            Console.WriteLine($"ModuleSize: {moduleSize}pt, Margin: {margin}pt => Width: {width}px, Height: {height}px, FileSize: {fileSize} bytes");
                        }
                    }
                }
            }
        }
    }
}