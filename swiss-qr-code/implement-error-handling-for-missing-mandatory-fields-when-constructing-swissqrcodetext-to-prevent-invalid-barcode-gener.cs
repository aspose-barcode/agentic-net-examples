using System;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a Swiss QR code barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Constructs a Swiss QR code, validates it,
    /// generates the barcode image, and saves it to disk.
    /// </summary>
    static void Main()
    {
        // Construct SwissQR codetext object which holds all QR bill data
        var swissQr = new SwissQRCodetext();

        // Populate mandatory fields required for a valid QR bill
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        try
        {
            // Validate mandatory fields before attempting barcode generation
            ValidateSwissQr(swissQr);

            // Define output file path and generate the barcode image
            const string outputPath = "SwissQR.png";
            using (var generator = new ComplexBarcodeGenerator(swissQr))
            {
                // Save the generated barcode as a PNG file
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"SwissQR barcode saved to {outputPath}");
        }
        // Handle validation errors explicitly
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Validation error: {ex.Message}");
        }
        // Catch any other unexpected exceptions
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }

    /// <summary>
    /// Validates that all required properties of the SwissQRCodetext instance are set.
    /// Throws <see cref="ArgumentException"/> if any mandatory field is missing or invalid.
    /// </summary>
    /// <param name="swissQr">The SwissQRCodetext object to validate.</param>
    static void ValidateSwissQr(SwissQRCodetext swissQr)
    {
        // Ensure the SwissQRCodetext instance itself is not null
        if (swissQr == null)
            throw new ArgumentException("SwissQRCodetext instance cannot be null.");

        var bill = swissQr.Bill;
        // Ensure the Bill property is not null
        if (bill == null)
            throw new ArgumentException("Bill cannot be null.");

        // Creditor name is mandatory
        if (string.IsNullOrWhiteSpace(bill.Creditor.Name))
            throw new ArgumentException("Creditor Name is mandatory.");

        // Creditor country code is mandatory
        if (string.IsNullOrWhiteSpace(bill.Creditor.CountryCode))
            throw new ArgumentException("Creditor CountryCode is mandatory.");

        // Account number is mandatory
        if (string.IsNullOrWhiteSpace(bill.Account))
            throw new ArgumentException("Account is mandatory.");

        // Amount must be a positive value
        if (bill.Amount <= 0)
            throw new ArgumentException("Amount must be greater than zero.");

        // Version must be a defined enum value
        if (!Enum.IsDefined(typeof(SwissQRBill.QrBillStandardVersion), bill.Version))
            throw new ArgumentException("Bill.Version is mandatory and must be a valid enum value.");
    }
}