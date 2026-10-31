using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        string tempDir = Path.Combine(Path.GetTempPath(), "GS1CompositeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        TestLinearComponent("EAN13", tempDir);
        TestLinearComponent("UPCA", tempDir);
    }

    static void TestLinearComponent(string linearTypeName, string outputDir)
    {
        // Resolve EncodeTypes.<linearTypeName> via reflection
        FieldInfo field = typeof(EncodeTypes).GetField(linearTypeName);
        if (field == null)
        {
            Console.WriteLine($"EncodeTypes does not contain member '{linearTypeName}'.");
            return;
        }

        BaseEncodeType linearEncode = (BaseEncodeType)field.GetValue(null);

        // Prepare GS1 Composite code text
        // Linear part uses a valid GTIN-14 (01234567890128) and the 2D part is a simple AI
        string linearPart = "(01)01234567890128";
        string twoDPart = "(10)ABCD0123";
        string codeText = $"{linearPart}|{twoDPart}";

        string imagePath = Path.Combine(outputDir, $"GS1Composite_{linearTypeName}.png");

        // Generate barcode
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2;
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = linearEncode;
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Failed to create barcode image at '{imagePath}'.");
            return;
        }

        // Read barcode and verify linear component type
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.GS1CompositeBar))
        {
            BarCodeResult[] results = reader.ReadBarCodes();
            if (results.Length == 0)
            {
                Console.WriteLine($"No barcode detected in '{imagePath}'.");
                return;
            }

            foreach (BarCodeResult result in results)
            {
                BaseEncodeType detectedLinearType = result.Extended.GS1CompositeBar.OneDType;
                bool match = detectedLinearType.Equals(linearEncode);
                Console.WriteLine($"Test for linear type '{linearTypeName}': {(match ? "PASS" : "FAIL")}");
                Console.WriteLine($"  Expected: {linearEncode}");
                Console.WriteLine($"  Detected: {detectedLinearType}");
            }
        }
    }
}