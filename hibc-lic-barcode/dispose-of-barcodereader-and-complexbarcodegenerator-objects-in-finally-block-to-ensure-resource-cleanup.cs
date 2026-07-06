// Title: Generate and read a Swiss QR Code using Aspose.BarCode
// Description: Demonstrates creating a Swiss QR bill barcode, saving it as PNG, and reading it back.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation and recognition category. It showcases the use of ComplexBarcodeGenerator for creating Swiss QR codes and BarCodeReader for decoding QR symbols. Developers often need to generate payment QR codes and validate them programmatically, making this pattern common in financial and invoicing applications.
// Prompt: Dispose of BarCodeReader and ComplexBarcodeGenerator objects in a finally block to ensure resource cleanup.
// Tags: swiss qr, barcode generation, barcode reading, qr, aspnet, aspose.barcode, complexbarcodegenerator, barcodereader

using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generating a Swiss QR bill barcode, saving it, and reading it back using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Swiss QR code, saves it as PNG, then reads and prints its content.
    /// </summary>
    static void Main()
    {
        // Output file path for the generated barcode image
        const string outputPath = "sample.png";

        // Declare variables for generator and reader; will be instantiated later
        ComplexBarcodeGenerator complexGenerator = null;
        BarCodeReader reader = null;

        try
        {
            // ------------------------------------------------------------
            // Prepare Swiss QR codetext with required fields
            // ------------------------------------------------------------
            var swissQr = new SwissQRCodetext();
            swissQr.Bill.Creditor.Name = "John Doe";
            swissQr.Bill.Creditor.CountryCode = "CH";
            swissQr.Bill.Account = "CH9300762011623852957";
            swissQr.Bill.Amount = 199.95m;
            swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

            // ------------------------------------------------------------
            // Generate and save the complex barcode image
            // ------------------------------------------------------------
            complexGenerator = new ComplexBarcodeGenerator(swissQr);
            complexGenerator.Save(outputPath, BarCodeImageFormat.Png);

            // ------------------------------------------------------------
            // Read the generated barcode image and output its content
            // ------------------------------------------------------------
            reader = new BarCodeReader(outputPath, DecodeType.QR);
            var results = reader.ReadBarCodes();
            foreach (var result in results)
            {
                Console.WriteLine($"Detected CodeText: {result.CodeText}");
            }
        }
        finally
        {
            // ------------------------------------------------------------
            // Ensure proper disposal of resources regardless of success/failure
            // ------------------------------------------------------------
            if (reader != null)
            {
                reader.Dispose();
            }

            if (complexGenerator != null)
            {
                complexGenerator.Dispose();
            }
        }
    }
}