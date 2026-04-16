using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

Console.WriteLine("STT Sample");

using var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
var config = SpeechConfig.FromEndpoint(new Uri(Environment.GetEnvironmentVariable("AzureSpeechEndpoint") ?? ""), Environment.GetEnvironmentVariable("AzureSpeechApiKey"));
config.SpeechRecognitionLanguage = "ja-JP";

using var recognizer = new SpeechRecognizer(config, audioConfig);
var stopRecognition = new TaskCompletionSource<int>(TaskCreationOptions.RunContinuationsAsynchronously);

recognizer.Recognizing += (s, e) =>
{
    Console.WriteLine($"RECOGNIZING: {e.Result.Text}");
};

recognizer.Recognized += (s, e) =>
{
    if (e.Result.Reason == ResultReason.RecognizedSpeech)
    {
        Console.WriteLine($"RECOGNIZED: {e.Result.Text}");
        stopRecognition.TrySetResult(0);
    }
    else if (e.Result.Reason == ResultReason.NoMatch)
    {
        Console.WriteLine($"NOMATCH: Speech could not be recognized.");
    }
};

await recognizer.StartContinuousRecognitionAsync();

do
{
    await Task.Delay(TimeSpan.FromMilliseconds(200));
}
while (true);

//await recognizer.StopContinuousRecognitionAsync();
