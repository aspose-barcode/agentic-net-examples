// Title: Log warning for moderate barcode confidence and suggest image enhancement
// Description: Demonstrates generating a QR code, reading it, and logging a warning when the recognition confidence is moderate, advising the user to improve image quality.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create barcodes and BarCodeReader to decode them. Developers often need to assess recognition confidence (BarCodeResult.Confidence) and provide guidance for image quality improvement in scenarios such as scanning low‑resolution images or noisy backgrounds.
// Prompt: Log a warning when BarCodeResult.Confidence equals Confidence.Moderate and suggest image enhancement to the user.
// Tags: barcode, qr, generation, recognition, confidence, warning, image-enhancement, aspose.barcode, aspnet

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR barcode, reads it back, and logs a warning
/// if the detection confidence is moderate, suggesting image enhancement.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a simple QR barcode image and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "SampleText"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image file was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Error: Barcode image file was not created.");
            Cleanup(tempFolder);
            return;
        }

        // Set the decode type to QR for reading the generated barcode
        BaseDecodeType decodeType = DecodeType.QR;

        // Read the barcode from the image and evaluate the confidence level
        using (var reader = new BarCodeReader(barcodePath, decodeType))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Code Text: {result.CodeText}");
                Console.WriteLine($"Code Type: {result.CodeTypeName}");
                Console.WriteLine($"Confidence: {result.Confidence}");

                // Log a warning if the confidence is moderate and suggest image enhancement
                if (result.Confidence == BarCodeConfidence.Moderate)
                {
                    Console.WriteLine("Warning: Moderate confidence detected. Consider enhancing the image quality for better recognition.");
                }
            }
        }

        // Clean up temporary files and folders
        Cleanup(tempFolder);
    }

    /// <summary>
    /// Deletes the specified folder and its contents, suppressing any exceptions.
    /// </summary>
    /// <param name="folderPath">The path of the folder to delete.</param>
    static void Cleanup(string folderPath)
    {
        try
        {
            if (Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath, true);
            }
        }
        catch
        {
            // Suppress any cleanup exceptions
        }
    }
}