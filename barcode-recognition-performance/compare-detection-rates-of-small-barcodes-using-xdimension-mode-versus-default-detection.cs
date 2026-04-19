using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Sample data for barcodes
        string[] codes = { "12345", "67890", "ABCDE", "FGHIJ", "KLMNO" };
        string[] files = new string[codes.Length];

        // Generate small‑XDimension barcodes
        for (int i = 0; i < codes.Length; i++)
        {
            string fileName = $"barcode_{i}.png";
            files[i] = fileName;

            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codes[i]))
            {
                // Set a very small XDimension (1 point)
                generator.Parameters.Barcode.XDimension.Point = 1f;
                generator.Save(fileName);
            }
        }

        // Compare detection using default (Auto) vs Small XDimension mode
        int detectedDefault = 0;
        int detectedSmall = 0;

        foreach (string file in files)
        {
            if (!File.Exists(file))
                continue;

            // Default detection (Auto)
            if (Recognize(file, XDimensionMode.Auto))
                detectedDefault++;

            // Small XDimension detection
            if (Recognize(file, XDimensionMode.Small))
                detectedSmall++;
        }

        Console.WriteLine($"Total barcodes: {codes.Length}");
        Console.WriteLine($"Detected with default (Auto) mode: {detectedDefault}/{codes.Length}");
        Console.WriteLine($"Detected with Small XDimension mode: {detectedSmall}/{codes.Length}");
    }

    // Recognizes a barcode image using the specified XDimension mode.
    // Returns true if at least one barcode is found.
    static bool Recognize(string imagePath, XDimensionMode mode)
    {
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Use a high‑performance preset for speed
            reader.QualitySettings = QualitySettings.HighPerformance;
            // Set the XDimension recognition mode
            reader.QualitySettings.XDimension = mode;

            foreach (var result in reader.ReadBarCodes())
            {
                // Successful detection
                return true;
            }
        }
        return false;
    }
}