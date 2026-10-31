// Title: Validate non‑customer fields for C40 character set before Mailmark 2D barcode generation
// Description: Demonstrates how to validate specific Mailmark 2D fields against the C40 character set prior to generating a barcode image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as Mailmark 2D. It showcases the use of Aspose.BarCode.ComplexBarcode.Mailmark2DCodetext, ComplexBarcodeGenerator, and validation logic for character set compliance. Developers creating postal or logistics barcodes often need to ensure data fields meet encoding requirements before rendering the barcode.
// Prompt: Validate that all non‑customer fields conform to the C40 character set before generation.
// Tags: mailmark,barcode,generation,c40,validation,complexbarcode,aspnet,aspnetcore,aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that validates non‑customer fields against the C40 character set
/// and generates a Mailmark 2D barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates sample Mailmark 2D data, validates required fields,
    /// and generates a PNG barcode image if validation succeeds.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare sample Mailmark 2D data (non‑customer fields and content)
        // ------------------------------------------------------------
        var mailmark = new Mailmark2DCodetext
        {
            UPUCountryID = "JGB ",
            InformationTypeID = "0",
            VersionID = "1",
            Class = "1",
            SupplyChainID = 123,
            ItemID = 456,
            DestinationPostCodeAndDPS = "EF61AH8T ",
            CustomerContent = "ABC123",
            CustomerContentEncodeMode = DataMatrixEncodeMode.C40,
            DataMatrixType = Mailmark2DType.Type_7
        };

        // ------------------------------------------------------------
        // Validate non‑customer fields to ensure they contain only C40‑allowed characters
        // ------------------------------------------------------------
        if (!ValidateC40(mailmark.UPUCountryID, "UPUCountryID") ||
            !ValidateC40(mailmark.InformationTypeID, "InformationTypeID") ||
            !ValidateC40(mailmark.VersionID, "VersionID") ||
            !ValidateC40(mailmark.Class, "Class") ||
            !ValidateC40(mailmark.DestinationPostCodeAndDPS, "DestinationPostCodeAndDPS"))
        {
            Console.WriteLine("Validation failed. Barcode generation aborted.");
            return;
        }

        // ------------------------------------------------------------
        // Generate the Mailmark 2D barcode and save it as a PNG file
        // ------------------------------------------------------------
        string outputPath = Path.Combine(Path.GetTempPath(), "Mailmark2D.png");
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode generated successfully at: {outputPath}");
    }

    /// <summary>
    /// Validates that the provided text conforms to the C40 character set (digits, uppercase A‑Z, and space).
    /// </summary>
    /// <param name="text">The string to validate.</param>
    /// <param name="fieldName">Name of the field being validated (used for error messages).</param>
    /// <returns>True if the text is valid; otherwise, false.</returns>
    static bool ValidateC40(string text, string fieldName)
    {
        if (string.IsNullOrEmpty(text))
        {
            Console.WriteLine($"{fieldName} is null or empty.");
            return false;
        }

        foreach (char c in text)
        {
            if (c == ' ') continue;
            if (c >= '0' && c <= '9') continue;
            if (c >= 'A' && c <= 'Z') continue;

            Console.WriteLine($"{fieldName} contains invalid character '{c}'. Allowed: digits, uppercase A‑Z, space.");
            return false;
        }

        return true;
    }
}