using FurnitureApp.Repository.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FurnitureApp.Models
{
    public class BoardSizeCalculator
    {
        private CommonData cd = CommonData.GetInstance();

        private double bodyWidth;
        private double bodyDepth;
        private double bodyHeight;

        private double fillerL;
        private double fillerR;

        private double tobiraTenitaHikae;
        private double daiwaHeight;

        private double tobiraTenitaSukima;
        private double tobiraKanMokuji;
        private double tobiraGawaitaMokuji;
        private double shikiriitaGawaitaHikae;
        private double tanaitaGawaitaHikae;

        private int tenitaQuantity;
        private double tenitaThickness;
        private double tenitaWidth;
        private double tenitaLength;

        private int tenshitaQuantity;
        private double tenshitaThickness;
        private double tenshitaWidth;
        private double tenshitaLength;

        private int gawaitaQuantity;
        private double gawaitaThickness;
        private double gawaitaWidth;
        private double gawaitaLength;

        private int shikiriitaQuantity;
        private double shikiriitaThickness;
        private double shikiriitaWidth;
        private double shikiriitaLength;

        private int tanaitaQuantity;
        private double tanaitaThickness;
        private double tanaitaWidth;
        private double tanaitaLength;

        private int tobiraQuantity;
        private double tobiraThickness;
        private double tobiraWidth;
        private double tobiraLength;

        private int seitaQuantity;
        private double seitaThickness;
        private double seitaWidth;
        private double seitaLength;

        private int jiitaQuantity;
        private double jiitaThickness;
        private double jiitaWidth;
        private double jiitaLength;

        private int daiwaFrontQuantity;
        private double daiwaFrontThickness;
        private double daiwaFrontWidth;
        private double daiwaFrontLength;

        private int daiwaBackQuantity;
        private double daiwaBackThickness;
        private double daiwaBackWidth;
        private double daiwaBackLength;

        private int daiwaLeftQuantity;
        private double daiwaLeftThickness;
        private double daiwaLeftWidth;
        private double daiwaLeftLength;

        private int daiwaRightQuantity;
        private double daiwaRightThickness;
        private double daiwaRightWidth;
        private double daiwaRightLength;


        public BoardSizeCalculator(Product product)
        {
            this.bodyWidth = this.GetDouble(product.BodyWidth);
            this.bodyDepth = this.GetDouble(product.BodyDepth);
            this.bodyHeight = this.GetDouble(product.BodyHeight);

            this.fillerL = this.GetDouble(product.FillerL);
            this.fillerR = this.GetDouble(product.FillerR);

            this.tobiraTenitaHikae = this.GetDouble(product.TobiraTenitaHikae);
            this.tobiraTenitaSukima = this.GetDouble(product.TobiraTenitaSukima);
            this.tobiraKanMokuji = this.GetDouble(product.TobiraKanMokuji);
            this.tobiraGawaitaMokuji = this.GetDouble(product.TobiraGawaitaMokuji);
            this.daiwaHeight = this.GetDouble(product.DaiwaHeight);

            this.shikiriitaGawaitaHikae = this.GetDouble(product.ShikiriitaGawaitaHikae);
            this.tanaitaGawaitaHikae = this.GetDouble(product.TanaitaGawaitaHikae);

            (this.tenitaThickness, this.tenitaQuantity) = this.GetBoardInfo(BoardType.Tenita, product);
            (this.tenshitaThickness, this.tenshitaQuantity) = this.GetBoardInfo(BoardType.Tenshita, product);
            (this.gawaitaThickness, this.gawaitaQuantity) = this.GetBoardInfo(BoardType.Gawaita, product);
            (this.shikiriitaThickness, this.shikiriitaQuantity) = this.GetBoardInfo(BoardType.Shikiriita, product);
            (this.tanaitaThickness, this.tanaitaQuantity) = this.GetBoardInfo(BoardType.Tanaita, product);
            (this.tobiraThickness, this.tobiraQuantity) = this.GetBoardInfo(BoardType.Tobira, product);
            (this.seitaThickness, this.seitaQuantity) = this.GetBoardInfo(BoardType.Seita, product);
            (this.jiitaThickness, this.jiitaQuantity) = this.GetBoardInfo(BoardType.Jiita, product);
            (this.daiwaFrontThickness, this.daiwaFrontQuantity) = this.GetBoardInfo(BoardType.DaiwaFront, product);
            (this.daiwaBackThickness, this.daiwaBackQuantity) = this.GetBoardInfo(BoardType.DaiwaBack, product);
            (this.daiwaLeftThickness, this.daiwaLeftQuantity) = this.GetBoardInfo(BoardType.DaiwaLeft, product);
            (this.daiwaRightThickness, this.daiwaRightQuantity) = this.GetBoardInfo(BoardType.DaiwaRight, product);

            // 棚板枚数専用処理 入力値は棚板の段数として、仕切り板で実際の棚板枚数を算出する
            this.tanaitaQuantity = (this.shikiriitaQuantity + 1) * this.tanaitaQuantity;

            
            #region 天板
            this.tenitaWidth = this.bodyWidth;
            this.tenitaLength = this.bodyDepth;
            #endregion

            #region 天下
            this.tenshitaWidth = this.bodyWidth - (this.fillerL + this.fillerR + this.gawaitaThickness * this.gawaitaQuantity);
            this.tenshitaLength = this.bodyDepth - (this.tobiraTenitaHikae + this.tobiraThickness);
            #endregion

            #region 側板
            this.gawaitaWidth = this.bodyDepth - (this.tobiraTenitaHikae + this.tobiraThickness);
            this.gawaitaLength = this.bodyHeight - (this.tenitaThickness + this.daiwaHeight);
            #endregion

            #region 仕切板
            this.shikiriitaWidth = this.gawaitaWidth - (this.shikiriitaGawaitaHikae + this.seitaThickness);
            this.shikiriitaLength = this.bodyHeight - (this.tenitaThickness + this.daiwaHeight + this.tenshitaThickness + this.jiitaThickness);
            #endregion

            #region 棚板
            this.tanaitaWidth = (this.bodyWidth - (this.fillerL + this.fillerR + this.gawaitaThickness * this.gawaitaQuantity + this.shikiriitaThickness * this.shikiriitaQuantity)) / (this.shikiriitaQuantity + 1);
            this.tanaitaLength = this.bodyDepth - (this.tobiraTenitaHikae + this.tobiraThickness + this.tanaitaGawaitaHikae);
            #endregion

            #region 扉
            if(this.tobiraQuantity > 0)
            {
                this.tobiraWidth = (this.bodyWidth - (this.fillerL + this.fillerR + this.tobiraKanMokuji * this.tobiraQuantity)) / this.tobiraQuantity;
                this.tobiraLength = this.bodyHeight - (this.tenitaThickness + this.tobiraTenitaSukima + this.daiwaHeight);
            }
            #endregion

            #region 背板
            this.seitaWidth = this.bodyWidth - (this.fillerL + this.fillerR + this.gawaitaThickness * this.gawaitaQuantity);
            this.seitaLength = this.bodyHeight - (this.tenitaThickness + this.tenshitaThickness + this.jiitaThickness + this.daiwaHeight);
            #endregion

            #region 地板
            this.jiitaWidth = this.bodyWidth - (this.fillerL + this.fillerR + this.gawaitaThickness * this.gawaitaQuantity);
            this.jiitaLength = this.bodyDepth - (this.tobiraTenitaHikae + this.tobiraThickness);
            #endregion

            #region 台輪正面
            this.daiwaFrontWidth = this.bodyWidth - (this.fillerL + this.fillerR);
            this.daiwaFrontLength = this.daiwaHeight;
            #endregion

            #region 台輪背面
            this.daiwaBackWidth = this.bodyWidth - (this.fillerL + this.fillerR);
            this.daiwaBackLength = this.daiwaHeight;
            #endregion

            #region 台輪左
            this.daiwaLeftWidth = this.bodyDepth - (this.daiwaFrontThickness + this.daiwaBackThickness + this.tobiraTenitaHikae + this.tobiraThickness);
            this.daiwaLeftLength = this.daiwaHeight;
            #endregion

            #region 台輪右
            this.daiwaRightWidth = this.bodyDepth - (this.daiwaFrontThickness + this.daiwaBackThickness + this.tobiraTenitaHikae + this.tobiraThickness);
            this.daiwaRightLength = this.daiwaHeight;
            #endregion
        }

        private (double thickness, int quantity) GetBoardInfo(BoardType boardType, Product product)
        {
            var board = product.Boards.FirstOrDefault(x => x.BoardCode == boardType);

            if (board == null) { return (0, 0); }

            return (this.GetDouble(board.GetThickness(this.cd.MaterialInfoDict)), (int)board.Quantity);
        }
        public List<BoardSize> GetBoardSizes()
        {
            var boardSizes = new List<BoardSize>();

            boardSizes.Add(this.GetTenita());
            boardSizes.Add(this.GetTenshita());
            boardSizes.Add(this.GetGawaita());
            boardSizes.Add(this.GetShikiriita());
            boardSizes.Add(this.GetTanaita());
            boardSizes.Add(this.GetTobira());
            boardSizes.Add(this.GetSeita());
            boardSizes.Add(this.GetJiita());
            boardSizes.Add(this.GetDaiwaFront());
            boardSizes.Add(this.GetDaiwaBack());
            boardSizes.Add(this.GetDaiwaLeft());
            boardSizes.Add(this.GetDaiwaRight());

            boardSizes = boardSizes.Where(x => x.Quantity > 0).ToList();

            return boardSizes;
        }

        private BoardSize GetTenita()
        {
            return new BoardSize
            {
                BoardType = BoardType.Tenita,
                Width = this.tenitaWidth,
                Length = this.tenitaLength,
                Thickness = this.tenitaThickness,
                Quantity = this.tenitaQuantity
            };
        }
        private BoardSize GetTenshita()
        {
            return new BoardSize
            {
                BoardType = BoardType.Tenshita,
                Width = this.tenshitaWidth,
                Length = this.tenshitaLength,
                Thickness = this.tenshitaThickness,
                Quantity = this.tenshitaQuantity,
            };
        }
        private BoardSize GetGawaita()
        {
            return new BoardSize
            {
                BoardType = BoardType.Gawaita,
                Width = this.gawaitaWidth,
                Length = this.gawaitaLength,
                Thickness = this.gawaitaThickness,
                Quantity = this.gawaitaQuantity,
            };
        }
        private BoardSize GetShikiriita()
        {
            return new BoardSize
            {
                BoardType = BoardType.Shikiriita,
                Width = this.shikiriitaWidth,
                Length = this.shikiriitaLength,
                Thickness = this.shikiriitaThickness,
                Quantity = this.shikiriitaQuantity,
            };
        }
        private BoardSize GetTanaita()
        {
            return new BoardSize
            {
                BoardType = BoardType.Tanaita,
                Width = this.tanaitaWidth,
                Length = this.tanaitaLength,
                Thickness = this.tanaitaThickness,
                Quantity = this.tanaitaQuantity,
            };
        }
        private BoardSize GetTobira()
        {
            return new BoardSize
            {
                BoardType = BoardType.Tobira,
                Width = this.tobiraWidth,
                Length = this.tobiraLength,
                Thickness = this.tobiraThickness,
                Quantity = this.tobiraQuantity,
            };
        }
        private BoardSize GetSeita()
        {
            return new BoardSize
            {
                BoardType = BoardType.Seita,
                Width = this.seitaWidth,
                Length = this.seitaLength,
                Thickness = this.seitaThickness,
                Quantity = this.seitaQuantity,
            };
        }
        private BoardSize GetJiita()
        {
            return new BoardSize
            {
                BoardType = BoardType.Jiita,
                Width = this.jiitaWidth,
                Length = this.jiitaLength,
                Thickness = this.jiitaThickness,
                Quantity = this.jiitaQuantity,
            };
        }
        private BoardSize GetDaiwaFront()
        {
            return new BoardSize
            {
                BoardType = BoardType.DaiwaFront,
                Width = this.daiwaFrontWidth,
                Length = this.daiwaFrontLength,
                Thickness = this.daiwaFrontThickness,
                Quantity = this.daiwaFrontQuantity,
            };
        }
        private BoardSize GetDaiwaBack()
        {
            return new BoardSize
            {
                BoardType = BoardType.DaiwaBack,
                Width = this.daiwaBackWidth,
                Length = this.daiwaBackLength,
                Thickness = this.daiwaBackThickness,
                Quantity = this.daiwaBackQuantity,
            };
        }
        private BoardSize GetDaiwaLeft()
        {
            return new BoardSize
            {
                BoardType = BoardType.DaiwaLeft,
                Width = this.daiwaLeftWidth,
                Length = this.daiwaLeftLength,
                Thickness = this.daiwaLeftThickness,
                Quantity = this.daiwaLeftQuantity,
            };
        }
        private BoardSize GetDaiwaRight()
        {
            return new BoardSize
            {
                BoardType = BoardType.DaiwaRight,
                Width = this.daiwaRightWidth,
                Length = this.daiwaRightLength,
                Thickness = this.daiwaRightThickness,
                Quantity = this.daiwaRightQuantity,
            };
        }

        private double GetDouble(double? number)
        {
            return number ?? 0;
        }
    }

    public class BoardSize
    {
        public BoardType BoardType { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Thickness { get; set; }
        public int Quantity { get; set; }
        public BoardSize Clone()
        {
            return (BoardSize)MemberwiseClone();
        }
    }
}
