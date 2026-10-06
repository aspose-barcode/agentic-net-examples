// Title: Mailmark 2D Barcode Generation with Customer Data Length Validation
// Description: Demonstrates how to validate the length of customer data against the capacity of a selected Mailmark 2D type and generate a Mailmark barcode image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, focusing on Mailmark 2D symbology. It showcases the use of Mailmark2DCodetext, ComplexBarcodeGenerator, and related parameter settings to create high‑density DataMatrix barcodes for postal applications. Developers working with postal automation, logistics, or any scenario requiring Mailmark barcodes can reference this pattern for data validation, barcode configuration, and image output.
// Prompt: Validate that customer data length does not exceed capacity for the selected Mailmark type.
// Tags: mailmark, barcode, validation, csharp, aspose.barcode, complexbarcode, datamatrix, image, png

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that validates customer data length for a Mailmark 2D type and generates a barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Validates data length, creates Mailmark2DCodetext, and saves the barcode as PNG.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Sample input data and selected Mailmark type
        // ------------------------------------------------------------
        string customerData = "CUSTOM";
        Mailmark2DType selectedType = Mailmark2DType.Type_7;

        // ------------------------------------------------------------
        // Validate that the customer data fits within the allowed capacity
        // ------------------------------------------------------------
        int maxLength = GetMaxCustomerDataLength(selectedType);
        if (customerData.Length > maxLength)
        {
            throw new ArgumentException(
                $"Customer data length ({customerData.Length}) exceeds maximum ({maxLength}) for type {selectedType}.");
        }

        // ------------------------------------------------------------
        // Populate Mailmark2DCodetext with required fields
        // ------------------------------------------------------------
        Mailmark2DCodetext mailmark2D = new Mailmark2DCodetext
        {
            UPUCountryID = "JGB ",
            InformationTypeID = "0",
            VersionID = "1",
            Class = "1",
            SupplyChainID = 123,
            ItemID = 456,
            DestinationPostCodeAndDPS = "EF61AH8T ",
            CustomerContent = customerData,
            DataMatrixType = selectedType
        };

        // ------------------------------------------------------------
        // Generate the barcode image and save it as PNG
        // ------------------------------------------------------------
        string outputPath = Path.Combine(Path.GetTempPath(), "Mailmark2D.png");
        using (var generator = new ComplexBarcodeGenerator(mailmark2D))
        {
            // Set X-dimension (module size) in pixels for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode generated successfully: {outputPath}");
    }

    /// <summary>
    /// Returns the maximum allowed length of the CustomerContent field for a given Mailmark2D type.
    /// </summary>
    /// <param name="type">The Mailmark2D type to evaluate.</param>
    /// <returns>Maximum number of characters allowed for CustomerContent.</returns>
    static int GetMaxCustomerDataLength(Mailmark2DType type)
    {
        // Capacities based on Mailmark 2D specifications
        switch (type)
        {
            case Mailmark2DType.Type_7:
                return 6;
            case Mailmark2DType.Type_9:
                return 12;
            case Mailmark2DType.Type_29:
                return 30;
            default:
                throw new ArgumentOutOfRangeException(
                    nameof(type), $"Unsupported Mailmark2DType: {type}");
        }
    }
}