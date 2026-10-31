// Title: Mailmark2D Barcode Generation with Customer Data Length Validation
// Description: Demonstrates how to validate that the customer data fits within the capacity of a selected Mailmark2D type before generating the barcode.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, focusing on Mailmark2D symbology. It shows the use of ComplexBarcodeGenerator, Mailmark2DCodetext, and related parameter settings to create a Mailmark2D barcode. Developers commonly need to ensure data fits the symbol's capacity, configure encoding modes, and save the barcode image.
// Prompt: Validate that customer data length does not exceed capacity for the selected Mailmark type.
// Tags: mailmark2d, barcode, validation, capacity, aspnet, aspose.barcode, complexbarcode, datamatrix, c40, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an example of generating a Mailmark2D barcode after validating that
/// the supplied customer content does not exceed the capacity of the chosen Mailmark2D type.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Validates customer data length, builds the Mailmark2D codetext,
    /// generates the barcode, and saves it as a PNG image.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Define sample data and output location
        // ------------------------------------------------------------
        string outputPath = Path.Combine(Path.GetTempPath(), "Mailmark2D.png");
        string customerContent = "CUSTOMER DATA EXAMPLE";
        Mailmark2DType selectedType = Mailmark2DType.Type_9;

        // ------------------------------------------------------------
        // Validate that the customer content fits within the selected type's capacity
        // ------------------------------------------------------------
        int capacity = GetCustomerContentCapacity(selectedType);
        if (customerContent.Length > capacity)
        {
            Console.WriteLine($"Error: Customer content length ({customerContent.Length}) exceeds capacity ({capacity}) for Mailmark2D type {selectedType}.");
            return;
        }

        // ------------------------------------------------------------
        // Build the Mailmark2D codetext with required fields
        // ------------------------------------------------------------
        Mailmark2DCodetext mailmark2D = new Mailmark2DCodetext
        {
            UPUCountryID = "JGB ",
            InformationTypeID = "0",
            VersionID = "1",
            Class = "1",
            SupplyChainID = 123,
            ItemID = 1234,
            DestinationPostCodeAndDPS = "EF61AH8T ",
            CustomerContent = customerContent,
            DataMatrixType = selectedType,
            CustomerContentEncodeMode = DataMatrixEncodeMode.C40
        };

        // ------------------------------------------------------------
        // Generate the barcode and save it to the specified file
        // ------------------------------------------------------------
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(mailmark2D))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Mailmark2D barcode generated successfully at: {outputPath}");
    }

    // ------------------------------------------------------------
    // Helper method: approximate capacities for each Mailmark2D type (C40 encoding)
    // ------------------------------------------------------------
    static int GetCustomerContentCapacity(Mailmark2DType type)
    {
        return type switch
        {
            Mailmark2DType.Type_7 => 20,
            Mailmark2DType.Type_9 => 30,
            Mailmark2DType.Type_29 => 70,
            _ => 0
        };
    }
}