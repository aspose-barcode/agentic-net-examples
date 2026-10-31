using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Configuration file path (same directory as executable)
        string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.txt");
        string linearComponentName = "GS1Code128"; // default

        if (File.Exists(configPath))
        {
            try
            {
                string line = File.ReadAllText(configPath).Trim();
                if (!string.IsNullOrEmpty(line))
                {
                    // Expect format: LinearComponent=ComponentName
                    var parts = line.Split('=', StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 2 && parts[0].Trim().Equals("LinearComponent", StringComparison.OrdinalIgnoreCase))
                    {
                        linearComponentName = parts[1].Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading config: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Config file not found. Using default linear component.");
        }

        // Resolve EncodeTypes member via reflection
        FieldInfo field = typeof(EncodeTypes).GetField(linearComponentName);
        if (field == null)
        {
            Console.WriteLine($"Unknown linear component: {linearComponentName}. Falling back to GS1Code128.");
            field = typeof(EncodeTypes).GetField("GS1Code128");
        }
        BaseEncodeType linearEncodeType = (BaseEncodeType)field.GetValue(null);

        // Determine sample linear code text based on component
        string linearCodeText = linearComponentName switch
        {
            "EAN8" => "20123451",
            "UPCA" => "001234567895",
            "EAN13" => "2001234567893",
            "UPCE" => "04252614",
            "GS1Code128" => "(01)01234567890128",
            "DatabarOmniDirectional" => "(01)24012345678905",
            "DatabarStackedOmniDirectional" => "(01)24012345678905",
            "DatabarStacked" => "(01)24012345678905",
            "DatabarLimited" => "(01)24012345678905",
            "DatabarExpanded" => "(01)24012345678905",
            "DatabarExpandedStacked" => "(01)24012345678905",
            _ => "(01)01234567890128"
        };

        // 2D component sample (non‑GS1 data)
        string twoDComponent = "(10)ABCD0123(240)0123456789";

        string fullCodeText = $"{linearCodeText}|{twoDComponent}";

        // Output image path
        string outputPath = Path.Combine(Path.GetTempPath(), "GS1Composite.png");

        // Generate barcode
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, fullCodeText))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = linearEncodeType;
            generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;

            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"GS1 Composite barcode saved to: {outputPath}");

        // Read back barcode
        BaseDecodeType decodeType = DecodeType.GS1CompositeBar;
        using (BarCodeReader reader = new BarCodeReader(outputPath, decodeType))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Linear Component Type: {result.Extended.GS1CompositeBar.OneDType}");
                Console.WriteLine($"Linear Component Text: {result.Extended.GS1CompositeBar.OneDCodeText}");
                Console.WriteLine($"2D Component Type: {result.Extended.GS1CompositeBar.TwoDType}");
                Console.WriteLine($"2D Component Text: {result.Extended.GS1CompositeBar.TwoDCodeText}");
            }
        }
    }
}