// Title: Australia Post Barcode Generation with Customer Information Validation
// Description: Demonstrates generating an Australia Post barcode while validating customer information for unsupported characters and skipping generation when invalid.
// Category-Description: Shows how to use Aspose.BarCode to create Australia Post barcodes, covering the BarcodeGenerator class, encoding tables, and error handling. Typical use cases include postal services and logistics where customer data must conform to specific symbology rules. Developers often need to validate input, set encoding tables, and manage code‑text errors.
// Prompt: Handle cases where customer information uses unsupported characters by logging a warning and skipping generation.
// Tags: barcode, australia post, validation, customer information, aspose.barcode, encoding table, ctable, ntable, other, image output

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates an Australia Post barcode after validating
/// customer information against the selected interpreting type. If validation fails,
/// a warning is logged and barcode generation is skipped.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs validation and generates the barcode image.
    /// </summary>
    static void Main()
    {
        // Example customer information that may contain unsupported characters
        string customerInfo = "ABC$123";

        // Choose the interpreting type for the customer information field
        var interpretingType = CustomerInformationInterpretingType.CTable;

        // Validate the customer information against the selected interpreting type
        if (!IsValidCustomerInfo(customerInfo, interpretingType))
        {
            // Log a warning and exit without generating the barcode
            Console.WriteLine($"Warning: Customer information contains characters not allowed for {interpretingType}. Barcode generation skipped.");
            return;
        }

        // Combine the mandatory part of the Australia Post code with the customer information
        string fullCodeText = "5912345678" + customerInfo;

        // Initialize the barcode generator for Australia Post symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, fullCodeText))
        {
            // Set the interpreting type (CTable, NTable, or Other)
            generator.Parameters.Barcode.AustralianPost.EncodingTable = interpretingType;

            // Ensure that code‑text errors do not raise exceptions for this symbology
            generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

            // Generate the barcode image (Aspose.Drawing.Bitmap)
            using (Aspose.Drawing.Bitmap image = generator.GenerateBarCodeImage())
            {
                // Save the generated image to a file
                string outputPath = "AustraliaPost.png";
                image.Save(outputPath);
                Console.WriteLine($"Barcode saved to {outputPath}");
            }
        }
    }

    // Validates customer information according to the selected interpreting type
    static bool IsValidCustomerInfo(string info, CustomerInformationInterpretingType type)
    {
        switch (type)
        {
            case CustomerInformationInterpretingType.CTable:
                foreach (char c in info)
                {
                    // CTable allows letters, digits, space and '#'
                    if (!(char.IsLetterOrDigit(c) || c == ' ' || c == '#'))
                        return false;
                }
                return true;

            case CustomerInformationInterpretingType.NTable:
                foreach (char c in info)
                {
                    // NTable allows digits only
                    if (!char.IsDigit(c))
                        return false;
                }
                return true;

            case CustomerInformationInterpretingType.Other:
                // Other allows only 0‑3 symbols and a maximum length of 3
                if (info.Length > 3) return false;
                foreach (char c in info)
                {
                    if (c < '0' || c > '3')
                        return false;
                }
                return true;

            default:
                return false;
        }
    }
}