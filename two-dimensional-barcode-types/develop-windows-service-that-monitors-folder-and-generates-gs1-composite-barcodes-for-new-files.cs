using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main(string[] args)
    {
        // Define input and output folders
        string inputFolder = "Input";
        string outputFolder = "Output";

        // Ensure folders exist
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Seed a sample file if the input folder is empty
        if (Directory.GetFiles(inputFolder).Length == 0)
        {
            string samplePath = Path.Combine(inputFolder, "Sample1.txt");
            File.WriteAllText(samplePath, "Sample content");
        }

        // Process each file in the input folder
        string[] files = Directory.GetFiles(inputFolder);
        foreach (string filePath in files)
        {
            // Use the file name (without extension) as part of the 2D component
            string fileName = Path.GetFileNameWithoutExtension(filePath);

            // Construct GS1 Composite codetext: linear part | 2D part
            string linearPart = "(01)03212345678906"; // Example GTIN
            string twoDPart = $"(21){fileName}";
            string codetext = $"{linearPart}|{twoDPart}";

            // Define output image path
            string outputFile = Path.Combine(outputFolder, $"{fileName}.png");

            // Generate the GS1 Composite barcode
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codetext))
            {
                // Set linear and 2D component types
                generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
                generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

                // Configure additional parameters
                generator.Parameters.Barcode.Pdf417.AspectRatio = 3f;
                generator.Parameters.Barcode.XDimension.Pixels = 3f;
                generator.Parameters.Barcode.BarHeight.Pixels = 100f;

                // Save the barcode image
                generator.Save(outputFile);
            }
        }
    }
}