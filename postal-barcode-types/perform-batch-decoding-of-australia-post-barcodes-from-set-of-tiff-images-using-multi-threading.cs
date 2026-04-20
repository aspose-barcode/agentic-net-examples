using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Define folder for sample TIFF images
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "InputTiff");
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }

        // Create a few sample Australia Post barcodes if the folder is empty
        string[] existingFiles = Directory.GetFiles(inputFolder, "*.tif");
        if (existingFiles.Length == 0)
        {
            for (int i = 1; i <= 5; i++)
            {
                string codeText = $"5912345678ABC{i}";
                string filePath = Path.Combine(inputFolder, $"Sample{i}.tif");
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
                {
                    // Set interpreting type to CTable
                    generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;
                    // Save as TIFF
                    generator.Save(filePath, BarCodeImageFormat.Tiff);
                }
            }
        }

        // Get all TIFF files in the folder
        string[] tiffFiles = Directory.GetFiles(inputFolder, "*.tif");
        if (tiffFiles.Length == 0)
        {
            Console.WriteLine("No TIFF files found to decode.");
            return;
        }

        // Use a lock for thread‑safe console output
        object consoleLock = new object();

        // Decode images in parallel
        Parallel.ForEach(tiffFiles, filePath =>
        {
            try
            {
                using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.AustraliaPost))
                {
                    // Configure decoding settings
                    reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;
                    reader.BarcodeSettings.AustraliaPost.IgnoreEndingFillingPatternsForCTable = true;

                    BarCodeResult[] results = reader.ReadBarCodes();
                    lock (consoleLock)
                    {
                        Console.WriteLine($"File: {Path.GetFileName(filePath)}");
                        if (results.Length == 0)
                        {
                            Console.WriteLine("  No barcode detected.");
                        }
                        else
                        {
                            foreach (BarCodeResult result in results)
                            {
                                Console.WriteLine($"  Type: {result.CodeTypeName}");
                                Console.WriteLine($"  CodeText: {result.CodeText}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lock (consoleLock)
                {
                    Console.WriteLine($"Error processing file '{Path.GetFileName(filePath)}': {ex.Message}");
                }
            }
        });
    }
}