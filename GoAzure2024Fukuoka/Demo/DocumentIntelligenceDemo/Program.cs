using Azure;
using Azure.AI.DocumentIntelligence;
using DocumentIntelligenceDemo;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

var endpoint = config.GetSection("DocumentIntelligence")["Endpoint"] ?? "";
var apiKey = config.GetSection("DocumentIntelligence")["ApiKey"] ?? "";

var client = new DocumentIntelligenceClient(
            new Uri(endpoint),
            new AzureKeyCredential(apiKey),
            new DocumentIntelligenceClientOptions(DocumentIntelligenceClientOptions.ServiceVersion.V2024_02_29_Preview)
            );

var modelId = "prebuilt-layout";

Console.Write("input document uri: ");
var documentUri = Console.ReadLine() ?? "";

// using var file = File.OpenRead(@"fuga.pdf");

var ops = await client.AnalyzeDocumentAsync(
    WaitUntil.Started,
    modelId,
    analyzeRequest: new AnalyzeDocumentContent()
    {
        UrlSource = new Uri(documentUri)
        //        Base64Source = await BinaryData.FromStreamAsync(file)
    },
    features:
    [
        DocumentAnalysisFeature.KeyValuePairs,
        DocumentAnalysisFeature.OcrHighResolution,
    ],
    outputContentFormat: ContentFormat.Markdown,
    cancellationToken: default
);

var res = await ops.WaitForCompletionAsync();
var result = res.Value;

// markdown
await File.WriteAllTextAsync("content.md", result.Content);

Console.WriteLine("--- figures ---");
foreach (var f in result.Figures.Take(2))
{
    Console.WriteLine(f.ToJson());
}

Console.WriteLine("finished");