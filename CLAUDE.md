# unity1week-202603

unity1week ゲームジャム用プロジェクト（2026年3月）

## プロジェクト構成

```
Assets/
├── Animations/        # アニメーションクリップ・コントローラー
├── Audio/BGM/, SE/    # BGM・効果音素材
├── Editor/            # エディタ拡張スクリプト
├── Fonts/             # フォントファイル
├── Ignore/            # git管理外アセット（.gitignoreで除外）
├── Materials/         # マテリアル
├── Plugins/           # 外部プラグイン
├── Prefabs/Gameplay/, UI/
├── Scenes/Master/     # 本番シーン（TitleScene, GameScene）
├── Scenes/Dev/        # 開発・テスト用シーン
├── Scripts/Core/      # 基盤（GameManager, AudioManager, SceneLoader）
├── Scripts/Gameplay/  # ゲームロジック
├── Scripts/UI/        # UI制御
├── Scripts/Utils/     # ユーティリティ
├── Settings/          # URP設定
├── Sprites/Gameplay/, UI/
├── VFX/               # パーティクル・エフェクト
└── _Develop/          # 開発者個人作業フォルダ（Akutsu, Maruyama, Watanabe）
```

## 規約

- C#は namespace で分類（Core, Gameplay, UI, Utils）
- シングルトンは SingletonMonoBehaviour<T> を継承
- 本番シーンは Scenes/Master/、テスト用は Scenes/Dev/
- 大容量バイナリアセットは Assets/Ignore/ へ
- 個人の実験は _Develop/{名前}/ 配下で

## 技術スタック

- Unity (URP)
- Input System (Both モード)
- TextMeshPro
- 2D Sprite / Tilemap / Animation
