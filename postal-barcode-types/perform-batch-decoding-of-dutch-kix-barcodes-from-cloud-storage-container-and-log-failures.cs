using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main(string[] args)
    {
        // Input folder – can be passed as first argument, otherwise use a default "Input" folder.
        string inputFolder = args.Length > 0 ? args[0] : "Input";

        // Ensure the input folder exists.
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
            // Seed a sample Dutch KIX barcode so the example can run end‑to‑end.
            string samplePath = Path.Combine(inputFolder, "SampleDutchKIX.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.DutchKIX, "1234567890"))
            {
                generator.Save(samplePath);
            }
        }

        // Prepare a log file for failures.
        string logPath = Path.Combine(inputFolder, "decode_log.txt");
        using (var logWriter = new StreamWriter(logPath, false))
        {
            // Process image files in the folder (common image extensions).
            string[] files = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string file in files)
            {
                string extension = Path.GetExtension(file).ToLowerInvariant();
                if (extension != ".png" && extension != ".jpg" && extension != ".jpeg" && extension != ".bmp" && extension != ".tif" && extension != ".tiff")
                {
                    continue; // Skip non‑image files.
                }

                try
                {
                    using (var reader = new BarCodeReader(file, DecodeType.DutchKIX))
                    {
                        // Perform the recognition.
                        reader.ReadBarCodes();

                        if (reader.FoundCount > 0)
                        {
                            for (int i = 0; i < reader.FoundCount; i++)
                            {
                                var result = reader.FoundBarCodes[i];
                                Console.WriteLine($"File: {Path.GetFileName(file)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                            }
                        }
                        else
                        {
                            // No barcode found – log as failure.
                            string message = $"File: {Path.GetFileName(file)} – No Dutch KIX barcode detected.";
                            Console.WriteLine(message);
                            logWriter.WriteLine(message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Unexpected error – log details.
                    string error = $"File: {Path.GetFileName(file)} – Exception: {ex.Message}";
                    Console.WriteLine(error);
                    logWriter.WriteLine(error);
                }
            }
        }

        Console.WriteLine("Batch decoding completed. See decode_log.txt for any failures.");
    }
}