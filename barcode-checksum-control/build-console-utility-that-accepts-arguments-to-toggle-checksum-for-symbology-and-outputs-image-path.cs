using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main(string[] args)
    {
        // Resolve symbology name
        string symbologyName = args.Length > 0 ? args[0] : "Code128";
        var symProp = typeof(EncodeTypes).GetProperty(
            symbologyName,
            BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase);
        if (symProp == null)
        {
            Console.WriteLine($"Invalid symbology '{symbologyName}'.");
            return;
        }
        BaseEncodeType encodeType = (BaseEncodeType)symProp.GetValue(null);

        // Resolve code text
        string codeText = args.Length > 1 ? args[1] : "123456";

        // Resolve checksum flag
        string checksumArg = args.Length > 2 ? args[2] : "enable";
        Aspose.BarCode.Generation.EnableChecksum checksumSetting =
            string.Equals(checksumArg, "disable", StringComparison.OrdinalIgnoreCase)
                ? Aspose.BarCode.Generation.EnableChecksum.No
                : Aspose.BarCode.Generation.EnableChecksum.Yes;

        // Generate barcode
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            generator.Parameters.Barcode.IsChecksumEnabled = checksumSetting;

            string fileName = $"{symbologyName}_{DateTime.Now:yyyyMMddHHmmss}.png";
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            generator.Save(outputPath);
            Console.WriteLine($"Barcode saved to: {outputPath}");
        }
    }
}