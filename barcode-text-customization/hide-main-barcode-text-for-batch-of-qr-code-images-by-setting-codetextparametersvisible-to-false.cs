using System;
using System.IO;
using Aspose.BarCode.Generation;
class Program
{
    static void Main()
    {
        // Define folders for input texts and output barcode images
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "InputTexts");
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "OutputBarcodes");

        // Ensure input folder exists; create sample files if empty
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
            File.WriteAllText(Path.Combine(inputFolder, "Sample1.txt"), "Hello World");
            File.WriteAllText(Path.Combine(inputFolder, "Sample2.txt"), "Aspose.BarCode");
        }

        // Ensure output folder exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Process each .txt file in the input folder
        string[] txtFiles = Directory.GetFiles(inputFolder, "*.txt");
        foreach (string txtFile in txtFiles)
        {
            // Use file name (without extension) as the barcode file name
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(txtFile);
            string outputPath = Path.Combine(outputFolder, fileNameWithoutExt + ".png");

            // Read the text to encode
            string codeText = File.ReadAllText(txtFile);

            // Create QR code generator, hide the human‑readable text
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                // Hide main barcode text for 2D barcodes
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

                // Optionally clear any alternative display text
                generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = string.Empty;

                // Save the barcode image
                generator.Save(outputPath);
            }
        }

        Console.WriteLine("QR code images generated with hidden text in: " + outputFolder);
    }
}