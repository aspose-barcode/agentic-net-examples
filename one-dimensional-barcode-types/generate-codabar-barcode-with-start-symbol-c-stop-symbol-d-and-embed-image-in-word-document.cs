using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using Aspose.Words;

namespace CodabarWordExample
{
    class Program
    {
        static void Main()
        {
            // Sample codetext without start/stop symbols
            const string codeText = "123456";

            // Create barcode generator for Codabar
            using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, codeText))
            {
                // Set start and stop symbols to C and D
                generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.C;
                generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.D;

                // Generate barcode image as Bitmap
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    // Save bitmap to memory stream in PNG format
                    using (var imageStream = new MemoryStream())
                    {
                        bitmap.Save(imageStream, ImageFormat.Png);
                        byte[] imageBytes = imageStream.ToArray();

                        // Create a new Word document and insert the barcode image
                        var doc = new Document();
                        var builder = new DocumentBuilder(doc);
                        // Insert image with desired size (width, height) in points
                        builder.InsertImage(imageBytes, 200.0, 50.0);

                        // Save the Word document
                        doc.Save("Codabar.docx");
                    }
                }
            }

            Console.WriteLine("Codabar barcode embedded in Codabar.docx successfully.");
        }
    }
}