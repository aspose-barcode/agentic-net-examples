using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        const string pdfPath = "sample.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.WriteLine("PDF file not found: " + pdfPath);
            return;
        }

        using (var pdfDocument = new Document(pdfPath))
        {
            int pageCount = pdfDocument.Pages.Count;

            for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
            {
                using (var imageStream = new MemoryStream())
                {
                    var resolution = new Resolution(300);
                    var pngDevice = new PngDevice(resolution);
                    pngDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
                    imageStream.Position = 0;

                    using (var bitmap = (Bitmap)Aspose.Drawing.Image.FromStream(imageStream))
                    {
                        using (var reader = new BarCodeReader())
                        {
                            reader.BarCodeReadType = new MultiDecodeType(DecodeType.AllSupportedTypes);
                            reader.SetBarCodeImage(bitmap);

                            foreach (var result in reader.ReadBarCodes())
                            {
                                Console.WriteLine($"Page {pageNumber}: Type = {result.CodeTypeName}, Text = {result.CodeText}");
                            }
                        }
                    }
                }
            }
        }
    }
}