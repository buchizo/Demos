# Global Azure 2024 in Fukuoka Demo

[Globa Azure 2024 in Fukuoka](https://jazug.connpass.com/event/313994/)の「使ってみよう Azure Document Intelligence」のデモです。

- [デモアプリ](./Demo/) … デモ用Visual Studio 2022のソリューション
  - 実行時に使用するDocument IntelligenceのエンドポイントとAPIキーをユーザーシークレットに追記する必要があります。
  - 以下の内容でユーザーシークレットを追記・値を修正してください。
  ```
    {
        "DocumentIntelligence": {
            "Endpoint": "https://_____.cognitiveservices.azure.com/",
            "ApiKey": "________"
        }
    }
  ```
  - デモではAzure AI Document Intelligenceの `2024-02-29-preview` APIを使用しているのでサポートしているリージョンでAzure AI Document Intelligenceを作成・使用してください。

## その他関連情報

- [発表スライド](https://speakerdeck.com/kosmosebi/shi-tutemiyou-azure-ai-document-intelligence)
- [Unlocking Advanced Document Insights with Azure AI Document Intelligence](https://techcommunity.microsoft.com/t5/ai-azure-ai-services-blog/unlocking-advanced-document-insights-with-azure-ai-document/ba-p/4109675)
- [Figure understanding & hierarchical document structure analysis](https://github.com/microsoft/Form-Recognizer-Toolkit/blob/main/SampleCode/Python/sample_figure_understanding.ipynb) … PDFから画像を抜き出してGPT-4Vで説明テキストを生成するデモ用
