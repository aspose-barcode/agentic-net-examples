using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Create a unique temporary folder
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Paths for the generated barcode image and the PowerShell script
        string barcodeImagePath = Path.Combine(tempFolder, "sample_barcode.png");
        string psScriptPath = Path.Combine(tempFolder, "ReadBarcodes.ps1");

        // Generate a sample barcode image
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            generator.Save(barcodeImagePath, BarCodeImageFormat.Png);
        }

        // Determine the path to the Aspose.BarCode assembly
        string assemblyPath = typeof(BarcodeGenerator).Assembly.Location;

        // Build the PowerShell script content
        string psScript = $@"
# PowerShell script to read barcodes and output confidence scores
$assemblyPath = '{assemblyPath}'
Add-Type -Path $assemblyPath

$imagePath = '{barcodeImagePath}'

$reader = New-Object Aspose.BarCode.BarCodeRecognition.BarCodeReader($imagePath, [Aspose.BarCode.DecodeType]::AllSupportedTypes)
$results = $reader.ReadBarCodes()
foreach ($result in $results) {{
    Write-Output ""CodeText: $($result.CodeText)""
    Write-Output ""CodeType: $($result.CodeTypeName)""
    Write-Output ""Confidence: $($result.Confidence)""
    Write-Output ""ReadingQuality: $($result.ReadingQuality)""
}}
";

        // Write the PowerShell script to file
        File.WriteAllText(psScriptPath, psScript);

        // Output information for the user
        Console.WriteLine("Generated barcode image at:");
        Console.WriteLine(barcodeImagePath);
        Console.WriteLine();
        Console.WriteLine("PowerShell script created at:");
        Console.WriteLine(psScriptPath);
        Console.WriteLine();
        Console.WriteLine("Run the script with PowerShell Core to see confidence scores.");
    }
}