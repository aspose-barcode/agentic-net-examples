// Title: Read Barcodes from Files via Command Line
// Description: Demonstrates how to read barcodes from image files supplied as command‑line arguments, generating sample images when no arguments are provided.
// Category-Description: This example belongs to the Aspose.BarCode reading operations collection. It showcases the use of BarCodeReader and BarCodeResult to detect and extract barcode data from image files. Typical scenarios include batch processing of scanned documents, automated inventory checks, and validation of generated barcodes. Developers often need to iterate over multiple files, handle missing or unreadable images, and output results to the console or logs.
// Prompt: Develop a console application that reads barcodes from a list of file paths supplied via command line.
// Tags: barcode, read, command-line, aspose.barcode, qr, code128, datamatrix

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Console application that reads barcodes from image files specified on the command line.
/// If no arguments are supplied, it generates sample barcode images in a temporary folder
/// and then reads them back to demonstrate the workflow.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    /// <param name="args">Array of file paths to barcode images.</param>
    static void Main(string[] args)
    {
        // Collect file paths from command‑line arguments.
        List<string> files = new List<string>();

        if (args.Length > 0)
        {
            // Use the supplied arguments as file paths.
            foreach (string arg in args)
            {
                files.Add(arg);
            }
        }
        else
        {
            // No arguments supplied – create a temporary directory and generate sample barcodes.
            string tempDir = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(tempDir);

            var samples = new[]
            {
                new { Sym = EncodeTypes.QR, Text = "HelloQR", Name = "qr.png" },
                new { Sym = EncodeTypes.Code128, Text = "1234567890", Name = "code128.png" },
                new { Sym = EncodeTypes.DataMatrix, Text = "DM123", Name = "datamatrix.png" }
            };

            foreach (var s in samples)
            {
                string filePath = Path.Combine(tempDir, s.Name);
                // Generate and save each sample barcode image.
                using (BarcodeGenerator generator = new BarcodeGenerator(s.Sym, s.Text))
                {
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }
                files.Add(filePath);
            }
        }

        // Process each file: verify existence, read barcodes, and output results.
        foreach (string file in files)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                // Initialize the barcode reader for the current file.
                using (BarCodeReader reader = new BarCodeReader(file))
                {
                    bool any = false;
                    // Iterate over all detected barcodes.
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        any = true;
                        Console.WriteLine($"{Path.GetFileName(file)}: {result.CodeTypeName} - {result.CodeText}");
                    }
                    // If no barcodes were found, inform the user.
                    if (!any)
                    {
                        Console.WriteLine($"{Path.GetFileName(file)}: No barcodes detected.");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                // Handle cases where the file cannot be processed as a barcode image.
                Console.WriteLine($"Failed to read {file}: {ex.Message}");
            }
        }
    }
}