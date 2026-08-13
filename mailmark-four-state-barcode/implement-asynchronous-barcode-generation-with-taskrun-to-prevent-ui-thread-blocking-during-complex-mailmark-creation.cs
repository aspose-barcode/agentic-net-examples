// Title: Asynchronous Mailmark Barcode Generation Example
// Description: Demonstrates generating a Royal Mailmark barcode asynchronously to keep the UI responsive.
// Category-Description: Shows how to use Aspose.BarCode's ComplexBarcodeGenerator for creating Mailmark barcodes. This example belongs to the barcode generation category, illustrating background processing with Task.Run, setting visual parameters, and saving the image. Developers working with complex barcode symbologies such as Mailmark often need to off‑load generation to a background thread to avoid UI thread blocking.
// Prompt: Implement asynchronous barcode generation with Task.Run to prevent UI thread blocking during complex Mailmark creation.
// Tags: mailmark, barcode, asynchronous, task.run, complexbarcode, generation, png, aspose.barcode

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates asynchronous generation of a Mailmark barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates and saves a Mailmark barcode without blocking the UI thread.
    /// </summary>
    static async Task Main()
    {
        // Prepare a valid Mailmark codetext (4‑state Royal Mailmark)
        var mailmark = new MailmarkCodetext
        {
            Format = 4,                     // 4‑state format
            VersionID = 1,
            Class = "0",                    // test class
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T " // trailing space is required
        };

        string outputPath = "mailmark.png";

        // Generate the barcode on a background thread to avoid UI blocking
        await Task.Run(() =>
        {
            // Initialize the complex barcode generator with the Mailmark data
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                // Optional visual settings
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Save the generated image to a file
                generator.Save(outputPath);
            }
        });

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Mailmark barcode saved to {Path.GetFullPath(outputPath)}");
    }
}