// Title: Load Barcode Reader State from XML, Set Image, and Re‑Export State
// Description: Demonstrates loading a BarCodeReader configuration from an XML state file, assigning a barcode image, and exporting the updated state back to XML.
// Category-Description: This example belongs to the Aspose.BarCode state management category, showcasing how to persist and reuse BarCodeReader settings using ImportFromXml and ExportToXml. It covers key API classes such as BarCodeReader, BarcodeGenerator, and related settings, which developers commonly use to configure barcode recognition, serialize configurations, and apply them across sessions or environments.
// Prompt: Write a script that loads an XML state, sets an image, and re‑exports the state to a file.
// Tags: barcode, qr, xml, state, import, export, aspose.barcode, image, reader

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR code, saves a BarCodeReader state to XML,
/// imports the state, reassigns the image, and re‑exports the state.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs image generation, state export/import,
    /// and displays the locations of generated files.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary working folder to store all generated files.
        string workFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Define file paths for the barcode image and XML state files.
        string imagePath = Path.Combine(workFolder, "sample.png");
        string statePath1 = Path.Combine(workFolder, "readerState1.xml");
        string statePath2 = Path.Combine(workFolder, "readerState2.xml");
        string outputImagePath = Path.Combine(workFolder, "generated.png");

        // 1. Generate a sample QR code image and save it as PNG.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "SampleText"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // 2. Create a BarCodeReader, configure its settings, assign the image,
        //    set the decode type, and export the current configuration to XML.
        using (BarCodeReader reader = new BarCodeReader())
        {
            // Example reader settings.
            reader.BarcodeSettings.StripFNC = true;
            reader.QualitySettings = QualitySettings.HighPerformance;
            reader.QualitySettings.XDimension = XDimensionMode.Small;

            // Assign the generated image and specify that only QR codes should be read.
            reader.SetBarCodeImage(imagePath);
            reader.SetBarCodeReadType(DecodeType.QR);

            // Export the configured reader state to the first XML file.
            reader.ExportToXml(statePath1);
        }

        // 3. Import the previously saved state, reassign the image (required after import),
        //    set the decode type again, and export the updated state to a new XML file.
        using (BarCodeReader importedReader = BarCodeReader.ImportFromXml(statePath1))
        {
            // Image must be set again after importing the state.
            importedReader.SetBarCodeImage(imagePath);
            importedReader.SetBarCodeReadType(DecodeType.QR);

            // Optionally, barcode reading could be performed here.
            // var results = importedReader.ReadBarCodes();

            // Export the re‑imported and updated state to the second XML file.
            importedReader.ExportToXml(statePath2);
        }

        // 4. Output the locations of the generated files for verification.
        Console.WriteLine("Barcode image generated at: " + imagePath);
        Console.WriteLine("Initial reader state saved at: " + statePath1);
        Console.WriteLine("Re‑imported reader state saved at: " + statePath2);
    }
}