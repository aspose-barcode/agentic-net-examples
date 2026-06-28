using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generation of a Mailmark 2D barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a Mailmark2DCodetext, configures its fields,
    /// generates the barcode, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Instantiate a Mailmark2DCodetext object to hold barcode data.
        var mailmark2d = new Mailmark2DCodetext();

        // -------------------------
        // Routing information
        // -------------------------
        // Post code and DPS (Delivery Point Suffix) – note the trailing space is required.
        mailmark2d.DestinationPostCodeAndDPS = "EF61AH8T ";

        // -------------------------
        // Service / class information
        // -------------------------
        mailmark2d.VersionID = "1";               // Version identifier
        mailmark2d.InformationTypeID = "0";       // Information type identifier
        mailmark2d.Class = "1";                   // Class of the item
        mailmark2d.RTSFlag = "0";                 // Return‑to‑sender flag

        // -------------------------
        // Customer data
        // -------------------------
        mailmark2d.SupplyChainID = 1234567;       // Supply chain identifier
        mailmark2d.ItemID = 12345678;             // Unique item identifier
        mailmark2d.CustomerContent = "CUSTOMER123"; // Optional customer content
        // Encoding mode for the customer content (C40 is a compact alphanumeric mode).
        mailmark2d.CustomerContentEncodeMode = DataMatrixEncodeMode.C40;
        // Determines the size of the DataMatrix barcode; Auto lets the library choose.
        mailmark2d.DataMatrixType = Mailmark2DType.Auto;

        // -------------------------
        // Barcode generation and saving
        // -------------------------
        string outputPath = "mailmark2d.png";

        // Use ComplexBarcodeGenerator to create the barcode image.
        using (var generator = new ComplexBarcodeGenerator(mailmark2d))
        {
            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the full path of the saved file for user confirmation.
        Console.WriteLine($"Mailmark 2D barcode saved to: {Path.GetFullPath(outputPath)}");
    }
}