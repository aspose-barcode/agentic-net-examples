using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path to the high‑resolution TIFF file to be scanned
        string imagePath = "highres.tif";

        // Verify that the file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Initialize the barcode reader with a set of common decode types
        using (BarCodeReader reader = new BarCodeReader(imagePath,
            DecodeType.Code39, DecodeType.Code128, DecodeType.QR, DecodeType.DataMatrix))
        {
            // Switch to the MaxQuality preset for maximum recognition accuracy
            reader.QualitySettings = QualitySettings.MaxQuality;

            // Read and output all detected barcodes
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }
    }
}