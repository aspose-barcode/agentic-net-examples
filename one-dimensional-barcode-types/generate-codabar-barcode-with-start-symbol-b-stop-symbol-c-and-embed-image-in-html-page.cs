using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Create a Codabar barcode generator with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar))
        {
            // Set the data to encode (without start/stop symbols)
            generator.CodeText = "123456";

            // Configure start and stop symbols: B and C
            generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.B;
            generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.C;

            // Generate the barcode image
            using (var bitmap = generator.GenerateBarCodeImage())
            {
                // Convert the image to a Base64 string
                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    string base64 = Convert.ToBase64String(ms.ToArray());

                    // Build a simple HTML page embedding the barcode image
                    string html = $"<html><body><h2>Codabar Barcode (Start B, Stop C)</h2>" +
                                  $"<img src='data:image/png;base64,{base64}' alt='Codabar Barcode'/>" +
                                  $"</body></html>";

                    // Save the HTML file
                    File.WriteAllText("CodabarBarcode.html", html);
                }
            }
        }
    }
}