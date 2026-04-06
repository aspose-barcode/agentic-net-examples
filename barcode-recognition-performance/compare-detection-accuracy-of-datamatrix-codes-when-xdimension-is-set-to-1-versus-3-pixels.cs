using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Text to encode in DataMatrix barcodes
        string codeText = "Test123";

        // Generate DataMatrix with XDimension = 1 pixel
        using (var generator1 = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            generator1.Parameters.Barcode.XDimension.Pixels = 1f;
            generator1.Save("datamatrix_x1.png");
        }

        // Generate DataMatrix with XDimension = 3 pixels
        using (var generator3 = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            generator3.Parameters.Barcode.XDimension.Pixels = 3f;
            generator3.Save("datamatrix_x3.png");
        }

        // Read barcode generated with XDimension = 1 pixel
        using (var reader1 = new BarCodeReader("datamatrix_x1.png", DecodeType.DataMatrix))
        {
            // Use recognition mode suitable for small XDimension
            reader1.QualitySettings.XDimension = XDimensionMode.Small;
            foreach (BarCodeResult result in reader1.ReadBarCodes())
            {
                Console.WriteLine($"XDimension=1px | CodeText: {result.CodeText} | Confidence: {result.Confidence}");
            }
        }

        // Read barcode generated with XDimension = 3 pixels
        using (var reader3 = new BarCodeReader("datamatrix_x3.png", DecodeType.DataMatrix))
        {
            // Use recognition mode suitable for large XDimension
            reader3.QualitySettings.XDimension = XDimensionMode.Large;
            foreach (BarCodeResult result in reader3.ReadBarCodes())
            {
                Console.WriteLine($"XDimension=3px | CodeText: {result.CodeText} | Confidence: {result.Confidence}");
            }
        }
    }
}