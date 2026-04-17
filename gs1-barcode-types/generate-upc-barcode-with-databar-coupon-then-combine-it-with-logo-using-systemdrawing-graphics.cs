using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Example UPC‑A with GS1 DataBar coupon text
        const string barcodeText = "514141100906(8110)106141416543213500110000310123196000";

        // Create the barcode generator for UPC‑A DataBar coupon
        using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1DatabarCoupon, barcodeText))
        {
            // Optional: adjust space between main barcode and supplement part
            generator.Parameters.Barcode.Coupon.SupplementSpace.Point = 50f;

            // Set colors (optional)
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Generate the barcode image
            using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
            {
                // Create a simple logo bitmap (red circle on transparent background)
                using (Bitmap logo = new Bitmap(100, 100))
                {
                    using (Graphics gLogo = Graphics.FromImage(logo))
                    {
                        gLogo.Clear(Aspose.Drawing.Color.Transparent);
                        using (Brush brush = new SolidBrush(Aspose.Drawing.Color.Red))
                        {
                            gLogo.FillEllipse(brush, 10, 10, 80, 80);
                        }
                    }

                    // Determine combined image size (barcode on top, logo below)
                    int combinedWidth = Math.Max(barcodeImage.Width, logo.Width);
                    int combinedHeight = barcodeImage.Height + logo.Height;

                    using (Bitmap combined = new Bitmap(combinedWidth, combinedHeight))
                    {
                        using (Graphics g = Graphics.FromImage(combined))
                        {
                            // Fill background
                            g.Clear(Aspose.Drawing.Color.White);

                            // Draw barcode at the top-left corner
                            g.DrawImage(barcodeImage, 0, 0, barcodeImage.Width, barcodeImage.Height);

                            // Center the logo below the barcode
                            int logoX = (combinedWidth - logo.Width) / 2;
                            int logoY = barcodeImage.Height;
                            g.DrawImage(logo, logoX, logoY, logo.Width, logo.Height);
                        }

                        // Save the combined image
                        const string outputPath = "CombinedBarcode.png";
                        combined.Save(outputPath, ImageFormat.Png);
                        Console.WriteLine($"Combined barcode saved to {Path.GetFullPath(outputPath)}");
                    }
                }
            }
        }
    }
}