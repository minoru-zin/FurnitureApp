using FurnitureApp.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace FurnitureApp.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            ProcessStartInfo pInfo = new ProcessStartInfo();
            pInfo.FileName = @"C:\Program Files (x86)\RectPacker\RectPacker.exe";
            //pInfo.Arguments = @"/Problem=2D /PartsPanel=""C:\Users\dypsea\Desktop\FurnitureApp\部材リスト.csv"" /StocksPanel=""C:\Users\dypsea\Desktop\FurnitureApp\原材リスト.csv"" /R=2 /SearchLevel=4 /Run /SaveStocksData=""C:\Users\dypsea\Desktop\xxxxxx.csv""";
            pInfo.Arguments = @"""C:\Users\dypsea\Desktop\test.recx"" /Run /Save=""C:\Users\dypsea\Desktop\result.recx""";
            //pInfo.Arguments = @" / PartsPanel=""部材リスト.csv""";

            Process p = Process.Start(pInfo);

            p.WaitForExit();
            Assert.Pass();
        }
        [Test]
        public void 板取XMLテスト()
        {
            var xml = new RectPackerXml();
            xml.Option = new OptionXml
            {
                Material = "Sheet1",
                SameSizePartsMerge = "true",
                Problem = "2D",
                LengthFormat = "ftDecimal",
                Precision = "1",
                Decimals = "0",
                Rotate = "2",
                SmallSourcePriorityPoint = "5",
                SearchLevel = "4",
                HighRatio = "1",
                PartsColorListType = "1",
                KerfSize = "3",
                MinimumSearchTime = "2",
                ConvergenceJudgmentTime = "30",
                TopTrimSize = "0",
                BottomTrimSize = "0",
                LeftTrimSize = "0",
                RightTrimSize = "0"
            };
            xml.SourceBoardList = new List<BoardXml>
            {
                new BoardXml {Comment = "ベニヤ1", Width = "950", Height = "900", Count = "999", Cost = "1000" }
            };
            xml.PartsBoardList = new List<BoardXml> {
                new BoardXml{ Comment="ベニヤ", Width="120", Height="90", Count="12", CanRotate="1"},
                new BoardXml{ Comment="ベニヤ", Width="500", Height="250", Count="6", CanRotate="1"},
            };

            Utility.XmlWriter.WriteXml(xml, Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "test.xml"), "UTF-8");

        }
        [Test]
        public void 画像を開く()
        {
            var app = new ProcessStartInfo();
            app.FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "est.csv");
            app.UseShellExecute = true;

            Process.Start(app);
        }
    }
}