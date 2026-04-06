using System;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        var barcodes = new List<(string Text, Point Position)>
        {
            ("12345", new Point(50, 50)),
            ("ABCDEF", new Point(300, 80)),
            ("987654321", new Point(150, 250)),
            ("XYZ", new Point(500, 200))
        };

        int canvasWidth = 800;
        int canvasHeight = 600;
        using (Bitmap canvas = new Bitmap(canvasWidth, canvasHeight))
        {
            using (Graphics g = Graphics.FromImage(canvas))
            {
                g.Clear(Color.White);

                foreach (var (text, pos) in barcodes)
                {
                    using (Bitmap barcodeBmp = GenerateBarcodeBitmap(text))
                    {
                        g.DrawImage(barcodeBmp, pos);
                    }
                }
            }

            canvas.Save("combined.png", ImageFormat.Png);
        }

        using (Bitmap combinedBmp = new Bitmap("combined.png"))
        {
            using (BarCodeReader reader = new BarCodeReader(combinedBmp, DecodeType.AllSupportedTypes))
            {
                reader.QualitySettings.XDimension = XDimensionMode.Normal;

                BarCodeResult[] results = reader.ReadBarCodes();

                using (Bitmap heatMapBmp = new Bitmap(combinedBmp.Width, combinedBmp.Height))
                {
                    using (Graphics g = Graphics.FromImage(heatMapBmp))
                    {
                        g.DrawImage(combinedBmp, 0, 0);

                        using (SolidBrush brush = new SolidBrush(Color.FromArgb(128, 255, 0, 0)))
                        {
                            foreach (BarCodeResult result in results)
                            {
                                var rect = result.Region.Rectangle;
                                float x = rect.X;
                                float y = rect.Y;
                                float width = rect.Width;
                                float height = rect.Height;

                                g.FillRectangle(brush, x, y, width, height);
                            }
                        }
                    }

                    heatMapBmp.Save("heatmap.png", ImageFormat.Png);
                }
            }
        }
    }

    private static Bitmap GenerateBarcodeBitmap(string text)
    {
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = text;
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Parameters.Barcode.BarHeight.Point = 40f;

            return generator.GenerateBarCodeImage();
        }
    }
}