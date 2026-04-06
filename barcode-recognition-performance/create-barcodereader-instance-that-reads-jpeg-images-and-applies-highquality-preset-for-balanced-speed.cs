using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a sample barcode image (JPEG) to be read later
        string imagePath = Path.Combine(Path.GetTempPath(), "sample_barcode.jpg");
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            // Save as JPEG
            generator.Save(imagePath);
        }

        // Read the JPEG image with HighQuality preset
        using (var reader = new BarCodeReader())
        {
            // Detect all supported barcode types
            reader.SetBarCodeReadType(DecodeType.AllSupportedTypes);
            // Load the image file
            reader.SetBarCodeImage(imagePath);
            // Apply HighQuality preset (balanced speed for low‑quality images)
            reader.QualitySettings = QualitySettings.HighQuality;

            // Perform recognition and output results
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"Barcode Text: {result.CodeText}");
                Console.WriteLine($"Region: {result.Region.Rectangle}");
                Console.WriteLine();
            }
        }

        // Clean up the temporary image file
        if (File.Exists(imagePath))
        {
            File.Delete(imagePath);
        }
    }
}