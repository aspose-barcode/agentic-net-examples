using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Define different configurations: margin (padding) in points and module size (XDimension) in points
        var configurations = new (float Margin, float ModuleSize)[]
        {
            (5f, 2f),
            (10f, 2f),
            (5f, 3f),
            (10f, 3f)
        };

        foreach (var cfg in configurations)
        {
            // Prepare Swiss QR bill data (mandatory fields)
            var swissQr = new SwissQRCodetext();
            swissQr.Bill.Creditor.Name = "John Doe";
            swissQr.Bill.Creditor.CountryCode = "CH";
            swissQr.Bill.Account = "CH9300762011623852957";
            swissQr.Bill.Amount = 199.95m;
            swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

            // Create generator for the complex barcode
            using (var generator = new ComplexBarcodeGenerator(swissQr))
            {
                // Set padding (margin) on all sides
                generator.Parameters.Barcode.Padding.Left.Point = cfg.Margin;
                generator.Parameters.Barcode.Padding.Top.Point = cfg.Margin;
                generator.Parameters.Barcode.Padding.Right.Point = cfg.Margin;
                generator.Parameters.Barcode.Padding.Bottom.Point = cfg.Margin;

                // Set module size (XDimension)
                generator.Parameters.Barcode.XDimension.Point = cfg.ModuleSize;

                // Use interpolation auto‑size mode so the image size follows the settings
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

                // Generate bitmap to obtain dimensions
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    int width = bitmap.Width;
                    int height = bitmap.Height;

                    // Save to memory stream to obtain file size
                    using (var ms = new MemoryStream())
                    {
                        generator.Save(ms, BarCodeImageFormat.Png);
                        long fileSize = ms.Length;

                        Console.WriteLine($"Margin: {cfg.Margin}pt, ModuleSize: {cfg.ModuleSize}pt => Width: {width}px, Height: {height}px, FileSize: {fileSize} bytes");
                    }
                }
            }
        }
    }
}