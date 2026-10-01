// Title: Extract Aztec barcode layer count and compact mode flag
// Description: Demonstrates how to generate an Aztec barcode, read it back, and retrieve the layer count and compact mode flag using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition and generation category. It showcases the use of BarcodeGenerator for creating Aztec symbols and BarCodeReader with DecodeType.Aztec for extracting extended Aztec parameters via the Extended property. Developers working with Aztec codes often need to access metadata such as layers count and compact mode to validate encoding settings or adapt processing logic.
// Prompt: Extract Aztec Code layer count and compact mode flag from an image containing Aztec barcodes.
// Tags: aztec, barcode, extraction, layer count, compact mode, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating an Aztec barcode, extracting its layer count and compact mode flag, and cleaning up the temporary file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a sample Aztec barcode, reads its metadata, and deletes the temporary image.
/// </summary>
    static void Main()
    {
        // Create a temporary Aztec barcode image to demonstrate extraction.
        string tempImagePath = Path.Combine(Path.GetTempPath(), "aztec_sample.png");
        GenerateAztecBarcode(tempImagePath, "DemoText");

        // Extract Aztec layer count and compact mode flag.
        ExtractAztecInfo(tempImagePath);

        // Clean up the temporary file.
        try
        {
            if (File.Exists(tempImagePath))
                File.Delete(tempImagePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Warning: could not delete temporary file. {ex.Message}");
        }
    }

    /// <summary>
    /// Generates an Aztec barcode image at the specified path using the provided text.
    /// </summary>
    /// <param name="filePath">Full path where the barcode image will be saved.</param>
    /// <param name="codeText">Text to encode into the Aztec barcode.</param>
    private static void GenerateAztecBarcode(string filePath, string codeText)
    {
        // Ensure the directory exists.
        string dir = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        // Generate Aztec barcode.
        using (var generator = new BarcodeGenerator(EncodeTypes.Aztec, codeText))
        {
            // Save as PNG.
            generator.Save(filePath, BarCodeImageFormat.Png);
        }
    }

    /// <summary>
    /// Reads the Aztec barcode from the given image and outputs its layer count and compact mode flag.
    /// </summary>
    /// <param name="imagePath">Path to the image containing the Aztec barcode.</param>
    private static void ExtractAztecInfo(string imagePath)
    {
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: File not found - {imagePath}");
            return;
        }

        // Use the Aztec decode type.
        BaseDecodeType decodeType = DecodeType.Aztec;

        using (var reader = new BarCodeReader(imagePath, decodeType))
        {
            var results = reader.ReadBarCodes();
            if (results == null || results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
                return;
            }

            foreach (var result in results)
            {
                // Verify that the detected barcode is Aztec.
                if (result.CodeType != DecodeType.Aztec)
                {
                    Console.WriteLine($"Detected barcode is not Aztec (type: {result.CodeType}).");
                    continue;
                }

                // Access extended Aztec parameters via reflection.
                var aztecExt = result.Extended.Aztec;
                if (aztecExt == null)
                {
                    Console.WriteLine("Extended Aztec parameters are not available.");
                    continue;
                }

                var aztecType = aztecExt.GetType();

                // Attempt to read LayersCount property.
                int? layersCount = null;
                var layersProp = aztecType.GetProperty("LayersCount");
                if (layersProp != null && layersProp.PropertyType == typeof(int))
                {
                    layersCount = (int)layersProp.GetValue(aztecExt);
                }

                // Attempt to read IsCompact property.
                bool? isCompact = null;
                var compactProp = aztecType.GetProperty("IsCompact");
                if (compactProp != null && compactProp.PropertyType == typeof(bool))
                {
                    isCompact = (bool)compactProp.GetValue(aztecExt);
                }

                // Output results.
                if (layersCount.HasValue)
                    Console.WriteLine($"Aztec Layers Count: {layersCount.Value}");
                else
                    Console.WriteLine("Aztec Layers Count: not available via API.");

                if (isCompact.HasValue)
                    Console.WriteLine($"Aztec Compact Mode: {isCompact.Value}");
                else
                    Console.WriteLine("Aztec Compact Mode: not available via API.");
            }
        }
    }
}