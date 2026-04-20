using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        List<string> codeTexts = new List<string>
        {
            "1234567890",
            "0987654321",
            "1122334455",
            "5566778899"
        };

        List<MemoryStream> imageStreams = new List<MemoryStream>();

        using (Document pdfDoc = new Document())
        {
            foreach (string code in codeTexts)
            {
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, code))
                {
                    generator.Parameters.ImageWidth.Point = 300f;
                    generator.Parameters.ImageHeight.Point = 150f;
                    generator.Parameters.Resolution = 300f;

                    using (MemoryStream ms = new MemoryStream())
                    {
                        generator.Save(ms, BarCodeImageFormat.Png);
                        ms.Position = 0;

                        using (Bitmap bitmap = new Bitmap(ms))
                        {
                            using (MemoryStream imgStream = new MemoryStream())
                            {
                                bitmap.Save(imgStream, ImageFormat.Png);
                                imgStream.Position = 0;

                                // Keep the stream alive for PDF saving
                                MemoryStream pdfImgStream = new MemoryStream(imgStream.ToArray());
                                imageStreams.Add(pdfImgStream);

                                Page page = pdfDoc.Pages.Add();

                                Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image
                                {
                                    ImageStream = pdfImgStream
                                };

                                pdfImage.FixWidth = page.PageInfo.Width;
                                pdfImage.FixHeight = page.PageInfo.Height;

                                page.Paragraphs.Add(pdfImage);
                            }
                        }
                    }
                }
            }

            string outputPdfPath = Path.Combine(Directory.GetCurrentDirectory(), "SwissPostParcelBarcodes.pdf");
            pdfDoc.Save(outputPdfPath);
        }

        // Dispose image streams after PDF is saved
        foreach (MemoryStream stream in imageStreams)
        {
            stream.Dispose();
        }

        Console.WriteLine($"PDF saved to: {Path.Combine(Directory.GetCurrentDirectory(), "SwissPostParcelBarcodes.pdf")}");
    }
}