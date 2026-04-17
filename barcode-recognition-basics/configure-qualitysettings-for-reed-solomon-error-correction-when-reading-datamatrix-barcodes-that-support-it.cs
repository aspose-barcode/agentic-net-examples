using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path to the DataMatrix image file
        string imagePath = "datamatrix.png";

        // Verify that the file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Create a BarCodeReader for DataMatrix symbology
        using (var reader = new BarCodeReader(imagePath, DecodeType.DataMatrix))
        {
            // Configure QualitySettings to improve Reed‑Solomon error correction handling.
            // HighQuality preset is suitable for low‑quality or damaged barcodes.
            reader.QualitySettings = QualitySettings.HighQuality;

            // Read all detected barcodes
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");
                Console.WriteLine($"Confidence: {result.Confidence}");
                Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                Console.WriteLine();
            }
        }
    }
}