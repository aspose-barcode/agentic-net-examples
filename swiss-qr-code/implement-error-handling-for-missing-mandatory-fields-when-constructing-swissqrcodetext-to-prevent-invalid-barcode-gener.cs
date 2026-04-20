using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

namespace SwissQRExample
{
    class Program
    {
        static void Main()
        {
            try
            {
                // Create SwissQR codetext and populate mandatory fields
                var swissQRCodetext = new SwissQRCodetext();
                swissQRCodetext.Bill.Account = "CH9300762011623852957"; // valid IBAN
                swissQRCodetext.Bill.Creditor.CountryCode = "CH";       // creditor country code
                swissQRCodetext.Bill.Amount = 199.95m;                  // payment amount
                swissQRCodetext.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

                // Validate mandatory fields before generation
                ValidateSwissQR(swissQRCodetext);

                // Generate the SwissQR barcode
                using (var generator = new ComplexBarcodeGenerator(swissQRCodetext))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        generator.Save(memoryStream, BarCodeImageFormat.Png);
                        File.WriteAllBytes("SwissQR.png", memoryStream.ToArray());
                        Console.WriteLine("SwissQR barcode generated successfully: SwissQR.png");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Validation error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Generation failed: {ex.Message}");
            }
        }

        // Checks that all mandatory fields for SwissQR are provided
        private static void ValidateSwissQR(SwissQRCodetext codetext)
        {
            if (codetext == null)
                throw new ArgumentException("SwissQRCodetext instance cannot be null.");

            var bill = codetext.Bill;
            if (string.IsNullOrWhiteSpace(bill.Account))
                throw new ArgumentException("Bill.Account (IBAN) is mandatory and cannot be empty.");

            if (string.IsNullOrWhiteSpace(bill.Creditor?.CountryCode))
                throw new ArgumentException("Bill.Creditor.CountryCode is mandatory and cannot be empty.");

            // Version is an enum; ensure it is set to a defined value
            if (!Enum.IsDefined(typeof(SwissQRBill.QrBillStandardVersion), bill.Version))
                throw new ArgumentException("Bill.Version must be a valid SwissQR bill standard version.");

            // Additional mandatory checks can be added here (e.g., creditor name, reference for certain IBAN ranges)
        }
    }
}