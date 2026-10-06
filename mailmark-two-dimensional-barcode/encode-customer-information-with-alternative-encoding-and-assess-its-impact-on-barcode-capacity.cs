// Title: QR Code Encoding Modes – Auto vs UTF‑8 (ECI) and Capacity Impact
// Description: Demonstrates generating QR barcodes using the default auto encoding and explicit UTF‑8 (ECI) encoding, then evaluates how each mode handles data that exceeds the QR code's maximum capacity.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on QR code creation, encoding selection, and capacity considerations. It showcases key API classes such as BarcodeGenerator, EncodeTypes, and BarCodeImageFormat. Developers commonly use these APIs to produce QR codes for various data payloads, choose appropriate encoding (auto or explicit ECI), and understand size limitations for reliable scanning.
// Prompt: Encode customer information with an alternative encoding and assess its impact on barcode capacity.
// Tags: qr code, encoding, capacity, aspose.barcode, generation, png, eci

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates encoding customer information using different QR code encoding modes and evaluates barcode capacity.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates QR codes with auto and explicit UTF‑8 encoding, and attempts to encode data exceeding QR capacity.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary output folder for generated barcode images.
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeEncodingDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // --------------------------------------------------------------------
        // Sample short customer information to encode.
        // --------------------------------------------------------------------
        string shortInfo = "CustomerInfo123";

        // --------------------------------------------------------------------
        // Generate QR barcode using default (Auto) encoding.
        // --------------------------------------------------------------------
        GenerateAndReport("Auto", shortInfo, outputDir, useExplicitEncoding: false);

        // --------------------------------------------------------------------
        // Generate QR barcode using explicit UTF‑8 (ECI) encoding.
        // --------------------------------------------------------------------
        GenerateAndReport("ECI", shortInfo, outputDir, useExplicitEncoding: true);

        // --------------------------------------------------------------------
        // Create a long string that exceeds the maximum QR code capacity (~2953 bytes).
        // --------------------------------------------------------------------
        string longInfo = new string('A', 3000); // exceeds QR max capacity

        Console.WriteLine();
        Console.WriteLine("Attempting to encode long data with default (Auto) mode:");
        TryEncodeLongData(longInfo, outputDir, useExplicitEncoding: false);

        Console.WriteLine();
        Console.WriteLine("Attempting to encode long data with explicit UTF‑8 (ECI) mode:");
        TryEncodeLongData(longInfo, outputDir, useExplicitEncoding: true);
    }

    /// <summary>
    /// Generates a QR barcode with the specified encoding mode, saves it, and reports the file size.
    /// </summary>
    /// <param name="modeName">Friendly name of the encoding mode (e.g., "Auto" or "ECI").</param>
    /// <param name="data">The text to encode into the barcode.</param>
    /// <param name="folder">Folder where the barcode image will be saved.</param>
    /// <param name="useExplicitEncoding">If true, sets the code text with UTF‑8 encoding; otherwise uses the default auto mode.</param>
    static void GenerateAndReport(string modeName, string data, string folder, bool useExplicitEncoding)
    {
        // Build the output file path.
        string filePath = Path.Combine(folder, $"QR_{modeName}.png");

        // Create and configure the barcode generator.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            if (useExplicitEncoding)
                generator.SetCodeText(data, Encoding.UTF8); // Explicit UTF‑8 (ECI) encoding.
            else
                generator.CodeText = data; // Default auto encoding.

            // Save the generated QR code as a PNG image.
            generator.Save(filePath, BarCodeImageFormat.Png);
        }

        // Report the size of the saved image file.
        long fileSize = new FileInfo(filePath).Length;
        Console.WriteLine($"{modeName} encoding: saved barcode to '{filePath}' (size: {fileSize} bytes)");
    }

    /// <summary>
    /// Attempts to encode a long data string into a QR barcode using the specified encoding mode,
    /// handling any exceptions that arise due to capacity limits.
    /// </summary>
    /// <param name="data">The long text to encode.</param>
    /// <param name="folder">Folder where the barcode image will be saved if encoding succeeds.</param>
    /// <param name="useExplicitEncoding">If true, uses explicit UTF‑8 (ECI) encoding; otherwise uses auto mode.</param>
    static void TryEncodeLongData(string data, string folder, bool useExplicitEncoding)
    {
        string mode = useExplicitEncoding ? "ECI" : "Auto";
        string filePath = Path.Combine(folder, $"QR_Long_{mode}.png");

        try
        {
            // Create and configure the barcode generator for the long data.
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                if (useExplicitEncoding)
                    generator.SetCodeText(data, Encoding.UTF8);
                else
                    generator.CodeText = data;

                // Attempt to save the QR code image.
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // If successful, report the file size.
            long size = new FileInfo(filePath).Length;
            Console.WriteLine($"Successfully encoded long data with {mode} mode. File size: {size} bytes");
        }
        catch (Exception ex)
        {
            // Report any failure, typically due to exceeding QR capacity.
            Console.WriteLine($"Failed to encode long data with {mode} mode. Exception: {ex.Message}");
        }
    }
}