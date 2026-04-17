using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Input CSV file path
        string inputCsv = "input.csv";
        // Output directory for generated barcode images
        string outputDir = "Barcodes";

        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Seed a sample CSV file if it does not exist
        if (!File.Exists(inputCsv))
        {
            string[] sampleLines = new[]
            {
                "(01)03212345678906|(21)A1B2C3D4E5F6G7H8",
                "(01)12345678901231|(21)XYZ1234567890",
                "(01)98765432109876|(21)TEST123456"
            };
            File.WriteAllLines(inputCsv, sampleLines);
        }

        // Read all lines from the CSV file
        string[] lines = File.ReadAllLines(inputCsv);
        int index = 1;

        foreach (string rawLine in lines)
        {
            string codetext = rawLine.Trim();
            if (string.IsNullOrEmpty(codetext))
                continue;

            // Create a barcode generator for GS1 Composite Bar with the provided codetext
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codetext))
            {
                // Linear component type (GS1 Code128)
                generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
                // 2D component type (CC-A)
                generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

                // X-Dimension for both components
                generator.Parameters.Barcode.XDimension.Pixels = 3f;
                // Height of the linear component
                generator.Parameters.Barcode.BarHeight.Pixels = 100f;
                // Aspect ratio for the PDF417 part (used by CC-A)
                generator.Parameters.Barcode.Pdf417.AspectRatio = 3f;

                // Save the barcode image as PNG
                string outputPath = Path.Combine(outputDir, $"barcode_{index}.png");
                generator.Save(outputPath);
            }

            index++;
        }

        Console.WriteLine($"Generated {index - 1} barcode image(s) in '{outputDir}'.");
    }
}