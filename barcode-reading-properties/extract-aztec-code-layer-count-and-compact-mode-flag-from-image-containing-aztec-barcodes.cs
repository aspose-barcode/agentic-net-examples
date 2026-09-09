// Title: Extract Aztec barcode layer count and compact mode flag
// Description: Demonstrates how to generate an Aztec barcode, then read it back to obtain the layers count and whether it is in compact mode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows usage of BarcodeGenerator for creating Aztec symbols and BarCodeReader with DecodeType.Aztec for extracting extended Aztec properties such as LayersCount and SymbolMode. Developers working with Aztec symbology often need to verify encoding parameters after scanning, making this pattern useful for validation and debugging scenarios.
// Prompt: Extract Aztec Code layer count and compact mode flag from an image containing Aztec barcodes.
// Tags: aztec, barcode, generation, recognition, layerscount, compactmode, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that generates an Aztec barcode, then reads it back to extract
/// the layers count and compact mode flag using Aspose.BarCode APIs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the sample. Generates a temporary Aztec barcode image,
    /// reads it, and prints the detected LayersCount and Compact mode status.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to store the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "AztecSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "aztec.png");

        // Generate an Aztec barcode with known settings (compact mode, 3 layers)
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Aztec, "SampleText"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.Aztec.SymbolMode = AztecSymbolMode.Compact;
            generator.Parameters.Barcode.Aztec.LayersCount = 3; // Compact mode supports 1-3 layers
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode from the image and extract extended Aztec properties
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Aztec))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Use reflection to safely access extended Aztec properties (may vary by version)
                object aztecExt = result.Extended?.Aztec;
                int layersCount = -1;
                bool isCompact = false;

                if (aztecExt != null)
                {
                    var layersProp = aztecExt.GetType().GetProperty("LayersCount");
                    if (layersProp != null && layersProp.PropertyType == typeof(int))
                    {
                        layersCount = (int)layersProp.GetValue(aztecExt);
                    }

                    var modeProp = aztecExt.GetType().GetProperty("SymbolMode");
                    if (modeProp != null && modeProp.PropertyType == typeof(AztecSymbolMode))
                    {
                        AztecSymbolMode mode = (AztecSymbolMode)modeProp.GetValue(aztecExt);
                        isCompact = mode == AztecSymbolMode.Compact;
                    }
                }

                Console.WriteLine($"Detected LayersCount: {layersCount}");
                Console.WriteLine($"Is Compact Mode: {isCompact}");
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
            // Ignore cleanup errors
        }
    }
}