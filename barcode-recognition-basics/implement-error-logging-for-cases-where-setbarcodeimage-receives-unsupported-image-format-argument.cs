// Title: Error handling for unsupported barcode image formats
// Description: Demonstrates how to catch and log errors when SetBarCodeImage is called with a file that is not a supported image format.
// Category-Description: This example belongs to the Aspose.BarCode image handling category, showcasing the use of BarCodeReader to load barcode images. It highlights typical scenarios where developers need to validate input files, handle unsupported formats, and log exceptions without crashing the application. Key API classes include BarCodeReader and standard .NET I/O utilities, useful for robust barcode processing pipelines.
// Prompt: Implement error logging for cases where SetBarCodeImage receives an unsupported image format argument.
// Tags: barcode, error-handling, image-format, setbarcodeimage, aspose.barcode, barcodereader

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that attempts to load an unsupported image file into a <see cref="BarCodeReader"/>
/// and logs any resulting errors to a file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary environment, triggers an error by
    /// providing a non‑image file to <c>SetBarCodeImage</c>, logs the exception, and cleans up.
    /// </summary>
    static void Main()
    {
        // ----------------------------------------------------------------------
        // Set up a temporary folder and create an unsupported file (plain text)
        // ----------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string unsupportedFile = Path.Combine(tempFolder, "sample.txt");
        File.WriteAllText(unsupportedFile, "This is not an image file.");

        // Path for the error log file
        string logPath = Path.Combine(tempFolder, "error.log");

        // --------------------------------------------------------------
        // Attempt to load the unsupported file into the barcode reader
        // --------------------------------------------------------------
        using (var reader = new BarCodeReader())
        {
            try
            {
                // Verify the file exists before attempting to load it
                if (!File.Exists(unsupportedFile))
                {
                    throw new FileNotFoundException("File not found.", unsupportedFile);
                }

                // This call is expected to fail because the file is not a supported image format
                reader.SetBarCodeImage(unsupportedFile);
                Console.WriteLine("SetBarCodeImage succeeded unexpectedly.");
            }
            catch (Exception ex)
            {
                // Build a descriptive error message
                string message = $"Error setting barcode image: {ex.Message}";
                Console.WriteLine(message);

                // Attempt to append the error details to the log file
                try
                {
                    File.AppendAllText(logPath, $"{DateTime.Now:u} - {message}{Environment.NewLine}");
                }
                catch
                {
                    // Swallow any logging exceptions to keep the program running
                }
            }
        }

        // ----------------------------------------------
        // Clean up temporary files and the working folder
        // ----------------------------------------------
        try
        {
            if (File.Exists(unsupportedFile))
                File.Delete(unsupportedFile);
            if (File.Exists(logPath))
                File.Delete(logPath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Suppress any exceptions that occur during cleanup
        }
    }
}