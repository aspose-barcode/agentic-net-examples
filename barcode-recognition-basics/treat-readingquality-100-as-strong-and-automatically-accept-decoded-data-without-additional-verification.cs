using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path for the generated barcode image
        string imagePath = "barcode.png";

        // Generate a QR barcode with known content
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "12345"))
        {
            generator.Save(imagePath);
        }

        // Verify that the image was created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode and evaluate its reading quality
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // ReadingQuality of 100 indicates strong confidence
                if (result.ReadingQuality == 100.0)
                {
                    Console.WriteLine($"Accepted: {result.CodeText} (ReadingQuality: {result.ReadingQuality})");
                }
                else
                {
                    Console.WriteLine($"Rejected: {result.CodeText} (ReadingQuality: {result.ReadingQuality})");
                }
            }
        }
    }
}