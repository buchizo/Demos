using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech.Translation;

Console.WriteLine("Translate Sample");

using var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
var config = SpeechTranslationConfig.FromEndpoint(new Uri(Environment.GetEnvironmentVariable("AzureSpeechEndpoint") ?? ""), Environment.GetEnvironmentVariable("AzureSpeechApiKey"));

config.SpeechRecognitionLanguage = "ja-JP"; // 元音声の言語
config.AddTargetLanguage("en"); // 翻訳先の言語
config.SpeechSynthesisVoiceName = "ja-jp-Nanami:DragonHDLatestNeural";

using var translationRecognizer = new TranslationRecognizer(config, audioConfig);
var stopRecognition = new TaskCompletionSource<int>(TaskCreationOptions.RunContinuationsAsynchronously);

using var audioOutputConfig = AudioConfig.FromDefaultSpeakerOutput();
using var speechSynthesizer = new SpeechSynthesizer(config, audioOutputConfig);

translationRecognizer.Recognizing += (s, e) =>
{
    Console.WriteLine($"\tRECOGNIZING: {e.Result.Text}");
    foreach (var (language, translation) in e.Result.Translations)
    {
        Console.WriteLine($"\tTRANSLATING into '{language}': {translation}");
    }
};

translationRecognizer.Recognized += (s, e) =>
{
    if (e.Result.Reason == ResultReason.RecognizedSpeech)
    {
        Console.WriteLine($"RECOGNIZED: {e.Result.Text}");
        stopRecognition.TrySetResult(0);
    }
    else if (e.Result.Reason == ResultReason.TranslatedSpeech)
    {
        Console.WriteLine($"RECOGNIZED: {e.Result.Text}");
        foreach (var (language, translation) in e.Result.Translations)
        {
            Console.WriteLine($"TRANSLATED into '{language}': {translation}");
            _ = speechSynthesizer.SpeakTextAsync(translation);
        }
    }
};

// continuous
//await translationRecognizer.StartContinuousRecognitionAsync();
//do
//{
//    await Task.Delay(TimeSpan.FromMilliseconds(200));
//}
//while (true);

// await translationRecognizer.StopContinuousRecognitionAsync();

// once
await translationRecognizer.RecognizeOnceAsync();
Console.WriteLine("hit any key.");
Console.ReadKey();
