// Title: Generate a Mailmark barcode with service type, routing code, and customer reference
// Description: Demonstrates how to create a Mailmark barcode by configuring its codetext fields and saving the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, illustrating the use of MailmarkCodetext with ComplexBarcodeGenerator. Developers working with postal and logistics solutions often need to encode service type, routing information, and customer references into Mailmark barcodes; this snippet shows the essential API classes and typical workflow for such scenarios.
// Prompt: Instantiate a MailmarkCodetext and set service type, routing code, and customer reference.
// Tags: mailmark, barcode, complexbarcode, generation, png, aspnet, aspnetcore, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

namespace MailmarkExample
{
    /// <summary>
    /// Demonstrates creating and saving a Mailmark barcode using Aspose.BarCode.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point. Configures MailmarkCodetext, generates the barcode, and saves it as PNG.
        /// </summary>
        static void Main()
        {
            // Instantiate MailmarkCodetext and set required fields
            var mailmark = new MailmarkCodetext();
            mailmark.Format = 4; // 4-state barcode format
            mailmark.VersionID = 1;
            mailmark.Class = "0"; // service type
            mailmark.SupplychainID = 384224; // routing code
            mailmark.ItemID = 16563762; // customer reference
            mailmark.DestinationPostCodePlusDPS = "EF61AH8T "; // required postcode+DP

            // Create ComplexBarcodeGenerator with the Mailmark codetext
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                // Generate the barcode image
                generator.GenerateBarCodeImage();

                // Save the barcode to a PNG file
                string outputPath = "mailmark.png";
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Mailmark barcode saved to {Path.GetFullPath(outputPath)}");
            }
        }
    }
}