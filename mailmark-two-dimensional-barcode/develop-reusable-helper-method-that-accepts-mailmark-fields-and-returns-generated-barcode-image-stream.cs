using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

namespace MailmarkBarcodeDemo
{
    class Program
    {
        static void Main()
        {
            // Sample Mailmark data
            int versionId = 1;
            string classCode = "0";
            int supplyChainId = 384224;
            int itemId = 16563762;
            string destinationPostCodePlusDps = "EF61AH8T ";

            // Generate barcode image stream
            using (MemoryStream barcodeStream = GenerateMailmarkBarcode(versionId, classCode, supplyChainId, itemId, destinationPostCodePlusDps))
            {
                // Save to a file for demonstration purposes
                const string outputPath = "mailmark.png";
                using (FileStream file = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    barcodeStream.CopyTo(file);
                }

                Console.WriteLine($"Mailmark barcode saved to {outputPath}");
            }
        }

        /// <summary>
        /// Generates a Mailmark 4‑state barcode image and returns it as a MemoryStream (PNG format).
        /// </summary>
        /// <param name="versionId">Version ID (integer).</param>
        /// <param name="classCode">Class code (single‑character string, e.g., "0").</param>
        /// <param name="supplyChainId">Supply chain ID (integer).</param>
        /// <param name="itemId">Item ID (integer).</param>
        /// <param name="destinationPostCodePlusDps">Destination postcode plus DPS (9‑character string, trailing spaces allowed).</param>
        /// <returns>MemoryStream containing the PNG image of the generated barcode.</returns>
        static MemoryStream GenerateMailmarkBarcode(int versionId, string classCode, int supplyChainId, int itemId, string destinationPostCodePlusDps)
        {
            // Validate required parameters
            if (string.IsNullOrEmpty(classCode) || classCode.Length != 1)
                throw new ArgumentException("Class code must be a single character.", nameof(classCode));
            if (destinationPostCodePlusDps == null || destinationPostCodePlusDps.Length != 9)
                throw new ArgumentException("DestinationPostCodePlusDPS must be exactly 9 characters.", nameof(destinationPostCodePlusDps));

            // Prepare Mailmark codetext
            var mailmark = new MailmarkCodetext
            {
                // Format is fixed to 4 for 4‑state Mailmark
                Format = 4,
                VersionID = versionId,
                Class = classCode,
                SupplychainID = supplyChainId,
                ItemID = itemId,
                DestinationPostCodePlusDPS = destinationPostCodePlusDps
            };

            // Generate barcode image into a memory stream (PNG)
            var memoryStream = new MemoryStream();
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                // Save directly to the stream in PNG format
                generator.Save(memoryStream, BarCodeImageFormat.Png);
            }

            // Reset stream position for reading
            memoryStream.Position = 0;
            return memoryStream;
        }
    }
}