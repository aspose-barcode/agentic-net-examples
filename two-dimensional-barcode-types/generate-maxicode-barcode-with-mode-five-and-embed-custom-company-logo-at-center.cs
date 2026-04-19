using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Paths for output barcode image and optional logo image
        const string outputPath = "MaxiCodeMode5.png";
        const string logoPath = "logo.png";

        // Prepare MaxiCode standard codetext for mode 5
        var maxiCodeCodetext = new MaxiCodeStandardCodetext();
        maxiCodeCodetext.Mode = MaxiCodeMode.Mode5;
        maxiCodeCodetext.Message = "Sample message";

        // Generate the MaxiCode barcode image
        using (var complexGenerator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            using (Bitmap barcodeBitmap = complexGenerator.GenerateBarCodeImage())
            {
                // If a logo file exists, embed it at the center of the barcode
                if (File.Exists(logoPath))
                {
                    using (Image logoImage = Image.FromFile(logoPath))
                    {
                        // Scale logo to 20% of barcode dimensions while preserving aspect ratio
                        int targetWidth = (int)(barcodeBitmap.Width * 0.2f);
                        int targetHeight = (int)(barcodeBitmap.Height * 0.2f);
                        using (Bitmap resizedLogo = new Bitmap(logoImage, new Size(targetWidth, targetHeight)))
                        {
                            int posX = (barcodeBitmap.Width - resizedLogo.Width) / 2;
                            int posY = (barcodeBitmap.Height - resizedLogo.Height) / 2;

                            using (Graphics graphics = Graphics.FromImage(barcodeBitmap))
                            {
                                graphics.DrawImage(resizedLogo, posX, posY, resizedLogo.Width, resizedLogo.Height);
                            }
                        }
                    }
                }

                // Save the final image (barcode with optional logo) as PNG
                using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    barcodeBitmap.Save(fileStream, ImageFormat.Png);
                }
            }
        }

        Console.WriteLine($"Barcode saved to '{outputPath}'.");
    }
}