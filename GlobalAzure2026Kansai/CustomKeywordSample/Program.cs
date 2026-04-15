using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

var keyword = "hey kosmos";

using var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
using var keywordRecognizer = new KeywordRecognizer(audioConfig);
var keywordModel = KeywordRecognitionModel.FromFile("hey_kosmos.table");

Console.WriteLine($"\"{keyword}\" が検出されるまで待ちます..");

await keywordRecognizer.RecognizeOnceAsync(keywordModel);

Console.WriteLine($"\"{keyword}\" を検出しました (enterで終了)");
Console.ReadKey();

await keywordRecognizer.StopRecognitionAsync();
