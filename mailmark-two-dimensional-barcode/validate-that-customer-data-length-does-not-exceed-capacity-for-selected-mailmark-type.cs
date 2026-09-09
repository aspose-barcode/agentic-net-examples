// Title: Validate Mailmark2D Customer Data Length Against Capacity
// Description: Demonstrates how to verify that a customer data string fits within the allowed length for a selected Mailmark2D type before generating the barcode.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of Mailmark2DCodetext, ComplexBarcodeGenerator, and related classes to create Mailmark2D barcodes. Typical scenarios include postal automation and logistics where developers need to ensure data fits the symbology's capacity limits before rendering the barcode.
// Prompt: Validate that customer data length does not exceed capacity for the selected Mailmark type.
// Tags: barcode, mailmark, validation, datamatrix, aspose.barcode, complexbarcode, csharp

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that validates customer data length for a chosen Mailmark2D type
/// and generates a Mailmark2D barcode if the data fits within the allowed capacity.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs validation, creates the Mailmark2D codetext,
    /// and saves the generated barcode image to a temporary file.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Define the Mailmark2D type and sample customer data to validate
        // ------------------------------------------------------------
        Mailmark2DType selectedType = Mailmark2DType.Type_9;
        string customerData = "CUSTOMER DATA EXAMPLE";

        // ------------------------------------------------------------
        // Validate that the customer data length does not exceed the type's capacity
        // ------------------------------------------------------------
        int maxLength = GetCustomerContentCapacity(selectedType);
        if (customerData.Length > maxLength)
        {
            Console.WriteLine($"Error: Customer data length ({customerData.Length}) exceeds capacity ({maxLength}) for Mailmark2D type {selectedType}.");
            return;
        }
        Console.WriteLine($"Customer data length ({customerData.Length}) is within capacity ({maxLength}) for Mailmark2D type {selectedType}.");

        // ------------------------------------------------------------
        // Build the Mailmark2DCodetext object with the validated data
        // ------------------------------------------------------------
        var mailmark2D = new Mailmark2DCodetext
        {
            UPUCountryID = "JGB ",
            InformationTypeID = "0",
            VersionID = "1",
            Class = "1",
            SupplyChainID = 123,
            ItemID = 456,
            DestinationPostCodeAndDPS = "EF61AH8T ",
            CustomerContent = customerData,
            CustomerContentEncodeMode = DataMatrixEncodeMode.C40,
            DataMatrixType = selectedType
        };

        // ------------------------------------------------------------
        // Generate the barcode image and save it to a temporary location
        // ------------------------------------------------------------
        string outputPath = Path.Combine(Path.GetTempPath(), "Mailmark2D.png");
        using (var generator = new ComplexBarcodeGenerator(mailmark2D))
        {
            // Set the X-dimension (module size) for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Save(outputPath);
        }

        Console.WriteLine($"Barcode generated at: {outputPath}");
    }

    /// <summary>
    /// Retrieves the maximum allowed length of the CustomerContent field for a given Mailmark2D type.
    /// </summary>
    /// <param name="type">The Mailmark2D type whose capacity is required.</param>
    /// <returns>The maximum number of characters allowed for CustomerContent.</returns>
    /// <exception cref="ArgumentException">Thrown when an unsupported Mailmark2D type is supplied.</exception>
    static int GetCustomerContentCapacity(Mailmark2DType type)
    {
        // Mapping of Mailmark2D types to their respective customer content capacities
        var capacities = new Dictionary<Mailmark2DType, int>
        {
            { Mailmark2DType.Type_7, 20 },
            { Mailmark2DType.Type_9, 30 },
            { Mailmark2DType.Type_29, 50 }
        };

        if (!capacities.TryGetValue(type, out int capacity))
        {
            throw new ArgumentException($"Unsupported Mailmark2D type: {type}");
        }

        return capacity;
    }
}