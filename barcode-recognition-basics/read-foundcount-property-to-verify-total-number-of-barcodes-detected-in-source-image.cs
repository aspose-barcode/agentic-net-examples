using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Path for the generated barcode image
        const string imagePath = "barcode.png";

        // Create a sample barcode and save it to a file
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Save(imagePath);
        }

        // Read the barcode image and count detected barcodes
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Perform the recognition
            reader.ReadBarCodes();

            // Retrieve the total number of barcodes found
            int totalBarcodes = reader.FoundCount;

            // Output the result
            Console.WriteLine($"Total barcodes detected: {totalBarcodes}");
        }
    }
}