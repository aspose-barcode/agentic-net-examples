using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

namespace MailmarkExample
{
    /// <summary>
    /// Helper class for building Mailmark barcodes.
    /// </summary>
    public class MailmarkHelper
    {
        // Internal Mailmark codetext object that holds all barcode data.
        private readonly MailmarkCodetext _mailmark = new MailmarkCodetext();

        /// <summary>
        /// Sets the format of the Mailmark barcode.
        /// </summary>
        /// <param name="format">1 = Letter, 2 = Large Letter, 4 = unspecified/default.</param>
        public void SetFormat(int format)
        {
            // Validate allowed format values.
            if (format != 1 && format != 2 && format != 4)
                throw new ArgumentOutOfRangeException(nameof(format), "Format must be 1, 2, or 4.");

            // Assign the validated format to the codetext.
            _mailmark.Format = format;
        }

        /// <summary>
        /// Sets the version identifier for the barcode.
        /// </summary>
        /// <param name="versionId">Positive integer version ID.</param>
        public void SetVersionID(int versionId)
        {
            // Ensure version ID is positive.
            if (versionId <= 0)
                throw new ArgumentOutOfRangeException(nameof(versionId), "VersionID must be positive.");

            _mailmark.VersionID = versionId;
        }

        /// <summary>
        /// Sets the class code for the barcode.
        /// </summary>
        /// <param name="classCode">Non‑empty class string.</param>
        public void SetClass(string classCode)
        {
            // Validate that class code is not null, empty, or whitespace.
            if (string.IsNullOrWhiteSpace(classCode))
                throw new ArgumentException("Class cannot be null or empty.", nameof(classCode));

            _mailmark.Class = classCode;
        }

        /// <summary>
        /// Sets the supply chain identifier.
        /// </summary>
        /// <param name="supplychainId">Positive integer supply chain ID.</param>
        public void SetSupplychainID(int supplychainId)
        {
            // Ensure supply chain ID is positive.
            if (supplychainId <= 0)
                throw new ArgumentOutOfRangeException(nameof(supplychainId), "SupplychainID must be positive.");

            _mailmark.SupplychainID = supplychainId;
        }

        /// <summary>
        /// Sets the item identifier.
        /// </summary>
        /// <param name="itemId">Integer between 1 and 99,999,999.</param>
        public void SetItemID(int itemId)
        {
            // Validate range of item ID.
            if (itemId <= 0 || itemId > 99999999)
                throw new ArgumentOutOfRangeException(nameof(itemId), "ItemID must be between 1 and 99,999,999.");

            _mailmark.ItemID = itemId;
        }

        /// <summary>
        /// Sets the destination postcode plus DPS.
        /// </summary>
        /// <param name="postcodePlusDps">Non‑empty postcode string.</param>
        public void SetDestinationPostCodePlusDPS(string postcodePlusDps)
        {
            // Validate that postcode string is not null or whitespace.
            if (string.IsNullOrWhiteSpace(postcodePlusDps))
                throw new ArgumentException("DestinationPostCodePlusDPS cannot be null or empty.", nameof(postcodePlusDps));

            _mailmark.DestinationPostCodePlusDPS = postcodePlusDps;
        }

        /// <summary>
        /// Generates the barcode image and saves it as a PNG file.
        /// </summary>
        /// <param name="outputPath">File path where the PNG will be saved.</param>
        public void Generate(string outputPath)
        {
            // Verify that all required fields have been set before generation.
            if (_mailmark.Format == 0)
                throw new InvalidOperationException("Format is not set.");
            if (_mailmark.VersionID == 0)
                throw new InvalidOperationException("VersionID is not set.");
            if (string.IsNullOrEmpty(_mailmark.Class))
                throw new InvalidOperationException("Class is not set.");
            if (_mailmark.SupplychainID == 0)
                throw new InvalidOperationException("SupplychainID is not set.");
            if (_mailmark.ItemID == 0)
                throw new InvalidOperationException("ItemID is not set.");
            if (string.IsNullOrEmpty(_mailmark.DestinationPostCodePlusDPS))
                throw new InvalidOperationException("DestinationPostCodePlusDPS is not set.");

            // Use Aspose's ComplexBarcodeGenerator to create the barcode.
            using (var generator = new ComplexBarcodeGenerator(_mailmark))
            {
                // Write the barcode image to a memory stream.
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    // Persist the image bytes to the specified file.
                    File.WriteAllBytes(outputPath, ms.ToArray());
                }
            }
        }

        /// <summary>
        /// Returns the constructed codetext string (useful for debugging).
        /// </summary>
        /// <returns>Codetext string generated from the current settings.</returns>
        public string GetConstructedCodetext()
        {
            return _mailmark.GetConstructedCodetext();
        }
    }

    class Program
    {
        /// <summary>
        /// Entry point of the program demonstrating MailmarkHelper usage.
        /// </summary>
        static void Main()
        {
            // Create an instance of the helper.
            var helper = new MailmarkHelper();

            // Populate fields with a known valid example.
            helper.SetFormat(4);                     // 4‑state format
            helper.SetVersionID(1);                  // version 1
            helper.SetClass("0");                    // class "0" (null/test)
            helper.SetSupplychainID(384224);         // example supply chain ID
            helper.SetItemID(16563762);              // example item ID
            helper.SetDestinationPostCodePlusDPS("EF61AH8T "); // valid postcode+DP

            // Define output file path.
            string outputFile = "mailmark.png";

            // Generate and save the barcode image.
            helper.Generate(outputFile);

            // Inform the user of success and display the codetext.
            Console.WriteLine($"Mailmark barcode generated and saved to '{outputFile}'.");
            Console.WriteLine($"Constructed codetext: {helper.GetConstructedCodetext()}");
        }
    }
}