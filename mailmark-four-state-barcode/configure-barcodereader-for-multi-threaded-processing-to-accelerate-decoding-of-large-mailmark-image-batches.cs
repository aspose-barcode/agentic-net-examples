using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Folder to store sample Mailmark images
        string folder = Path.Combine(Directory.GetCurrentDirectory(), "MailmarkSamples");
        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        // Generate a few sample Mailmark barcode images
        for (int i = 1; i <= 5; i++)
        {
            string filePath = Path.Combine(folder, $"mailmark_{i}.png");
            if (!File.Exists(filePath))
            {
                var mailmark = new MailmarkCodetext
                {
                    Format = 4,                     // 4‑state format
                    VersionID = 1,
                    Class = "0",
                    SupplychainID = 384224,
                    ItemID = 16563762,
                    DestinationPostCodePlusDPS = "EF61AH8T " // trailing space required
                };

                using (var generator = new ComplexBarcodeGenerator(mailmark))
                {
                    using (var ms = new MemoryStream())
                    {
                        generator.Save(ms, BarCodeImageFormat.Png);
                        ms.Position = 0;
                        using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                        {
                            ms.CopyTo(fileStream);
                        }
                    }
                }
            }
        }

        // Configure BarCodeReader to use all available processor cores
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;

        // Process the generated images
        string[] files = Directory.GetFiles(folder, "*.png");
        foreach (string file in files)
        {
            using (var reader = new BarCodeReader(file, DecodeType.Mailmark))
            {
                // Optional quality tweaks for faster multi‑core processing
                reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;
                reader.QualitySettings.AllowIncorrectBarcodes = true;

                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(file)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                }
            }
        }
    }
}