# unity1week-202603

Unity 1週間ゲームジャム用リポジトリ（2026年3月）

## はじめに

1. このリポジトリを `git clone` する
2. Unity Hub からプロジェクトを開く
3. 自分の担当フォルダで作業を始める

## 開発の流れ

### 作業の進め方
1. 作業前に **必ず `git pull` する**（他の人の変更を取り込む）
2. 自分の担当フォルダ内で作業する
3. 作業が終わったら `git add` → `git commit` → `git push`

### ブランチ
- **main ブランチのみ** で作業します（ブランチ切り替え不要）

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

- **プルしてからプッシュ！**（忘れるとコンフリクトします）
- **シーンファイルの同時編集は避ける**（壊れやすいので担当を分けましょう）
- 困ったらまず `git status` で状態を確認

## コードレビュー

PRを作ると [CodeRabbit](https://coderabbit.ai/) が自動でレビューしてくれます。