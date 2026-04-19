using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeDemo
{
    class Program
    {
        static void Main()
        {
            // Simulate an ASP.NET response output stream
            using (var responseStream = new MemoryStream())
            {
                // Create a barcode generator for Code128 with sample text
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
                {
                    // Optional: set barcode bar color
                    generator.Parameters.Barcode.BarColor = Color.Blue;

                    // Save the barcode image directly to the simulated response stream as PNG
                    generator.Save(responseStream, BarCodeImageFormat.Png);
                }

                // Demonstrate the result by writing the stream to a file
                File.WriteAllBytes("barcode.png", responseStream.ToArray());
                Console.WriteLine("Barcode generated and written to simulated response stream. Saved to barcode.png");
            }
        }
    }
}