using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path for the generated barcode image
        string barcodePath = "barcode.png";

        // Create a Code128 barcode and save it to a file
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345ABC"))
        {
            generator.Save(barcodePath);
        }

        // -----------------------------------------------------------------
        // Example: Switch to HighPerformance preset for fast recognition
        // -----------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            // Apply the HighPerformance quality preset
            reader.QualitySettings = QualitySettings.HighPerformance;

            // Read and display the recognized barcode(s)
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine("HighPerformance - CodeText: " + result.CodeText);
            }
        }

        // -----------------------------------------------------------------
        // Example: Switch to HighQuality preset for low‑quality barcodes
        // -----------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            // Apply the HighQuality quality preset
            reader.QualitySettings = QualitySettings.HighQuality;

            // Read and display the recognized barcode(s)
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine("HighQuality - CodeText: " + result.CodeText);
            }
        }
    }
}