// Title: Retrieve DotCode version and error correction level from a scanned barcode
// Description: Demonstrates generating a DotCode barcode, scanning it, and extracting version information and error correction level using Aspose.BarCode extended metadata.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, focusing on extracting extended metadata from scanned symbologies. It showcases the use of BarCodeGenerator for creating a DotCode symbol, BarCodeReader with DecodeType.DotCode for scanning, and accessing the DotCode extended properties via the Result.Extended object. Developers working with DotCode or needing detailed barcode parameters such as version and error correction level can use this pattern in applications like inventory tracking or document processing.
// Prompt: Obtain DotCode version information and error correction level from a scanned DotCode barcode.
// Tags: dotcode, barcode, recognition, metadata, version, error correction, aspose.barcode, csharp

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a DotCode barcode, scanning it, and retrieving version and error correction level information.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a temporary DotCode image, reads it, and prints extended metadata.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory to store the generated barcode image
        string tempDir = Path.Combine(Path.GetTempPath(), "DotCodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "dotcode.png");

        // Generate a sample DotCode barcode and save it as PNG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DotCode, "AsposeDemo"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4; // Set module size
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode and attempt to obtain version and error correction level
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.DotCode))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");

                // Access extended DotCode metadata if available
                var dotCodeExt = result.Extended?.DotCode;
                if (dotCodeExt != null)
                {
                    Type extType = dotCodeExt.GetType();
                    PropertyInfo[] props = extType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    foreach (PropertyInfo prop in props)
                    {
                        object value = prop.GetValue(dotCodeExt);
                        Console.WriteLine($"{prop.Name}: {value}");
                    }
                }
                else
                {
                    Console.WriteLine("No DotCode extended metadata available.");
                }

                Console.WriteLine();
            }
        }

        // Cleanup temporary files and directory
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}