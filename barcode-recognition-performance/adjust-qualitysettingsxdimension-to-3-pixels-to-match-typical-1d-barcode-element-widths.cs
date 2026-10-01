// Title: Adjust XDimension for 1D Barcode Reading
// Description: Demonstrates how to set the XDimension quality setting to 3 pixels when reading a Code128 barcode, ensuring accurate detection of typical 1D barcode element widths.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating the use of BarCodeReader and QualitySettings to fine‑tune XDimension for 1D symbologies such as Code128. Developers often need to adjust XDimension to match the physical size of barcode modules, improving reading reliability in varied imaging conditions. The sample shows generating a barcode, configuring minimal XDimension, and extracting decoded information.
// Prompt: Adjust QualitySettings.XDimension to 3 pixels to match typical 1D barcode element widths.
// Tags: code128, xdimension, qualitysettings, barcode recognition, aspose.barcode, image processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode, configures minimal XDimension for reading, and outputs the decoded result.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the sample. Creates a temporary barcode image, reads it with custom XDimension settings, and cleans up resources.
    /// </summary>
    static void Main()
    {
        // Create a temporary directory for sample files
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define the full path for the barcode image
        string barcodePath = Path.Combine(tempDir, "barcode.png");

        // Generate a simple Code128 barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was successfully created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Specify the decode type matching the generated barcode
        BaseDecodeType decodeType = DecodeType.Code128;

        // Read the barcode using a BarCodeReader with custom QualitySettings
        using (var reader = new BarCodeReader(barcodePath, decodeType))
        {
            // Configure XDimension to use a minimal value of 3 pixels
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 3f; // pixels

            // Perform the barcode reading operation
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output the decoding results to the console
            if (results != null && results.Length > 0)
            {
                foreach (var result in results)
                {
                    Console.WriteLine($"CodeText: {result.CodeText}");
                    Console.WriteLine($"CodeType: {result.CodeTypeName}");
                    Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");
                }
            }
            else
            {
                Console.WriteLine("No barcode detected.");
            }
        }

        // Clean up temporary files and directory
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}