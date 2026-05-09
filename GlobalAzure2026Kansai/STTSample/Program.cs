using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

Console.WriteLine("STT Sample");

using var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
var config = SpeechConfig.FromEndpoint(new Uri(Environment.GetEnvironmentVariable("AzureSpeechEndpoint") ?? ""), Environment.GetEnvironmentVariable("AzureSpeechApiKey"));
config.SpeechRecognitionLanguage = "ja-JP";

// use post refinement (2pass)
config.SetProperty(PropertyId.SpeechServiceResponse_PostProcessingOption, "PostRefinement");

using var recognizer = new SpeechRecognizer(config, audioConfig);
var stopRecognition = new TaskCompletionSource<int>(TaskCreationOptions.RunContinuationsAsynchronously);

recognizer.Recognizing += (s, e) =>
{
    Console.WriteLine($"{DateTimeOffset.Now:HH:mm.ss} RECOGNIZING: {e.Result.Text}");
};

recognizer.Recognized += (s, e) =>
{
    if (e.Result.Reason == ResultReason.RecognizedSpeech)
    {
        Console.WriteLine($"{DateTimeOffset.Now:HH:mm.ss} RECOGNIZED: {e.Result.Text}");
        stopRecognition.TrySetResult(0);
    }
};

// continuous
//await recognizer.StartContinuousRecognitionAsync();
//do
//{
//    await Task.Delay(TimeSpan.FromMilliseconds(200));
//}
//while (true);

//await recognizer.StopContinuousRecognitionAsync();

// once
await recognizer.RecognizeOnceAsync();
Console.WriteLine("hit any key.");
Console.ReadKey();
