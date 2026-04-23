using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Prepare Mailmark codetext with required default values
        var mailmark = new MailmarkCodetext
        {
            Format = 4,                     // 4‑state format
            VersionID = 1,                  // version
            Class = "0",                    // class
            SupplychainID = 384224,         // supply chain identifier
            ItemID = 16563762,              // item identifier
            DestinationPostCodePlusDPS = "EF61AH8T " // destination postcode + DPS (9 chars, trailing space)
        };

        // Generate the Mailmark barcode image into a memory stream
        using (var barcodeStream = new MemoryStream())
        {
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                using (var bitmap = generator.GenerateBarCodeImage())
                {
                    // Save bitmap as PNG into the stream
                    bitmap.Save(barcodeStream, ImageFormat.Png);
                }
            }

            // Reset stream position for reading
            barcodeStream.Position = 0;

            // Create PDF document and embed the barcode image
            using (var pdfDoc = new Document())
            {
                var page = pdfDoc.Pages.Add();

                // Create Aspose.Pdf.Image using the same stream (keep it alive until Save)
                var pdfImage = new Aspose.Pdf.Image
                {
                    ImageStream = barcodeStream,
                    // Optional: set image dimensions (points)
                    FixWidth = 200,
                    FixHeight = 100
                };

                // Add image to the page
                page.Paragraphs.Add(pdfImage);

                // Save PDF to file
                pdfDoc.Save("Mailmark.pdf");
            }
        }

        Console.WriteLine("Mailmark PDF generated successfully.");
    }
}