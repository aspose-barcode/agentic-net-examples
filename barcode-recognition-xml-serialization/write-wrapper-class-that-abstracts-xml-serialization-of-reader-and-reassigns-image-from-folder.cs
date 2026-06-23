using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Wrapper around Aspose.BarCode's <see cref="BarCodeReader"/> providing simplified
/// configuration loading, image assignment, and barcode reading functionality.
/// </summary>
public class BarcodeReaderWrapper
{
    // Internal Aspose barcode reader instance.
    private readonly BarCodeReader _reader;

    /// <summary>
    /// Initializes a new instance of <see cref="BarcodeReaderWrapper"/>.
    /// </summary>
    public BarcodeReaderWrapper()
    {
        // Create a fresh BarCodeReader object.
        _reader = new BarCodeReader();
    }

    /// <summary>
    /// Loads reader configuration from an XML file.
    /// </summary>
    /// <param name="xmlFilePath">Path to the XML configuration file.</param>
    public void LoadConfiguration(string xmlFilePath)
    {
        // Verify that the configuration file exists before attempting to load it.
        if (!File.Exists(xmlFilePath))
        {
            Console.WriteLine($"Configuration file not found: {xmlFilePath}");
            return;
        }

        // Open the XML file as a read‑only stream and import its settings.
        using (var stream = new FileStream(xmlFilePath, FileMode.Open, FileAccess.Read))
        {
            // Apply the XML configuration to the internal reader.
            BarCodeReader.ImportFromXml(stream);
        }
    }

    /// <summary>
    /// Assigns an image file located in a folder to the reader.
    /// </summary>
    /// <param name="folderPath">Folder containing the image.</param>
    /// <param name="imageFileName">Name of the image file.</param>
    public void SetImageFromFolder(string folderPath, string imageFileName)
    {
        // Build the full path to the image file.
        string imagePath = Path.Combine(folderPath, imageFileName);

        // Ensure the image file exists before setting it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Provide the image to the internal barcode reader.
        _reader.SetBarCodeImage(imagePath);
    }

    /// <summary>
    /// Reads barcodes using the configured reader.
    /// </summary>
    /// <returns>Array of <see cref="BarCodeResult"/> objects representing detected barcodes.</returns>
    public BarCodeResult[] ReadBarcodes()
    {
        // Execute the barcode reading operation.
        return _reader.ReadBarCodes();
    }
}

/// <summary>
/// Demonstrates generation of a barcode, exporting its configuration,
/// and using <see cref="BarcodeReaderWrapper"/> to read the barcode back.
/// </summary>
public class Program
{
    /// <summary>
    /// Application entry point.
    /// </summary>
    public static void Main()
    {
        // Define file names for the generated barcode image and its configuration.
        string imageFile = "sample_barcode.png";
        string configFile = "sample_config.xml";

        // Generate a sample barcode and export its configuration to XML.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Save the barcode image to disk.
            generator.Save(imageFile);
            // Export the generator's settings to an XML configuration file.
            generator.ExportToXml(configFile);
        }

        // Create the wrapper, load the configuration, assign the image, and read barcodes.
        var wrapper = new BarcodeReaderWrapper();
        wrapper.LoadConfiguration(configFile);
        wrapper.SetImageFromFolder(".", imageFile);
        var results = wrapper.ReadBarcodes();

        // Check if any barcodes were detected and output the results.
        if (results == null || results.Length == 0)
        {
            Console.WriteLine("No barcodes detected.");
        }
        else
        {
            foreach (var result in results)
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");
            }
        }
    }
}