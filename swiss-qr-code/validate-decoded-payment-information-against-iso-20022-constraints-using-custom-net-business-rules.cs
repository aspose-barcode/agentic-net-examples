// Title: Validate payment barcode against ISO 20022 rules
// Description: Demonstrates generating a Code128 barcode with payment data, decoding it, and validating fields per ISO 20022 constraints using custom .NET business rules.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator, BarCodeReader, and related parameter classes to create, read, and process barcodes. Typical use cases include encoding payment information, scanning, and applying business‑level validation such as ISO 20022 compliance. Developers often need to generate barcodes, extract data, and enforce domain‑specific rules.
// Prompt: Validate decoded payment information against ISO 20022 constraints using custom .NET business rules.
// Tags: barcode, code128, generation, recognition, iso20022, validation, payment, aspnet, aspose.barcode

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a Code128 barcode containing simple payment data, decodes it,
/// and validates the extracted fields against a subset of ISO 20022 rules.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode creation, decoding, and validation.
    /// </summary>
    static void Main()
    {
        // Sample payment data encoded in a simple key=value; format
        string paymentData = "IBAN=DE89370400440532013000;BIC=DEUTDEFF;Amt=123.45;Ccy=EUR";

        // Prepare temporary file path for the barcode image
        string tempFolder = Path.GetTempPath();
        string barcodePath = Path.Combine(tempFolder, "payment.png");

        // Generate a Code128 barcode containing the payment data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, paymentData))
        {
            // Optional: set barcode appearance
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the barcode image to the temporary location
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Decode the barcode from the saved image
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            // Disable checksum validation for this simple example
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;

            // Read barcodes (there should be exactly one)
            BarCodeResult[] results = reader.ReadBarCodes();
            if (results == null || results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
                return;
            }

            // Extract the decoded text
            string decodedText = results[0].CodeText;
            Console.WriteLine("Decoded text: " + decodedText);

            // Parse key=value pairs into a dictionary
            Dictionary<string, string> fields = ParseKeyValuePairs(decodedText);

            // Validate the extracted fields according to simplified ISO 20022 rules
            List<string> validationErrors = ValidatePaymentFields(fields);

            // Output validation results
            if (validationErrors.Count == 0)
            {
                Console.WriteLine("Payment information is valid.");
            }
            else
            {
                Console.WriteLine("Validation errors:");
                foreach (string err in validationErrors)
                {
                    Console.WriteLine("- " + err);
                }
            }
        }

        // Clean up the temporary barcode image file
        try
        {
            File.Delete(barcodePath);
        }
        catch
        {
            // Ignored – best effort cleanup
        }
    }

    /// <summary>
    /// Parses a string formatted as "Key1=Value1;Key2=Value2" into a case‑insensitive dictionary.
    /// </summary>
    /// <param name="input">The input string containing key/value pairs.</param>
    /// <returns>A dictionary of parsed fields.</returns>
    private static Dictionary<string, string> ParseKeyValuePairs(string input)
    {
        var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        string[] pairs = input.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string pair in pairs)
        {
            int idx = pair.IndexOf('=');
            if (idx > 0 && idx < pair.Length - 1)
            {
                string key = pair.Substring(0, idx).Trim();
                string value = pair.Substring(idx + 1).Trim();
                dict[key] = value;
            }
        }
        return dict;
    }

    /// <summary>
    /// Performs simple ISO 20022‑style validation on payment fields.
    /// </summary>
    /// <param name="fields">Dictionary containing payment fields.</param>
    /// <returns>List of validation error messages; empty if all checks pass.</returns>
    private static List<string> ValidatePaymentFields(Dictionary<string, string> fields)
    {
        var errors = new List<string>();

        // IBAN validation (basic length and format)
        if (fields.TryGetValue("IBAN", out string iban))
        {
            if (iban.Length != 22)
                errors.Add("IBAN must be 22 characters long.");
            else if (!char.IsLetter(iban[0]) || !char.IsLetter(iban[1]))
                errors.Add("IBAN must start with two letters.");
            else if (!IsAllAlphanumeric(iban))
                errors.Add("IBAN must contain only alphanumeric characters.");
        }
        else
        {
            errors.Add("IBAN field is missing.");
        }

        // BIC validation (8 or 11 characters, letters/digits)
        if (fields.TryGetValue("BIC", out string bic))
        {
            if (bic.Length != 8 && bic.Length != 11)
                errors.Add("BIC must be 8 or 11 characters long.");
            else if (!IsAllLettersOrDigits(bic))
                errors.Add("BIC must contain only letters and digits.");
        }
        else
        {
            errors.Add("BIC field is missing.");
        }

        // Amount validation (positive decimal)
        if (fields.TryGetValue("Amt", out string amtStr))
        {
            if (!decimal.TryParse(amtStr, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal amt) || amt <= 0)
                errors.Add("Amount must be a positive decimal number.");
        }
        else
        {
            errors.Add("Amt (amount) field is missing.");
        }

        // Currency validation (3 uppercase letters)
        if (fields.TryGetValue("Ccy", out string ccy))
        {
            if (ccy.Length != 3 || !IsAllUppercaseLetters(ccy))
                errors.Add("Currency code must be three uppercase letters.");
        }
        else
        {
            errors.Add("Ccy (currency) field is missing.");
        }

        return errors;
    }

    // Helper: checks that a string contains only letters or digits
    private static bool IsAllAlphanumeric(string s)
    {
        foreach (char c in s)
        {
            if (!char.IsLetterOrDigit(c))
                return false;
        }
        return true;
    }

    // Helper: duplicate of IsAllAlphanumeric (kept for semantic clarity)
    private static bool IsAllLettersOrDigits(string s)
    {
        foreach (char c in s)
        {
            if (!char.IsLetterOrDigit(c))
                return false;
        }
        return true;
    }

    // Helper: checks that a string contains only uppercase letters
    private static bool IsAllUppercaseLetters(string s)
    {
        foreach (char c in s)
        {
            if (!char.IsUpper(c) || !char.IsLetter(c))
                return false;
        }
        return true;
    }
}