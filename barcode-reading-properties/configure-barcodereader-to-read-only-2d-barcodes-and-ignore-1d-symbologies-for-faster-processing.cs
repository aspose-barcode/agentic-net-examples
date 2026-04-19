using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Prepare sample barcode images
        string qrPath = "qr.png";
        string code128Path = "code128.png";

        // Generate a QR code (2D)
        using (var qrGenerator = new BarcodeGenerator(EncodeTypes.QR, "Hello 2D"))
        {
            qrGenerator.Save(qrPath);
        }

        // Generate a Code128 barcode (1D)
        using (var code128Generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            code128Generator.Save(code128Path);
        }

        // Read only 2D barcodes from the QR image (should detect)
        Console.WriteLine("Reading QR image (2D barcode):");
        ReadOnly2DBarcodes(qrPath);

        // Read only 2D barcodes from the Code128 image (should ignore)
        Console.WriteLine("\nReading Code128 image (1D barcode, should be ignored):");
        ReadOnly2DBarcodes(code128Path);
    }

    static void ReadOnly2DBarcodes(string imagePath)
    {
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Initialize reader to process only 2D symbologies
        using (var reader = new BarCodeReader(imagePath, DecodeType.Types2D))
        {
            // Perform recognition
            var results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No 2D barcodes detected.");
                return;
            }

            foreach (var result in results)
            {
                Console.WriteLine($"Type: {result.CodeTypeName}");
                Console.WriteLine($"Text: {result.CodeText}");
            }
        }
    }
}