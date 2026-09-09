// Title: Validate C40 character set for Mailmark 2D barcode fields
// Description: Demonstrates how to validate non‑customer fields against the C40 character set before generating a Mailmark 2D barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode creation and data validation. It showcases the use of ComplexBarcodeGenerator, Mailmark2DCodetext, and BarCodeImageFormat classes to produce a Mailmark 2D barcode after ensuring required fields meet the C40 character set constraints. Developers working with postal barcodes often need to validate field content before encoding to avoid generation errors.
// Prompt: Validate that all non‑customer fields conform to the C40 character set before generation.
// Tags: mailmark, c40, validation, barcode generation, aspose.barcode, complexbarcode, png

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates validation of Mailmark 2D barcode fields against the C40 character set
/// and generation of the barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Validates fields, generates barcode if validation passes, and writes output path.
    /// </summary>
    static void Main()
    {
        // Define sample non‑customer fields for a Mailmark 2D barcode
        var mailmark = new Mailmark2DCodetext
        {
            UPUCountryID = "JGB ",
            InformationTypeID = "0",
            VersionID = "1",
            Class = "1",
            SupplyChainID = 123,
            ItemID = 1234
        };

        // Gather string fields that must conform to the C40 character set
        var fieldsToValidate = new List<(string Name, string Value)>
        {
            ("UPUCountryID", mailmark.UPUCountryID),
            ("InformationTypeID", mailmark.InformationTypeID),
            ("VersionID", mailmark.VersionID),
            ("Class", mailmark.Class)
        };

        // Perform validation and report any invalid fields
        bool allValid = true;
        foreach (var (name, value) in fieldsToValidate)
        {
            if (!IsC40Valid(value))
            {
                Console.WriteLine($"Field '{name}' contains invalid characters for C40 set: \"{value}\"");
                allValid = false;
            }
        }

        // Abort generation if validation failed
        if (!allValid)
        {
            Console.WriteLine("Validation failed. Barcode will not be generated.");
            return;
        }

        // Generate the Mailmark 2D barcode and save it as a PNG file
        string outputPath = Path.Combine(Path.GetTempPath(), "Mailmark2D.png");
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode generated successfully at: {outputPath}");
    }

    // C40 character set: digits 0‑9, uppercase A‑Z, space
    static bool IsC40Valid(string text)
    {
        if (string.IsNullOrEmpty(text))
            return true;

        foreach (char ch in text)
        {
            if (ch == ' ')
                continue;
            if (ch >= '0' && ch <= '9')
                continue;
            if (ch >= 'A' && ch <= 'Z')
                continue;
            return false;
        }
        return true;
    }
}