// Title: Generate PowerShell script for Aspose.BarCode barcode reading with confidence scores
// Description: This example creates a temporary PowerShell script that loads the Aspose.BarCode .NET library, reads a list of barcode images, and outputs each barcode's text, type, confidence score, and reading quality.
// Category-Description: Demonstrates how to use Aspose.BarCode's BarCodeReader class from a PowerShell script executed via .NET Core. The example shows loading the Aspose.BarCode assembly, initializing BarCodeReader with all supported decode types, iterating over detection results, and retrieving detailed properties such as Confidence and ReadingQuality. Developers working with barcode recognition automation, batch processing, or integration with scripting environments will find this pattern useful for generating reusable scripts that expose rich barcode metadata.
// Prompt: Create a PowerShell script that invokes BarCodeReader via .NET Core to process barcode images and output confidence scores.
// Tags: barcode symbology, reading, confidence, powershell, aspose.barcode, .net core

using System;
using System.IO;

/// <summary>
/// Generates a PowerShell script that utilizes Aspose.BarCode's BarCodeReader to read barcodes
/// from image files and prints detailed information including confidence scores.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a temporary folder, writes the PowerShell script,
    /// and informs the user of the script location.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the generated PowerShell script
        string tempFolder = Path.Combine(Path.GetTempPath(), "PsScript_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the PowerShell script content that loads Aspose.BarCode and reads barcodes
        string scriptContent = @"
# PowerShell script to read barcodes and output confidence scores using Aspose.BarCode
$assemblyPath = ""<Path_To_Aspose.BarCode.dll>""
Add-Type -Path $assemblyPath

# List of barcode image files to process
$images = @(
    ""sample1.png"",
    ""sample2.png""
)

foreach ($img in $images) {
    if (Test-Path $img) {
        $reader = New-Object Aspose.BarCode.BarCodeRecognition.BarCodeReader($img, [Aspose.BarCode.BarCodeRecognition.DecodeType]::AllSupportedTypes)
        $results = $reader.ReadBarCodes()
        foreach ($res in $results) {
            Write-Output ""File: $img""
            Write-Output ""CodeText: $($res.CodeText)""
            Write-Output ""CodeType: $($res.CodeTypeName)""
            Write-Output ""Confidence: $($res.Confidence)""
            Write-Output ""ReadingQuality: $($res.ReadingQuality)""
        }
        $reader.Dispose()
    } else {
        Write-Output ""File not found: $img""
    }
}
".TrimStart();

        // Write the script content to a .ps1 file inside the temporary folder
        string scriptPath = Path.Combine(tempFolder, "ReadBarcodes.ps1");
        File.WriteAllText(scriptPath, scriptContent);

        // Inform the user where the PowerShell script has been saved
        Console.WriteLine($"PowerShell script written to: {scriptPath}");
    }
}