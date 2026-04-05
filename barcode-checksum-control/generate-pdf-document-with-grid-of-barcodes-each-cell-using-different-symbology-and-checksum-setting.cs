using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const int columns = 3;
        const int rows = 4;

        var specs = new[]
        {
            new { Type = EncodeTypes.Code128, Text = "ABC123", Checksum = EnableChecksum.Yes, ShowChecksum = true },
            new { Type = EncodeTypes.Code39FullASCII, Text = "CODE39", Checksum = EnableChecksum.No, ShowChecksum = false },
            new { Type = EncodeTypes.EAN13, Text = "1234567890128", Checksum = EnableChecksum.Yes, ShowChecksum = false },
            new { Type = EncodeTypes.QR, Text = "https://example.com", Checksum = EnableChecksum.Default, ShowChecksum = false },
            new { Type = EncodeTypes.Pdf417, Text = "PDF417 Sample", Checksum = EnableChecksum.Default, ShowChecksum = false },
            new { Type = EncodeTypes.ITF14, Text = "12345678901231", Checksum = EnableChecksum.Yes, ShowChecksum = false },
            new { Type = EncodeTypes.Codabar, Text = "A123456A", Checksum = EnableChecksum.Yes, ShowChecksum = false },
            new { Type = EncodeTypes.DataMatrix, Text = "DataMatrix", Checksum = EnableChecksum.Default, ShowChecksum = false },
            new { Type = EncodeTypes.UPCA, Text = "012345678905", Checksum = EnableChecksum.Yes, ShowChecksum = false },
            new { Type = EncodeTypes.UPCE, Text = "01234565", Checksum = EnableChecksum.Yes, ShowChecksum = false },
            new { Type = EncodeTypes.Code93, Text = "CODE93", Checksum = EnableChecksum.Default, ShowChecksum = false },
            new { Type = EncodeTypes.Interleaved2of5, Text = "12345678", Checksum = EnableChecksum.Yes, ShowChecksum = false }
        };

        using (var doc = new Document())
        {
            var page = doc.Pages.Add();

            double pageWidth = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;

            double cellWidth = pageWidth / columns;
            double cellHeight = pageHeight / rows;

            for (int i = 0; i < specs.Length; i++)
            {
                int col = i % columns;
                int row = i / columns;

                double llx = col * cellWidth;
                double lly = pageHeight - (row + 1) * cellHeight;
                double urx = (col + 1) * cellWidth;
                double ury = pageHeight - row * cellHeight;

                var spec = specs[i];

                using (var generator = new BarcodeGenerator(spec.Type, spec.Text))
                {
                    generator.Parameters.Barcode.IsChecksumEnabled = spec.Checksum;
                    generator.Parameters.Barcode.ChecksumAlwaysShow = spec.ShowChecksum;

                    generator.Parameters.ImageWidth.Point = 150f;
                    generator.Parameters.ImageHeight.Point = 50f;

                    using (var ms = new MemoryStream())
                    {
                        generator.Save(ms, BarCodeImageFormat.Png);
                        ms.Position = 0;

                        var rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);
                        page.AddImage(ms, rect, (int)cellWidth, (int)cellHeight, true);
                    }
                }
            }

            doc.Save("BarcodesGrid.pdf");
        }
    }
}