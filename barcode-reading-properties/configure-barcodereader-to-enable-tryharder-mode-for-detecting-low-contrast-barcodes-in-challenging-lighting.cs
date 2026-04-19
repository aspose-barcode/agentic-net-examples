using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        const string imagePath = "sample.png";

        // Create a sample barcode image if it does not exist.
        if (!File.Exists(imagePath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Save the barcode to a PNG file.
                generator.Save(imagePath);
            }
        }

        // Verify the image file exists before attempting to read.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Barcode image file '{imagePath}' not found.");
            return;
        }

        // Initialize the reader with the image file and the expected symbology.
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Enable try‑harder mode by applying the HighQuality preset.
            reader.QualitySettings = QualitySettings.HighQuality;

            // Optionally, enable additional processing for low‑contrast images.
            // reader.QualitySettings.ComplexBackground = ComplexBackgroundMode.Enabled;

            // Read and output all detected barcodes.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Detected Text: {result.CodeText}");
            }
        }
    }
}