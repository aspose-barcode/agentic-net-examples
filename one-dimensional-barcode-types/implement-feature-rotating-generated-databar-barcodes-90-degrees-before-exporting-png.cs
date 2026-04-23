using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Output folder
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "DataBarOutputs");
        Directory.CreateDirectory(outputDir);

        // DataBar symbologies to generate
        BaseEncodeType[] dataBarTypes = new BaseEncodeType[]
        {
            EncodeTypes.DatabarOmniDirectional,
            EncodeTypes.DatabarStacked,
            EncodeTypes.DatabarStackedOmniDirectional,
            EncodeTypes.DatabarLimited,
            EncodeTypes.DatabarExpanded,
            EncodeTypes.DatabarExpandedStacked
        };

        foreach (BaseEncodeType type in dataBarTypes)
        {
            // Choose a valid code text for each DataBar type
            string codeText = type == EncodeTypes.DatabarLimited
                ? "(01)08888888888888"
                : "(01)12345678901231";

            // Create generator, set rotation, and save PNG
            using (var generator = new BarcodeGenerator(type, codeText))
            {
                // Rotate the barcode image 90 degrees
                generator.Parameters.RotationAngle = 90f;

                // Optional: set image size if needed
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 150f;

                // Save as PNG
                string filePath = Path.Combine(outputDir, $"{type.TypeName}_Rotated.png");
                generator.Save(filePath);
                Console.WriteLine($"Saved rotated DataBar barcode: {filePath}");
            }
        }
    }
}