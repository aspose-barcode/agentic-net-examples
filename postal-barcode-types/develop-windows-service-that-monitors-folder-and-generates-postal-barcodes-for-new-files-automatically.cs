using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Define input and output folders
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Input");
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Ensure folders exist
        if (!Directory.Exists(inputFolder))
            Directory.CreateDirectory(inputFolder);
        if (!Directory.Exists(outputFolder))
            Directory.CreateDirectory(outputFolder);

        // Seed a sample file if the input folder is empty
        string[] existingFiles = Directory.GetFiles(inputFolder);
        if (existingFiles.Length == 0)
        {
            string samplePath = Path.Combine(inputFolder, "SampleZip.txt");
            File.WriteAllText(samplePath, "12345"); // Example ZIP code
        }

        // Process each text file in the input folder
        foreach (string filePath in Directory.GetFiles(inputFolder, "*.txt"))
        {
            try
            {
                // Read the ZIP code (or any postal code) from the file
                string codeText = File.ReadAllText(filePath).Trim();
                if (string.IsNullOrEmpty(codeText))
                {
                    Console.WriteLine($"File '{Path.GetFileName(filePath)}' is empty. Skipping.");
                    continue;
                }

                // Create a barcode generator for Postnet (postal barcode)
                using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, codeText))
                {
                    // Optional: customize appearance
                    generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                    generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                    // Set image size (optional)
                    generator.Parameters.ImageWidth.Point = 300f;
                    generator.Parameters.ImageHeight.Point = 150f;

                    // Save the barcode image to the output folder
                    string outputFileName = Path.GetFileNameWithoutExtension(filePath) + ".png";
                    string outputPath = Path.Combine(outputFolder, outputFileName);
                    generator.Save(outputPath);
                    Console.WriteLine($"Generated barcode for '{Path.GetFileName(filePath)}' -> '{outputFileName}'.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing file '{Path.GetFileName(filePath)}': {ex.Message}");
            }
        }

        Console.WriteLine("Processing completed.");
    }
}