// Title: Demonstrate error logging for unsupported barcode image format
// Description: Shows how to handle and log errors when SetBarCodeImage is called with a file that is not a supported image format.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, illustrating the use of BarCodeReader and its SetBarCodeImage method. It demonstrates typical error‑handling patterns developers need when working with barcode image inputs, such as validating file existence, catching exceptions, and writing diagnostic information to a log file. Useful for scenarios where input validation and robust logging are required.
// Prompt: Implement error logging for cases where SetBarCodeImage receives an unsupported image format argument.
// Tags: barcode, error-handling, setbarcodeimage, log, aspose.barcode, barcodereader

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that attempts to set a barcode image using an unsupported file format
/// and logs any errors encountered during the operation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Creates a temporary environment, attempts to load an invalid image,
    /// and records success or failure details to a log file.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "SetBarCodeImageDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Create a dummy file with an unsupported image format (e.g., .txt)
        string dummyFilePath = Path.Combine(tempFolder, "dummy.txt");
        File.WriteAllText(dummyFilePath, "This is not an image.");

        // Define the path for the error log file
        string logPath = Path.Combine(tempFolder, "error.log");

        // Use BarCodeReader to attempt loading the unsupported file
        using (BarCodeReader reader = new BarCodeReader())
        {
            try
            {
                // Verify that the dummy file exists before calling SetBarCodeImage
                if (!File.Exists(dummyFilePath))
                {
                    string msg = $"File not found: {dummyFilePath}";
                    Console.WriteLine(msg);
                    File.AppendAllText(logPath, msg + Environment.NewLine);
                }
                else
                {
                    // This call is expected to fail because the file is not a supported image
                    reader.SetBarCodeImage(dummyFilePath);
                    Console.WriteLine("SetBarCodeImage succeeded unexpectedly.");
                }
            }
            catch (Exception ex)
            {
                // Capture and log any exception thrown by SetBarCodeImage
                string errorMessage = $"Error setting barcode image: {ex.Message}";
                Console.WriteLine(errorMessage);
                File.AppendAllText(logPath, errorMessage + Environment.NewLine);
            }
        }

        // Inform the user that the demo has completed and provide the log location
        Console.WriteLine($"Demo completed. Log file: {logPath}");
    }
}