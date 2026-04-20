using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

namespace BarcodeStripFncExample
{
    class Program
    {
        static void Main()
        {
            // Sample GS1-128 barcode containing FNC1 characters (represented by parentheses)
            const string codeText = "(02)04006664241007(37)1(400)7019590754";

            // Generate the barcode image
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
            {
                using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
                {
                    // Read the barcode from the generated image
                    using (var reader = new BarCodeReader(barcodeImage, DecodeType.Code128))
                    {
                        // Enable stripping of FNC characters (true = strip)
                        reader.BarcodeSettings.StripFNC = true;

                        // Iterate through all detected barcodes
                        foreach (var result in reader.ReadBarCodes())
                        {
                            Console.WriteLine("Detected CodeText: " + result.CodeText);
                        }
                    }
                }
            }
        }
    }
}