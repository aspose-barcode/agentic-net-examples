using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        string folderPath = "Barcodes";
        string logFilePath = "OneCodeDecodeLog.txt";

        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine($"Folder '{folderPath}' does not exist.");
            return;
        }

        using (var logWriter = new StreamWriter(logFilePath, false))
        {
            logWriter.WriteLine($"OneCode barcode decoding log - {DateTime.UtcNow:u}");

            int processed = 0;
            foreach (string filePath in Directory.GetFiles(folderPath))
            {
                if (processed >= 10)
                    break;

                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = new BarCodeReader(fileStream, DecodeType.OneCode))
                    {
                        bool anyFound = false;
                        foreach (var result in reader.ReadBarCodes())
                        {
                            anyFound = true;
                            logWriter.WriteLine($"File: {Path.GetFileName(filePath)}, Type: {result.CodeTypeName}, CodeText: {result.CodeText}");
                        }

                        if (!anyFound)
                        {
                            logWriter.WriteLine($"File: {Path.GetFileName(filePath)} - No OneCode barcode found.");
                        }
                    }
                }

                processed++;
            }
        }

        Console.WriteLine($"Decoding completed. Log written to {logFilePath}");
    }
}