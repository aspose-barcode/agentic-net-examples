using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        const string imagePath = "qr.png";
        const string logPath = "diagnostics.log";

        // Clean up any previous files.
        if (File.Exists(imagePath)) File.Delete(imagePath);
        if (File.Exists(logPath)) File.Delete(logPath);

        // Create a QR code image.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            generator.CodeText = "Hello Aspose!";
            generator.Save(imagePath);
        }

        // Read the QR code and log its confidence level.
        using (var reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                string logEntry = $"Confidence: {result.Confidence}";
                File.AppendAllText(logPath, logEntry + Environment.NewLine);
            }
        }
    }
}