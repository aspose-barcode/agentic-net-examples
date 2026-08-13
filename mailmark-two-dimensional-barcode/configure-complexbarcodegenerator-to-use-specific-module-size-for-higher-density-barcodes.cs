// Title: Generate Swiss QR Code with Custom Module Size
// Description: Demonstrates creating a Swiss QR bill barcode using Aspose.BarCode and configuring a smaller XDimension for higher density output.
// Category-Description: This example belongs to the Aspose.BarCode ComplexBarcode generation category, showcasing how to work with the ComplexBarcodeGenerator class to produce Swiss QR bill barcodes. Typical use cases include generating payment QR codes for invoices and financial documents, where developers often need to adjust barcode density, format, and visual parameters. The example highlights key API classes such as SwissQRCodetext, ComplexBarcodeGenerator, and BarCodeImageFormat, providing a reference for developers seeking to customize barcode appearance and export options.
// Prompt: Configure ComplexBarcodeGenerator to use a specific module size for higher density barcodes.
// Tags: swissqr, barcode, complexbarcode, xdimension, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Swiss QR bill barcode with a custom module size (XDimension) for higher density.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Builds the Swiss QR codetext, configures the generator, and saves the barcode as a PNG image.
    /// </summary>
    static void Main()
    {
        // Initialize Swiss QR codetext with required billing fields
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Create a ComplexBarcodeGenerator instance using the prepared codetext
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Adjust the module size (XDimension) to 0.5 points for higher barcode density
            generator.Parameters.Barcode.XDimension.Point = 0.5f;

            // Define output file name and save the generated barcode as a PNG image
            string outputFile = "SwissQR.png";
            generator.Save(outputFile, BarCodeImageFormat.Png);

            // Output the full path of the saved image for verification
            Console.WriteLine($"Barcode saved to {Path.GetFullPath(outputFile)}");
        }
    }
}