// Title: Configure ComplexBarcodeGenerator with custom module size for high-density Swiss QR codes
// Description: Demonstrates setting a smaller XDimension on ComplexBarcodeGenerator to produce a higher density Swiss QR barcode.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, illustrating how to customize barcode parameters such as module size, resolution, and codetext using classes like ComplexBarcodeGenerator, SwissQRCodetext, and BarcodeParameters. Developers often need to adjust these settings to meet specific printing or scanning requirements, especially for QR codes used in financial documents.
// Prompt: Configure ComplexBarcodeGenerator to use a specific module size for higher density barcodes.
// Tags: barcode, complex barcode, module size, high density, swissqr, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates configuring a ComplexBarcodeGenerator to use a specific module size for higher density Swiss QR barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Swiss QR code with a reduced XDimension for higher density and saves it as an image.
    /// </summary>
    static void Main()
    {
        // Prepare SwissQR codetext with required fields
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Create ComplexBarcodeGenerator with the codetext
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Set a smaller module size (higher density) using XDimension
            generator.Parameters.Barcode.XDimension.Point = 0.5f;

            // Optional: set image resolution
            generator.Parameters.Resolution = 300;

            // Save the generated barcode image
            string outputPath = "SwissQR_HighDensity.png";
            generator.Save(outputPath);
            Console.WriteLine($"Barcode saved to {Path.GetFullPath(outputPath)}");
        }
    }
}