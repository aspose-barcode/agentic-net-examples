// Title: Retrieve DotCode Version and Error Correction Level from Scanned Barcode
// Description: Demonstrates how to generate a DotCode barcode, scan it, and extract version and error correction level information using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create a DotCode symbol, BarCodeReader to decode it, and reflection to access extended DotCode parameters such as version and error correction level. Developers working with 2D symbologies often need to retrieve these technical details for validation, quality control, or downstream processing.
// Prompt: Obtain DotCode version information and error correction level from a scanned DotCode barcode.
// Tags: dotcode, barcode, version, error-correction, generation, recognition, aspose.barcode

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a DotCode barcode, reads it back, and extracts
/// version and error correction level information using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a temporary DotCode image, scans it,
    /// and prints the extracted metadata to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample files
        string tempFolder = Path.Combine(Path.GetTempPath(), "DotCodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "dotcode.png");

        // Generate a DotCode barcode and save it as PNG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DotCode, "Sample"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4; // Set module size
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to generate the DotCode barcode image.");
            return;
        }

        // Initialize a reader for DotCode symbology and decode the generated image
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.DotCode))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Basic barcode information
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");

                // Attempt to retrieve extended DotCode parameters via reflection
                object dotExt = result.Extended?.GetType().GetProperty("DotCode")?.GetValue(result.Extended);
                if (dotExt != null)
                {
                    // Extract the Version property if available
                    PropertyInfo versionProp = dotExt.GetType().GetProperty("Version");
                    if (versionProp != null)
                    {
                        object versionValue = versionProp.GetValue(dotExt);
                        Console.WriteLine($"Version: {versionValue}");
                    }
                    else
                    {
                        Console.WriteLine("Version property not available.");
                    }

                    // Extract the ErrorCorrectionLevel property if available
                    PropertyInfo ecProp = dotExt.GetType().GetProperty("ErrorCorrectionLevel");
                    if (ecProp != null)
                    {
                        object ecValue = ecProp.GetValue(dotExt);
                        Console.WriteLine($"ErrorCorrectionLevel: {ecValue}");
                    }
                    else
                    {
                        Console.WriteLine("ErrorCorrectionLevel property not available.");
                    }
                }
                else
                {
                    Console.WriteLine("DotCode extended parameters not available.");
                }
            }
        }

        // Clean up temporary files (optional)
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}