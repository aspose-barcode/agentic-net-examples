// Title: Extract Aztec Code layer count and compact mode flag
// Description: Demonstrates how to read an Aztec barcode from an image and retrieve its layer count and compact mode flag using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating the use of BarCodeReader, DecodeType, and the Extended Aztec properties to obtain detailed symbology information. Typical use cases include analyzing Aztec codes for metadata such as layers and compact mode, which developers often need when processing scanned documents or validating barcode specifications. The snippet serves as a reference for extracting low‑level Aztec parameters in .NET applications.
// Prompt: Extract Aztec Code layer count and compact mode flag from an image containing Aztec barcodes.
// Tags: aztec, barcode, recognition, layerscount, iscompact, aspose.barcode, csharp, .net

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Program to read Aztec barcode from an image and display its layer count and compact mode flag.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts optional image path argument, reads Aztec barcode, and prints details.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument may be the image file path.</param>
    static void Main(string[] args)
    {
        // Determine image path: use first argument if provided, otherwise default to "aztec.png"
        string imagePath = args.Length > 0 ? args[0] : "aztec.png";

        // Verify that the file exists before attempting to read
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Initialize BarCodeReader for Aztec symbology
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Aztec))
        {
            // Iterate through all detected barcodes in the image
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Output basic barcode information
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");

                // Access extended Aztec-specific properties via reflection
                var aztecExt = result.Extended.Aztec;

                var layersProp = aztecExt.GetType().GetProperty("LayersCount");
                var compactProp = aztecExt.GetType().GetProperty("IsCompact");

                // Retrieve and display LayersCount if available
                if (layersProp != null && layersProp.PropertyType == typeof(int))
                {
                    int layers = (int)layersProp.GetValue(aztecExt);
                    Console.WriteLine($"LayersCount: {layers}");
                }
                else
                {
                    Console.WriteLine("LayersCount: not available");
                }

                // Retrieve and display IsCompact flag if available
                if (compactProp != null && compactProp.PropertyType == typeof(bool))
                {
                    bool isCompact = (bool)compactProp.GetValue(aztecExt);
                    Console.WriteLine($"IsCompact: {isCompact}");
                }
                else
                {
                    Console.WriteLine("IsCompact: not available");
                }
            }
        }
    }
}