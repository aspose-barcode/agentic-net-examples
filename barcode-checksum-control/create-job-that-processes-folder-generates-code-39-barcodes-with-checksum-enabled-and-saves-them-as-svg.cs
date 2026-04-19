using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main(string[] args)
    {
        // Input and output folders (can be provided via command‑line arguments)
        string inputFolder = args.Length > 0 ? args[0] : "InputBarcodes";
        string outputFolder = args.Length > 1 ? args[1] : "OutputBarcodes";

        // Ensure input folder exists; create a sample file if it was missing
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
            File.WriteAllText(Path.Combine(inputFolder, "Sample1.txt"), "Sample content");
        }

        // Ensure output folder exists
        if (!Directory.Exists(outputFolder))
            Directory.CreateDirectory(outputFolder);

        // Process each file in the input folder
        string[] files = Directory.GetFiles(inputFolder);
        foreach (string filePath in files)
        {
            // Use the file name (without extension) as the barcode text
            string codeText = Path.GetFileNameWithoutExtension(filePath);
            string outputPath = Path.Combine(outputFolder, codeText + ".svg");

            // Generate Code 39 barcode with checksum enabled and save as SVG
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39, codeText))
            {
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
                generator.Save(outputPath, BarCodeImageFormat.Svg);
            }
        }

        Console.WriteLine($"Processed {files.Length} file(s). Barcodes saved to \"{outputFolder}\".");
    }
}