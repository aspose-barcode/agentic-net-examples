using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Prepare test data
        string codetext = "(01)03212345678906|(21)A12345678";
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "GS1CompositeTest");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Linear component types to test
        BaseEncodeType[] linearTypes = new BaseEncodeType[]
        {
            EncodeTypes.GS1Code128,
            EncodeTypes.EAN13
        };

        foreach (BaseEncodeType linearType in linearTypes)
        {
            string fileName = $"GS1Composite_{linearType.GetType().Name}_{Guid.NewGuid()}.png";
            string filePath = Path.Combine(outputDir, fileName);

            // Generate GS1 Composite barcode with specified linear component type
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codetext))
            {
                generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = linearType;
                generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;
                generator.Parameters.Barcode.XDimension.Pixels = 3f;
                generator.Parameters.Barcode.BarHeight.Pixels = 100f;
                generator.Save(filePath);
            }

            // Recognize the generated barcode
            using (var reader = new BarCodeReader(filePath, DecodeType.GS1CompositeBar))
            {
                bool found = false;
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // Get the recognized linear component type
                    var extended = result.Extended;
                    if (extended?.GS1CompositeBar != null)
                    {
                        var recognizedLinearType = extended.GS1CompositeBar.OneDType;
                        // Map expected encode type to corresponding decode type
                        SingleDecodeType expectedDecodeType = MapEncodeToDecode(linearType);
                        if (recognizedLinearType == expectedDecodeType)
                        {
                            Console.WriteLine($"PASS: Linear type {linearType} correctly recognized as {recognizedLinearType}.");
                        }
                        else
                        {
                            Console.WriteLine($"FAIL: Linear type {linearType} recognized as {recognizedLinearType}, expected {expectedDecodeType}.");
                        }
                        found = true;
                    }
                }
                if (!found)
                {
                    Console.WriteLine($"FAIL: No GS1 Composite barcode detected in {filePath}.");
                }
            }
        }
    }

    // Helper to map EncodeTypes to corresponding SingleDecodeType
    private static SingleDecodeType MapEncodeToDecode(BaseEncodeType encodeType)
    {
        if (encodeType == EncodeTypes.GS1Code128)
            return DecodeType.GS1Code128;
        if (encodeType == EncodeTypes.EAN13)
            return DecodeType.EAN13;
        // Add more mappings if needed
        throw new ArgumentException($"Unsupported encode type: {encodeType}");
    }
}