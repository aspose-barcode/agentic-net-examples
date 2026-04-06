using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Path for the temporary barcode image
        string imagePath = "ean13.png";

        // Generate a sample EAN-13 barcode
        using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, "1234567890128"))
        {
            generator.Save(imagePath);
        }

        // Read the barcode using DecodeType.EAN13 to detect only EAN-13 barcodes
        using (var reader = new BarCodeReader(imagePath, DecodeType.EAN13))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("Detected Barcode Type: " + result.CodeTypeName);
                Console.WriteLine("Code Text: " + result.CodeText);
                Console.WriteLine("Value (without checksum): " + result.Extended.OneD.Value);
                Console.WriteLine("Checksum: " + result.Extended.OneD.CheckSum);
                var rect = result.Region.Rectangle;
                Console.WriteLine($"Region: X={rect.X}, Y={rect.Y}, Width={rect.Width}, Height={rect.Height}");
            }
        }

        // Clean up the temporary image file
        if (System.IO.File.Exists(imagePath))
        {
            System.IO.File.Delete(imagePath);
        }
    }
}