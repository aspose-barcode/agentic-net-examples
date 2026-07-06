// Title: Mailmark 4‑state barcode generation helper example
// Description: Demonstrates how to build a Mailmark barcode by setting individual data fields and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, showcasing the MailmarkCodetext and ComplexBarcodeGenerator classes. Developers use these APIs to create UK Mailmark 4‑state barcodes for postal automation, needing to configure format, version, class, supply chain ID, item ID, and destination postcode. The snippet serves as a reference for building similar barcode helpers.
// Prompt: Create a helper class abstracting Mailmark barcode generation, exposing methods to set individual data fields.
// Tags: mailmark, barcode, generation, png, aspnet, aspose.barcode, complexbarcode, helper

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

namespace MailmarkDemo
{
    /// <summary>
    /// Helper class for building Mailmark 4‑state barcodes using Aspose.BarCode.
    /// Provides methods to set each component of the Mailmark codetext and to generate the barcode image.
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

        // Set the format (1 = Letter, 2 = Large Letter, 4 = unspecified/default)
        public void SetFormat(int format)
        {
            if (format != 1 && format != 2 && format != 4)
                throw new ArgumentOutOfRangeException(nameof(format), "Format must be 1, 2, or 4.");
            _mailmark.Format = format;
        }

        // Set version identifier (typically 1)
        public void SetVersionID(int versionId)
        {
            if (versionId < 0)
                throw new ArgumentOutOfRangeException(nameof(versionId), "VersionID must be non‑negative.");
            _mailmark.VersionID = versionId;
        }

        // Set the service class (string values "0"‑"9")
        public void SetClass(string serviceClass)
        {
            if (string.IsNullOrEmpty(serviceClass) || serviceClass.Length != 1)
                throw new ArgumentException("Class must be a single character string.", nameof(serviceClass));
            _mailmark.Class = serviceClass;
        }

        // Set the supply chain identifier (max 999999 for large letters)
        public void SetSupplychainID(int supplychainId)
        {
            if (supplychainId < 0)
                throw new ArgumentOutOfRangeException(nameof(supplychainId), "SupplychainID must be non‑negative.");
            _mailmark.SupplychainID = supplychainId;
        }

        // Set the unique item identifier (max 99 999 999)
        public void SetItemID(int itemId)
        {
            if (itemId < 0 || itemId > 99999999)
                throw new ArgumentOutOfRangeException(nameof(itemId), "ItemID must be between 0 and 99,999,999.");
            _mailmark.ItemID = itemId;
        }

        // Set the destination postcode plus DPS (exact 9‑character format, trailing spaces allowed)
        public void SetDestinationPostCodePlusDPS(string postcodePlusDps)
        {
            if (string.IsNullOrWhiteSpace(postcodePlusDps) || postcodePlusDps.Length != 9)
                throw new ArgumentException("DestinationPostCodePlusDPS must be a 9‑character string.", nameof(postcodePlusDps));
            _mailmark.DestinationPostCodePlusDPS = postcodePlusDps;
        }

        // Generate the barcode image and save to the specified file path
        public void Generate(string outputPath)
        {
            if (string.IsNullOrWhiteSpace(outputPath))
                throw new ArgumentException("Output path cannot be null or empty.", nameof(outputPath));

            // Ensure the output directory exists
            string directory = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            // Use ComplexBarcodeGenerator to create the Mailmark barcode
            using (var generator = new ComplexBarcodeGenerator(_mailmark))
            {
                // Optional: set foreground/background colors
                generator.Parameters.Barcode.BarColor = Color.Black;
                generator.Parameters.BackColor = Color.White;

                // Save the barcode as a PNG file
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }
        }
    }

    class Program
    {
        /// <summary>
        /// Entry point demonstrating the usage of <see cref="MailmarkHelper"/> to create and save a Mailmark barcode.
        /// </summary>
        static void Main()
        {
            // Sample usage of the MailmarkHelper
            var helper = new MailmarkHelper();

            // Populate required fields with known‑valid sample data
            helper.SetFormat(4);                     // 4‑state barcode
            helper.SetVersionID(1);
            helper.SetClass("0");                    // Null/Test class
            helper.SetSupplychainID(384224);
            helper.SetItemID(16563762);
            helper.SetDestinationPostCodePlusDPS("EF61AH8T ");

            // Generate and save the barcode image
            string outputFile = "mailmark.png";
            helper.Generate(outputFile);

            Console.WriteLine($"Mailmark barcode saved to {Path.GetFullPath(outputFile)}");
        }
    }
}