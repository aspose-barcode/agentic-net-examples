// Title: Generate Mailmark 4-State Barcode and Return Image Stream
// Description: Demonstrates how to create a Mailmark 4‑state barcode using Aspose.BarCode, encode specific fields, and obtain the barcode as a PNG image stream.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It shows how to use MailmarkCodetext and ComplexBarcodeGenerator classes to produce Mailmark barcodes, a common requirement for postal and logistics applications. Developers often need reusable helpers that accept Mailmark fields and output barcode images for integration with mailing systems or document workflows.
// Prompt: Develop a reusable helper method that accepts Mailmark fields and returns a generated barcode image stream.
// Tags: mailmark, barcode, generation, stream, png, aspose.barcode, complexbarcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Mailmark 4‑state barcode and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates sample Mailmark data, generates the barcode, and writes it to a file.
    /// </summary>
    static void Main()
    {
        // Sample Mailmark 4-state fields
        int format = 4;
        int versionId = 1;
        string classValue = "0";
        int supplychainId = 384224;
        int itemId = 16563762;
        string destinationPostCodePlusDps = "EF61AH8T ";

        // Generate the barcode and obtain it as a memory stream
        using (MemoryStream barcodeStream = GenerateMailmarkBarcode(
            format,
            versionId,
            classValue,
            supplychainId,
            itemId,
            destinationPostCodePlusDps))
        {
            // Save the stream to a PNG file in the current directory
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "Mailmark4State.png");
            File.WriteAllBytes(outputPath, barcodeStream.ToArray());
            Console.WriteLine($"Mailmark barcode saved to: {outputPath}");
        }
    }

    /// <summary>
    /// Generates a Mailmark 4‑state barcode based on the supplied fields and returns the image as a <see cref="MemoryStream"/>.
    /// </summary>
    /// <param name="format">Mailmark format identifier (e.g., 4 for 4‑state).</param>
    /// <param name="versionId">Version identifier of the Mailmark specification.</param>
    /// <param name="classValue">Class value (single character) for the Mailmark.</param>
    /// <param name="supplychainId">Supply‑chain identifier.</param>
    /// <param name="itemId">Item identifier.</param>
    /// <param name="destinationPostCodePlusDps">Destination postcode plus DPS information.</param>
    /// <returns>A memory stream containing the generated PNG barcode image.</returns>
    static MemoryStream GenerateMailmarkBarcode(
        int format,
        int versionId,
        string classValue,
        int supplychainId,
        int itemId,
        string destinationPostCodePlusDps)
    {
        // Validate required string parameters
        if (string.IsNullOrEmpty(classValue))
            throw new ArgumentException("Class value cannot be null or empty.", nameof(classValue));
        if (string.IsNullOrEmpty(destinationPostCodePlusDps))
            throw new ArgumentException("DestinationPostCodePlusDPS cannot be null or empty.", nameof(destinationPostCodePlusDps));

        // Create Mailmark 4-state codetext using the provided fields
        MailmarkCodetext mailmark = new MailmarkCodetext
        {
            Format = format,
            VersionID = versionId,
            Class = classValue,
            SupplychainID = supplychainId,
            ItemID = itemId,
            DestinationPostCodePlusDPS = destinationPostCodePlusDps
        };

        // Prepare a memory stream to hold the generated barcode image
        MemoryStream ms = new MemoryStream();

        // Use ComplexBarcodeGenerator to render the Mailmark barcode
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Optional: adjust module size for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save the barcode as PNG into the memory stream
            generator.Save(ms, BarCodeImageFormat.Png);
        }

        // Reset stream position before returning
        ms.Position = 0;
        return ms;
    }
}