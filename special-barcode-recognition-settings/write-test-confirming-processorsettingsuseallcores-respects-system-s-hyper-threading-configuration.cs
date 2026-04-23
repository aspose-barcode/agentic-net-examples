using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Prepare a temporary barcode image
        string tempPath = Path.Combine(Path.GetTempPath(), "testbarcode.png");
        if (!File.Exists(tempPath))
        {
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
            {
                generator.Save(tempPath);
            }
        }

        // Test with UseAllCores = true (should utilize all logical processors)
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        Console.WriteLine($"ProcessorSettings.UseAllCores set to: {BarCodeReader.ProcessorSettings.UseAllCores}");
        Console.WriteLine($"Logical processors (including hyper‑threading): {Environment.ProcessorCount}");

        using (BarCodeReader readerAll = new BarCodeReader(tempPath, DecodeType.Code128))
        {
            foreach (BarCodeResult result in readerAll.ReadBarCodes())
            {
                Console.WriteLine($"[AllCores] Detected: {result.CodeText}");
            }
        }

        // Test with UseAllCores = false and limit to half of the logical processors
        BarCodeReader.ProcessorSettings.UseAllCores = false;
        int limitedCores = Math.Max(1, Environment.ProcessorCount / 2);
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = limitedCores;
        Console.WriteLine($"ProcessorSettings.UseAllCores set to: {BarCodeReader.ProcessorSettings.UseAllCores}");
        Console.WriteLine($"ProcessorSettings.UseOnlyThisCoresCount set to: {BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount}");

        using (BarCodeReader readerLimited = new BarCodeReader(tempPath, DecodeType.Code128))
        {
            foreach (BarCodeResult result in readerLimited.ReadBarCodes())
            {
                Console.WriteLine($"[LimitedCores] Detected: {result.CodeText}");
            }
        }

        // Cleanup temporary file (optional)
        try
        {
            File.Delete(tempPath);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}