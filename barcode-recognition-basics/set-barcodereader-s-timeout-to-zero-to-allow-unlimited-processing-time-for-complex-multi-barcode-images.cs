using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path for the sample barcode image
        const string imagePath = "sample.png";

        // Create a sample barcode image if it does not exist
        if (!File.Exists(imagePath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                generator.CodeText = "Sample123";
                generator.Save(imagePath);
            }
        }

        // Read the barcode with unlimited timeout (0 means no timeout)
        using (var reader = new BarCodeReader(imagePath))
        {
            reader.Timeout = 0; // Unlimited processing time

            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }
    }
}