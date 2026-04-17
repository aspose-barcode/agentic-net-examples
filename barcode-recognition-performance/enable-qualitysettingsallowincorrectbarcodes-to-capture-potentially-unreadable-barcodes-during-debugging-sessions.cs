using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Define a temporary file for the barcode image
        string imagePath = Path.Combine(Path.GetTempPath(), "sample_barcode.png");

        // Generate a simple Code128 barcode and save it to the file
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            generator.Save(imagePath);
        }

        // Ensure the file was created before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode with AllowIncorrectBarcodes enabled
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Enable recognition of potentially incorrect barcodes
            reader.QualitySettings.AllowIncorrectBarcodes = true;

            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");
                Console.WriteLine($"Confidence: {result.Confidence}");
            }
        }

        // Clean up the temporary image file
        try
        {
            File.Delete(imagePath);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}