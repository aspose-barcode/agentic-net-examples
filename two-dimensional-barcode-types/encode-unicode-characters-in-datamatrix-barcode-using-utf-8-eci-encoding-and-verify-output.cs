using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Unicode text to encode
        const string unicodeText = "犬Right狗";

        // Output file path
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "datamatrix.png");

        // Create DataMatrix barcode with UTF‑8 ECI encoding
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, unicodeText))
        {
            // Set ECI encoding to UTF‑8 (used when EncodeMode is Auto)
            generator.Parameters.Barcode.DataMatrix.ECIEncoding = ECIEncodings.UTF8;

            // Save the barcode image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify the barcode by reading it back
        using (var reader = new BarCodeReader(outputPath, DecodeType.DataMatrix))
        {
            // Ensure the reader detects encoding for Unicode characters
            reader.BarcodeSettings.DetectEncoding = true;

            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("Decoded CodeText: " + result.CodeText);
            }
        }
    }
}