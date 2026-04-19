using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        string csvPath = "input.csv";
        string barcodeFolder = "Barcodes";
        string pdfPath = "BarcodesReport.pdf";

        if (!File.Exists(csvPath))
        {
            File.WriteAllLines(csvPath, new[]
            {
                "Id,Value",
                "1,https://example.com/1",
                "2,https://example.com/2",
                "3,https://example.com/3",
                "4,https://example.com/4",
                "5,https://example.com/5"
            });
        }

        if (!Directory.Exists(barcodeFolder))
        {
            Directory.CreateDirectory(barcodeFolder);
        }

        var values = new List<string>();
        foreach (var line in File.ReadAllLines(csvPath))
        {
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith("Id", StringComparison.OrdinalIgnoreCase))
                continue;
            var parts = line.Split(',');
            if (parts.Length >= 2)
                values.Add(parts[1].Trim());
        }

        var barcodePaths = new List<string>();
        int index = 1;
        foreach (var text in values)
        {
            string imagePath = Path.Combine(barcodeFolder, $"qr_{index}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, text))
            {
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
                generator.Parameters.ImageWidth.Point = 200f;
                generator.Parameters.ImageHeight.Point = 200f;
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }
            barcodePaths.Add(imagePath);
            index++;
        }

        using (var pdfDoc = new Document())
        {
            int maxPages = 4;
            int imagesPerPage = (int)Math.Ceiling((double)barcodePaths.Count / maxPages);
            int currentImage = 0;

            for (int pageIndex = 0; pageIndex < maxPages && currentImage < barcodePaths.Count; pageIndex++)
            {
                var page = pdfDoc.Pages.Add();

                for (int i = 0; i < imagesPerPage && currentImage < barcodePaths.Count; i++, currentImage++)
                {
                    var img = new Image
                    {
                        File = barcodePaths[currentImage],
                        FixWidth = page.PageInfo.Width / 2,
                        FixHeight = page.PageInfo.Height / 2
                    };
                    page.Paragraphs.Add(img);
                    page.Paragraphs.Add(new TextFragment("\n"));
                }
            }

            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF report generated at: {Path.GetFullPath(pdfPath)}");
    }
}