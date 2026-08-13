// Title: Code 39 Barcode Generation with Optional Checksum
// Description: Demonstrates how to generate Code 39 barcodes using Aspose.BarCode with the checksum either enabled or disabled.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and checksum settings. Developers often need to create Code 39 barcodes for inventory or tracking systems, and may require control over checksum inclusion for validation purposes. The snippet shows typical configuration steps and saving the output as PNG images.
// Prompt: Produce a sample project demonstrating generation of Code 39 barcodes with optional checksum both enabled and disabled.
// Tags: barcode symbology, checksum, code39, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace Code39ChecksumDemo
{
    /// <summary>
    /// Demonstrates generation of Code 39 barcodes with checksum enabled and disabled using Aspose.BarCode.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the sample. Creates output folder, generates two barcodes, and writes their file paths to the console.
        /// </summary>
        static void Main()
        {
            // Ensure the output directory exists
            string outputDir = "Barcodes";
            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            // ---------- Generate Code 39 barcode with checksum enabled ----------
            string withChecksumPath = Path.Combine(outputDir, "code39_checksum.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, "CODE39"))
            {
                // Enable checksum calculation and display the checksum character
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
                generator.Parameters.Barcode.ChecksumAlwaysShow = true;
                // Save the barcode image as PNG
                generator.Save(withChecksumPath);
            }

            // ---------- Generate Code 39 barcode with checksum disabled ----------
            string withoutChecksumPath = Path.Combine(outputDir, "code39_no_checksum.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, "CODE39"))
            {
                // Disable checksum calculation
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;
                // Save the barcode image as PNG
                generator.Save(withoutChecksumPath);
            }

            // Output the locations of the generated barcode images
            Console.WriteLine($"Barcode with checksum saved to: {withChecksumPath}");
            Console.WriteLine($"Barcode without checksum saved to: {withoutChecksumPath}");
        }
    }
}