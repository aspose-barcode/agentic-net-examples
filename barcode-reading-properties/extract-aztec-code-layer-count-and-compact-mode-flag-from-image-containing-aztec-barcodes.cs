// Title: Extract Aztec Code layer count and compact mode flag
// Description: Demonstrates how to read an Aztec barcode from an image and retrieve its layer count and compact mode flag using Aspose.BarCode.
// Prompt: Extract Aztec Code layer count and compact mode flag from an image containing Aztec barcodes.
// Tags: aztec, barcode, extraction, layer count, compact mode, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.Generation;

/// <summary>
/// Program to extract Aztec barcode layer count and compact mode flag from an image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Reads the specified image, detects Aztec barcodes, and prints their details.
    /// </summary>
    static void Main()
    {
        // Path to the image containing the Aztec barcode.
        string imagePath = "aztec.png";

        // Verify that the image file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Initialize the barcode reader for Aztec symbology.
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Aztec))
        {
            bool anyFound = false;

            // Iterate through all detected barcodes in the image.
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                anyFound = true;

                // Output basic barcode information.
                Console.WriteLine($"Barcode type: {result.CodeTypeName}");
                Console.WriteLine($"Codetext: {result.CodeText}");

                // Access extended Aztec-specific information, if available.
                var aztecInfo = result.Extended?.Aztec;
                if (aztecInfo != null)
                {
                    // Use reflection to obtain LayersCount if the property exists.
                    var layersProp = aztecInfo.GetType().GetProperty("LayersCount");
                    if (layersProp != null)
                    {
                        int layers = (int)layersProp.GetValue(aztecInfo);
                        Console.WriteLine($"Aztec layers count: {layers}");
                    }
                    else
                    {
                        Console.WriteLine("Aztec layers count: unavailable in this library version.");
                    }

                    // Use reflection to obtain SymbolMode if the property exists.
                    var modeProp = aztecInfo.GetType().GetProperty("SymbolMode");
                    if (modeProp != null)
                    {
                        object modeValue = modeProp.GetValue(aztecInfo);
                        // Determine whether the mode is Compact, if the enum is present.
                        bool isCompact = false;
                        var enumType = modeValue?.GetType();
                        if (enumType != null && Enum.IsDefined(enumType, "Compact"))
                        {
                            var compactValue = Enum.Parse(enumType, "Compact");
                            isCompact = modeValue.Equals(compactValue);
                        }
                        Console.WriteLine($"Compact mode: {isCompact}");
                    }
                    else
                    {
                        Console.WriteLine("Compact mode flag: unavailable in this library version.");
                    }
                }
                else
                {
                    Console.WriteLine("No Aztec extended information available.");
                }

                Console.WriteLine();
            }

            // Inform the user if no Aztec barcodes were detected.
            if (!anyFound)
            {
                Console.WriteLine("No Aztec barcodes were detected in the image.");
            }
        }
    }
}