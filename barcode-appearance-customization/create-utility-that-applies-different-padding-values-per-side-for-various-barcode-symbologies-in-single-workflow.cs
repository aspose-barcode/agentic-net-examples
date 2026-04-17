using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Define a set of barcode configurations with per‑side padding values.
        var configs = new[]
        {
            new
            {
                Type = EncodeTypes.Code128,
                Text = "CODE128",
                Left = 5f,
                Top = 10f,
                Right = 5f,
                Bottom = 10f,
                FileName = "code128.png"
            },
            new
            {
                Type = EncodeTypes.QR,
                Text = "https://example.com",
                Left = 2f,
                Top = 2f,
                Right = 2f,
                Bottom = 2f,
                FileName = "qr.png"
            },
            new
            {
                Type = EncodeTypes.DataMatrix,
                Text = "DataMatrix",
                Left = 8f,
                Top = 4f,
                Right = 8f,
                Bottom = 4f,
                FileName = "datamatrix.png"
            },
            new
            {
                Type = EncodeTypes.Pdf417,
                Text = "PDF417 Sample",
                Left = 0f,
                Top = 0f,
                Right = 0f,
                Bottom = 0f,
                FileName = "pdf417.png"
            }
        };

        // Generate each barcode with its specific padding.
        foreach (var cfg in configs)
        {
            using (var generator = new BarcodeGenerator(cfg.Type, cfg.Text))
            {
                // Apply per‑side padding (points).
                generator.Parameters.Barcode.Padding.Left.Point = cfg.Left;
                generator.Parameters.Barcode.Padding.Top.Point = cfg.Top;
                generator.Parameters.Barcode.Padding.Right.Point = cfg.Right;
                generator.Parameters.Barcode.Padding.Bottom.Point = cfg.Bottom;

                // Save the barcode image.
                generator.Save(cfg.FileName);
                Console.WriteLine($"Saved {cfg.FileName} (L:{cfg.Left} T:{cfg.Top} R:{cfg.Right} B:{cfg.Bottom})");
            }
        }
    }
}