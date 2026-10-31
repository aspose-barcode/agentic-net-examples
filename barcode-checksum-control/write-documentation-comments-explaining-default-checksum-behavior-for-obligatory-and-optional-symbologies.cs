// Title: Demonstrate default checksum settings for optional and obligatory barcode symbologies
// Description: This example generates barcodes for Code39 (optional checksum) and Code128 (obligatory checksum) and shows the default IsChecksumEnabled values.
// Category-Description: Shows how Aspose.BarCode handles checksum defaults across different symbologies. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Developers working with barcode generation often need to know when checksums are applied automatically versus when they must be enabled manually. This snippet belongs to the barcode generation and configuration category.
// Prompt: Write documentation comments explaining default checksum behavior for obligatory and optional symbologies.
// Tags: barcode symbology, checksum, code39, code128, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

namespace ChecksumDocumentationDemo
{
    /// <summary>
    /// Provides helper methods that demonstrate the default checksum behavior
    /// for barcode symbologies with optional and obligatory checksum requirements.
    /// </summary>
    public static class ChecksumHelper
    {
        /// <summary>
        /// Generates sample barcodes for an optional-checksum symbology (Code39) and an
        /// obligatory-checksum symbology (Code128) and prints the default
        /// <c>IsChecksumEnabled</c> setting for each.
        /// <para>
        /// According to Aspose.BarCode documentation, the <c>IsChecksumEnabled</c>
        /// property defaults to <c>EnableChecksum.No</c> for symbologies where the
        /// checksum is optional, and to <c>EnableChecksum.Yes</c> for symbologies
        /// where the checksum is obligatory.
        /// </para>
        /// </summary>
        /// <param name="outputFolder">Folder where generated barcode images will be saved.</param>
        public static void ShowDefaultChecksumSettings(string outputFolder)
        {
            // Ensure the output directory exists.
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            // Optional checksum symbology: Code39
            using (var genOptional = new BarcodeGenerator(EncodeTypes.Code39, "12345"))
            {
                string optionalPath = Path.Combine(outputFolder, "Code39_Optional.png");
                genOptional.Save(optionalPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Generated optional-checksum barcode: {optionalPath}");
                Console.WriteLine($"Default IsChecksumEnabled for Code39: {genOptional.Parameters.Barcode.IsChecksumEnabled}");
            }

            // Obligatory checksum symbology: Code128
            using (var genObligatory = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
            {
                string obligatoryPath = Path.Combine(outputFolder, "Code128_Obligatory.png");
                genObligatory.Save(obligatoryPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Generated obligatory-checksum barcode: {obligatoryPath}");
                Console.WriteLine($"Default IsChecksumEnabled for Code128: {genObligatory.Parameters.Barcode.IsChecksumEnabled}");
            }
        }
    }

    /// <summary>
    /// Entry point for the checksum demonstration application.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Creates a temporary folder, runs the checksum demo, and writes completion status to the console.
        /// </summary>
        static void Main()
        {
            // Use a temporary folder for demonstration purposes.
            string tempFolder = Path.Combine(Path.GetTempPath(), "ChecksumDemo_" + Guid.NewGuid().ToString("N"));
            ChecksumHelper.ShowDefaultChecksumSettings(tempFolder);
            Console.WriteLine("Demo completed.");
        }
    }
}