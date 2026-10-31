using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Create a unique temporary folder for the batch
        string batchFolder = Path.Combine(Path.GetTempPath(), "BatchQR_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Prepare sample texts
        List<string> texts = new List<string>();
        for (int i = 0; i < 5; i++)
        {
            texts.Add($"Sample QR {i}");
        }

        // Generate QR code images
        List<string> generatedFiles = new List<string>();
        for (int i = 0; i < texts.Count; i++)
        {
            string filePath = Path.Combine(batchFolder, $"qr_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, texts[i]))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            generatedFiles.Add(filePath);
        }

        Console.WriteLine("Generated QR code images:");
        foreach (var f in generatedFiles)
        {
            Console.WriteLine(f);
        }

        // Set up a cancellation token that cancels after 2 seconds
        using (var cts = new CancellationTokenSource(2000))
        {
            CancellationToken token = cts.Token;

            Console.WriteLine("\nStarting batch decoding with cancellation support...");

            for (int i = 0; i < generatedFiles.Count; i++)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Operation cancelled by token.");
                    break;
                }

                string file = generatedFiles[i];
                try
                {
                    using (var reader = new BarCodeReader(file, DecodeType.QR))
                    {
                        reader.ReadBarCodes();
                        Console.WriteLine($"\nFile: {Path.GetFileName(file)} - Found {reader.FoundCount} barcode(s)");
                        foreach (BarCodeResult result in reader.FoundBarCodes)
                        {
                            Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
                        }
                    }
                }
                catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
                {
                    Console.WriteLine($"Skipping unreadable file '{file}': {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{file}': {ex.Message}");
                }
            }
        }

        // Clean up temporary files and folder
        try
        {
            foreach (var file in generatedFiles)
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            }
            if (Directory.Exists(batchFolder))
            {
                Directory.Delete(batchFolder, true);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup error: {ex.Message}");
        }

        Console.WriteLine("\nBatch operation completed.");
    }
}