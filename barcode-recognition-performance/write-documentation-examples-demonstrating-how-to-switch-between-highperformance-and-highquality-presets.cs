using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Define a temporary file path for the barcode image
        string imagePath = Path.Combine(Path.GetTempPath(), "sample_barcode.png");

        // Generate a Code128 barcode and save it to the file
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            generator.Save(imagePath);
        }

        // Verify that the image was created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // ----------- HighPerformance preset -----------
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Switch to HighPerformance recognition mode
            reader.QualitySettings = QualitySettings.HighPerformance;

            Console.WriteLine("Reading with HighPerformance preset:");
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }

        // ----------- HighQuality preset -----------
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Switch to HighQuality recognition mode
            reader.QualitySettings = QualitySettings.HighQuality;

            Console.WriteLine("Reading with HighQuality preset:");
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
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