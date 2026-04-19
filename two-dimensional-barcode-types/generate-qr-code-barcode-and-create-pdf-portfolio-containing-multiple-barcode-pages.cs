using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Prepare QR code texts
        string[] qrTexts = { "First QR Code", "Second QR Code", "Third QR Code" };
        string[] imageFiles = new string[qrTexts.Length];

        // Generate QR code images
        for (int i = 0; i < qrTexts.Length; i++)
        {
            string fileName = $"qr{i + 1}.png";
            imageFiles[i] = fileName;

            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                generator.CodeText = qrTexts[i];
                // Set high error correction level
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
                // Optional: define module size
                generator.Parameters.Barcode.XDimension.Point = 2f;
                // Save as PNG
                generator.Save(fileName, BarCodeImageFormat.Png);
            }
        }

        // Create PDF portfolio and add each QR code as a separate page
        using (var pdfDoc = new Document())
        {
            for (int i = 0; i < imageFiles.Length; i++)
            {
                // Ensure we do not exceed the limit of 4 pages per collection
                if (i >= 4) break;

                using (var page = pdfDoc.Pages.Add())
                {
                    // Add the QR code image to the page
                    var img = new Image
                    {
                        ImageStream = new FileStream(imageFiles[i], FileMode.Open, FileAccess.Read)
                    };
                    // Center the image on the page
                    img.FixWidth = 200;
                    img.FixHeight = 200;
                    img.HorizontalAlignment = HorizontalAlignment.Center;
                    img.VerticalAlignment = VerticalAlignment.Center;
                    page.Paragraphs.Add(img);
                }
            }

            // Save the PDF portfolio
            pdfDoc.Save("BarcodesPortfolio.pdf");
        }

        // Optional cleanup of temporary image files
        foreach (var file in imageFiles)
        {
            if (File.Exists(file))
            {
                try { File.Delete(file); } catch { /* ignore */ }
            }
        }

        Console.WriteLine("PDF portfolio with QR codes has been created successfully.");
    }
}