using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

class Program
{
    static void Main(string[] args)
    {
        // Sample image paths; replace with actual paths or pass via command‑line arguments.
        List<string> imagePaths = new List<string>
        {
            @"MailmarkSample1.png",
            @"MailmarkSample2.png",
            @"MailmarkSample3.png"
        };

        // If arguments are provided, use them as image paths.
        if (args.Length > 0)
        {
            imagePaths = new List<string>(args);
        }

        string logFile = "MailmarkDecodeLog.txt";
        // Ensure a fresh log for each run.
        if (File.Exists(logFile))
        {
            File.Delete(logFile);
        }

        foreach (string path in imagePaths)
        {
            if (!File.Exists(path))
            {
                LogFailure(logFile, path, "File does not exist.");
                continue;
            }

            try
            {
                // Use AllSupportedTypes as required.
                using (var reader = new BarCodeReader(path, DecodeType.AllSupportedTypes))
                {
                    BarCodeResult[] results = reader.ReadBarCodes();

                    if (results.Length == 0)
                    {
                        LogFailure(logFile, path, "No barcode detected.");
                        continue;
                    }

                    bool decodedAny = false;

                    foreach (var result in results)
                    {
                        // Attempt to decode as Mailmark using ComplexCodetextReader.
                        MailmarkCodetext mailmark = ComplexCodetextReader.TryDecodeMailmark(result.CodeText);
                        if (mailmark != null)
                        {
                            decodedAny = true;
                            Console.WriteLine($"SUCCESS: {path} -> Mailmark decoded. VersionID={mailmark.VersionID}, ItemID={mailmark.ItemID}");
                        }
                    }

                    if (!decodedAny)
                    {
                        LogFailure(logFile, path, "Barcode detected but Mailmark decoding returned null.");
                    }
                }
            }
            catch (BarCodeRecognitionException ex)
            {
                LogFailure(logFile, path, $"BarCodeRecognitionException: {ex.Message}");
            }
            catch (Exception ex)
            {
                LogFailure(logFile, path, $"Unexpected exception: {ex.GetType().Name}: {ex.Message}");
            }
        }

        Console.WriteLine("Processing completed. See log file for failures.");
    }

    static void LogFailure(string logFile, string imagePath, string message)
    {
        string entry = $"FAILURE: Image=\"{imagePath}\" | Reason=\"{message}\"{Environment.NewLine}";
        File.AppendAllText(logFile, entry);
        Console.WriteLine(entry.TrimEnd());
    }
}