using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Sample data for ITF14 barcodes (14 digits each)
        var codeTexts = new List<string>
        {
            "12345678901231",
            "98765432109876",
            "11111111111117",
            "22222222222228"
        };

        // Corresponding border styles to alternate per row
        var borderTypes = new List<ITF14BorderType>
        {
            ITF14BorderType.Frame,
            ITF14BorderType.Bar,
            ITF14BorderType.FrameOut,
            ITF14BorderType.BarOut
        };

        // Store generated barcode images in memory streams
        var barcodeStreams = new List<MemoryStream>();

        for (int i = 0; i < Math.Min(codeTexts.Count, 4); i++)
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.ITF14, codeTexts[i]))
            {
                // Set alternating border style
                generator.Parameters.Barcode.ITF.BorderType = borderTypes[i];
                // Optional: set border thickness
                generator.Parameters.Barcode.ITF.BorderThickness.Point = 2f;
                // Set barcode colors
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Save barcode to a memory stream as PNG
                var ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset for reading later
                barcodeStreams.Add(ms);
            }
        }

        // Create a multi‑page PDF and add each barcode image to a separate page
        using (var pdfDoc = new Aspose.Pdf.Document())
        {
            for (int i = 0; i < barcodeStreams.Count; i++)
            {
                var page = pdfDoc.Pages.Add();

                // Add the barcode image to the page; keep the stream alive until after Save
                var pdfImage = new Aspose.Pdf.Image
                {
                    ImageStream = barcodeStreams[i],
                    // Fit the image to the page width while preserving aspect ratio
                    FixWidth = page.PageInfo.Width,
                    FixHeight = page.PageInfo.Height
                };
                page.Paragraphs.Add(pdfImage);
            }

            // Save the PDF document
            pdfDoc.Save("ITF_Barcodes.pdf");
        }

        // Dispose all memory streams
        foreach (var stream in barcodeStreams)
        {
            stream.Dispose();
        }

        Console.WriteLine("PDF with ITF14 barcodes created successfully.");
    }
}