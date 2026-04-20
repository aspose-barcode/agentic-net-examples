using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        const string configPath = "config.txt";

        if (!File.Exists(configPath))
        {
            Console.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        var config = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (var line in File.ReadAllLines(configPath))
        {
            if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("#"))
                continue;

            var parts = line.Split(new[] { '=' }, 2);
            if (parts.Length != 2)
                continue;

            var key = parts[0].Trim();
            var value = parts[1].Trim();
            config[key] = value;
        }

        var swissQr = new SwissQRCodetext();

        string Get(string field) => config.TryGetValue(field, out var v) ? v : null;

        swissQr.Bill.Account = Get("Account") ?? "CH9300762011623852957";
        swissQr.Bill.Amount = decimal.Parse(Get("Amount") ?? "199.95", CultureInfo.InvariantCulture);
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        swissQr.Bill.Creditor.Name = Get("CreditorName") ?? "John Doe";
        swissQr.Bill.Creditor.Street = Get("CreditorStreet") ?? "Main Street 1";
        swissQr.Bill.Creditor.PostalCode = Get("CreditorPostalCode") ?? "8000";
        swissQr.Bill.Creditor.Town = Get("CreditorCity") ?? "Zurich";
        swissQr.Bill.Creditor.CountryCode = Get("CreditorCountry") ?? "CH";

        swissQr.Bill.Debtor.Name = Get("DebtorName") ?? "Jane Smith";
        swissQr.Bill.Debtor.Street = Get("DebtorStreet") ?? "Second Street 2";
        swissQr.Bill.Debtor.PostalCode = Get("DebtorPostalCode") ?? "3000";
        swissQr.Bill.Debtor.Town = Get("DebtorCity") ?? "Bern";
        swissQr.Bill.Debtor.CountryCode = Get("DebtorCountry") ?? "CH";

        swissQr.Bill.Reference = Get("Reference") ?? "RF18539007547034";
        swissQr.Bill.UnstructuredMessage = Get("Message") ?? "Invoice 2023-001";

        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            generator.Save("SwissQR.png");
        }

        Console.WriteLine("Swiss QR barcode generated: SwissQR.png");
    }
}