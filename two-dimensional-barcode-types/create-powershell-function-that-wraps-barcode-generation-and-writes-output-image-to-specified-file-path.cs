// Title: Generate PowerShell Barcode Function with Aspose.BarCode
// Description: Demonstrates how to create a PowerShell script that wraps Aspose.BarCode to generate a barcode image and save it to a specified file path.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of EncodeTypes, BarcodeGenerator, and BarCodeImageFormat classes. It illustrates a typical scenario where developers need to expose barcode creation functionality to PowerShell scripts for automation or integration purposes. Ideal for developers automating document workflows, inventory systems, or any application requiring on‑the‑fly barcode image generation.
// Prompt: Create a PowerShell function that wraps barcode generation and writes output image to specified file path.
// Tags: barcode, symbology, generation, powershell, image, png, aspose.barcode

using System;
using System.IO;

/// <summary>
/// Provides functionality to generate a PowerShell script containing a barcode generation function.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Writes the PowerShell function to a temporary file and reports its location.
    /// </summary>
    static void Main()
    {
        // Define the path for the generated PowerShell script in the system's temporary folder.
        string scriptPath = Path.Combine(Path.GetTempPath(), "GenerateBarcode.ps1");

        // Write the PowerShell function definition to the specified file.
        WritePowerShellFunction(scriptPath);

        // Inform the user where the script was saved.
        Console.WriteLine($"PowerShell function written to: {scriptPath}");
    }

    /// <summary>
    /// Generates a PowerShell script file that defines a <c>Generate-Barcode</c> function.
    /// The function loads Aspose.BarCode, creates a barcode based on provided parameters, and saves it as a PNG image.
    /// </summary>
    /// <param name="filePath">Full path of the PowerShell script file to create.</param>
    static void WritePowerShellFunction(string filePath)
    {
        // PowerShell function source code as a verbatim string.
        string psFunction = @"
function Generate-Barcode {
    param(
        [Parameter(Mandatory=$true)][string]$CodeText,
        [Parameter(Mandatory=$true)][string]$Symbology,
        [Parameter(Mandatory=$true)][string]$OutputPath
    )

    # Load Aspose.BarCode assembly (assumes Aspose.BarCode.dll is in the same directory as this script)
    $assemblyPath = Join-Path -Path $PSScriptRoot -ChildPath 'Aspose.BarCode.dll'
    if (-not (Test-Path $assemblyPath)) {
        Write-Error ""Aspose.BarCode.dll not found at $assemblyPath""
        return
    }
    Add-Type -Path $assemblyPath

    # Resolve symbology name to BaseEncodeType via reflection
    $field = [Aspose.BarCode.Generation.EncodeTypes].GetField($Symbology)
    if ($null -eq $field) {
        Write-Error ""Unknown symbology: $Symbology""
        return
    }
    $encodeType = $field.GetValue($null)

    # Create the barcode generator
    $generator = New-Object Aspose.BarCode.Generation.BarcodeGenerator($encodeType, $CodeText)

    # Save the barcode image as PNG
    $generator.Save($OutputPath, [Aspose.BarCode.Generation.BarCodeImageFormat]::Png)
}
";

        // Ensure the target directory exists; create it if necessary.
        string directory = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Write the PowerShell script content to the file.
        File.WriteAllText(filePath, psFunction);
    }
}