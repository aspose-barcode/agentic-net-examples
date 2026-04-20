using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Define a temporary file for the barcode image
        string imagePath = Path.Combine(Path.GetTempPath(), "gs1code128.png");

        // Create a GS1 Code128 barcode that contains FNC characters (AI delimiters)
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

        // Decode with StripFNC disabled (retain FNC symbols)
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.GS1Code128))
        {
            reader.BarcodeSettings.StripFNC = false;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("StripFNC disabled - CodeText: " + result.CodeText);
            }
        }

        // Decode with StripFNC enabled (strip FNC symbols)
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.GS1Code128))
        {
            reader.BarcodeSettings.StripFNC = true;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("StripFNC enabled  - CodeText: " + result.CodeText);
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