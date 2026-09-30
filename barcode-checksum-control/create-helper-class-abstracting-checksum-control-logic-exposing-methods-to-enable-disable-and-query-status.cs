// Title: Demonstrate checksum control for Code39 barcode using Aspose.BarCode
// Description: Shows how to enable, disable, and query the checksum setting of a barcode generator and save the result as an image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating manipulation of the IsChecksumEnabled property via the BarcodeGenerator.Parameters.Barcode API. Developers often need to toggle checksum calculation for symbologies such as Code39 to meet validation requirements or reduce data size. The sample demonstrates typical use cases: checking the default state, turning the checksum on or off, and persisting the barcode image.
// Prompt: Create a helper class abstracting checksum control logic, exposing methods to enable, disable, and query status.
// Tags: barcode, checksum, code39, generation, aspnet, aspose.barcode, image, png

using System;
using System.IO;
using Aspose.BarCode.Generation;

namespace ChecksumHelperDemo
{
    /// <summary>
    /// Provides static helper methods to control the checksum setting of a <see cref="BarcodeGenerator"/>.
    /// </summary>
    public static class ChecksumHelper
    {
        // Enable checksum calculation
        public static void SetChecksumOn(BarcodeGenerator generator)
        {
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
        }

        // Disable checksum calculation
        public static void SetChecksumOff(BarcodeGenerator generator)
        {
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;
        }

        // Query current checksum status
        public static EnableChecksum GetChecksumStatus(BarcodeGenerator generator)
        {
            return generator.Parameters.Barcode.IsChecksumEnabled;
        }
    }

    class Program
    {
        /// <summary>
        /// Demonstrates using <see cref="ChecksumHelper"/> with a Code39 barcode and saves the image.
        /// </summary>
        static void Main()
        {
            // Use Code39 which allows checksum to be turned on or off
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "12345"))
            {
                // Display the initial checksum status
                Console.WriteLine($"Initial checksum status: {ChecksumHelper.GetChecksumStatus(generator)}");

                // Enable checksum and display the updated status
                ChecksumHelper.SetChecksumOn(generator);
                Console.WriteLine($"After enabling: {ChecksumHelper.GetChecksumStatus(generator)}");

                // Disable checksum and display the updated status
                ChecksumHelper.SetChecksumOff(generator);
                Console.WriteLine($"After disabling: {ChecksumHelper.GetChecksumStatus(generator)}");

                // Save the barcode image to a temporary file
                string outputPath = Path.Combine(Path.GetTempPath(), "checksum_demo.png");
                using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    generator.Save(fileStream, BarCodeImageFormat.Png);
                }
                Console.WriteLine($"Barcode saved to: {outputPath}");
            }
        }
    }
}