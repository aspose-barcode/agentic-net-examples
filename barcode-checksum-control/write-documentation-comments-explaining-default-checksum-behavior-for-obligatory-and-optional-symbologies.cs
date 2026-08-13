// Title: Default Checksum Behavior Demonstration
// Description: Shows how Aspose.BarCode handles default checksum generation for symbologies with obligatory and optional checksum requirements.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the default checksum settings for different barcode symbologies. It uses BarcodeGenerator, BarcodeParameters, and EnableChecksum to demonstrate how the library automatically adds a checksum for mandatory symbologies (e.g., EAN13) and omits it for optional ones (e.g., Code39FullASCII). Developers working with barcode creation often need to understand when checksums are applied automatically versus when they must be enabled manually.
// Prompt: Write documentation comments explaining default checksum behavior for obligatory and optional symbologies.
// Tags: barcode symbology checksum generation aspose.barcode ean13 code39fullascii

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace AsposeBarcodeChecksumDemo
{
    /// <summary>
    /// Demonstrates the default checksum behavior of Aspose.BarCode for
    /// symbologies where the checksum is obligatory and for those where it is optional.
    /// </summary>
    internal static class ChecksumDocumentation
    {
        /// <summary>
        /// Generates an EAN13 barcode. EAN13 requires a checksum, therefore the
        /// default value of <see cref="BarcodeParameters.IsChecksumEnabled"/> is
        /// <see cref="EnableChecksum.Yes"/> for this symbology. When the code text
        /// contains only the 12 data digits, Aspose.BarCode automatically calculates
        /// and appends the 13th checksum digit during generation.
        /// </summary>
        /// <param name="outputPath">File path where the barcode image will be saved.</param>
        public static void GenerateObligatoryChecksumBarcode(string outputPath)
        {
            // 12 digits without checksum – the library will add the 13th digit automatically.
            const string codeText = "123456789012";

            // Create a generator for EAN13 with the provided code text.
            using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, codeText))
            {
                // Explicitly set to Default to show that the library decides based on symbology.
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Default;
                // Save the generated barcode image to the specified path.
                generator.Save(outputPath);
            }
        }

        /// <summary>
        /// Generates a Code39FullASCII barcode. For this symbology the checksum is optional.
        /// The default value of <see cref="BarcodeParameters.IsChecksumEnabled"/> is
        /// <see cref="EnableChecksum.No"/> when the checksum is only possible.
        /// Consequently, the library does not add a checksum unless the property is set
        /// to <see cref="EnableChecksum.Yes"/>. The example leaves the setting at its
        /// default, so the resulting barcode contains no checksum digit.
        /// </summary>
        /// <param name="outputPath">File path where the barcode image will be saved.</param>
        public static void GenerateOptionalChecksumBarcode(string outputPath)
        {
            // Sample data for Code39FullASCII; checksum is optional.
            const string codeText = "ABC-123";

            // Create a generator for Code39FullASCII with the provided code text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, codeText))
            {
                // Keep the default (No) – checksum will not be generated.
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Default;
                // Save the generated barcode image to the specified path.
                generator.Save(outputPath);
            }
        }
    }

    /// <summary>
    /// Entry point for the checksum demonstration application.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Creates output directories, generates example barcodes, and writes their locations to the console.
        /// </summary>
        private static void Main()
        {
            // Determine a temporary folder for output files.
            string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeChecksumDemo");
            Directory.CreateDirectory(outputDir);

            // Define file paths for the generated barcode images.
            string ean13Path = Path.Combine(outputDir, "EAN13_DefaultChecksum.png");
            string code39Path = Path.Combine(outputDir, "Code39FullASCII_NoChecksum.png");

            // Generate an EAN13 barcode with the default (mandatory) checksum.
            ChecksumDocumentation.GenerateObligatoryChecksumBarcode(ean13Path);
            Console.WriteLine($"Generated EAN13 barcode with default checksum at: {ean13Path}");

            // Generate a Code39FullASCII barcode without a checksum (optional).
            ChecksumDocumentation.GenerateOptionalChecksumBarcode(code39Path);
            Console.WriteLine($"Generated Code39FullASCII barcode without checksum at: {code39Path}");
        }
    }
}