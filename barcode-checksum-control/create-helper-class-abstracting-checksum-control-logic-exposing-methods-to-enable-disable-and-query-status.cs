// Title: Barcode Checksum Helper Demo
// Description: Demonstrates using a helper class to control checksum generation for a barcode and saving the result as an image.
// Prompt: Create a helper class abstracting checksum control logic, exposing methods to enable, disable, and query status.
// Tags: barcode, checksum, helper, aspnet, csharp, aspose.barcode, code128, image

using System;
using Aspose.BarCode.Generation;

namespace BarcodeChecksumHelperDemo
{
    /// <summary>
    /// Provides static methods to enable, disable, and query the checksum setting of a <see cref="BarcodeGenerator"/>.
    /// </summary>
    public static class BarcodeChecksumHelper
    {
        // Enables checksum generation for the supplied generator.
        public static void SetChecksumOn(BarcodeGenerator generator)
        {
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
        }

        // Disables checksum generation for the supplied generator.
        public static void SetChecksumOff(BarcodeGenerator generator)
        {
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;
        }

        // Returns true if checksum is enabled; otherwise false.
        public static bool GetChecksumStatus(BarcodeGenerator generator)
        {
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            return generator.Parameters.Barcode.IsChecksumEnabled == EnableChecksum.Yes;
        }
    }

    class Program
    {
        /// <summary>
        /// Entry point of the demo application. Creates a barcode, toggles checksum settings, and saves the image.
        /// </summary>
        static void Main()
        {
            // Create a barcode generator for Code128 with sample text.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Initially disable checksum.
                BarcodeChecksumHelper.SetChecksumOff(generator);
                Console.WriteLine($"Checksum enabled? {BarcodeChecksumHelper.GetChecksumStatus(generator)}");

                // Enable checksum.
                BarcodeChecksumHelper.SetChecksumOn(generator);
                Console.WriteLine($"Checksum enabled? {BarcodeChecksumHelper.GetChecksumStatus(generator)}");

                // Save the barcode image to a file.
                string outputPath = "barcode.png";
                generator.Save(outputPath);
                Console.WriteLine($"Barcode saved to {outputPath}");
            }
        }
    }
}