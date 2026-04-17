using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        const string imagePath = "sample.jpg";

        // Ensure the image exists; if not, create a sample barcode image.
        if (!File.Exists(imagePath))
        {
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
            {
                generator.Save(imagePath);
            }
        }

        // Verify the file still exists before attempting to read.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Image file '{imagePath}' not found.");
            return;
        }

        // Read all supported barcodes from the image.
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"Barcode Text: {result.CodeText}");
                Console.WriteLine();
            }

            if (reader.FoundCount == 0)
            {
                Console.WriteLine("No barcodes were detected in the image.");
            }
        }
    }
}