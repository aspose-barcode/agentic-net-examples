using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Define temporary image path
        string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "gs1code128.png");

        // Generate a GS1 Code128 barcode with sample data containing FNC characters
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1Code128, "(02)04006664241007(37)1(400)7019590754"))
        {
            generator.Save(imagePath);
        }

        // Verify that the image was created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Decode without stripping FNC characters
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            reader.BarcodeSettings.StripFNC = false;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("StripFNC = false");
                Console.WriteLine("  CodeType: " + result.CodeTypeName);
                Console.WriteLine("  CodeText: " + result.CodeText);
            }
        }

        // Decode with stripping FNC characters
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            reader.BarcodeSettings.StripFNC = true;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("StripFNC = true");
                Console.WriteLine("  CodeType: " + result.CodeTypeName);
                Console.WriteLine("  CodeText: " + result.CodeText);
            }
        }

        // Clean up temporary file
        try
        {
            File.Delete(imagePath);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}