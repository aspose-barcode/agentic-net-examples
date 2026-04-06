using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        const string imagePath = "barcode.png";

        // Generate a sample barcode image
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Save(imagePath);
        }

        // Read the barcode and extract region coordinates
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Rectangle rect = result.Region.Rectangle;
                Console.WriteLine($"Detected barcode '{result.CodeText}' at [{rect.X}, {rect.Y}, {rect.Width}, {rect.Height}]");
            }
        }
    }
}