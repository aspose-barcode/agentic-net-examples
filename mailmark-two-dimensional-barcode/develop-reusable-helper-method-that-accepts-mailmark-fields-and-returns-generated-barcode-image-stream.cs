using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

namespace MailmarkBarcodeExample
{
    /// <summary>
    /// Demonstrates generation of a Mailmark barcode using Aspose.BarCode.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the application. Generates a Mailmark barcode and saves it as a PNG file.
        /// </summary>
        static void Main()
        {
            // Sample Mailmark data
            int format = 4;               // 4‑state format
            int versionId = 1;
            string classCode = "0";       // Test class
            int supplyChainId = 384224;
            int itemId = 16563762;
            string destinationPostCodePlusDPS = "EF61AH8T ";

            // Generate the barcode image as a memory stream
            using (MemoryStream barcodeStream = GenerateMailmarkBarcode(
                format,
                versionId,
                classCode,
                supplyChainId,
                itemId,
                destinationPostCodePlusDPS))
            {
                // Save the stream to a file for verification
                using (FileStream file = File.Create("mailmark.png"))
                {
                    barcodeStream.CopyTo(file);
                }

                Console.WriteLine("Mailmark barcode generated and saved as 'mailmark.png'.");
            }
        }

        /// <summary>
        /// Generates a Mailmark barcode image and returns it as a <see cref="MemoryStream"/>.
        /// </summary>
        /// <param name="format">Mailmark format (e.g., 4 for 4‑state).</param>
        /// <param name="versionId">Version identifier.</param>
        /// <param name="classCode">Class code as a string.</param>
        /// <param name="supplyChainId">Supply chain identifier.</param>
        /// <param name="itemId">Item identifier.</param>
        /// <param name="destinationPostCodePlusDPS">Destination postcode plus DPS (9‑character string).</param>
        /// <returns>MemoryStream containing the PNG image of the generated barcode.</returns>
        public static MemoryStream GenerateMailmarkBarcode(
            int format,
            int versionId,
            string classCode,
            int supplyChainId,
            int itemId,
            string destinationPostCodePlusDPS)
        {
            // Validate required string parameters
            if (string.IsNullOrEmpty(classCode))
                throw new ArgumentException("Class code must be provided.", nameof(classCode));
            if (string.IsNullOrEmpty(destinationPostCodePlusDPS))
                throw new ArgumentException("DestinationPostCodePlusDPS must be provided.", nameof(destinationPostCodePlusDPS));

            // Populate the MailmarkCodetext object with supplied values
            MailmarkCodetext mailmark = new MailmarkCodetext
            {
                Format = format,
                VersionID = versionId,
                Class = classCode,
                SupplychainID = supplyChainId,
                ItemID = itemId,
                DestinationPostCodePlusDPS = destinationPostCodePlusDPS
            };

            // Generate the barcode using ComplexBarcodeGenerator
            using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(mailmark))
            {
                MemoryStream ms = new MemoryStream();
                // Save as PNG; BarCodeImageFormat is defined in Aspose.BarCode.Generation
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading by the caller
                return ms;
            }
        }
    }
}