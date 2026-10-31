// Title: Extract Aztec barcode layer count and compact mode flag
// Description: Demonstrates how to generate an Aztec barcode, read it, and retrieve the layer count and compact mode flag using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows usage of BarcodeGenerator for creating Aztec symbols and BarCodeReader with reflection to access extended Aztec properties such as LayersCount and SymbolMode. Developers working with Aztec symbology often need to verify encoding parameters after scanning, making this pattern useful for debugging and validation scenarios.
// Prompt: Extract Aztec Code layer count and compact mode flag from an image containing Aztec barcodes.
// Tags: aztec, barcode, generation, recognition, layerscount, symbolmode, aspose.barcode, csharp

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Sample program that creates an Aztec barcode, reads it back, and extracts
/// the LayersCount and SymbolMode (compact/full‑range) using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a temporary Aztec barcode, reads it, prints
    /// extended properties, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary directory and file path for the generated barcode
        // --------------------------------------------------------------------
        string tempDir = Path.Combine(Path.GetTempPath(), "AztecSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "aztec.png");

        // --------------------------------------------------------------
        // Generate a sample Aztec barcode with known settings (compact mode, 3 layers)
        // --------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Aztec, "Sample"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 5;
            generator.Parameters.Barcode.Aztec.SymbolMode = AztecSymbolMode.Compact;
            generator.Parameters.Barcode.Aztec.LayersCount = 3; // valid for compact mode (max 3)
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // --------------------------------------------------------------
        // Read the Aztec barcode and extract extended properties via reflection
        // --------------------------------------------------------------
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.Aztec))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");

                // Access the extended Aztec object (may contain additional metadata)
                object extAztec = result.Extended.Aztec;
                Type extType = extAztec.GetType();

                // Retrieve LayersCount property if it exists
                PropertyInfo layersProp = extType.GetProperty("LayersCount");
                if (layersProp != null)
                {
                    object layersValue = layersProp.GetValue(extAztec);
                    Console.WriteLine($"LayersCount: {layersValue}");
                }
                else
                {
                    Console.WriteLine("LayersCount: not available");
                }

                // Retrieve SymbolMode property (compact / full‑range) if it exists
                PropertyInfo modeProp = extType.GetProperty("SymbolMode");
                if (modeProp != null)
                {
                    object modeValue = modeProp.GetValue(extAztec);
                    Console.WriteLine($"SymbolMode: {modeValue}");
                }
                else
                {
                    Console.WriteLine("SymbolMode: not available");
                }

                // Additional known metadata for completeness
                Console.WriteLine($"IsReaderInitialization: {result.Extended.Aztec.IsReaderInitialization}");
                Console.WriteLine($"StructuredAppendBarcodeId: {result.Extended.Aztec.StructuredAppendBarcodeId}");
                Console.WriteLine($"StructuredAppendBarcodesCount: {result.Extended.Aztec.StructuredAppendBarcodesCount}");
                Console.WriteLine($"StructuredAppendFileId: {result.Extended.Aztec.StructuredAppendFileId}");
            }
        }

        // --------------------------------------------------------------
        // Cleanup temporary files and directory
        // --------------------------------------------------------------
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignore any cleanup errors (e.g., file still in use)
        }
    }
}