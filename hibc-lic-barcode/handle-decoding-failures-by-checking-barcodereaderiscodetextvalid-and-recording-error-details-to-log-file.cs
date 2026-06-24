using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation, reading, and error logging using Aspose.BarCode.
/// </summary>
class Program
{
    // Path for temporary barcode images and log file
    private static readonly string TempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
    private static readonly string LogFilePath = Path.Combine(TempFolder, "barcode_log.txt");

    /// <summary>
    /// Entry point of the application. Generates sample barcodes, attempts to read them,
    /// and logs any decoding errors.
    /// </summary>
    static void Main()
    {
        // Ensure the temporary folder exists and clear any previous log content
        Directory.CreateDirectory(TempFolder);
        File.WriteAllText(LogFilePath, string.Empty);

        // Generate a valid barcode image (Code128) and save it to the temp folder
        string validBarcodePath = Path.Combine(TempFolder, "valid.png");
        GenerateBarcodeImage(EncodeTypes.Code128, "123456789012", validBarcodePath);

        // Attempt to read the valid barcode and display the result
        Console.WriteLine("Reading valid barcode:");
        ReadBarcodeAndLog(validBarcodePath);

        // Generate a blank PNG image (no barcode) to simulate a decoding failure scenario
        string blankImagePath = Path.Combine(TempFolder, "blank.png");
        GenerateBlankImage(blankImagePath);

        // Attempt to read the blank image; expect no barcode to be detected
        Console.WriteLine("\nReading blank image (expected failure):");
        ReadBarcodeAndLog(blankImagePath);

        // Inform the user that processing is complete and where to find the log file
        Console.WriteLine($"\nProcessing completed. See log file at: {LogFilePath}");
    }

    /// <summary>
    /// Generates a barcode image using the specified symbology and code text.
    /// </summary>
    /// <param name="encodeType">The barcode symbology to use.</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="outputPath">File path where the generated image will be saved.</param>
    private static void GenerateBarcodeImage(BaseEncodeType encodeType, string codeText, string outputPath)
    {
        // Create a BarcodeGenerator instance with the desired type and text
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Save the generated barcode as a PNG file (default format)
            generator.Save(outputPath);
        }
    }

    /// <summary>
    /// Creates a simple blank PNG image (no barcode content) for testing decoding failures.
    /// </summary>
    /// <param name="outputPath">File path where the blank image will be saved.</param>
    private static void GenerateBlankImage(string outputPath)
    {
        // Create a bitmap with a white background
        using (var bitmap = new Bitmap(200, 100))
        {
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Aspose.Drawing.Color.White);
            }

            // Save the blank bitmap as a PNG file
            bitmap.Save(outputPath, Aspose.Drawing.Imaging.ImageFormat.Png);
        }
    }

    /// <summary>
    /// Reads a barcode from the specified image file and logs any errors if decoding fails.
    /// </summary>
    /// <param name="imagePath">Path to the image file containing the barcode.</param>
    private static void ReadBarcodeAndLog(string imagePath)
    {
        // Verify that the image file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            LogError($"File not found: {imagePath}");
            Console.WriteLine($"Error: File not found - {imagePath}");
            return;
        }

        // Create a BarCodeReader that supports all barcode types
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Enable checksum validation (optional, demonstrates usage)
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Read all barcodes present in the image
            var results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                // No barcode detected – treat as a decoding failure
                LogError($"No barcode detected in image: {imagePath}");
                Console.WriteLine("No barcode detected.");
                return;
            }

            // Iterate through each detected barcode result
            foreach (var result in results)
            {
                // Consider a result invalid if the decoded text is null or empty
                if (string.IsNullOrEmpty(result.CodeText))
                {
                    LogError($"Decoding failed for barcode type {result.CodeTypeName} in image {imagePath}. CodeText is empty.");
                    Console.WriteLine($"Decoding failed for {result.CodeTypeName} (empty CodeText).");
                }
                else
                {
                    // Successful decode – display the barcode type and its text
                    Console.WriteLine($"Detected {result.CodeTypeName}: {result.CodeText}");
                }
            }
        }
    }

    /// <summary>
    /// Appends an error message with a timestamp to the log file.
    /// </summary>
    /// <param name="message">The error message to log.</param>
    private static void LogError(string message)
    {
        // Format the log entry with date, time, and error label
        string entry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ERROR: {message}{Environment.NewLine}";
        File.AppendAllText(LogFilePath, entry);
    }
}