// Title: Checksum Control Helper for Aspose.BarCode
// Description: Demonstrates enabling, disabling, and querying the checksum of a Code39 barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to manipulate checksum settings via the BarcodeGenerator.Parameters.Barcode.IsChecksumEnabled property. It highlights the use of EnableChecksum enum, BarcodeGenerator, and image saving APIs—common tasks for developers creating barcodes that require optional checksum validation.
// Prompt: Create a helper class abstracting checksum control logic, exposing methods to enable, disable, and query status.
// Tags: barcode, checksum, code39, generation, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

namespace ChecksumControlDemo
{
    /// <summary>
    /// Provides methods to enable, disable, and query the checksum status of a <see cref="BarcodeGenerator"/>.
    /// </summary>
    public static class ChecksumHelper
    {
        /// <summary>
        /// Enables checksum calculation for the specified barcode generator.
        /// </summary>
        /// <param name="generator">The <see cref="BarcodeGenerator"/> whose checksum should be enabled.</param>
        public static void SetChecksumOn(BarcodeGenerator generator)
        {
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
        }

        /// <summary>
        /// Disables checksum calculation for the specified barcode generator.
        /// </summary>
        /// <param name="generator">The <see cref="BarcodeGenerator"/> whose checksum should be disabled.</param>
        public static void SetChecksumOff(BarcodeGenerator generator)
        {
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;
        }

        /// <summary>
        /// Retrieves the current checksum status of the specified barcode generator.
        /// </summary>
        /// <param name="generator">The <see cref="BarcodeGenerator"/> to query.</param>
        /// <returns>The <see cref="EnableChecksum"/> value indicating whether checksum is enabled.</returns>
        public static EnableChecksum GetChecksumStatus(BarcodeGenerator generator)
        {
            return generator.Parameters.Barcode.IsChecksumEnabled;
        }
    }

    class Program
    {
        /// <summary>
        /// Generates a Code39 barcode with and without checksum, saves the images to a temporary folder,
        /// and writes status information to the console.
        /// </summary>
        static void Main()
        {
            // Create a unique temporary directory for the demo output
            string tempDir = Path.Combine(Path.GetTempPath(), "ChecksumDemo_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(tempDir);

            // Define file paths for the two barcode images
            string pathNoChecksum = Path.Combine(tempDir, "Code39_NoChecksum.png");
            string pathWithChecksum = Path.Combine(tempDir, "Code39_WithChecksum.png");

            // Initialize a barcode generator for Code39 (checksum optional)
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39, "12345"))
            {
                // Disable checksum, save the image, and display status
                ChecksumHelper.SetChecksumOff(generator);
                generator.Save(pathNoChecksum, BarCodeImageFormat.Png);
                Console.WriteLine($"Saved barcode without checksum to: {pathNoChecksum}");
                Console.WriteLine($"Checksum status: {ChecksumHelper.GetChecksumStatus(generator)}");

                // Enable checksum, save the image, and display status
                ChecksumHelper.SetChecksumOn(generator);
                generator.Save(pathWithChecksum, BarCodeImageFormat.Png);
                Console.WriteLine($"Saved barcode with checksum to: {pathWithChecksum}");
                Console.WriteLine($"Checksum status: {ChecksumHelper.GetChecksumStatus(generator)}");
            }

            // Optional: clean up the temporary directory
            // Directory.Delete(tempDir, true);
        }
    }
}