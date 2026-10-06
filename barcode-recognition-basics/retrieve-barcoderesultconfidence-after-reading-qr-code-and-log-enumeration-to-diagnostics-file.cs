// Title: Read QR Code and Log Confidence
// Description: This example generates a QR code image, reads it back using Aspose.BarCode, retrieves the confidence level of the detection, and writes the result to a diagnostics log file.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition APIs. It uses BarcodeGenerator to create a QR code, BarCodeReader to decode it, and accesses BarCodeResult.Confidence to assess detection reliability. Developers working with barcode scanning, quality assessment, or logging diagnostic information commonly use these classes to validate and monitor barcode processing pipelines.
// Prompt: Retrieve BarCodeResult.Confidence after reading a QR code and log the enumeration to a diagnostics file.
// Tags: qr code, barcode generation, barcode recognition, confidence, diagnostics, logging, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a QR code, reading it, retrieving confidence, and logging diagnostics.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR code, reads it, logs confidence to a file.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define paths for the QR code image and the diagnostics log
        string barcodePath = Path.Combine(tempFolder, "qr.png");
        string diagnosticsPath = Path.Combine(tempFolder, "diagnostics.log");

        // Generate a QR code image and save it as PNG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Text"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the QR code image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to generate barcode image.");
            return;
        }

        // Read the QR code, retrieve confidence, and log the information
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            BarCodeResult[] results = reader.ReadBarCodes();
            foreach (BarCodeResult result in results)
            {
                BarCodeConfidence confidence = result.Confidence;
                string logLine = $"File: {barcodePath}, Confidence: {confidence}";
                File.AppendAllText(diagnosticsPath, logLine + Environment.NewLine);
                Console.WriteLine(logLine);
            }
        }

        // Inform the user where the diagnostics log was written
        Console.WriteLine($"Diagnostics written to: {diagnosticsPath}");
    }
}