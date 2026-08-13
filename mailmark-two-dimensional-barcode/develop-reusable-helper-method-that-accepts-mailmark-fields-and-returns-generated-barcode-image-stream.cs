// Title: Generate Mailmark 4‑state barcode and save as PNG
// Description: Demonstrates creating a Mailmark 4‑state barcode using Aspose.BarCode and returning it as a PNG stream.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, showcasing the MailmarkCodetext class and ComplexBarcodeGenerator. Developers use these APIs to produce postal Mailmark barcodes for logistics, tracking, and automated mail processing. Typical use cases include generating barcode images for printing on envelopes or integrating with mailing software.
// Prompt: Develop a reusable helper method that accepts Mailmark fields and returns a generated barcode image stream.
// Tags: mailmark, barcode, complex barcode, png, stream, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

namespace MailmarkBarcodeHelper
{
    /// <summary>
    /// Provides a console entry point that demonstrates generating a Mailmark 4‑state barcode
    /// and saving the resulting PNG image to disk.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the example. Creates sample Mailmark data, generates the barcode,
        /// and writes the PNG image to a file named <c>mailmark.png</c>.
        /// </summary>
        static void Main()
        {
            // Sample Mailmark data (valid example)
            int format = 4;                     // Mailmark 4‑state format identifier
            int versionId = 1;                  // Version identifier
            string @class = "0";                // Class must be a string (e.g., "0")
            int supplychainId = 384224;         // Supply‑chain identifier
            int itemId = 16563762;              // Item identifier (max 99 999 999)
            string destinationPostCodePlusDPS = "EF61AH8T "; // Destination postcode plus DPS; trailing space is required

            // Generate the barcode image stream using the helper method
            using (MemoryStream barcodeStream = GenerateMailmarkBarcode(
                format, versionId, @class, supplychainId, itemId, destinationPostCodePlusDPS))
            {
                // Save the stream to a PNG file for verification
                using (FileStream file = new FileStream("mailmark.png", FileMode.Create, FileAccess.Write))
                {
                    barcodeStream.CopyTo(file);
                }

                Console.WriteLine("Mailmark barcode generated and saved as 'mailmark.png'.");
            }
        }

        /// <summary>
        /// Generates a Mailmark 4‑state barcode image and returns it as a PNG <see cref="MemoryStream"/>.
        /// </summary>
        /// <param name="format">Mailmark format (must be 4 for 4‑state barcodes).</param>
        /// <param name="versionId">Version identifier (e.g., 1).</param>
        /// <param name="class">Class string (e.g., "0").</param>
        /// <param name="supplychainId">Supply chain identifier.</param>
        /// <param name="itemId">Item identifier (max 99 999 999).</param>
        /// <param name="destinationPostCodePlusDPS">Destination postcode plus DPS (must include trailing space).</param>
        /// <returns>A <see cref="MemoryStream"/> containing the PNG image of the generated barcode.</returns>
        /// <exception cref="ArgumentException">Thrown when required parameters are missing or invalid.</exception>
        public static MemoryStream GenerateMailmarkBarcode(
            int format,
            int versionId,
            string @class,
            int supplychainId,
            int itemId,
            string destinationPostCodePlusDPS)
        {
            // Basic validation of input parameters
            if (format != 4)
                throw new ArgumentException("Mailmark 4‑state barcode requires format = 4.", nameof(format));

            if (string.IsNullOrEmpty(@class))
                throw new ArgumentException("Class cannot be null or empty.", nameof(@class));

            if (string.IsNullOrEmpty(destinationPostCodePlusDPS) || destinationPostCodePlusDPS.Length < 9)
                throw new ArgumentException("DestinationPostCodePlusDPS must be a valid postcode string (including trailing spaces).", nameof(destinationPostCodePlusDPS));

            // Construct the Mailmark codetext object with supplied fields
            var mailmark = new MailmarkCodetext
            {
                Format = format,
                VersionID = versionId,
                Class = @class,
                SupplychainID = supplychainId,
                ItemID = itemId,
                DestinationPostCodePlusDPS = destinationPostCodePlusDPS
            };

            // Generate the barcode using ComplexBarcodeGenerator and write to a memory stream
            var memoryStream = new MemoryStream();
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);
            }

            // Reset stream position so callers can read from the beginning
            memoryStream.Position = 0;
            return memoryStream;
        }
    }
}