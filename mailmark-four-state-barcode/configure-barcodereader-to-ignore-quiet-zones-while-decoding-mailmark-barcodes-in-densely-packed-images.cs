using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Prepare Mailmark codetext (4‑state)
        var mailmark = new MailmarkCodetext
        {
            Format = 4,                         // 4‑state format
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Generate Mailmark barcode image into a memory stream
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        using (var memory = new MemoryStream())
        {
            generator.Save(memory, BarCodeImageFormat.Png);
            memory.Position = 0;

            // Read the barcode, configuring quality settings to improve detection
            // (quiet‑zone handling is internal; there is no public API to ignore it)
            using (var reader = new BarCodeReader(memory, DecodeType.Mailmark))
            {
                reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;
                reader.QualitySettings.AllowIncorrectBarcodes = true;

                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                    Console.WriteLine($"Decoded Text : {result.CodeText}");
                }
            }
        }
    }
}