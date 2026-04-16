# Global Azure 2026 Kansai Demo

サンプルは基本的には .NET 10 を使用。

- CustomKeywordSample
  - カスタムキーワード機能を使ったWake wordのサンプル。 `Hey Kosmos` を認識する。
- STTSample
  - 音声 → テキスト変換のサンプル。環境変数に Azure Speech のAPIエンドポイントとAPIキーを設定する。
    - `AzureSpeechEndpoint` … APIエンドポイント
    - `AzureSpeechApiKey` … APIキー

[公式サンプル](https://github.com/Azure-Samples/cognitive-services-speech-sdk/tree/master/samples/)も参考に。
