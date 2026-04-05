using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Encode text for Code39 barcode
        const string codeText = "ABC123";

        // Generate barcode with default settings (checksum not explicitly set)
        byte[] defaultImage;
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, codeText))
        {
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                defaultImage = ms.ToArray();
            }
        }

        // Generate barcode with checksum explicitly disabled
        byte[] disabledImage;
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, codeText))
        {
            // Disable checksum for optional‑checksum symbology
            generator.Parameters.Barcode.IsChecksumEnabled = Aspose.BarCode.Generation.EnableChecksum.No;
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                disabledImage = ms.ToArray();
            }
        }

        // Compare the two images byte by byte
        bool imagesIdentical = defaultImage.Length == disabledImage.Length;
        if (imagesIdentical)
        {
            for (int i = 0; i < defaultImage.Length; i++)
            {
                if (defaultImage[i] != disabledImage[i])
                {
                    imagesIdentical = false;
                    break;
                }
            }
        }

        Console.WriteLine(imagesIdentical
            ? "Images are identical – disabling checksum does not alter the barcode."
            : "Images differ – disabling checksum changed the barcode.");
    }
}