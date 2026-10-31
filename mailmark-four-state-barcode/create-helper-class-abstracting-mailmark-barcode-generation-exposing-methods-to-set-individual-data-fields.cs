// Title: Mailmark 4-State Barcode Generation Helper
// Description: Demonstrates how to generate a UK Mailmark 4‑state barcode using Aspose.BarCode by encapsulating the codetext fields in a helper class.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of MailmarkCodetext and ComplexBarcodeGenerator classes to create Mailmark barcodes, a common requirement for postal automation and tracking solutions. Developers working with postal symbologies often need to set individual data fields, configure barcode dimensions, and export the result to image formats.
// Prompt: Create a helper class abstracting Mailmark barcode generation, exposing methods to set individual data fields.
// Tags: mailmark, barcode, generation, complexbarcode, csharp, aspose.barcode, png, helper

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

namespace MailmarkBarcodeDemo
{
    /// <summary>
    /// Helper class that abstracts the creation of a Mailmark 4‑state barcode.
    /// It provides strongly‑typed methods for setting each component of the Mailmark codetext.
    /// </summary>
    class MailmarkHelper
    {
        // Internal MailmarkCodetext instance that holds all barcode data fields.
        private readonly MailmarkCodetext _codetext = new MailmarkCodetext();

        /// <summary>
        /// Sets the Mailmark format. Only format 4 (Mailmark 4‑state) is supported.
        /// </summary>
        /// <param name="format">The format identifier (must be 4).</param>
        public void SetFormat(int format)
        {
            if (format != 4)
                throw new ArgumentException("Only format 4 (Mailmark 4-state) is supported.");
            _codetext.Format = format;
        }

        /// <summary>
        /// Sets the version identifier of the Mailmark.
        /// </summary>
        /// <param name="versionId">Version ID value.</param>
        public void SetVersionID(int versionId)
        {
            _codetext.VersionID = versionId;
        }

        /// <summary>
        /// Sets the class field of the Mailmark.
        /// </summary>
        /// <param name="classValue">Class value (non‑empty string).</param>
        public void SetClass(string classValue)
        {
            if (string.IsNullOrEmpty(classValue))
                throw new ArgumentException("Class cannot be null or empty.");
            _codetext.Class = classValue;
        }

        /// <summary>
        /// Sets the supply‑chain identifier.
        /// </summary>
        /// <param name="supplyChainId">Supply chain ID value.</param>
        public void SetSupplyChainID(int supplyChainId)
        {
            _codetext.SupplychainID = supplyChainId;
        }

        /// <summary>
        /// Sets the item identifier.
        /// </summary>
        /// <param name="itemId">Item ID value.</param>
        public void SetItemID(int itemId)
        {
            _codetext.ItemID = itemId;
        }

        /// <summary>
        /// Sets the destination postcode plus DPS (Delivery Point Suffix).
        /// Must be exactly 9 characters, including a trailing space if required.
        /// </summary>
        /// <param name="value">Postcode plus DPS string.</param>
        public void SetDestinationPostCodePlusDPS(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length != 9)
                throw new ArgumentException("DestinationPostCodePlusDPS must be exactly 9 characters, including trailing space.");
            _codetext.DestinationPostCodePlusDPS = value;
        }

        /// <summary>
        /// Generates the Mailmark barcode image and saves it to the specified path.
        /// </summary>
        /// <param name="outputPath">Full file path where the PNG image will be saved.</param>
        public void Generate(string outputPath)
        {
            if (string.IsNullOrEmpty(outputPath))
                throw new ArgumentException("Output path must be provided.");

            // Ensure the output directory exists.
            string directory = Path.GetDirectoryName(outputPath);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            // Create the barcode generator with the prepared codetext.
            using (var generator = new ComplexBarcodeGenerator(_codetext))
            {
                // Set the X‑dimension (module size) to 4 pixels for better readability.
                generator.Parameters.Barcode.XDimension.Pixels = 4;

                // Save the barcode as a PNG image.
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }
        }
    }

    class Program
    {
        /// <summary>
        /// Demonstrates the usage of <see cref="MailmarkHelper"/> to create a Mailmark 4‑state barcode.
        /// </summary>
        static void Main()
        {
            // Initialize the helper and configure all required Mailmark fields.
            var helper = new MailmarkHelper();
            helper.SetFormat(4);
            helper.SetVersionID(1);
            helper.SetClass("0");
            helper.SetSupplyChainID(384224);
            helper.SetItemID(16563762);
            helper.SetDestinationPostCodePlusDPS("EF61AH8T ");

            // Define the output file location (temporary folder).
            string outputFile = Path.Combine(Path.GetTempPath(), "Mailmark4State.png");

            // Generate and save the barcode image.
            helper.Generate(outputFile);

            // Inform the user where the image was saved.
            Console.WriteLine($"Mailmark barcode generated at: {outputFile}");
        }
    }
}