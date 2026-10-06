// Title: Mailmark Barcode Generation Helper Example
// Description: Demonstrates how to generate a Mailmark barcode using Aspose.BarCode by configuring individual data fields through a helper class.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, focusing on Mailmark symbology. It shows the use of MailmarkCodetext and ComplexBarcodeGenerator classes to build and render a Mailmark barcode, a common requirement for postal automation and tracking solutions. Developers often need to set specific Mailmark fields such as format, version, class, supply chain ID, item ID, and destination postcode before rendering the barcode to an image.
// Prompt: Create a helper class abstracting Mailmark barcode generation, exposing methods to set individual data fields.
// Tags: mailmark, barcode, generation, complexbarcode, aspnet, csharp, aspose.barcode, image-output

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

namespace MailmarkDemoApp
{
    /// <summary>
    /// Helper class for constructing and generating Mailmark barcodes.
    /// </summary>
    public class MailmarkHelper
    {
        // Internal Mailmark codetext object that holds all field values.
        private readonly MailmarkCodetext _code = new MailmarkCodetext();

        /// <summary>
        /// Sets the Mailmark format (e.g., 4-state, 2-state).
        /// </summary>
        /// <param name="format">Integer representing the format.</param>
        public void SetFormat(int format)
        {
            _code.Format = format;
        }

        /// <summary>
        /// Sets the version identifier of the Mailmark.
        /// </summary>
        /// <param name="versionId">Integer version ID.</param>
        public void SetVersionID(int versionId)
        {
            _code.VersionID = versionId;
        }

        /// <summary>
        /// Sets the class value of the Mailmark.
        /// </summary>
        /// <param name="classValue">String representing the class.</param>
        public void SetClass(string classValue)
        {
            _code.Class = classValue;
        }

        /// <summary>
        /// Sets the supply chain identifier.
        /// </summary>
        /// <param name="id">Integer supply chain ID.</param>
        public void SetSupplyChainID(int id)
        {
            _code.SupplychainID = id;
        }

        /// <summary>
        /// Sets the item identifier.
        /// </summary>
        /// <param name="id">Integer item ID.</param>
        public void SetItemID(int id)
        {
            _code.ItemID = id;
        }

        /// <summary>
        /// Sets the destination postcode plus DPS (Delivery Point Suffix).
        /// </summary>
        /// <param name="value">String containing postcode and DPS.</param>
        public void SetDestinationPostCodePlusDPS(string value)
        {
            _code.DestinationPostCodePlusDPS = value;
        }

        /// <summary>
        /// Generates the Mailmark barcode image and saves it to the specified path.
        /// </summary>
        /// <param name="filePath">Full file path where the PNG image will be saved.</param>
        public void Generate(string filePath)
        {
            // Create a generator using the configured Mailmark codetext.
            using (var generator = new ComplexBarcodeGenerator(_code))
            {
                // Set the X-dimension (module size) in pixels for better readability.
                generator.Parameters.Barcode.XDimension.Pixels = 4f;

                // Save the generated barcode as a PNG image.
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }
    }

    class Program
    {
        /// <summary>
        /// Entry point that demonstrates usage of MailmarkHelper.
        /// </summary>
        /// <param name="args">Command-line arguments (not used).</param>
        static void Main(string[] args)
        {
            // Prepare a temporary directory for the output image.
            string outputDir = Path.Combine(Path.GetTempPath(), "MailmarkDemo");
            Directory.CreateDirectory(outputDir);
            string outputPath = Path.Combine(outputDir, "Mailmark4State.png");

            // Instantiate the helper and configure Mailmark fields.
            var helper = new MailmarkHelper();
            helper.SetFormat(4);
            helper.SetVersionID(1);
            helper.SetClass("0");
            helper.SetSupplyChainID(384224);
            helper.SetItemID(16563762);
            helper.SetDestinationPostCodePlusDPS("EF61AH8T ");

            // Generate the barcode and save it to the file system.
            helper.Generate(outputPath);

            // Inform the user where the barcode image was saved.
            Console.WriteLine($"Mailmark barcode generated at: {outputPath}");
        }
    }
}