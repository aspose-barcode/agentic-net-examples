// Title: HIBC Barcode Generation and Checksum Validation Example
// Description: Demonstrates creating a HIBC barcode, saving it as PNG, and verifying its checksum during recognition.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on HIBC symbology. It showcases the use of BarcodeGenerator for encoding, BarCodeReader for decoding, and enabling checksum validation via BarcodeSettings. Developers working with healthcare industry barcodes often need to generate HIBC codes and ensure their integrity by validating checksums during scanning.
// Prompt: Validate that the generated barcode complies with HIBC specifications by checking its checksum after creation.
// Tags: hibc,barcode,generation,recognition,checksum,validation,aspnet,aspose.barcode

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates HIBC barcode creation, saving, and checksum validation using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a HIBC barcode, reads it back with checksum validation, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // -----------------------------------------------------------------
        // Prepare a unique temporary folder and define the output file path
        // -----------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "HIBC_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "hibc.png");

        // -------------------------------------------------
        // Define the sample HIBC code text to encode
        // -------------------------------------------------
        string codeText = "A123B456C789";

        // -------------------------------------------------
        // Resolve the EncodeTypes.HIBC enum value via reflection
        // -------------------------------------------------
        BaseEncodeType encodeType = ResolveEncodeType("HIBC");
        if (encodeType == null)
        {
            Console.WriteLine("Encode type HIBC not found.");
            return;
        }

        // -------------------------------------------------
        // Generate the HIBC barcode and save it as PNG
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // -------------------------------------------------
        // Resolve the DecodeType.HIBC enum value via reflection
        // -------------------------------------------------
        BaseDecodeType decodeType = ResolveDecodeType("HIBC");
        if (decodeType == null)
        {
            Console.WriteLine("Decode type HIBC not found.");
            return;
        }

        // -------------------------------------------------
        // Read the barcode with checksum validation enabled
        // -------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, decodeType))
        {
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
            bool anyResult = false;

            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                anyResult = true;
                Console.WriteLine($"Read CodeText: {result.CodeText}");
                Console.WriteLine($"Checksum validation passed.");
                Console.WriteLine($"Extended OneD Value: {result.Extended.OneD.Value}");
                Console.WriteLine($"Extended OneD CheckSum: {result.Extended.OneD.CheckSum}");
            }

            if (!anyResult)
            {
                Console.WriteLine("No barcode read or checksum validation failed.");
            }
        }

        // -------------------------------------------------
        // Clean up temporary files and folder
        // -------------------------------------------------
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }

    /// <summary>
    /// Retrieves the BaseEncodeType for a given symbology name using reflection.
    /// </summary>
    /// <param name="symbologyName">The name of the symbology (e.g., "HIBC").</param>
    /// <returns>The corresponding BaseEncodeType, or null if not found.</returns>
    static BaseEncodeType ResolveEncodeType(string symbologyName)
    {
        FieldInfo field = typeof(EncodeTypes).GetField(symbologyName, BindingFlags.Public | BindingFlags.Static);
        if (field == null) return null;
        return (BaseEncodeType)field.GetValue(null);
    }

    /// <summary>
    /// Retrieves the BaseDecodeType for a given symbology name using reflection.
    /// </summary>
    /// <param name="symbologyName">The name of the symbology (e.g., "HIBC").</param>
    /// <returns>The corresponding BaseDecodeType, or null if not found.</returns>
    static BaseDecodeType ResolveDecodeType(string symbologyName)
    {
        FieldInfo field = typeof(DecodeType).GetField(symbologyName, BindingFlags.Public | BindingFlags.Static);
        if (field == null) return null;
        return (BaseDecodeType)field.GetValue(null);
    }
}