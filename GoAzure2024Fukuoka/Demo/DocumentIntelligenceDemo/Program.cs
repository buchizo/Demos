using Azure;
using Azure.AI.DocumentIntelligence;
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

var model = "prebuilt-layout";

Console.Write("input document uri: ");
var documentUri = Console.ReadLine() ?? "";

// using var file = File.OpenRead(@"fuga.pdf");

var ops = await client.AnalyzeDocumentAsync(
    WaitUntil.Started,
    model,
    analyzeRequest: new AnalyzeDocumentContent() {
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

var result = await ops.WaitForCompletionAsync();

Console.WriteLine("finished");