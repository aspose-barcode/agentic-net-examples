using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Words;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Create a QR code generator with desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            generator.CodeText = "https://example.com";
            // Set high error correction level
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the barcode image to a memory stream in PNG format
            using (var imageStream = new MemoryStream())
            {
                generator.Save(imageStream, BarCodeImageFormat.Png);
                imageStream.Position = 0;

                // Create a new Word document
                var doc = new Document();
                var builder = new DocumentBuilder(doc);
                // Insert the barcode image into the document
                builder.InsertImage(imageStream.ToArray());

                // Save the Word document
                doc.Save("QRCodeDocument.docx");
            }
        }
    }
}