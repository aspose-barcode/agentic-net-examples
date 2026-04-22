using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

namespace MailmarkDemo
{
    // Helper class for building Mailmark barcodes
    public class MailmarkHelper : IDisposable
    {
        private readonly MailmarkCodetext _mailmark;

        public MailmarkHelper()
        {
            _mailmark = new MailmarkCodetext();
        }

        // Sets the format (must be 4 for 4‑state Mailmark)
        public void SetFormat(int format)
        {
            if (format != 4)
                throw new ArgumentException("Mailmark format must be 4 for 4‑state barcodes.");
            _mailmark.Format = format;
        }

        // Sets the version ID (typically 1)
        public void SetVersionID(int versionId)
        {
            if (versionId < 0)
                throw new ArgumentOutOfRangeException(nameof(versionId));
            _mailmark.VersionID = versionId;
        }

        // Sets the class (single‑character string, e.g., "0")
        public void SetClass(string classValue)
        {
            if (string.IsNullOrEmpty(classValue) || classValue.Length != 1)
                throw new ArgumentException("Class must be a single character string.");
            _mailmark.Class = classValue;
        }

        // Sets the supply chain ID (max 99 for Barcode C, 999999 for Barcode L)
        public void SetSupplychainID(int supplychainId)
        {
            if (supplychainId < 0)
                throw new ArgumentOutOfRangeException(nameof(supplychainId));
            _mailmark.SupplychainID = supplychainId;
        }

        // Sets the item ID (max 99 999 999)
        public void SetItemID(int itemId)
        {
            if (itemId < 0 || itemId > 99999999)
                throw new ArgumentOutOfRangeException(nameof(itemId));
            _mailmark.ItemID = itemId;
        }

        // Sets the destination postcode plus DPS (must be 9 characters, trailing spaces allowed)
        public void SetDestinationPostCodePlusDPS(string postcode)
        {
            if (string.IsNullOrEmpty(postcode) || postcode.Length != 9)
                throw new ArgumentException("DestinationPostCodePlusDPS must be exactly 9 characters.");
            _mailmark.DestinationPostCodePlusDPS = postcode;
        }

        // Generates the barcode image and saves it to the specified file
        public void Generate(string outputPath)
        {
            if (string.IsNullOrWhiteSpace(outputPath))
                throw new ArgumentException("Output path must be a valid file name.", nameof(outputPath));

            // Ensure the directory exists
            string directory = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            using (var generator = new ComplexBarcodeGenerator(_mailmark))
            {
                // Save as PNG
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }
        }

        public void Dispose()
        {
            // MailmarkCodetext does not hold unmanaged resources, but implement IDisposable pattern for future safety
        }
    }

    class Program
    {
        static void Main()
        {
            // Sample usage of the MailmarkHelper
            using (var helper = new MailmarkHelper())
            {
                helper.SetFormat(4); // 4‑state format
                helper.SetVersionID(1);
                helper.SetClass("0"); // Null/Test class
                helper.SetSupplychainID(384224);
                helper.SetItemID(16563762);
                helper.SetDestinationPostCodePlusDPS("EF61AH8T "); // 9 characters (including trailing space)

                string outputFile = "mailmark.png";
                helper.Generate(outputFile);

                Console.WriteLine($"Mailmark barcode saved to {outputFile}");
            }
        }
    }
}