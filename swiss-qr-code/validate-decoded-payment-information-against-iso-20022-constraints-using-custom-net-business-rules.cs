using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a QR code containing simple ISO 20022‑like payment data,
/// reading it back, and validating the extracted information.
/// </summary>
class Program
{
    // Sample payment data in a simple "key:value" per line format.
    private const string SamplePaymentData =
        "BIC:DEUTDEFF" + "\n" +
        "IBAN:DE89370400440532013000" + "\n" +
        "Amount:1234.56" + "\n" +
        "Currency:EUR";

    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code, reads it, validates the decoded payment information,
    /// and cleans up the temporary image file.
    /// </summary>
    static void Main()
    {
        // Define a temporary file path for the generated QR code image.
        string imagePath = Path.Combine(Path.GetTempPath(), "payment_qr.png");

        // ------------------------------------------------------------
        // Generate QR code containing the sample payment data.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, SamplePaymentData))
        {
            // Use a high error correction level to improve readability.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
            generator.Save(imagePath);
        }

        // Verify that the image file was successfully created.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // ------------------------------------------------------------
        // Read and decode the QR code from the generated image.
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            // Disable checksum validation (not required for QR codes).
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;

            var results = reader.ReadBarCodes();
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
                return;
            }

            foreach (var result in results)
            {
                Console.WriteLine("Decoded Text:");
                Console.WriteLine(result.CodeText);
                Console.WriteLine();

                // Validate the decoded payment information.
                var validation = ValidatePaymentInfo(result.CodeText);
                Console.WriteLine("Validation Result: " + (validation.IsValid ? "Valid" : "Invalid"));
                if (!validation.IsValid)
                {
                    Console.WriteLine("Errors:");
                    foreach (var err in validation.Errors)
                    {
                        Console.WriteLine("- " + err);
                    }
                }
            }
        }

        // ------------------------------------------------------------
        // Clean up the temporary QR code image file.
        // ------------------------------------------------------------
        try { File.Delete(imagePath); } catch { }
    }

    // ------------------------------------------------------------------------
    // Helper classes and methods for validation.
    // ------------------------------------------------------------------------

    /// <summary>
    /// Represents the outcome of validating payment information.
    /// </summary>
    private class ValidationResult
    {
        /// <summary>
        /// Gets a value indicating whether the validation succeeded (no errors).
        /// </summary>
        public bool IsValid => Errors.Count == 0;

        /// <summary>
        /// Collection of validation error messages.
        /// </summary>
        public System.Collections.Generic.List<string> Errors { get; } = new System.Collections.Generic.List<string>();
    }

    /// <summary>
    /// Validates raw payment text against simple ISO 20022‑like rules.
    /// </summary>
    /// <param name="rawText">The decoded QR code text.</param>
    /// <returns>A <see cref="ValidationResult"/> containing validation status and errors.</returns>
    private static ValidationResult ValidatePaymentInfo(string rawText)
    {
        var result = new ValidationResult();

        // Ensure the text is not null, empty, or whitespace.
        if (string.IsNullOrWhiteSpace(rawText))
        {
            result.Errors.Add("Code text is empty.");
            return result;
        }

        // Split the text into lines and parse each "Key:Value" pair.
        var lines = rawText.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        var dict = new System.Collections.Generic.Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (var line in lines)
        {
            var parts = line.Split(new[] { ':' }, 2);
            if (parts.Length != 2)
            {
                result.Errors.Add($"Invalid line format: '{line}'. Expected 'Key:Value'.");
                continue;
            }
            dict[parts[0].Trim()] = parts[1].Trim();
        }

        // Validate each required field.
        ValidateBic(dict, result);
        ValidateIban(dict, result);
        ValidateAmount(dict, result);
        ValidateCurrency(dict, result);

        return result;
    }

    private static void ValidateBic(System.Collections.Generic.Dictionary<string, string> dict, ValidationResult result)
    {
        // Check for presence of BIC.
        if (!dict.TryGetValue("BIC", out var bic))
        {
            result.Errors.Add("Missing BIC.");
            return;
        }

        // BIC must be 8 or 11 alphanumeric characters.
        if (!Regex.IsMatch(bic, @"^[A-Z0-9]{8}([A-Z0-9]{3})?$"))
        {
            result.Errors.Add($"Invalid BIC format: '{bic}'.");
        }
    }

    private static void ValidateIban(System.Collections.Generic.Dictionary<string, string> dict, ValidationResult result)
    {
        // Check for presence of IBAN.
        if (!dict.TryGetValue("IBAN", out var iban))
        {
            result.Errors.Add("Missing IBAN.");
            return;
        }

        // Basic IBAN pattern: 2 letters, 2 digits, up to 30 alphanumerics.
        if (!Regex.IsMatch(iban, @"^[A-Z]{2}\d{2}[A-Z0-9]{1,30}$"))
        {
            result.Errors.Add($"Invalid IBAN format: '{iban}'.");
        }
    }

    private static void ValidateAmount(System.Collections.Generic.Dictionary<string, string> dict, ValidationResult result)
    {
        // Check for presence of Amount.
        if (!dict.TryGetValue("Amount", out var amountStr))
        {
            result.Errors.Add("Missing Amount.");
            return;
        }

        // Ensure the amount is a positive decimal number.
        if (!decimal.TryParse(
                amountStr,
                System.Globalization.NumberStyles.AllowDecimalPoint,
                System.Globalization.CultureInfo.InvariantCulture,
                out var amount) || amount <= 0)
        {
            result.Errors.Add($"Invalid Amount value: '{amountStr}'. Must be a positive number.");
        }
    }

    private static void ValidateCurrency(System.Collections.Generic.Dictionary<string, string> dict, ValidationResult result)
    {
        // Check for presence of Currency.
        if (!dict.TryGetValue("Currency", out var currency))
        {
            result.Errors.Add("Missing Currency.");
            return;
        }

        // Currency must be three uppercase letters (ISO 4217).
        if (!Regex.IsMatch(currency, @"^[A-Z]{3}$"))
        {
            result.Errors.Add($"Invalid Currency code: '{currency}'.");
        }
    }
}