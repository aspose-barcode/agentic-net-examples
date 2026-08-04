// Title: Mailmark Barcode Generation Helper Example
// Description: Demonstrates how to use Aspose.BarCode to generate a Mailmark 4‑state barcode by configuring individual data fields through a helper class.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, focusing on the Mailmark symbology. It showcases the MailmarkCodetext and ComplexBarcodeGenerator classes, typical for creating postal barcodes with custom data fields such as format, version ID, class, supply chain ID, item ID, and destination postcode plus DPS. Developers working with postal automation, logistics, or mailing solutions often need to generate Mailmark barcodes programmatically.
// Prompt: Create a helper class abstracting Mailmark barcode generation, exposing methods to set individual data fields.
// Tags: mailmark, barcode, generation, complexbarcode, aspose.barcode, csharp

using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

namespace MailmarkDemo
{
    /// <summary>
    /// Helper class for building and generating Mailmark barcodes.
    /// Provides methods to set each required data field before creating the barcode image.
    /// </summary>
    public class MailmarkHelper
    {
        // Underlying Mailmark codetext object that holds all field values.
        private readonly MailmarkCodetext _mailmark;

        /// <summary>
        /// Initializes a new instance of the <see cref="MailmarkHelper"/> class.
        /// </summary>
        public MailmarkHelper()
        {
            _mailmark = new MailmarkCodetext();
        }

        // Set the 4‑state format (must be 4 for Mailmark)
        public void SetFormat(int format)
        {
            if (format != 4)
                throw new ArgumentException("Mailmark format must be 4 for 4‑state barcodes.");
            _mailmark.Format = format;
        }

        // Set version ID (typically 1)
        public void SetVersionID(int versionId)
        {
            if (versionId <= 0)
                throw new ArgumentOutOfRangeException(nameof(versionId));
            _mailmark.VersionID = versionId;
        }

        // Set class (single‑character string, e.g., "0")
        public void SetClass(string classValue)
        {
            if (string.IsNullOrEmpty(classValue) || classValue.Length != 1)
                throw new ArgumentException("Class must be a single character string.");
            _mailmark.Class = classValue;
        }

        // Set supply chain ID (int)
        public void SetSupplychainID(int supplychainId)
        {
            if (supplychainId < 0)
                throw new ArgumentOutOfRangeException(nameof(supplychainId));
            _mailmark.SupplychainID = supplychainId;
        }

        // Set item ID (int)
        public void SetItemID(int itemId)
        {
            if (itemId < 0)
                throw new ArgumentOutOfRangeException(nameof(itemId));
            _mailmark.ItemID = itemId;
        }

        // Set destination postcode plus DPS (must retain trailing space)
        public void SetDestinationPostCodePlusDPS(string postcodePlusDps)
        {
            if (string.IsNullOrEmpty(postcodePlusDps) || !postcodePlusDps.EndsWith(" "))
                throw new ArgumentException("DestinationPostCodePlusDPS must end with a trailing space.");
            _mailmark.DestinationPostCodePlusDPS = postcodePlusDps;
        }

        // Generate the barcode image and save to the specified file path
        public void Generate(string outputPath)
        {
            if (string.IsNullOrWhiteSpace(outputPath))
                throw new ArgumentException("Output path must be a valid file name.", nameof(outputPath));

            // Ensure required fields are set (basic validation)
            if (_mailmark.Format != 4)
                throw new InvalidOperationException("Format must be set to 4 before generation.");
            if (string.IsNullOrEmpty(_mailmark.DestinationPostCodePlusDPS))
                throw new InvalidOperationException("DestinationPostCodePlusDPS must be set before generation.");

            // Create the generator with the configured Mailmark codetext and save the image.
            using (var generator = new ComplexBarcodeGenerator(_mailmark))
            {
                generator.Save(outputPath);
            }
        }
    }

    class Program
    {
        /// <summary>
        /// Entry point of the demo application. Configures a MailmarkHelper instance,
        /// sets all required fields, generates the barcode, and writes the output path to the console.
        /// </summary>
        static void Main()
        {
            var helper = new MailmarkHelper();

            // Configure Mailmark fields
            helper.SetFormat(4);                     // 4‑state Mailmark
            helper.SetVersionID(1);                  // version
            helper.SetClass("0");                    // class "0"
            helper.SetSupplychainID(384224);         // example supply chain ID
            helper.SetItemID(16563762);              // example item ID
            helper.SetDestinationPostCodePlusDPS("EF61AH8T "); // trailing space required

            // Generate barcode image
            string outputFile = "mailmark.png";
            helper.Generate(outputFile);

            Console.WriteLine($"Mailmark barcode saved to {outputFile}");
        }
    }
}