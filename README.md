# unity1week-202603

Unity 1週間ゲームジャム用リポジトリ（2026年3月）

- **Unity バージョン: 6000.3.8f1**（必ずこのバージョンで開いてください）

## はじめに

1. Unity Hub で **6000.3.8f1** をインストール
2. このリポジトリを `git clone` する
3. Unity Hub からプロジェクトを開く
3. 自分の担当フォルダで作業を始める

## 開発の流れ

### 作業の進め方
1. `git pull origin main` で最新を取得
2. ブランチを作る: `git checkout -b feature/名前/内容`
   - 例: `git checkout -b feature/maruyama/player-movement`
3. 作業して `git add` → `git commit` → `git push`
4. GitHubでPRを作る → CodeRabbitが自動レビュー
5. 問題なければmainにマージ

### ブランチ名のルール
- 機能追加: `feature/名前/内容`（例: `feature/akutsu/enemy-ai`）
- バグ修正: `fix/名前/内容`（例: `fix/watanabe/jump-bug`）
- **mainには直接pushしない**

## C# 命名規則

| 対象 | ルール | 例 |
|------|--------|-----|
| クラス・メソッド | PascalCase | `PlayerController`, `MovePlayer()` |
| ローカル変数 | camelCase | `moveSpeed`, `isJumping` |
| privateフィールド | _camelCase | `_rigidbody`, `_currentHp` |

## フォルダ構成

```
Assets/
├── Scripts/           # スクリプト（Core/ Gameplay/ UI/ Utils/）
├── Prefabs/           # プレハブ（Gameplay/ UI/）
├── Scenes/
│   ├── Master/        # 本番用シーン（TitleScene, GameScene）
│   └── Dev/           # テスト・実験用シーン
├── Audio/             # 音素材（BGM/ SE/）
├── Sprites/           # 画像素材（Gameplay/ UI/）
├── Animations/        # アニメーション
├── Materials/         # マテリアル
├── Fonts/             # フォント
├── VFX/               # エフェクト
├── Plugins/           # 外部プラグイン
└── Ignore/            # gitに上げたくないファイル置き場（後述）
```

## Assets/Ignore/ の使い方

`Assets/Ignore/` フォルダは **gitに上がらない** ように設定されています。

こんなファイルをここに入れてください：
- 有料アセットストアの素材（ライセンス的にgitに上げられない）
- 仮で使っている大きい画像や音声ファイル
- 自分のPCだけで使うテスト用素材

Unity上では普通に使えるけど、git push しても他の人には共有されません。

## 注意事項

- **テキスト表示は TextMeshPro（TMP）を使う**（古いUI Textは使わない）
- **プルしてからプッシュ！**（忘れるとコンフリクトします）
- **シーンファイルの同時編集は避ける**（壊れやすいので担当を分けましょう）
- 困ったらまず `git status` で状態を確認
- 原則「**何をしているか（What）**はコメント不要、**なぜそうしているか（Why）**はコメントする」

## コードレビュー

PRを作ると [CodeRabbit](https://github.com/apps/coderabbitai) が自動でレビューしてくれます。
