using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Create a unique temporary folder for input files
        string inputFolder = Path.Combine(Path.GetTempPath(), "Gs1CompositeInput_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(inputFolder);

        // Create a few sample files
        List<string> createdFiles = new List<string>();
        for (int i = 1; i <= 3; i++)
        {
            string filePath = Path.Combine(inputFolder, $"SampleFile{i}.txt");
            File.WriteAllText(filePath, $"This is the content of file {i}.");
            createdFiles.Add(filePath);
        }

        // Create a unique temporary folder for output barcodes
        string outputFolder = Path.Combine(Path.GetTempPath(), "Gs1CompositeOutput_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Process each created file and generate a GS1 Composite barcode
        foreach (string filePath in createdFiles)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            // Linear component: a valid GTIN-14 with AI (01)
            string linearComponent = "(01)01234567890128";

            // Two‑dimensional component: use the file name (without path) as data
            string twoDComponent = $"(21){Path.GetFileNameWithoutExtension(filePath)}";

            // Combine components with pipe separator
            string codeText = $"{linearComponent}|{twoDComponent}";

            // Generate the barcode
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
            {
                // Set 1D component type (GS1 Code128)
                generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

                // Set 2D component type (PDF417 based)
                generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_C;

                // Configure PDF417 columns for the 2D component
                generator.Parameters.Barcode.Pdf417.Columns = 30;

                // Allow non‑GS1 data in the 2D component
                generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;

                // Save the barcode image
                string outputPath = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(filePath) + "_GS1Composite.png");
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Generated barcode for '{filePath}' -> '{outputPath}'");
            }
        }

        // Cleanup: (optional) delete temporary folders after use
        // Directory.Delete(inputFolder, true);
        // Directory.Delete(outputFolder, true);
    }
}