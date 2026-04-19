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
        string imagePath = "upc.png";

        // Generate a UPC-A barcode image if it does not exist
        if (!File.Exists(imagePath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.UPCA, "012345678905"))
            {
                // Save the generated barcode image
                generator.Save(imagePath);
            }
        }

        // Verify the image exists before attempting recognition
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a BarCodeReader and configure MultiDecodeType for UPC-A, UPC-E, and EAN-8
        using (var reader = new BarCodeReader())
        {
            // Set the decode types to be recognized
            reader.BarCodeReadType = new MultiDecodeType(DecodeType.UPCA, DecodeType.UPCE, DecodeType.EAN8);

            // Load the image for recognition
            reader.SetBarCodeImage(imagePath);

            // Perform recognition
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");
            }

            // Output the configured decode type for verification
            Console.WriteLine($"Configured Decode Types: {reader.BarCodeReadType}");
        }
    }
}