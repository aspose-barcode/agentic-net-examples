using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        string emfPath = "barcode.emf";
        string pdfPath = "barcode.pdf";

        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39))
        {
            generator.CodeText = "1234567890";
            generator.Save(emfPath, BarCodeImageFormat.Emf);
        }

        using (Document pdfDocument = new Document())
        {
            var page = pdfDocument.Pages.Add();

            var image = new Aspose.Pdf.Image
            {
                File = emfPath
            };

            page.Paragraphs.Add(image);
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"Barcode saved as EMF: {Path.GetFullPath(emfPath)}");
        Console.WriteLine($"PDF created from EMF: {Path.GetFullPath(pdfPath)}");
    }
}