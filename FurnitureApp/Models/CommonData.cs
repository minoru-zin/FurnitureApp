using FurnitureApp.Repository.CostItemInfos;
using FurnitureApp.Repository.MaterialInfos;
using FurnitureApp.Repository.MaterialSizeInfos;
using FurnitureApp.Repository.Orders;
using FurnitureApp.Repository.PaintCostItemInfos;
using FurnitureApp.Repository.ProductCategoryInfos;
using FurnitureApp.Repository.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FurnitureApp.Models
{
    public class CommonData
    {
        private static CommonData commonData = new CommonData();
        public readonly string TempFileDirName = "TempFiles";
        public readonly string ExportOrderFileName = "Order.xml";
        public readonly string ExportMaterialInfoFileName = "MaterialInfos.xml";
        public readonly string ExportProductCategoryInfoFileName = "ProductCategoryInfos.xml";
        public readonly string ExportPaintCostItemInfoFileName = "PaintCostItemInfos.xml";
        

        #region メッセージダイアログ
        /// <summary>
        /// メッセージ表示はこれ経由
        /// </summary>
        public DialogService DialogService = new DialogService();
        #endregion

        #region リポジトリ
        public OrderRepository OrderRepository = new OrderRepository();
        public MaterialInfoRepository MaterialInfoRepository = new MaterialInfoRepository();
        public MaterialSizeInfoRepository MaterialSizeInfoRepository = new MaterialSizeInfoRepository();
        public ProductCategoryInfoRepository ProductCategoryInfoRepository = new ProductCategoryInfoRepository();
        public CostItemInfoRepository CostItemInfoRepository = new CostItemInfoRepository();
        public PaintCostItemInfoRepository PaintCostItemInfoRepository = new PaintCostItemInfoRepository();
        #endregion

        #region マスタ
        public List<MaterialInfo> MaterialInfos { get; } = new List<MaterialInfo>();
        public List<MaterialSizeInfo> MaterialSizeInfos { get; } = new List<MaterialSizeInfo>();
        public List<ProductCategoryInfo> ProductCategoryInfos { get; } = new List<ProductCategoryInfo>();
        public List<CostItemInfo> CostItemInfos { get; } = new List<CostItemInfo>();
        public List<PaintCostItemInfo> PaintCostItemInfos { get; } = new List<PaintCostItemInfo>();

        public Dictionary<int?, MaterialInfo> MaterialInfoDict { get; private set; }
        public Dictionary<int?, ProductCategoryInfo> ProductCategoryInfoDict { get; private set; }
        #endregion

        #region BoardType 板タイプ
        public List<DisplayInfo<BoardType>> BoardTypes { get; } = new List<DisplayInfo<BoardType>>
        {
            new DisplayInfo<BoardType>(BoardType.Tenita, "天板"),
            new DisplayInfo<BoardType>(BoardType.Tenshita, "天下"),
            new DisplayInfo<BoardType>(BoardType.Gawaita, "側板"),
            new DisplayInfo<BoardType>(BoardType.Shikiriita, "仕切板"),
            new DisplayInfo<BoardType>(BoardType.Tanaita, "棚板"),
            new DisplayInfo<BoardType>(BoardType.Tobira, "扉"),
            new DisplayInfo<BoardType>(BoardType.Seita, "背板"),
            new DisplayInfo<BoardType>(BoardType.Jiita, "地板"),
            new DisplayInfo<BoardType>(BoardType.DaiwaFront, "台輪正面"),
            new DisplayInfo<BoardType>(BoardType.DaiwaBack, "台輪背面"),
            new DisplayInfo<BoardType>(BoardType.DaiwaLeft, "台輪左"),
            new DisplayInfo<BoardType>(BoardType.DaiwaRight, "台輪右"),
        };
        #endregion

        #region CutType カットタイプ
        public List<DisplayInfo<CutType>> CutTypes { get; } = new List<DisplayInfo<CutType>>
        {
            new DisplayInfo<CutType>(CutType.Normal, "通常"),
            new DisplayInfo<CutType>(CutType.Lvl, "LVL(フラッシュ)")
        };
        #endregion

        #region 木口化粧箇所
        public List<DisplayInfo<KoguchiMakeupArea>> KoguchiKeshouAreas { get; } = new List<DisplayInfo<KoguchiMakeupArea>>
        {
            new DisplayInfo<KoguchiMakeupArea>(KoguchiMakeupArea.Nashi, "無し"),
            new DisplayInfo<KoguchiMakeupArea>(KoguchiMakeupArea.Front, "正面"),
            new DisplayInfo<KoguchiMakeupArea>(KoguchiMakeupArea.BothSide, "左右"),
            new DisplayInfo<KoguchiMakeupArea>(KoguchiMakeupArea.FrontAndOneSide, "正面,左or右"),
            new DisplayInfo<KoguchiMakeupArea>(KoguchiMakeupArea.FrontAndBothSide, "正面左右"),
            new DisplayInfo<KoguchiMakeupArea>(KoguchiMakeupArea.All, "全面"),
        };
        #endregion
        #region 塗装箇所
        public List<DisplayInfo<PaintArea>> PaintAreas { get; } = new List<DisplayInfo<PaintArea>>()
        {
            new DisplayInfo<PaintArea>(PaintArea.Nashi, "無し"),
            new DisplayInfo<PaintArea>(PaintArea.OneSide_KoguchiFront, "片面_木口(正面)"),
            new DisplayInfo<PaintArea>(PaintArea.OneSide_KoguchiBothSide, "片面_木口(左右)"),
            new DisplayInfo<PaintArea>(PaintArea.OneSide_KoguchiFrontAndOneSide, "片面_木口(正面_左or右)"),
            new DisplayInfo<PaintArea>(PaintArea.OneSide_KoguchiFrontAndBothSide, "片面_木口(正面_左右)"),
            new DisplayInfo<PaintArea>(PaintArea.OneSide_KoguchiAll, "片面_木口(全面)"),
            new DisplayInfo<PaintArea>(PaintArea.BothSide_KoguchiFront, "両面_木口(正面)"),
            new DisplayInfo<PaintArea>(PaintArea.BothSide_KoguchiBothSide, "両面_木口(左右)"),
            new DisplayInfo<PaintArea>(PaintArea.BothSide_KoguchiFrontAndOneSide, "両面_木口(正面_左or右)"),
            new DisplayInfo<PaintArea>(PaintArea.BothSide_KoguchiFrontAndBothSide, "両面_木口(正面_左右)"),
            new DisplayInfo<PaintArea>(PaintArea.BothSide_KoguchiAll, "両面_木口(全面)"),
            new DisplayInfo<PaintArea>(PaintArea.KoguchiFront, "木口(正面)"),
            new DisplayInfo<PaintArea>(PaintArea.KoguchiBothSide, "木口(左右)"),
            new DisplayInfo<PaintArea>(PaintArea.KoguchiFrontAndOneSide, "木口(正面_左or右)"),
            new DisplayInfo<PaintArea>(PaintArea.KoguchiFrontAndBothSide, "木口(正面_左右)"),
            new DisplayInfo<PaintArea>(PaintArea.KoguchiAll, "木口(全面)"),
        };
        #endregion

        #region 木目方向
        public List<DisplayInfo<MokumeDirectionType>> MokumeDirectionTypes { get; } = new List<DisplayInfo<MokumeDirectionType>>
        {
            new DisplayInfo<MokumeDirectionType>(MokumeDirectionType.Nashi, "無し"),
            new DisplayInfo<MokumeDirectionType>(MokumeDirectionType.Length, "↓"),
            new DisplayInfo<MokumeDirectionType>(MokumeDirectionType.Width, "→"),
        };
        #endregion
        private CommonData()
        {
            // DB作成
            new SqliteDbCreator().Create();

            this.RefreshMasters();


        }
        public static CommonData GetInstance()
        {
            return commonData;
        }

        public void RefreshMasters()
        {
            this.MaterialInfos.Clear();
            this.MaterialSizeInfos.Clear();
            this.ProductCategoryInfos.Clear();
            this.CostItemInfos.Clear();
            this.PaintCostItemInfos.Clear();


            this.MaterialInfos.AddRange(this.MaterialInfoRepository.SelectAll());
            this.MaterialSizeInfos.AddRange(this.MaterialSizeInfoRepository.SelectAll());
            this.ProductCategoryInfos.AddRange(this.ProductCategoryInfoRepository.SelectAll());
            this.CostItemInfos.AddRange(this.CostItemInfoRepository.SelectAll());
            this.PaintCostItemInfos.AddRange(this.PaintCostItemInfoRepository.SelectAll());


            this.MaterialInfoDict = this.MaterialInfos.ToDictionary(x => x.Code);
            this.ProductCategoryInfoDict = this.ProductCategoryInfos.ToDictionary(x => x.Code);
        }
    }
}
