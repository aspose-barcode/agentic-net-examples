// Title: Barcode Generation and Decoding with Error Handling
// Description: Demonstrates creating a Code128 barcode image, saving it to a temporary folder, and decoding it while handling possible errors such as missing files or incomplete data.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Typical scenarios include automated label creation, inventory tracking, and data capture where developers need robust error handling for missing images, unreadable barcodes, or incomplete data.
// Prompt: Implement error handling to manage cases where barcode decoding fails or returns incomplete data.
// Tags: barcode, code128, generation, decoding, error-handling, aspose.barcode, image, temporary-files

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation, saving to a temporary location, and decoding with comprehensive error handling.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, saves it, attempts to decode it, and cleans up temporary resources.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // -------------------- Barcode Generation --------------------
        try
        {
            // Initialize the generator with Code128 symbology and sample data
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Set the X-dimension (module width) in pixels for better readability
                generator.Parameters.Barcode.XDimension.Pixels = 2f;

                // Save the generated barcode as a PNG file
                generator.Save(barcodePath, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode generated at: {barcodePath}");
            }
        }
        catch (Exception ex)
        {
            // Handle any errors that occur during barcode creation
            Console.WriteLine($"Error during barcode generation: {ex.Message}");
            Cleanup(tempFolder);
            return;
        }

        // -------------------- Verify Image Existence --------------------
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Barcode image file does not exist.");
            Cleanup(tempFolder);
            return;
        }

        // -------------------- Barcode Decoding --------------------
        try
        {
            // Initialize the reader for Code128 barcodes
            using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.Code128))
            {
                // Enforce checksum validation to ensure data integrity
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                // Attempt to read all barcodes from the image
                BarCodeResult[] results = reader.ReadBarCodes();

                // Check if any results were returned
                if (results == null || results.Length == 0)
                {
                    Console.WriteLine("No barcode detected or decoding failed.");
                }
                else
                {
                    // Iterate through each detected barcode result
                    foreach (BarCodeResult result in results)
                    {
                        // Verify that the decoded text is not empty
                        if (string.IsNullOrEmpty(result.CodeText))
                        {
                            Console.WriteLine("Barcode detected but data is incomplete.");
                        }
                        else
                        {
                            // Output detailed decoding information
                            Console.WriteLine($"Decoded Type: {result.CodeTypeName}");
                            Console.WriteLine($"Decoded Text: {result.CodeText}");
                            Console.WriteLine($"Confidence: {result.Confidence}");
                            Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                        }
                    }
                }
            }
        }
        catch (ArgumentException ex)
        {
            // Handle cases where the image path is invalid or the file cannot be loaded
            Console.WriteLine($"Invalid image or loading error: {ex.Message}");
        }
        catch (RecognitionAbortedException ex)
        {
            // Handle timeout or abort scenarios during recognition
            Console.WriteLine($"Recognition aborted after timeout: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Catch any other unexpected errors during decoding
            Console.WriteLine($"Unexpected error during decoding: {ex.Message}");
        }
        finally
        {
            // Ensure temporary files are removed regardless of success or failure
            Cleanup(tempFolder);
        }
    }

    /// <summary>
    /// Deletes the specified temporary folder and its contents, logging any cleanup errors.
    /// </summary>
    /// <param name="folderPath">The path of the folder to delete.</param>
    static void Cleanup(string folderPath)
    {
        try
        {
            if (Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath, true);
                Console.WriteLine("Temporary files cleaned up.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup failed: {ex.Message}");
        }
    }
}