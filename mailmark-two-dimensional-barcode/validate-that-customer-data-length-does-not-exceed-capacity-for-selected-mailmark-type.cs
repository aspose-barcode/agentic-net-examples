// Title: Validate Mailmark2D Customer Content Length
// Description: Demonstrates how to verify that customer data fits within the capacity limits of selected Mailmark 2D types before generating barcodes.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, focusing on Mailmark2D validation and image creation. It showcases key API classes such as Mailmark2DCodetext, ComplexBarcodeGenerator, and BarCodeImageFormat, which developers commonly use to produce Mailmark barcodes for postal services while ensuring data compliance.
// Prompt: Validate that customer data length does not exceed capacity for the selected Mailmark type.
// Tags: mailmark, validation, png, complexbarcode, generation, aspnet.barcode

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that validates customer content length for Mailmark2D types
/// and generates corresponding barcode images.
/// </summary>
class Program
{
    // Mapping of Mailmark 2D type to maximum allowed customer content length.
    private static readonly Dictionary<int, int> MaxContentLengthByType = new()
    {
        { 7, 6 },   // Type 7: 6 characters
        { 9, 45 },  // Type 9: 45 characters
        { 29, 25 }  // Type 29: 25 characters
    };

    /// <summary>
    /// Entry point. Iterates over sample records, validates content length,
    /// builds Mailmark2DCodetext objects, and saves barcode images.
    /// </summary>
    static void Main()
    {
        // Sample records: each tuple contains (Mailmark2D type, customer content)
        var records = new List<(int Type, string Content)>
        {
            (7, "ABC123"),          // exactly 6 chars – valid
            (9, "THIS IS A LONGER CONTENT EXAMPLE THAT FITS"), // 45 chars – valid
            (29, "TOO LONG CUSTOMER CONTENT EXCEEDING LIMIT") // exceeds 25 chars – invalid
        };

        int index = 1;
        foreach (var (type, content) in records)
        {
            try
            {
                // Validate that the content length is within the allowed limit for the type.
                ValidateCustomerContent(content, type);

                // Build Mailmark2DCodetext with required fields and sample values.
                var mailmark2d = new Mailmark2DCodetext
                {
                    // Required fields: InformationTypeID, VersionID, Class, RTSFlag, SupplyChainID, ItemID, DestinationPostCodeAndDPS
                    InformationTypeID = "0",
                    VersionID = "1",
                    Class = "1",
                    RTSFlag = "0",
                    SupplyChainID = 1234567,
                    ItemID = 1000 + index,
                    DestinationPostCodeAndDPS = "EF61AH8T ",
                    // Set the selected DataMatrix type (if needed). Assuming enum values match the integer.
                    // DataMatrixType = (DataMatrixType)type, // Uncomment if enum exists.
                    CustomerContent = content,
                    CustomerContentEncodeMode = DataMatrixEncodeMode.C40 // example encode mode
                };

                // Generate barcode image and write it to a PNG file.
                using (var generator = new ComplexBarcodeGenerator(mailmark2d))
                {
                    using (var ms = new MemoryStream())
                    {
                        generator.Save(ms, BarCodeImageFormat.Png);
                        ms.Position = 0;
                        string fileName = $"Mailmark2D_{index}.png";
                        File.WriteAllBytes(fileName, ms.ToArray());
                        Console.WriteLine($"Record {index}: Barcode saved to {fileName}");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                // Handle validation errors (e.g., content too long or unsupported type).
                Console.WriteLine($"Record {index}: Validation error – {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle any unexpected errors during barcode generation.
                Console.WriteLine($"Record {index}: Unexpected error – {ex.Message}");
            }

            index++;
        }
    }

    // Validates that the customer content length does not exceed the capacity for the given Mailmark type.
    private static void ValidateCustomerContent(string content, int mailmarkType)
    {
        // Ensure the Mailmark type is supported.
        if (!MaxContentLengthByType.TryGetValue(mailmarkType, out int maxLength))
        {
            throw new ArgumentException($"Unsupported Mailmark type '{mailmarkType}'.");
        }

        // Ensure content is not null.
        if (content == null)
        {
            throw new ArgumentException("Customer content cannot be null.");
        }

        // Ensure content length does not exceed the maximum allowed for the type.
        if (content.Length > maxLength)
        {
            throw new ArgumentException($"Customer content length ({content.Length}) exceeds maximum allowed ({maxLength}) for Mailmark type {mailmarkType}.");
        }
    }
}