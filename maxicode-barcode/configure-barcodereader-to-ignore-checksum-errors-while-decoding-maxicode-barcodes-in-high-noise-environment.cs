using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

class Program
{
    static void Main()
    {
        // Path to the MaxiCode image to be decoded.
        const string imagePath = "maxicode.png";

        // Verify that the image file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a BarCodeReader for MaxiCode symbology.
        using (var reader = new BarCodeReader(imagePath, DecodeType.MaxiCode))
        {
            // Ignore checksum validation to tolerate errors in noisy images.
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;

            // Use a high‑quality preset to improve recognition in a high‑noise environment.
            reader.QualitySettings = QualitySettings.HighQuality;

            // Perform the recognition.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected MaxiCode: {result.CodeText}");
            }
        }
    }
}