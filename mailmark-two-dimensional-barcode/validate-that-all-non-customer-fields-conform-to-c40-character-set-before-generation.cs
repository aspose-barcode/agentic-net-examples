// Title: Validate non‑customer fields for C40 character set and generate Mailmark 2D barcode
// Description: Demonstrates how to validate that non‑customer fields of a Mailmark 2D barcode conform to the C40 character set before generating the barcode image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of Aspose.BarCode.ComplexBarcode classes such as Mailmark2DCodetext and ComplexBarcodeGenerator to create Mailmark 2D symbols. Typical scenarios include postal automation and logistics where specific field validation (e.g., C40 character set) is required before barcode rendering. Developers often need to validate input data, configure encoding modes, and save the resulting image in common formats.
// Prompt: Validate that all non‑customer fields conform to the C40 character set before generation.
// Tags: barcode, validation, c40, mailmark, complexbarcode, generation, png, aspose.barcode

using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that validates non‑customer fields against the C40 character set
/// and generates a Mailmark 2D barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Sets up Mailmark2D data, validates fields, generates and saves the barcode.
    /// </summary>
    static void Main()
    {
        // Define sample Mailmark 2D data with required fields
        var mailmark2D = new Mailmark2DCodetext
        {
            UPUCountryID = "JGB ",
            InformationTypeID = "0",
            VersionID = "1",
            Class = "1",
            SupplyChainID = 123,
            ItemID = 1234,
            DestinationPostCodeAndDPS = "EF61AH8T ",
            CustomerContent = "ABC123",
            CustomerContentEncodeMode = DataMatrixEncodeMode.C40,
            DataMatrixType = Mailmark2DType.Type_7
        };

        // Validate each non‑customer field to ensure it contains only C40 characters
        ValidateC40(mailmark2D.UPUCountryID, nameof(mailmark2D.UPUCountryID));
        ValidateC40(mailmark2D.InformationTypeID, nameof(mailmark2D.InformationTypeID));
        ValidateC40(mailmark2D.VersionID, nameof(mailmark2D.VersionID));
        ValidateC40(mailmark2D.Class, nameof(mailmark2D.Class));
        ValidateC40(mailmark2D.DestinationPostCodeAndDPS, nameof(mailmark2D.DestinationPostCodeAndDPS));

        // Generate the barcode and save it as a PNG file in the temporary folder
        string outputPath = Path.Combine(Path.GetTempPath(), "Mailmark2D.png");
        using (var generator = new ComplexBarcodeGenerator(mailmark2D))
        {
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Mailmark 2D barcode saved to: {outputPath}");
    }

    /// <summary>
    /// Validates that a string contains only characters allowed in the C40 character set (digits, uppercase letters, space).
    /// </summary>
    /// <param name="value">The string value to validate.</param>
    /// <param name="fieldName">The name of the field being validated (used in exception messages).</param>
    static void ValidateC40(string value, string fieldName)
    {
        if (value == null)
            throw new ArgumentException($"{fieldName} cannot be null.");

        // C40 character set: digits, uppercase letters, space
        if (!Regex.IsMatch(value, @"^[0-9A-Z ]*$"))
            throw new ArgumentException($"{fieldName} contains characters outside the C40 set.");
    }
}