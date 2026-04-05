using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path for the generated barcode image
        const string filePath = "barcode.png";

        // Create a barcode generator, set interpolation mode and 300 dpi resolution, then save the image
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Resize using interpolation (may affect readability)
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set image resolution to 300 dpi
            generator.Parameters.Resolution = 300f;

            // Save the barcode image to file
            generator.Save(filePath);
        }

        // Read the saved barcode image and output recognition results
        using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"Confidence: {result.Confidence}");
                Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");
            }
        }
    }
}