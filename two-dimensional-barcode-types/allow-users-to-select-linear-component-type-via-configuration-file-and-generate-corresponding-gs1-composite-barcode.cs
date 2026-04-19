using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Path to the simple configuration file.
        const string configPath = "config.txt";

        // Default linear component type if configuration is missing or invalid.
        BaseEncodeType linearComponent = EncodeTypes.GS1Code128;

        // Attempt to read the linear component type from the configuration file.
        if (File.Exists(configPath))
        {
            try
            {
                string configValue = File.ReadAllText(configPath).Trim();

                if (!string.IsNullOrEmpty(configValue))
                {
                    // Resolve the string to a static field of EncodeTypes using reflection.
                    FieldInfo field = typeof(EncodeTypes).GetField(configValue, BindingFlags.Public | BindingFlags.Static);
                    if (field == null)
                        throw new ArgumentException($"Linear component type '{configValue}' is not a valid EncodeTypes field.");

                    object fieldValue = field.GetValue(null);
                    if (fieldValue is BaseEncodeType encodeType)
                        linearComponent = encodeType;
                    else
                        throw new ArgumentException($"Field '{configValue}' does not represent a BaseEncodeType.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Configuration error: {ex.Message}");
                // Continue with the default linear component.
            }
        }

        // Sample GS1 Composite codetext: linear part | 2D part.
        const string codeText = "(01)03212345678906|(21)A1B2C3D4E5F6G7H8";

        // Create the barcode generator for GS1 Composite Bar.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Apply the linear component type selected via configuration.
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = linearComponent;

            // Choose a 2D component type (CC_A is a common choice).
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Optional visual settings.
            generator.Parameters.Barcode.XDimension.Pixels = 3f;
            generator.Parameters.Barcode.BarHeight.Pixels = 100f;

            // Save the generated barcode image.
            const string outputPath = "gs1composite.png";
            generator.Save(outputPath);
        }
    }
}