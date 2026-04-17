using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using Aspose.Slides;
using Aspose.Slides.Export;

class Program
{
    static void Main()
    {
        const string barcodeText = "https://example.com";
        const string presentationPath = "QrBarcodePresentation.pptx";

        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, barcodeText))
        {
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            using (Bitmap barcodeBitmap = generator.GenerateBarCodeImage())
            {
                using (MemoryStream imageStream = new MemoryStream())
                {
                    barcodeBitmap.Save(imageStream, Aspose.Drawing.Imaging.ImageFormat.Png);
                    byte[] imageBytes = imageStream.ToArray();

                    using (Presentation presentation = new Presentation())
                    {
                        ISlide slide = presentation.Slides[0];
                        IPPImage pptImage = presentation.Images.AddImage(imageBytes);
                        slide.Shapes.AddPictureFrame(ShapeType.Rectangle, 50, 50, 300, 300, pptImage);
                        presentation.Save(presentationPath, SaveFormat.Pptx);
                    }
                }
            }
        }

        Console.WriteLine($"Presentation saved to '{presentationPath}'.");
    }
}