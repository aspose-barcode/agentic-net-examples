using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Output file
        const string outputPath = "combined.png";

        // Create UPC‑A with GS1 DataBar coupon barcode
        using (var upcGenerator = new BarcodeGenerator(EncodeTypes.UpcaGs1DatabarCoupon))
        {
            upcGenerator.CodeText = "514141100906(8110)106141416543213500110000310123196000";
            upcGenerator.Parameters.AutoSizeMode = AutoSizeMode.None;
            upcGenerator.Parameters.Barcode.XDimension.Point = 2f;
            upcGenerator.Parameters.Barcode.BarHeight.Pixels = 100f;

            // Generate barcode image
            using (Bitmap upcBitmap = upcGenerator.GenerateBarCodeImage())
            {
                // Create QR code to overlay
                using (var qrGenerator = new BarcodeGenerator(EncodeTypes.QR))
                {
                    qrGenerator.CodeText = "https://example.com";
                    qrGenerator.Parameters.AutoSizeMode = AutoSizeMode.None;
                    qrGenerator.Parameters.ImageWidth.Pixels = 150f;
                    qrGenerator.Parameters.ImageHeight.Pixels = 150f;
                    qrGenerator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                    // Generate QR image
                    using (Bitmap qrBitmap = qrGenerator.GenerateBarCodeImage())
                    {
                        // Overlay QR onto UPC barcode
                        using (Graphics graphics = Graphics.FromImage(upcBitmap))
                        {
                            const int margin = 10;
                            int x = upcBitmap.Width - qrBitmap.Width - margin;
                            int y = upcBitmap.Height - qrBitmap.Height - margin;
                            graphics.DrawImage(qrBitmap, x, y, qrBitmap.Width, qrBitmap.Height);
                        }

                        // Save combined image
                        upcBitmap.Save(outputPath, ImageFormat.Png);
                    }
                }
            }
        }

        Console.WriteLine($"Combined barcode saved to {outputPath}");
    }
}