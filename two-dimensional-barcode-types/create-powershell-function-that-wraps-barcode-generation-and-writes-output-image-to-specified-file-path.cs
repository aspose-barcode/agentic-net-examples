using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeGeneratorApp
{
    /// <summary>
    /// Entry point for the Barcode Generator console application.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Application entry point. Parses optional command‑line arguments and generates a barcode.
        /// </summary>
        /// <param name="args">
        /// Optional arguments:
        /// 0 - symbology name (e.g., "Code128")
        /// 1 - text to encode
        /// 2 - output file path
        /// </param>
        static void Main(string[] args)
        {
            // Default values for symbology, text, and output file
            string symbologyName = "Code128";
            string codeText = "123ABC";
            string outputPath = "barcode.png";

            // Override defaults with command‑line arguments if they are provided and not empty
            if (args.Length >= 1 && !string.IsNullOrWhiteSpace(args[0]))
                symbologyName = args[0];
            if (args.Length >= 2 && !string.IsNullOrWhiteSpace(args[1]))
                codeText = args[1];
            if (args.Length >= 3 && !string.IsNullOrWhiteSpace(args[2]))
                outputPath = args[2];

            try
            {
                // Generate the barcode image and save it to the specified path
                GenerateBarcode(symbologyName, codeText, outputPath);
                // Inform the user where the file was saved
                Console.WriteLine($"Barcode saved to: {Path.GetFullPath(outputPath)}");
            }
            catch (Exception ex)
            {
                // Output any errors that occurred during generation
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Generates a barcode image using Aspose.BarCode and saves it to the specified file.
        /// </summary>
        /// <param name="symbologyName">Name of the barcode symbology (e.g., "Code128", "QR").</param>
        /// <param name="codeText">Text to encode.</param>
        /// <param name="outputPath">File path where the image will be saved.</param>
        static void GenerateBarcode(string symbologyName, string codeText, string outputPath)
        {
            // Resolve the symbology name to a BaseEncodeType via reflection (EncodeTypes is a static class)
            FieldInfo field = typeof(EncodeTypes).GetField(symbologyName);
            if (field == null)
                throw new ArgumentException($"Unknown symbology: {symbologyName}");

            // Retrieve the actual encode type value from the field
            BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);
            if (encodeType == null)
                throw new ArgumentException($"Failed to obtain encode type for symbology: {symbologyName}");

            // Ensure the output directory exists; create it if necessary
            string directory = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            // Create a barcode generator, configure it, and save the image
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Example: set resolution (optional, 300 DPI)
                generator.Parameters.Resolution = 300f;

                // Save the barcode as PNG (default format) to the specified path
                generator.Save(outputPath);
            }
        }
    }
}