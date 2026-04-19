using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Define a temporary file path for the generated barcode image
        string imagePath = Path.Combine(Environment.CurrentDirectory, "ean13.png");

        // Create an EAN13 barcode and save it to the file
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.EAN13, "1234567890128"))
        {
            generator.Save(imagePath);
        }

        // Verify that the image file was created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Read the barcode using DecodeType.EAN13 to detect only EAN13 barcodes
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.EAN13))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");
                Console.WriteLine($"Value (without checksum): {result.Extended.OneD.Value}");
                Console.WriteLine($"Checksum: {result.Extended.OneD.CheckSum}");
            }
        }
    }
}