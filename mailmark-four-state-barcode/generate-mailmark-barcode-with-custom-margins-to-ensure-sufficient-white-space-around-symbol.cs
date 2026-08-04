// Title: Generate Mailmark Barcode with Custom Margins
// Description: Demonstrates how to create a Mailmark barcode using Aspose.BarCode, set custom padding to provide adequate white space, and save the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex symbologies such as Mailmark. It showcases the use of ComplexBarcodeGenerator and MailmarkCodetext classes to configure barcode data, visual appearance, and layout. Developers commonly need to adjust margins, colors, and output formats when integrating barcodes into documents, labels, or packaging.
// Prompt: Generate a Mailmark barcode with custom margins to ensure sufficient white space around the symbol.
// Tags: mailmark, barcode generation, png, complexbarcodegenerator, mailmarkcodetext

using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Mailmark barcode with custom margins.
/// </summary>
class Program
{
    /// <summary>
    /// Generates the barcode and saves it to a PNG file.
    /// </summary>
    static void Main()
    {
        // Prepare Mailmark codetext with valid values.
        var mailmark = new MailmarkCodetext
        {
            // 4-state Mailmark format.
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            // Destination post code plus DPS must retain the trailing space.
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Generate the Mailmark barcode using ComplexBarcodeGenerator.
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Optional: set foreground and background colors.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Configure custom margins (padding) to ensure sufficient white space.
            // Values are in points; adjust as needed.
            generator.Parameters.Barcode.Padding.Left.Point = 10f;
            generator.Parameters.Barcode.Padding.Top.Point = 10f;
            generator.Parameters.Barcode.Padding.Right.Point = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

            // Save the barcode image to a PNG file.
            generator.Save("mailmark.png");
        }

        Console.WriteLine("Mailmark barcode generated: mailmark.png");
    }
}