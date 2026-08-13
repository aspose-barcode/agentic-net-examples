// Title: Generate Mailmark 4‑state barcode and save as PNG
// Description: Demonstrates how to create a Mailmark 4‑state barcode using Aspose.BarCode's ComplexBarcodeGenerator and write the image to a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, showcasing the use of MailmarkCodetext and ComplexBarcodeGenerator classes. Developers often need to produce Mailmark 4‑state symbols for postal automation, requiring precise codetext configuration and image export. The snippet illustrates typical setup, parameter tuning, and saving the result, useful for integration into mailing systems.
// Prompt: Generate a Mailmark 4‑state barcode with ComplexBarcodeGenerator and save as PNG.
// Tags: mailmark, 4-state, barcode, generation, png, complexbarcodegenerator, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation of a Mailmark 4‑state barcode and saving it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Configures Mailmark codetext, generates the barcode, and writes the image file.
    /// </summary>
    static void Main()
    {
        // Initialize Mailmark 4‑state codetext with required fields
        var mailmark = new MailmarkCodetext();
        mailmark.Format = 4;                     // 4‑state Mailmark
        mailmark.VersionID = 1;                  // version
        mailmark.Class = "0";                    // class as string
        mailmark.SupplychainID = 384224;         // example supply‑chain ID
        mailmark.ItemID = 16563762;              // example item ID
        mailmark.DestinationPostCodePlusDPS = "EF61AH8T "; // trailing space required

        // Create generator for the specified codetext
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Adjust optional barcode parameters
            generator.Parameters.Barcode.FilledBars = false;
            generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

            // Export barcode image to PNG file
            string outputPath = "Mailmark4State.png";
            generator.Save(outputPath, BarCodeImageFormat.Png);
            Console.WriteLine($"Mailmark barcode saved to: {Path.GetFullPath(outputPath)}");
        }
    }
}