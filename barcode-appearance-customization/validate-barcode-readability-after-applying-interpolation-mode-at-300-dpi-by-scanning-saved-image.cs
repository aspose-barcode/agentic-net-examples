// Title: Validate barcode readability with interpolation scaling at 300 dpi
// Description: Demonstrates generating a DataMatrix barcode using Aspose.BarCode with interpolation auto‑size mode at 300 dpi, saving it as PNG, and confirming that the saved image can be successfully decoded.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to configure image resolution, scaling mode, and dimensions via the BarcodeGenerator.Parameters API, then validate the result using BarCodeReader. Typical use cases include preparing high‑resolution barcodes for printing or imaging systems and ensuring they remain readable after processing. Developers often need to adjust DPI, interpolation, and size settings to meet print quality requirements while verifying readability programmatically.
// Prompt: Validate barcode readability after applying Interpolation mode at 300 dpi by scanning the saved image.
// Tags: datamatrix, generation, recognition, interpolation, dpi, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation with interpolation scaling and subsequent readability validation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a DataMatrix barcode at 300 dpi using interpolation mode, saves it, reads it back, and outputs the decoding results.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Prepare a temporary file path for the barcode image.
        // --------------------------------------------------------------------
        string tempPath = Path.GetTempPath();
        string barcodeFile = Path.Combine(tempPath, "barcode_interpolation.png");

        // --------------------------------------------------------------------
        // 2. Generate a DataMatrix barcode with interpolation auto‑size mode
        //    and a resolution of 300 dpi, then save it as a PNG file.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "ASPOSE"))
        {
            // Use interpolation scaling to fit the requested pixel dimensions.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set the target image size in pixels.
            generator.Parameters.ImageWidth.Pixels = 300f;
            generator.Parameters.ImageHeight.Pixels = 300f;

            // Define the X‑dimension (module size) of the barcode.
            generator.Parameters.Barcode.XDimension.Pixels = 3f;

            // Set the image resolution (dots per inch).
            generator.Parameters.Resolution = 300f; // dpi

            // Save the generated barcode to the temporary PNG file.
            generator.Save(barcodeFile, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // 3. Verify that the barcode image file was created successfully.
        // --------------------------------------------------------------------
        if (!File.Exists(barcodeFile))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // --------------------------------------------------------------------
        // 4. Read and decode the saved barcode image to confirm readability.
        // --------------------------------------------------------------------
        BaseDecodeType decodeType = DecodeType.DataMatrix;
        using (var reader = new BarCodeReader(barcodeFile, decodeType))
        {
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length > 0)
            {
                // Output details for each detected barcode.
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"CodeType: {result.CodeTypeName}");
                    Console.WriteLine($"CodeText: {result.CodeText}");
                    Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");
                }

                Console.WriteLine("Barcode is readable.");
            }
            else
            {
                Console.WriteLine("No barcode detected.");
            }
        }

        // --------------------------------------------------------------------
        // 5. Clean up the temporary file (optional).
        // --------------------------------------------------------------------
        try
        {
            File.Delete(barcodeFile);
        }
        catch
        {
            // Ignore any cleanup errors.
        }
    }
}