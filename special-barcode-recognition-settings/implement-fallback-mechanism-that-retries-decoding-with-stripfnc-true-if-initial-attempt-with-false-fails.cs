// Title: Barcode decoding with fallback StripFNC setting
// Description: Demonstrates generating a Code128 barcode, attempting to decode it with StripFNC disabled, and retrying with StripFNC enabled if needed.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Developers often need to handle Function Code (FNC) characters; this sample shows how to toggle the StripFNC setting and implement a fallback strategy when the initial decode fails.
// Prompt: Implement a fallback mechanism that retries decoding with StripFNC true if initial attempt with false fails.
// Tags: code128, barcode, decoding, stripfnc, fallback, aspose.barcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Code128 barcode, attempts to decode it,
/// and applies a fallback decoding strategy using the StripFNC setting.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a unique temporary folder to store the generated barcode image.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeFallback_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "code128.png");

        // --------------------------------------------------------------
        // Generate a simple Code128 barcode and save it as a PNG file.
        // --------------------------------------------------------------
        BaseEncodeType encodeType = EncodeTypes.Code128;
        using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, "Aspose"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------
        // Prepare the decode type for reading the barcode.
        // --------------------------------------------------------------
        BaseDecodeType decodeType = DecodeType.Code128;

        BarCodeResult[] results = null;
        bool decodingSuccessful = false;

        // --------------------------------------------------------------
        // First decoding attempt with StripFNC set to false.
        // --------------------------------------------------------------
        using (BarCodeReader reader = new BarCodeReader(barcodePath, decodeType))
        {
            reader.BarcodeSettings.StripFNC = false;
            results = reader.ReadBarCodes();

            // Verify that the result does not contain any FNC characters.
            if (results != null && results.Length > 0)
            {
                bool containsFnc = false;
                foreach (BarCodeResult r in results)
                {
                    if (!string.IsNullOrEmpty(r.CodeText) && r.CodeText.Contains("<FNC"))
                    {
                        containsFnc = true;
                        break;
                    }
                }
                if (!containsFnc)
                {
                    decodingSuccessful = true;
                }
            }
        }

        // --------------------------------------------------------------
        // Fallback decoding attempt with StripFNC set to true if needed.
        // --------------------------------------------------------------
        if (!decodingSuccessful)
        {
            using (BarCodeReader reader = new BarCodeReader(barcodePath, decodeType))
            {
                reader.BarcodeSettings.StripFNC = true;
                results = reader.ReadBarCodes();
            }
        }

        // --------------------------------------------------------------
        // Output the decoding results to the console.
        // --------------------------------------------------------------
        if (results != null && results.Length > 0)
        {
            Console.WriteLine("Decoding results:");
            foreach (BarCodeResult r in results)
            {
                Console.WriteLine($"CodeType: {r.CodeTypeName}");
                Console.WriteLine($"CodeText: {r.CodeText}");
            }
        }
        else
        {
            Console.WriteLine("No barcode could be decoded.");
        }

        // --------------------------------------------------------------
        // Clean up temporary files and directories.
        // --------------------------------------------------------------
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit
        }
    }
}