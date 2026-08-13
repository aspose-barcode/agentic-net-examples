// Title: Generate a compact Mailmark barcode with reduced module size
// Description: Demonstrates how to create a Mailmark barcode, adjust its X‑dimension for a smaller module size, and remove padding to fit small labels.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as Mailmark. It showcases the use of ComplexBarcodeGenerator, MailmarkCodetext, and barcode parameter settings (XDimension, Padding) to customize output. Developers often need to produce compact barcodes for limited‑space applications like product labels, shipping tags, or inventory stickers.
// Prompt: Adjust barcode module size to produce a compact Mailmark barcode suitable for small labels.
// Tags: mailmark, barcode, generation, png, complexbarcodegenerator, mailmarkcodetext, xdimension, padding

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generating a Mailmark barcode with a reduced module size for compact label printing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates Mailmark codetext, configures barcode parameters, and saves the image.
    /// </summary>
    static void Main()
    {
        // Initialize Mailmark codetext with required values.
        var mailmark = new MailmarkCodetext();
        mailmark.Format = 4;                     // 4‑state Mailmark
        mailmark.VersionID = 1;                  // version
        mailmark.Class = "0";                    // class as string
        mailmark.SupplychainID = 384224;         // supply chain identifier
        mailmark.ItemID = 16563762;              // item identifier
        mailmark.DestinationPostCodePlusDPS = "EF61AH8T "; // trailing space required

        // Create a generator for the complex Mailmark barcode.
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Reduce module size for a compact barcode suitable for small labels.
            generator.Parameters.Barcode.XDimension.Point = 0.5f; // small module width

            // Minimize padding to keep the barcode tight.
            generator.Parameters.Barcode.Padding.Left.Point = 0f;
            generator.Parameters.Barcode.Padding.Top.Point = 0f;
            generator.Parameters.Barcode.Padding.Right.Point = 0f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 0f;

            // Save the generated barcode as a PNG image.
            generator.Save("mailmark.png");
        }

        Console.WriteLine("Mailmark barcode generated: mailmark.png");
    }
}