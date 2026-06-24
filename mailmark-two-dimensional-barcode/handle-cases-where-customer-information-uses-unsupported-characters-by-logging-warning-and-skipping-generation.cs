using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation of Australia Post barcodes with various customer information
/// validation based on the selected <see cref="CustomerInformationInterpretingType"/>.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Iterates over sample data, validates customer information,
    /// and generates barcode images when validation succeeds.
    /// </summary>
    static void Main()
    {
        // Sample data: each tuple contains (codeText, encodingType, customerInfo)
        var samples = new (string CodeText, CustomerInformationInterpretingType Encoding, string CustomerInfo)[]
        {
            ("5912345678ABCde", CustomerInformationInterpretingType.CTable, "ABC 123#"), // valid CTable
            ("5912345678ABCde", CustomerInformationInterpretingType.NTable, "123456"),   // valid NTable
            ("5912345678ABCde", CustomerInformationInterpretingType.Other, "AB"),        // valid Other (<=3 chars)
            ("5912345678ABCde", CustomerInformationInterpretingType.CTable, "Invalid@!"), // invalid CTable
            ("5912345678ABCde", CustomerInformationInterpretingType.NTable, "12A34"),    // invalid NTable
            ("5912345678ABCde", CustomerInformationInterpretingType.Other, "ABCD")      // invalid Other (>3 chars)
        };

        int index = 0;
        foreach (var sample in samples)
        {
            index++;

            // Validate the customer information according to the selected encoding type.
            if (!IsCustomerInfoValid(sample.CustomerInfo, sample.Encoding))
            {
                Console.WriteLine($"Warning: Sample {index} has unsupported characters for {sample.Encoding}. Skipping generation.");
                continue;
            }

            // Define output file name for the generated barcode image.
            string outputPath = $"AustraliaPost_{index}.png";

            // Create a barcode generator for Australia Post format.
            using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, sample.CodeText))
            {
                // Set the specific encoding table (CTable, NTable, or Other).
                generator.Parameters.Barcode.AustralianPost.AustralianPostEncodingTable = sample.Encoding;

                // Append customer information to the code text for demonstration purposes.
                generator.CodeText = sample.CodeText + sample.CustomerInfo;

                try
                {
                    // Save the generated barcode image to the specified path.
                    generator.Save(outputPath);
                    Console.WriteLine($"Generated barcode saved to {outputPath}");
                }
                catch (Exception ex)
                {
                    // Report any errors that occur during barcode generation.
                    Console.WriteLine($"Error generating barcode for sample {index}: {ex.Message}");
                }
            }
        }
    }

    /// <summary>
    /// Validates customer information based on the selected <see cref="CustomerInformationInterpretingType"/>.
    /// </summary>
    /// <param name="info">The customer information string to validate.</param>
    /// <param name="type">The interpreting type that defines allowed characters.</param>
    /// <returns>True if the information conforms to the rules of the specified type; otherwise, false.</returns>
    static bool IsCustomerInfoValid(string info, CustomerInformationInterpretingType type)
    {
        switch (type)
        {
            case CustomerInformationInterpretingType.CTable:
                // CTable allows letters, digits, space, and '#'.
                foreach (char c in info)
                {
                    if (!(char.IsLetterOrDigit(c) || c == ' ' || c == '#'))
                        return false;
                }
                return true;

            case CustomerInformationInterpretingType.NTable:
                // NTable allows digits only.
                foreach (char c in info)
                {
                    if (!char.IsDigit(c))
                        return false;
                }
                return true;

            case CustomerInformationInterpretingType.Other:
                // Other allows any characters but limits length to 3 symbols.
                return info.Length <= 3;
        }

        // If an unknown type is encountered, treat as invalid.
        return false;
    }
}