// Title: Generate GS1 Composite barcode with configurable linear component
// Description: Demonstrates reading a linear component type from a configuration file and creating a GS1 Composite barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on GS1 Composite symbology. It shows how to use EncodeTypes, BarcodeGenerator, and GS1CompositeBar parameters to combine linear and 2‑D components. Typical use cases include retail labeling and logistics where a linear barcode (e.g., EAN‑13, UPC‑A) is paired with a QR‑like component. Developers often need to switch linear types at runtime, read settings from files, and customize visual properties.
// Prompt: Allow users to select linear component type via configuration file and generate corresponding GS1 Composite barcode.
// Tags: barcode, gs1 composite, configuration, c#, aspose.barcode, png, encode types, linear component

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a GS1 Composite barcode where the linear component type is read from a configuration file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Reads configuration, selects the appropriate linear component, builds the composite code text, and saves the barcode image.
    /// </summary>
    static void Main()
    {
        // Path to the configuration file that may contain the linear component selection.
        string configPath = "config.txt";

        // Default linear component type if the configuration file is missing or does not specify a value.
        string linearComponentName = "GS1Code128";

        // Attempt to read the configuration file and extract the LinearComponent value.
        if (File.Exists(configPath))
        {
            foreach (string line in File.ReadAllLines(configPath))
            {
                if (line.StartsWith("LinearComponent=", StringComparison.OrdinalIgnoreCase))
                {
                    linearComponentName = line.Substring("LinearComponent=".Length).Trim();
                    break;
                }
            }
        }
        else
        {
            Console.WriteLine($"Configuration file not found. Using default linear component: {linearComponentName}");
        }

        // Resolve the EncodeTypes member that matches the name read from the configuration.
        var field = typeof(EncodeTypes).GetField(linearComponentName);
        if (field == null)
        {
            Console.WriteLine($"Unknown linear component type '{linearComponentName}'. Falling back to GS1Code128.");
            field = typeof(EncodeTypes).GetField("GS1Code128");
        }
        BaseEncodeType linearEncodeType = (BaseEncodeType)field.GetValue(null);

        // Prepare a sample linear part based on the selected component type.
        string linearPart;
        if (linearEncodeType == EncodeTypes.EAN8)
            linearPart = "20123451";
        else if (linearEncodeType == EncodeTypes.UPCA)
            linearPart = "001234567895";
        else if (linearEncodeType == EncodeTypes.EAN13)
            linearPart = "2001234567893";
        else if (linearEncodeType == EncodeTypes.UPCE)
            linearPart = "04252614";
        else if (linearEncodeType == EncodeTypes.DatabarOmniDirectional ||
                 linearEncodeType == EncodeTypes.DatabarStackedOmniDirectional ||
                 linearEncodeType == EncodeTypes.DatabarStacked)
            linearPart = "(01)24012345678905";
        else // GS1Code128 and other types
            linearPart = "(01)01234567890128";

        // Sample 2D part (non‑GS1 encoding) to be combined with the linear part.
        string twoDPart = "(10)ABCD0123(240)0123456789";

        // Combine linear and 2D parts using the pipe separator required for GS1 Composite.
        string codeText = $"{linearPart}|{twoDPart}";

        // Determine the output file path for the generated PNG image.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "GS1Composite.png");

        // Create and configure the barcode generator for GS1 Composite symbology.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Set the linear component type based on the configuration.
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = linearEncodeType;

            // Set the 2D component type (defaulting to CC_A).
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Optional visual settings.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Allow non‑GS1 data in the 2D component.
            generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;

            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"GS1 Composite barcode generated: {outputPath}");
    }
}