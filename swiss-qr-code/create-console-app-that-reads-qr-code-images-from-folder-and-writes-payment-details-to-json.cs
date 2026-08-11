// Title: Read QR Codes from Folder and Export Payment Details to JSON
// Description: Demonstrates reading QR code images from a directory, extracting embedded payment information, and writing the results to a formatted JSON file.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It shows how to use BarcodeGenerator to create QR codes and BarCodeReader to decode them. Typical scenarios include batch processing of barcode images for data extraction, such as payment records, inventory tags, or authentication tokens. Developers often need to combine barcode decoding with standard .NET I/O and serialization APIs to integrate barcode data into business workflows.
// Prompt: Create a console app that reads QR code images from a folder and writes payment details to JSON.
// Tags: qr, barcode, reading, json, aspose.barcode, payment, console

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Console application that scans QR code images in a folder, extracts payment information,
/// and writes the collected data to a JSON file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Sets up paths, ensures sample data exists,
    /// processes QR codes, and outputs the results as JSON.
    /// </summary>
    static void Main()
    {
        // Define input folder (relative to the current working directory) and output JSON file path.
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "InputBarcodes");
        string outputFile = Path.Combine(Directory.GetCurrentDirectory(), "paymentDetails.json");

        // Ensure the input folder exists; create it if it does not.
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }

        // If the folder is empty, generate a sample QR code containing dummy payment data.
        SeedSampleQrIfNeeded(inputFolder);

        // Read all QR code images from the folder and collect payment details.
        List<PaymentInfo> payments = ReadQrCodesFromFolder(inputFolder);

        // Serialize the payment list to a pretty‑printed JSON string.
        string json = JsonSerializer.Serialize(payments, new JsonSerializerOptions { WriteIndented = true });

        // Write the JSON output to the designated file.
        File.WriteAllText(outputFile, json);

        // Inform the user about the processing result.
        Console.WriteLine($"Processed {payments.Count} QR code(s). Output written to '{outputFile}'.");
    }

    /// <summary>
    /// Generates a sample QR code image with dummy payment data if the input folder contains no PNG files.
    /// </summary>
    /// <param name="folder">The folder where the sample QR code should be saved.</param>
    private static void SeedSampleQrIfNeeded(string folder)
    {
        // Check for existing PNG files; if any are found, skip seeding.
        string[] existingFiles = Directory.GetFiles(folder, "*.png");
        if (existingFiles.Length > 0) return;

        // Define the sample file path and the dummy payment payload.
        string samplePath = Path.Combine(folder, "samplePayment.png");
        string sampleData = "PAYMENT|ID=12345|AMOUNT=99.99|CURRENCY=USD|DATE=2023-01-01";

        // Create a QR code image using Aspose.BarCode's BarcodeGenerator.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, sampleData))
        {
            // Set QR encoding mode to automatic detection (optional but explicit).
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Auto;

            // Save the generated QR code as a PNG file.
            generator.Save(samplePath, BarCodeImageFormat.Png);
        }
    }

    /// <summary>
    /// Scans the specified folder for supported image files, decodes QR codes,
    /// and returns a list of extracted payment information.
    /// </summary>
    /// <param name="folder">The directory containing QR code images.</param>
    /// <returns>A list of <see cref="PaymentInfo"/> objects representing each decoded QR code.</returns>
    private static List<PaymentInfo> ReadQrCodesFromFolder(string folder)
    {
        var result = new List<PaymentInfo>();

        // Supported image file patterns.
        string[] patterns = new[] { "*.png", "*.jpg", "*.bmp" };
        var files = new List<string>();

        // Collect all matching files from the folder.
        foreach (var pattern in patterns)
        {
            if (Directory.Exists(folder))
            {
                files.AddRange(Directory.GetFiles(folder, pattern));
            }
        }

        // Process each image file.
        foreach (string filePath in files)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            // Use Aspose.BarCode's BarCodeReader to decode QR codes in the image.
            using (var reader = new BarCodeReader(filePath, DecodeType.QR))
            {
                foreach (var resultItem in reader.ReadBarCodes())
                {
                    var info = new PaymentInfo
                    {
                        FileName = Path.GetFileName(filePath),
                        CodeText = resultItem.CodeText ?? string.Empty
                    };
                    result.Add(info);
                }
            }
        }

        return result;
    }

    /// <summary>
    /// Simple data transfer object representing payment information extracted from a QR code.
    /// </summary>
    private class PaymentInfo
    {
        /// <summary>
        /// Name of the image file containing the QR code.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Raw text decoded from the QR code.
        /// </summary>
        public string CodeText { get; set; }
    }
}