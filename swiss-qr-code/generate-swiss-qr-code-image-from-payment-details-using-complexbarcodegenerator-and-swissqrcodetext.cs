// Title: Generate Swiss QR Code for payment using Aspose.BarCode
// Description: Demonstrates creating a Swiss QR Code image with payment details and saving it as PNG.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, showcasing the use of ComplexBarcodeGenerator and SwissQRCodetext to produce Swiss QR Bill codes. Developers often need to generate QR payment slips for Swiss banking, requiring correct creditor, account, and amount fields. The snippet illustrates typical setup, optional fields, and image export, useful for integration in billing systems.
// Prompt: Generate a Swiss QR Code image from payment details using ComplexBarcodeGenerator and SwissQRCodetext.
// Tags: swiss qr code, payment, complex barcode, generation, png, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

namespace SwissQRExample
{
    /// <summary>
    /// Example program that creates a Swiss QR Code (QR‑Bill) image using Aspose.BarCode.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point. Builds the QR‑Bill data, generates the barcode, and saves it as a PNG file.
        /// </summary>
        static void Main()
        {
            // Initialize Swiss QR code data container
            var swissQr = new SwissQRCodetext();

            // ----- Mandatory creditor information -----
            swissQr.Bill.Creditor.Name = "John Doe";
            swissQr.Bill.Creditor.CountryCode = "CH";

            // ----- Account (IBAN) -----
            // Valid IBAN for a Swiss bank account
            swissQr.Bill.Account = "CH9300762011623852957";

            // ----- Payment amount -----
            swissQr.Bill.Amount = 199.95m;

            // ----- QR‑Bill version -----
            // V2.0 is the current standard for Swiss QR Bills
            swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

            // ----- Optional fields (uncomment to use) -----
            // swissQr.Bill.Creditor.Street = "Example Street 1";
            // swissQr.Bill.Creditor.PostalCode = "8000";
            // swissQr.Bill.Creditor.Town = "Zurich";

            // Generate the barcode image using ComplexBarcodeGenerator
            using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(swissQr))
            {
                // Do not throw on minor codetext issues; allow generation to continue
                generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

                // Save the generated QR code as a PNG file
                generator.Save("SwissQR.png");
            }

            // Inform the user that the image has been created
            Console.WriteLine("Swiss QR Code image generated: SwissQR.png");
        }
    }
}