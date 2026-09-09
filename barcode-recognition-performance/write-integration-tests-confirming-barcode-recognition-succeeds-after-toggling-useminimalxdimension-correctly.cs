// Title: Barcode generation and recognition with XDimension mode variations
// Description: This example creates a Code128 barcode image, saves it, and then reads it back using different XDimension settings, demonstrating how UseMinimalXDimension affects recognition.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition APIs. It uses BarcodeGenerator to create barcodes and BarCodeReader with QualitySettings.XDimension to control barcode module width. Typical scenarios include testing barcode rendering options and ensuring reliable scanning across different XDimension configurations. Developers often need to validate that toggling UseMinimalXDimension yields correct decoding results.
// Prompt: Write integration tests confirming barcode recognition succeeds after toggling UseMinimalXDimension correctly.
// Tags: barcode, code128, generation, recognition, xdimension, minimalxdimension, aspose.barcode, .net

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation and recognition using different XDimension modes,
/// including the UseMinimalXDimension setting.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, saves it to a temporary file,
    /// and runs recognition tests with various XDimension configurations.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the barcode image
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define the output path and the text to encode
        string barcodePath = Path.Combine(tempDir, "code128.png");
        string codeText = "AsposeTest123";

        // Generate the barcode image using BarcodeGenerator
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Run recognition tests with normal, small, and minimal XDimension modes
        TestRecognition(barcodePath, codeText, XDimensionMode.Normal, "Normal");
        TestRecognition(barcodePath, codeText, XDimensionMode.Small, "Small");
        TestRecognitionWithMinimal(barcodePath, codeText, XDimensionMode.UseMinimalXDimension, "UseMinimalXDimension");
    }

    /// <summary>
    /// Tests barcode recognition using a specified XDimension mode.
    /// </summary>
    /// <param name="imagePath">Path to the barcode image.</param>
    /// <param name="expectedText">The expected decoded text.</param>
    /// <param name="mode">The XDimension mode to apply.</param>
    /// <param name="modeName">A friendly name for logging.</param>
    static void TestRecognition(string imagePath, string expectedText, XDimensionMode mode, string modeName)
    {
        // Set the decode type to Code128
        BaseDecodeType decodeType = DecodeType.Code128;

        // Initialize the reader with the image and decode type
        using (var reader = new BarCodeReader(imagePath, decodeType))
        {
            // Apply the desired XDimension mode
            reader.QualitySettings.XDimension = mode;

            // Perform barcode detection
            BarCodeResult[] results = reader.ReadBarCodes();

            // Determine if the expected text was found
            bool success = false;
            foreach (var result in results)
            {
                if (result.CodeText == expectedText)
                {
                    success = true;
                    break;
                }
            }

            // Output the test result
            Console.WriteLine($"{modeName} mode recognition {(success ? "succeeded" : "failed")} - found {results.Length} barcode(s).");
        }
    }

    /// <summary>
    /// Tests barcode recognition using the UseMinimalXDimension mode with a custom minimal X dimension value.
    /// </summary>
    /// <param name="imagePath">Path to the barcode image.</param>
    /// <param name="expectedText">The expected decoded text.</param>
    /// <param name="mode">The XDimension mode (should be UseMinimalXDimension).</param>
    /// <param name="modeName">A friendly name for logging.</param>
    static void TestRecognitionWithMinimal(string imagePath, string expectedText, XDimensionMode mode, string modeName)
    {
        // Set the decode type to Code128
        BaseDecodeType decodeType = DecodeType.Code128;

        // Initialize the reader with the image and decode type
        using (var reader = new BarCodeReader(imagePath, decodeType))
        {
            // Apply the UseMinimalXDimension mode and set a minimal X dimension value
            reader.QualitySettings.XDimension = mode;
            reader.QualitySettings.MinimalXDimension = 1f;

            // Perform barcode detection
            BarCodeResult[] results = reader.ReadBarCodes();

            // Determine if the expected text was found
            bool success = false;
            foreach (var result in results)
            {
                if (result.CodeText == expectedText)
                {
                    success = true;
                    break;
                }
            }

            // Output the test result
            Console.WriteLine($"{modeName} mode recognition {(success ? "succeeded" : "failed")} - found {results.Length} barcode(s).");
        }
    }
}