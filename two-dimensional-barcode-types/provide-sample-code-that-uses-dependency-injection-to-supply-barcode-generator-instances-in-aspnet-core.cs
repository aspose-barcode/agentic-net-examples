// Title: Dependency Injection for Aspose.BarCode Generator in ASP.NET Core
// Description: Demonstrates how to register and resolve a BarcodeGenerator using Microsoft.Extensions.DependencyInjection to create a Code128 barcode image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of the BarcodeGenerator class together with ASP.NET Core's built‑in dependency injection. Developers often need to inject barcode generators into controllers or services to produce various symbologies (e.g., Code128, QR) on demand, configure parameters, and output images or streams. The pattern shown here is common for creating reusable, testable barcode services in web applications.
// Prompt: Provide sample code that uses dependency injection to supply barcode generator instances in ASP.NET Core.
// Tags: code128, barcode generation, dependency injection, aspnet core, aspose.barcode, png

using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Sample console application that demonstrates registering an <see cref="BarcodeGenerator"/>
/// with ASP.NET Core's dependency injection container and using it to generate a Code128 barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Sets up DI, resolves a <see cref="BarcodeGenerator"/>, configures it,
    /// and saves the generated barcode to a PNG file.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // ------------------------------------------------------------
        // 1. Configure a simple DI container.
        // ------------------------------------------------------------
        var services = new ServiceCollection();

        // Register a transient BarcodeGenerator that creates a Code128 barcode.
        // The generator implements IDisposable, so the container will dispose it when the scope ends.
        services.AddTransient<BarcodeGenerator>(provider =>
        {
            // Initialise the generator with the desired symbology and value.
            return new BarcodeGenerator(EncodeTypes.Code128, "Sample123");
        });

        // Build the service provider to resolve services.
        var serviceProvider = services.BuildServiceProvider();

        // ------------------------------------------------------------
        // 2. Resolve the generator and generate the barcode.
        // ------------------------------------------------------------
        using (var generator = serviceProvider.GetRequiredService<BarcodeGenerator>())
        {
            // Configure visual parameters (e.g., module size and bar height).
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Parameters.Barcode.BarHeight.Point = 40f;

            // Determine the output file path in the current working directory.
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "code128.png");

            // Save the barcode image as PNG.
            generator.Save(outputPath);

            // Inform the user where the file was saved.
            Console.WriteLine($"Barcode saved to {outputPath}");
        }

        // Note: In a real ASP.NET Core web application the BarcodeGenerator would be injected
        // into controllers or services via constructor injection. This console program simply
        // illustrates the DI registration and usage pattern required for the task.
    }
}