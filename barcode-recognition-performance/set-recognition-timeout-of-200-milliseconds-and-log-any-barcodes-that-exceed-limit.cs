// Title: Barcode Recognition with Timeout and Logging
// Description: Demonstrates generating a Code128 barcode, reading it with a 200 ms timeout, and logging results or timeout events.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them, highlighting typical scenarios such as setting a recognition timeout, handling timeouts, and logging outcomes. Developers working with barcode scanning, automated data capture, or performance‑critical applications often need these patterns.
// Prompt: Set a recognition timeout of 200 milliseconds and log any barcodes that exceed the limit.
// Tags: barcode, code128, generation, recognition, timeout, logging, aspose.barcode, .net

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode, attempts to read it with a
/// 200 ms timeout, and logs the results or any timeout occurrences.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for output files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeTimeoutDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "barcode.png");

        // Generate a simple Code128 barcode and save it as PNG
        BaseEncodeType encodeType = EncodeTypes.Code128;
        using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, "1234567890"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Prepare a log file and write the start timestamp
        string logPath = Path.Combine(tempFolder, "log.txt");
        File.WriteAllText(logPath, $"Barcode generation completed at {DateTime.Now}{Environment.NewLine}");

        // Read the barcode with a timeout of 200 ms
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            reader.Timeout = 200; // milliseconds

            try
            {
                // Attempt to read barcodes within the timeout period
                BarCodeResult[] results = reader.ReadBarCodes();

                // Log each detected barcode
                foreach (BarCodeResult result in results)
                {
                    string message = $"Found barcode: Type={result.CodeTypeName}, Text={result.CodeText}";
                    Console.WriteLine(message);
                    File.AppendAllText(logPath, message + Environment.NewLine);
                }

                // If no barcodes were found, log that information
                if (results.Length == 0)
                {
                    string noResultMsg = "No barcodes were detected within the timeout.";
                    Console.WriteLine(noResultMsg);
                    File.AppendAllText(logPath, noResultMsg + Environment.NewLine);
                }
            }
            catch (RecognitionAbortedException ex)
            {
                // Handle timeout scenario and log execution time
                string timeoutMsg = $"Recognition timed out after {ex.ExecutionTime} ms.";
                Console.WriteLine(timeoutMsg);
                File.AppendAllText(logPath, timeoutMsg + Environment.NewLine);
            }
            catch (Exception ex)
            {
                // Log any unexpected errors
                string errorMsg = $"Unexpected error: {ex.Message}";
                Console.WriteLine(errorMsg);
                File.AppendAllText(logPath, errorMsg + Environment.NewLine);
            }
        }

        // Inform the user where the log file is located
        Console.WriteLine($"Log written to: {logPath}");
    }
}