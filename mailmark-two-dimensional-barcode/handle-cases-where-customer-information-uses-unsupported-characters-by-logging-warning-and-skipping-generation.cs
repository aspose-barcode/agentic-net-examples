// Title: Australia Post Barcode Generation with Customer Info Validation
// Description: Demonstrates generating Australia Post barcodes while validating customer information and handling unsupported characters.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on the EncodeTypes.AustraliaPost symbology. It showcases the use of BarcodeGenerator, encoding tables (CustomerInformationInterpretingType), and common validation patterns required when creating postal barcodes. Developers often need to ensure that customer data conforms to specific character sets before barcode creation, making this a typical use case for postal applications.
// Prompt: Handle cases where customer information uses unsupported characters by logging a warning and skipping generation.
// Tags: barcode, australia post, generation, validation, customer information, aspose.barcode, encoding table

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates Australia Post barcodes for a set of sample data, validating the customer information
/// part according to the selected <see cref="CustomerInformationInterpretingType"/>. Invalid data
/// triggers a warning and the barcode generation is skipped.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Iterates through sample codes, validates customer information,
    /// and creates barcode images when the data is supported.
    /// </summary>
    static void Main()
    {
        // Sample data: each tuple contains the full code text and the interpreting type to use.
        var samples = new (string CodeText, CustomerInformationInterpretingType Interpreting)[]
        {
            ("5912345678ABCde", CustomerInformationInterpretingType.CTable),   // valid CTable
            ("591234567812345", CustomerInformationInterpretingType.NTable),   // valid NTable (digits only)
            ("5912345678# #", CustomerInformationInterpretingType.CTable),     // valid CTable (space and #)
            ("5912345678XYZ@", CustomerInformationInterpretingType.CTable),   // invalid CTable (contains '@')
            ("5912345678AB12", CustomerInformationInterpretingType.Other),    // invalid Other (contains 'A','B')
            ("591234567800123", CustomerInformationInterpretingType.Other)    // valid Other (only 0,1,2,3)
        };

        int index = 0;
        foreach (var (codeText, interpreting) in samples)
        {
            // Customer information part is everything after the first 10 characters (postal part).
            string customerInfo = codeText.Length > 10 ? codeText.Substring(10) : string.Empty;

            // Validate the customer information according to the selected interpreting type.
            if (!IsCustomerInfoValid(customerInfo, interpreting))
            {
                // Log a warning and skip barcode generation for invalid data.
                Console.WriteLine($"Warning: Customer information \"{customerInfo}\" is invalid for {interpreting}. Skipping generation.");
                continue;
            }

            // Generate Australia Post barcode using the valid code text.
            using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
            {
                // Apply the appropriate encoding table for the customer information.
                generator.Parameters.Barcode.AustralianPost.AustralianPostEncodingTable = interpreting;

                // Save the barcode image to a PNG file.
                string fileName = $"AustraliaPost_{index}.png";
                generator.Save(fileName);
                Console.WriteLine($"Generated barcode saved to {Path.GetFullPath(fileName)}");
            }

            index++;
        }
    }

    // Validation according to the interpreting type.
    static bool IsCustomerInfoValid(string info, CustomerInformationInterpretingType type)
    {
        switch (type)
        {
            case CustomerInformationInterpretingType.CTable:
                // Allows A-Z, a-z, 1-9, space and '#'.
                foreach (char ch in info)
                {
                    if (char.IsLetter(ch) || (ch >= '1' && ch <= '9') || ch == ' ' || ch == '#')
                        continue;
                    return false;
                }
                return true;

            case CustomerInformationInterpretingType.NTable:
                // Allows digits only.
                foreach (char ch in info)
                {
                    if (!char.IsDigit(ch))
                        return false;
                }
                return true;

            case CustomerInformationInterpretingType.Other:
                // Allows only symbols '0', '1', '2', '3'.
                foreach (char ch in info)
                {
                    if (ch != '0' && ch != '1' && ch != '2' && ch != '3')
                        return false;
                }
                return true;

            default:
                return false;
        }
    }
}