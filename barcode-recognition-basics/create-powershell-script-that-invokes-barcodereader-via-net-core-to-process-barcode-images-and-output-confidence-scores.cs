using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path to the barcode image
        string imagePath = "sample.png";

        // If the image does not exist, generate a sample barcode image
        if (!File.Exists(imagePath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
            {
                // Optional: set image size
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 150f;

                // Save the generated barcode image
                generator.Save(imagePath);
            }
        }

        // Verify the image file exists before attempting to read
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Barcode image file not found at '{imagePath}'.");
            return;
        }

        // Create a BarCodeReader for the image, detecting all supported types
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Read all barcodes from the image
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes were detected in the image.");
                return;
            }

            // Output details for each detected barcode
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                Console.WriteLine($"BarCode CodeText: {result.CodeText}");
                Console.WriteLine($"BarCode Confidence: {result.Confidence}");
                Console.WriteLine($"BarCode ReadingQuality: {result.ReadingQuality}");
                Console.WriteLine();
            }
        }
    }
}