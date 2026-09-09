// Title: Mailmark 4-State Barcode Generation Helper
// Description: Demonstrates how to generate a Mailmark 4‑state barcode using Aspose.BarCode by configuring individual data fields through a helper class.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the MailmarkCodetext model and ComplexBarcodeGenerator classes, which are commonly used to create high‑security Mailmark barcodes for postal services. Developers often need to set specific fields such as format, version ID, class, and destination postcode before rendering the barcode to an image.
// Prompt: Create a helper class abstracting Mailmark barcode generation, exposing methods to set individual data fields.
// Tags: mailmark, barcode, generation, complexbarcode, aspose.barcode, png, csharp

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

namespace MailmarkDemo
{
    /// <summary>
    /// Provides a simple wrapper around <see cref="MailmarkCodetext"/> and <see cref="ComplexBarcodeGenerator"/>
    /// to configure and generate a Mailmark 4‑state barcode.
    /// </summary>
    public class MailmarkHelper
    {
        // Internal representation of the Mailmark data fields.
        private readonly MailmarkCodetext _codetext = new MailmarkCodetext();

        /// <summary>
        /// Sets the Mailmark format. Only the 4‑state format (value 4) is supported.
        /// </summary>
        /// <param name="format">The format identifier; must be 4.</param>
        public void SetFormat(int format)
        {
            if (format != 4)
                throw new ArgumentException("Mailmark 4-state format must be 4.", nameof(format));
            _codetext.Format = format;
        }

        /// <summary>
        /// Sets the version identifier of the Mailmark.
        /// </summary>
        /// <param name="versionId">A non‑negative integer representing the version.</param>
        public void SetVersionID(int versionId)
        {
            if (versionId < 0)
                throw new ArgumentOutOfRangeException(nameof(versionId));
            _codetext.VersionID = versionId;
        }

        /// <summary>
        /// Sets the class field of the Mailmark.
        /// </summary>
        /// <param name="classValue">A non‑empty string representing the class.</param>
        public void SetClass(string classValue)
        {
            if (string.IsNullOrEmpty(classValue))
                throw new ArgumentException("Class cannot be null or empty.", nameof(classValue));
            _codetext.Class = classValue;
        }

        /// <summary>
        /// Sets the supply‑chain identifier.
        /// </summary>
        /// <param name="supplyChainId">A non‑negative integer representing the supply chain ID.</param>
        public void SetSupplyChainID(int supplyChainId)
        {
            if (supplyChainId < 0)
                throw new ArgumentOutOfRangeException(nameof(supplyChainId));
            _codetext.SupplychainID = supplyChainId;
        }

        /// <summary>
        /// Sets the item identifier.
        /// </summary>
        /// <param name="itemId">A non‑negative integer representing the item ID.</param>
        public void SetItemID(int itemId)
        {
            if (itemId < 0)
                throw new ArgumentOutOfRangeException(nameof(itemId));
            _codetext.ItemID = itemId;
        }

        /// <summary>
        /// Sets the destination postcode plus DPS (Delivery Point Suffix).
        /// </summary>
        /// <param name="value">A non‑empty string containing the postcode and DPS.</param>
        public void SetDestinationPostCodePlusDPS(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("DestinationPostCodePlusDPS cannot be null or empty.", nameof(value));
            _codetext.DestinationPostCodePlusDPS = value;
        }

        /// <summary>
        /// Generates the Mailmark barcode image and saves it to the specified path.
        /// </summary>
        /// <param name="outputPath">Full file path where the PNG image will be saved.</param>
        public void Generate(string outputPath)
        {
            if (string.IsNullOrEmpty(outputPath))
                throw new ArgumentException("Output path cannot be null or empty.", nameof(outputPath));

            // Ensure the output directory exists.
            string directory = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            // Create the generator, configure X‑dimension, and save as PNG.
            using (var generator = new ComplexBarcodeGenerator(_codetext))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 4;
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }
        }
    }

    class Program
    {
        /// <summary>
        /// Entry point of the demo application. Configures a MailmarkHelper instance,
        /// generates a 4‑state Mailmark barcode, and writes the output location to the console.
        /// </summary>
        static void Main()
        {
            // Prepare a temporary output folder.
            string outputFolder = Path.Combine(Path.GetTempPath(), "MailmarkDemo_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(outputFolder);
            string outputFile = Path.Combine(outputFolder, "Mailmark4State.png");

            // Create helper and set required Mailmark fields.
            var helper = new MailmarkHelper();
            helper.SetFormat(4);
            helper.SetVersionID(1);
            helper.SetClass("0");
            helper.SetSupplyChainID(384224);
            helper.SetItemID(16563762);
            helper.SetDestinationPostCodePlusDPS("EF61AH8T ");

            // Generate the barcode image.
            helper.Generate(outputFile);

            // Inform the user where the image was saved.
            Console.WriteLine($"Mailmark 4-state barcode generated at: {outputFile}");
        }
    }
}