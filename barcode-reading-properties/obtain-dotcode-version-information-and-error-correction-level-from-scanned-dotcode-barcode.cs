using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path for the temporary barcode image
        string imagePath = Path.Combine(Path.GetTempPath(), "dotcode.png");

        // Create a DotCode barcode with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, "SampleData"))
        {
            // Save the barcode image
            generator.Save(imagePath);
        }

        // Read the barcode from the saved image
        using (var reader = new BarCodeReader(imagePath, DecodeType.DotCode))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("BarCode type: " + result.CodeTypeName);
                Console.WriteLine("BarCode codetext: " + result.CodeText);

                // Access extended DotCode parameters
                var dotCodeExt = result.Extended?.DotCode;
                if (dotCodeExt != null)
                {
                    // Try to obtain version information via reflection
                    var versionProp = dotCodeExt.GetType().GetProperty("Version");
                    if (versionProp != null)
                    {
                        var versionValue = versionProp.GetValue(dotCodeExt);
                        Console.WriteLine("DotCode version: " + versionValue);
                    }

                    // Try to obtain error correction level via reflection
                    var ecLevelProp = dotCodeExt.GetType().GetProperty("ErrorCorrectionLevel");
                    if (ecLevelProp != null)
                    {
                        var ecLevelValue = ecLevelProp.GetValue(dotCodeExt);
                        Console.WriteLine("Error correction level: " + ecLevelValue);
                    }

                    // Also display structured append information if present
                    Console.WriteLine("Structured Append Barcode ID: " + dotCodeExt.StructuredAppendModeBarcodeId);
                    Console.WriteLine("Structured Append Barcodes Count: " + dotCodeExt.StructuredAppendModeBarcodesCount);
                }
            }
        }

        // Clean up the temporary image file
        if (File.Exists(imagePath))
        {
            File.Delete(imagePath);
        }
    }
}