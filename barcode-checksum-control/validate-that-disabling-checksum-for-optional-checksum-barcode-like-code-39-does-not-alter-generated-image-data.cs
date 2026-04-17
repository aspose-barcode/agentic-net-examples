using System;
using System.IO;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Code text to encode
        const string codeText = "ABC123";

        // Generate barcode with default checksum (enabled where applicable)
        byte[] enabledImageBytes;
        using (var generatorEnabled = new BarcodeGenerator(EncodeTypes.Code39, codeText))
        {
            using (var msEnabled = new MemoryStream())
            {
                generatorEnabled.Save(msEnabled, BarCodeImageFormat.Png);
                enabledImageBytes = msEnabled.ToArray();
            }
        }

        // Generate barcode with checksum explicitly disabled
        byte[] disabledImageBytes;
        using (var generatorDisabled = new BarcodeGenerator(EncodeTypes.Code39, codeText))
        {
            // Disable optional checksum
            generatorDisabled.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;
            using (var msDisabled = new MemoryStream())
            {
                generatorDisabled.Save(msDisabled, BarCodeImageFormat.Png);
                disabledImageBytes = msDisabled.ToArray();
            }
        }

        // Compare the two images byte by byte
        bool imagesIdentical = enabledImageBytes.Length == disabledImageBytes.Length &&
                               enabledImageBytes.SequenceEqual(disabledImageBytes);

        Console.WriteLine("Checksum disabled does not alter image data: " + imagesIdentical);
    }
}