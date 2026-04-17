using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using System.Text;

Console.WriteLine("TTS Sample");

using var audioConfig = AudioConfig.FromDefaultSpeakerOutput();

var config = SpeechConfig.FromEndpoint(new Uri(Environment.GetEnvironmentVariable("AzureSpeechEndpoint") ?? ""), Environment.GetEnvironmentVariable("AzureSpeechApiKey"));
config.SpeechSynthesisLanguage = "ja-JP";
config.SpeechSynthesisVoiceName = "ja-jp-Nanami:DragonHDLatestNeural";

using var speechSynthesizer = new SpeechSynthesizer(config, audioConfig);

Console.OutputEncoding = Encoding.UTF8;
Console.Write("input text: ");
var text = Console.ReadLine();

speechSynthesizer.VisemeReceived += (s, e) =>
{
    Console.WriteLine($"offset: {e.AudioOffset / 10000} \t viseme: {e.VisemeId} \t\t {e.VisemeId switch
    {
        0 => "",
        1 => "ʌ",
        2 => "ɑ",
        3 => "ɔ",
        4 => "ɛ",
        5 => "ɝ",
        6 => "i",
        7 => "u",
        8 => "o",
        9 => "aʊ",
        10 => "ɔɪ",
        11 => "aɪ",
        12 => "h",
        13 => "ɹ",
        14 => "l",
        15 => "s",
        16 => "ʃ",
        17 => "ð",
        18 => "f",
        19 => "θ",
        20 => "k",
        21 => "p",
        _ => ""
    }}");
};

await speechSynthesizer.SpeakTextAsync(text);

Console.WriteLine("hit any key.");
Console.ReadKey();