// Title: MicroPdf417 Code128 Emulation Detection Example
// Description: Demonstrates generating MicroPdf417 barcodes with and without the Code128 emulation flag and reading the flag during decoding.
// Prompt: Identify Micro PDF417 Code128 emulation flag and handle accordingly in processing logic.
// Tags: barcode, micropdf417, code128, emulation, generation, recognition, aspose

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that shows how to work with the MicroPdf417 Code128 emulation flag:
/// - Generates two barcodes (with and without the flag)
/// - Reads each barcode and inspects the emulation flag
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcode images, then reads and processes them.
    /// </summary>
    static void Main()
    {
        // Sample codetext for MicroPdf417 Code128 emulation (Application Indicator + FNC1 separator)
        string codeText = "a\u001d1234567890";

        // Paths for temporary image files (saved in the current working directory)
        string imagePathEmulation = Path.Combine(Directory.GetCurrentDirectory(), "micropdf417_emulation.png");
        string imagePathNormal = Path.Combine(Directory.GetCurrentDirectory(), "micropdf417_normal.png");

        // -------------------------------------------------
        // 1. Generate MicroPdf417 with Code128 emulation flag
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.MicroPdf417, codeText))
        {
            // Enable Code128 emulation mode for the generated symbol
            generator.Parameters.Barcode.Pdf417.IsCode128Emulation = true;

            // Save image (optional, just for visual verification)
            generator.Save(imagePathEmulation);
        }

        // -------------------------------------------------
        // 2. Generate MicroPdf417 without Code128 emulation flag
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.MicroPdf417, codeText))
        {
            // Do NOT set IsCode128Emulation (defaults to false)
            generator.Save(imagePathNormal);
        }

        // -------------------------------------------------
        // 3. Read and process the barcode with emulation flag
        // -------------------------------------------------
        Console.WriteLine("Reading barcode with Code128 emulation flag set:");
        ProcessBarcodeImage(imagePathEmulation);

        // -------------------------------------------------
        // 4. Read and process the barcode without emulation flag
        // -------------------------------------------------
        Console.WriteLine("\nReading barcode without Code128 emulation flag:");
        ProcessBarcodeImage(imagePathNormal);
    }

    /// <summary>
    /// Reads a barcode image, extracts the Code128 emulation flag, and outputs handling information.
    /// </summary>
    /// <param name="imagePath">Full path to the barcode image file.</param>
    static void ProcessBarcodeImage(string imagePath)
    {
        // Verify that the image file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Use MicroPdf417 decode type to correctly interpret the symbol
        using (var reader = new BarCodeReader(imagePath, DecodeType.MicroPdf417))
        {
            bool anyFound = false;

            // Iterate through all detected barcodes in the image
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                anyFound = true;

                // The IsCode128Emulation property indicates whether the barcode was generated in emulation mode
                bool isEmulation = result.Extended.Pdf417.IsCode128Emulation;

                // Output basic barcode information
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"IsCode128Emulation: {isEmulation}");

                // Custom handling based on the emulation flag
                if (isEmulation)
                {
                    Console.WriteLine("-> Detected Code128 emulation mode. Process accordingly.");
                }
                else
                {
                    Console.WriteLine("-> Standard MicroPdf417 mode.");
                }
            }

            // Inform the user if no barcodes were detected
            if (!anyFound)
            {
                Console.WriteLine("No barcodes were detected in the image.");
            }
        }
    }
}