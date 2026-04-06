using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Folder that contains Code 39 barcode images
        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Code39Images");

        // Ensure the folder exists
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // If the folder is empty, create a sample barcode image
        if (Directory.GetFiles(folderPath).Length == 0)
        {
            string samplePath = Path.Combine(folderPath, "sample_code39.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "CODE39*"))
            {
                // Enable checksum generation for Code 39
                generator.Parameters.Barcode.IsChecksumEnabled = Aspose.BarCode.Generation.EnableChecksum.Yes;
                generator.Parameters.Barcode.ChecksumAlwaysShow = true;
                generator.Save(samplePath);
                Console.WriteLine($"Sample barcode created: {samplePath}");
            }
        }

        // Process each image file in the folder
        string[] imageFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string file in imageFiles)
        {
            // Only process common image extensions
            string ext = Path.GetExtension(file).ToLowerInvariant();
            if (ext != ".png" && ext != ".jpg" && ext != ".jpeg" && ext != ".bmp")
                continue;

            using (var reader = new BarCodeReader(file, DecodeType.Code39))
            {
                // Enable checksum validation during recognition
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                bool anyFound = false;
                foreach (var result in reader.ReadBarCodes())
                {
                    anyFound = true;
                    Console.WriteLine($"File: {Path.GetFileName(file)}");
                    Console.WriteLine($"  Type: {result.CodeTypeName}");
                    Console.WriteLine($"  CodeText: {result.CodeText}");
                    // For 1D barcodes, show value without checksum and the checksum itself
                    if (result.Extended?.OneD != null)
                    {
                        Console.WriteLine($"  Value (without checksum): {result.Extended.OneD.Value}");
                        Console.WriteLine($"  Checksum: {result.Extended.OneD.CheckSum}");
                    }
                }

                if (!anyFound)
                {
                    Console.WriteLine($"File: {Path.GetFileName(file)} - No valid Code 39 barcode detected or checksum validation failed.");
                }
            }
        }
    }
}