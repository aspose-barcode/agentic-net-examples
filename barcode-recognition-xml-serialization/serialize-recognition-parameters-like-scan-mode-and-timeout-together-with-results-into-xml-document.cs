// Title: Serialize barcode recognition parameters and results to XML
// Description: Generates a Code128 barcode, reads it using custom recognition settings, and writes both the settings and detection results into an XML file.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them with configurable parameters such as timeout, checksum validation, and quality settings. Developers often need to persist recognition metadata alongside results for auditing, debugging, or downstream processing, making XML serialization a common practice in barcode solutions.
// Prompt: Serialize recognition parameters like scan mode and timeout together with results into an XML document.
// Tags: barcode, generation, recognition, xml, serialization, aspose.barcode, code128

using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation, recognition with custom parameters, and serialization of results to an XML file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a barcode, reads it, and saves recognition data to an XML file.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary working folder for the demo files
        string workFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Define file paths for the generated barcode image and the output XML document
        string barcodePath = Path.Combine(workFolder, "barcode.png");
        string xmlPath = Path.Combine(workFolder, "recognition_result.xml");

        // -------------------- Barcode Generation --------------------
        // Create a simple Code128 barcode with the specified text
        string codeText = "ABC123456";
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Optional visual settings
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the barcode image as PNG
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // -------------------- Barcode Recognition --------------------
        // Configure the reader to support all barcode types
        BaseDecodeType decodeType = DecodeType.AllSupportedTypes;
        using (var reader = new BarCodeReader(barcodePath, decodeType))
        {
            // Set recognition parameters
            reader.Timeout = 5000; // milliseconds
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
            reader.BarcodeSettings.DetectEncoding = true;
            reader.BarcodeSettings.StripFNC = true;
            reader.QualitySettings = QualitySettings.HighPerformance;
            reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;
            reader.QualitySettings.InverseImage = InverseImageMode.Auto;

            // Attempt to read barcodes, handling possible timeout aborts
            BarCodeResult[] results;
            try
            {
                results = reader.ReadBarCodes();
            }
            catch (RecognitionAbortedException ex)
            {
                Console.WriteLine($"Recognition aborted after {ex.ExecutionTime} ms.");
                results = Array.Empty<BarCodeResult>();
            }

            // -------------------- XML Serialization --------------------
            // Build the XML document containing both parameters and results
            var doc = new XDocument(
                new XElement("RecognitionResult",
                    new XElement("Parameters",
                        new XElement("Timeout", reader.Timeout),
                        new XElement("ChecksumValidation", reader.BarcodeSettings.ChecksumValidation.ToString()),
                        new XElement("DetectEncoding", reader.BarcodeSettings.DetectEncoding),
                        new XElement("StripFNC", reader.BarcodeSettings.StripFNC),
                        new XElement("QualityPreset", reader.QualitySettings.ToString()),
                        new XElement("Deconvolution", reader.QualitySettings.Deconvolution.ToString()),
                        new XElement("InverseImage", reader.QualitySettings.InverseImage.ToString())
                    ),
                    new XElement("Results")
                )
            );

            var resultsElement = doc.Root.Element("Results");

            // Populate the XML with each recognition result
            foreach (var result in results)
            {
                var regionRect = result.Region.Rectangle;
                var resultElement = new XElement("Result",
                    new XElement("CodeText", result.CodeText),
                    new XElement("CodeTypeName", result.CodeTypeName),
                    new XElement("ReadingQuality", result.ReadingQuality),
                    new XElement("Region",
                        new XElement("X", regionRect.X),
                        new XElement("Y", regionRect.Y),
                        new XElement("Width", regionRect.Width),
                        new XElement("Height", regionRect.Height),
                        new XElement("Angle", result.Region.Angle)
                    )
                );
                resultsElement.Add(resultElement);
            }

            // Save the XML document to the specified path
            doc.Save(xmlPath);
        }

        // Output file locations for verification
        Console.WriteLine($"Barcode image saved to: {barcodePath}");
        Console.WriteLine($"Recognition XML saved to: {xmlPath}");
    }
}