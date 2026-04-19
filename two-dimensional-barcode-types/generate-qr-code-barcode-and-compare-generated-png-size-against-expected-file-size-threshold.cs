using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Output file path
        string outputPath = "qr.png";

        // Expected maximum file size in bytes
        long sizeThreshold = 5000;

        // Generate QR Code with high error correction level
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set QR error correction to Level H
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save as PNG
            generator.Save(outputPath);
        }

        // Verify that the file was created
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("QR code generation failed: file not found.");
            return;
        }

        // Get the generated file size
        long fileSize = new FileInfo(outputPath).Length;
        Console.WriteLine($"Generated QR code size: {fileSize} bytes.");

        // Compare against the threshold
        if (fileSize <= sizeThreshold)
        {
            Console.WriteLine("File size is within the expected threshold.");
        }
        else
        {
            Console.WriteLine("File size exceeds the expected threshold.");
        }
    }
}