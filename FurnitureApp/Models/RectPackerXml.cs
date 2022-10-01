using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace FurnitureApp.Models
{
    [XmlRoot("RectPacker")]
    public class RectPackerXml
    {
        [XmlAttribute]
        public string FileTypeVersion { get; set; } = "2.0";
        [XmlAttribute]
        public string ExeFileVersion { get; set; } = "10.80";
        public OptionXml Option { get; set; }
        [XmlArrayItem("Board")]
        /// <summary>
        /// 原材リスト
        /// </summary>
        public List<BoardXml> SourceBoardList { get; set; }
        [XmlArrayItem("Board")]
        public List<BoardXml> StockBoardList { get; set; }
        [XmlArrayItem("Board")]
        /// <summary>
        /// 部材リスト
        /// </summary>
        public List<BoardXml> PartsBoardList { get; set; }
        [XmlArrayItem("Board")]
        public List<BoardXml> SourceBoardData { get; set; }
    }

    public class OptionXml
    {
        [XmlAttribute]
        public string Material { get; set; }
        [XmlAttribute]
        public string Thickness { get; set; }
        [XmlAttribute]
        public string SameSizePartsMerge { get; set; } = "true";
        [XmlAttribute]
        public string TabColor { get; set; }
        [XmlAttribute]
        public string Problem { get; set; }
        [XmlAttribute]
        public string LengthFormat { get; set; }
        [XmlAttribute]
        public string Precision { get; set; }
        [XmlAttribute]
        public string Decimals { get; set; }
        [XmlAttribute]
        public string Rotate { get; set; }
        [XmlAttribute]
        public string SmallSourcePriorityPoint { get; set; }
        [XmlAttribute]
        public string SearchLevel { get; set; }
        [XmlAttribute]
        public string HighRatio { get; set; } = "1";
        [XmlAttribute]
        public string PartsColorListType { get; set; } = "1";
        [XmlAttribute]
        public string KerfSize { get; set; } = "3";
        [XmlAttribute]
        public string MaxCutLength { get; set; }
        [XmlAttribute]
        public string MinimumSearchTime { get; set; } = "2";
        [XmlAttribute]
        public string ConvergenceJudgmentTime { get; set; } = "30";
        [XmlAttribute]
        public string TopTrimSize { get; set; } = "0";
        [XmlAttribute]
        public string BottomTrimSize { get; set; } = "0";
        [XmlAttribute]
        public string LeftTrimSize { get; set; } = "0";
        [XmlAttribute]
        public string RightTrimSize { get; set; } = "0";
    }
    public class BoardXml
    {
        [XmlAttribute]
        public string Index { get; set; }
        [XmlAttribute]
        public string Comment { get; set; }
        [XmlAttribute]
        public string Width { get; set; }
        [XmlAttribute]
        public string Height { get; set; }
        [XmlAttribute]
        public string UsedNumber { get; set; }
        [XmlAttribute]
        public string Priority { get; set; }

        [XmlAttribute]
        public string Count { get; set; }
        [XmlAttribute]
        public string Cost { get; set; }
        [XmlAttribute]
        public string CanRotate { get; set; }
    }
}
