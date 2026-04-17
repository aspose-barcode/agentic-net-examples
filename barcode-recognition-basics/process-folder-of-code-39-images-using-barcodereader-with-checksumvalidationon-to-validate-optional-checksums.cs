using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Define the folder that will contain Code 39 images
        string inputFolder = Path.Combine(Environment.CurrentDirectory, "Code39Images");

        // Ensure the folder exists
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }

        // If the folder is empty, generate a few sample Code 39 barcodes
        string[] existingFiles = Directory.GetFiles(inputFolder);
        if (existingFiles.Length == 0)
        {
            for (int i = 1; i <= 3; i++)
            {
                string codeText = $"CODE39{i}";
                string filePath = Path.Combine(inputFolder, $"Sample{i}.png");

                using (var generator = new BarcodeGenerator(EncodeTypes.Code39, codeText))
                {
                    // Enable checksum generation (optional for Code 39)
                    generator.Parameters.Barcode.IsChecksumEnabled = Aspose.BarCode.Generation.EnableChecksum.Yes;
                    generator.Save(filePath);
                }
            }
        }

        // Process each image file in the folder
        string[] imageFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string imagePath in imageFiles)
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                continue;
            }

            using (var reader = new BarCodeReader(imagePath, DecodeType.Code39))
            {
                // Enable checksum validation for optional checksums
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                bool anyFound = false;
                foreach (var result in reader.ReadBarCodes())
                {
                    anyFound = true;
                    Console.WriteLine($"File: {Path.GetFileName(imagePath)}");
                    Console.WriteLine($"  Type      : {result.CodeTypeName}");
                    Console.WriteLine($"  CodeText  : {result.CodeText}");
                    // For 1D barcodes, Extended.OneD provides value and checksum
                    Console.WriteLine($"  Value     : {result.Extended.OneD.Value}");
                    Console.WriteLine($"  Checksum  : {result.Extended.OneD.CheckSum}");
                }

                if (!anyFound)
                {
                    Console.WriteLine($"File: {Path.GetFileName(imagePath)} - No Code 39 barcode detected.");
                }
            }
        }
    }
}