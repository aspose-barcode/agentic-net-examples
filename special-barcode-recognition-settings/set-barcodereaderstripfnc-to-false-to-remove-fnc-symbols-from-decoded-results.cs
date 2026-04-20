using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a barcode generator for GS1-128 with a sample code containing FNC characters
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, "(02)04006664241007(37)1(400)7019590754"))
        {
            // Generate the barcode image in memory
            using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
            {
                // Initialize the reader with the generated image and specify the decode type
                using (var reader = new BarCodeReader(barcodeImage, DecodeType.Code128))
                {
                    // Set StripFNC to false as requested
                    reader.BarcodeSettings.StripFNC = false;

                    // Read and output all detected barcodes
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine("BarCode Type: " + result.CodeTypeName);
                        Console.WriteLine("BarCode CodeText: " + result.CodeText);
                    }
                }
            }
        }
    }
}