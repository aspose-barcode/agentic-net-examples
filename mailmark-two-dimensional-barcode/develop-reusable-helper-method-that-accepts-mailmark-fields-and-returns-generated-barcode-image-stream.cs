// Title: Generate Mailmark 4‑State Barcode and Save as PNG
// Description: Demonstrates creating a Mailmark barcode using Aspose.BarCode, converting it to a PNG image stream, and saving it to a temporary file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as Mailmark. It shows how to use the MailmarkCodetext class together with ComplexBarcodeGenerator to produce a barcode image. Developers working with postal or logistics solutions often need to generate Mailmark barcodes for tracking and routing; this snippet illustrates the typical workflow of configuring barcode parameters, rendering to a stream, and handling the output.
// Prompt: Develop a reusable helper method that accepts Mailmark fields and returns a generated barcode image stream.
// Tags: mailmark, barcode, generation, png, stream, aspose.barcode, complexbarcode, codetext

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Mailmark 4‑state barcode and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates sample Mailmark data, generates the barcode stream, and writes it to a temporary file.
    /// </summary>
    static void Main()
    {
        // Sample Mailmark 4‑state fields
        int format = 4;
        int versionId = 1;
        string classValue = "0";
        int supplyChainId = 384224;
        int itemId = 16563762;
        string destinationPostCodePlusDps = "EF61AH8T ";

        // Generate barcode image stream using the helper method
        using (MemoryStream barcodeStream = GenerateMailmarkBarcode(
            format,
            versionId,
            classValue,
            supplyChainId,
            itemId,
            destinationPostCodePlusDps))
        {
            // Define a temporary file path for verification
            string outputPath = Path.Combine(Path.GetTempPath(), "Mailmark4State.png");

            // Write the stream contents to the file
            using (FileStream file = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                barcodeStream.CopyTo(file);
            }

            Console.WriteLine($"Mailmark barcode saved to: {outputPath}");
        }
    }

    /// <summary>
    /// Generates a Mailmark barcode image stream based on the provided field values.
    /// </summary>
    /// <param name="format">Mailmark format identifier.</param>
    /// <param name="versionId">Version identifier for the Mailmark.</param>
    /// <param name="classValue">Class value (as a string) for the Mailmark.</param>
    /// <param name="supplyChainId">Supply chain identifier.</param>
    /// <param name="itemId">Item identifier.</param>
    /// <param name="destinationPostCodePlusDps">Destination postcode plus DPS.</param>
    /// <returns>A <see cref="MemoryStream"/> containing the generated PNG barcode image.</returns>
    static MemoryStream GenerateMailmarkBarcode(
        int format,
        int versionId,
        string classValue,
        int supplyChainId,
        int itemId,
        string destinationPostCodePlusDps)
    {
        // Create Mailmark codetext object and assign fields
        MailmarkCodetext mailmark = new MailmarkCodetext
        {
            Format = format,
            VersionID = versionId,
            Class = classValue,
            SupplychainID = supplyChainId,
            ItemID = itemId,
            DestinationPostCodePlusDPS = destinationPostCodePlusDps
        };

        // Prepare an output stream for the barcode image
        MemoryStream output = new MemoryStream();

        // Generate barcode using ComplexBarcodeGenerator
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Set module size (optional) – 4 pixels per module
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save barcode image to the provided stream in PNG format
            generator.Save(output, BarCodeImageFormat.Png);
        }

        // Reset stream position for reading by the caller
        output.Position = 0;
        return output;
    }
}