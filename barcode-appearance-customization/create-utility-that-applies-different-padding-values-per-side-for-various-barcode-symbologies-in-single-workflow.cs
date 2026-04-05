using System;
using Aspose.BarCode.Generation;
namespace BarcodePaddingUtility
{
    class Program
    {
        static void Main()
        {
            // Define a set of barcode configurations with individual side paddings
            var barcodeConfigs = new[]
            {
                new
                {
                    Type = EncodeTypes.Code128,
                    Text = "CODE128-EXAMPLE",
                    Left = 2f,
                    Top = 5f,
                    Right = 2f,
                    Bottom = 5f,
                    FileName = "code128.png"
                },
                new
                {
                    Type = EncodeTypes.QR,
                    Text = "QR-EXAMPLE",
                    Left = 10f,
                    Top = 10f,
                    Right = 10f,
                    Bottom = 10f,
                    FileName = "qr.png"
                },
                new
                {
                    Type = EncodeTypes.EAN13,
                    Text = "1234567890128",
                    Left = 0f,
                    Top = 3f,
                    Right = 0f,
                    Bottom = 3f,
                    FileName = "ean13.png"
                }
            };

            foreach (var cfg in barcodeConfigs)
            {
                // Create a barcode generator for the specified symbology and code text
                using (var generator = new BarcodeGenerator(cfg.Type, cfg.Text))
                {
                    // Apply individual padding values (in points)
                    generator.Parameters.Barcode.Padding.Left.Point = cfg.Left;
                    generator.Parameters.Barcode.Padding.Top.Point = cfg.Top;
                    generator.Parameters.Barcode.Padding.Right.Point = cfg.Right;
                    generator.Parameters.Barcode.Padding.Bottom.Point = cfg.Bottom;

                    // Save the barcode image to a PNG file
                    generator.Save(cfg.FileName);
                    Console.WriteLine($"Saved {cfg.FileName} with padding L:{cfg.Left} T:{cfg.Top} R:{cfg.Right} B:{cfg.Bottom}");
                }
            }

            Console.WriteLine("All barcodes generated.");
        }
    }
}