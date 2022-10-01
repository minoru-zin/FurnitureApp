﻿using FurnitureApp.Repository.MaterialSizeInfos;
using FurnitureApp.Repository.Orders;
using FurnitureApp.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureApp.Models
{
    public class RectPacker
    {
        private readonly string coreDirectoryPath = "CuttingPlanner";
        public List<BoardCost> GetBoardCosts(string name, List<CutSize> cutSizes, List<MaterialSizeInfo> materialSizeInfos)
        {
            cutSizes = this.GetResizedCutSizes(cutSizes.Where(x => x.Length > 0 && x.Width > 0).ToList(), materialSizeInfos);

            var xmls = this.GetInputXmls(cutSizes, materialSizeInfos);

            xmls = this.GetResultXmls(name, xmls);

            return this.Convert(xmls);
        }

        #region 入力

        private List<CutSize> GetResizedCutSizes(List<CutSize> oldCutSizes, List<MaterialSizeInfo> materialSizeInfos)
        {
            var cutSizes = new List<CutSize>();

            foreach (var g in oldCutSizes.GroupBy(x => new { x.MaterialInfoId, x.MaterialName }))
            {
                var ms = materialSizeInfos.Where(x => x.MaterialInfoId == g.Key.MaterialInfoId).ToList();

                if (ms.Count == 0) { throw new Exception($"素材マスタ Id : {g.Key.MaterialInfoId} Name : {g.Key.MaterialName} に紐づく素材規格マスタが存在しません"); }

                foreach (var c in g)
                {
                    this.Resize(c, ms);
                }
            }

            foreach (var g in oldCutSizes.GroupBy(x => new { x.MaterialInfoId, x.MaterialName, x.Width, x.Length }))
            {
                cutSizes.Add(new CutSize
                {
                    MaterialInfoId = g.Key.MaterialInfoId,
                    MaterialName = g.Key.MaterialName,
                    Length = g.Key.Length,
                    Width = g.Key.Width,
                    Quantity = g.Sum(x => x.Quantity),
                });
            }

            return cutSizes;
        }
        private void Resize(CutSize cutSize, List<MaterialSizeInfo> materialSizeInfos)
        {
            foreach (var m in materialSizeInfos)
            {
                if (this.IsFit(cutSize, m)) { return; }
            }

            this.SplitSize(cutSize);

            this.Resize(cutSize, materialSizeInfos);
        }
        private bool IsFit(CutSize cutSize, MaterialSizeInfo materialSizeInfo)
        {
            if (cutSize.Width <= materialSizeInfo.Width && cutSize.Length <= materialSizeInfo.Length) { return true; }
            if (cutSize.Width <= materialSizeInfo.Length && cutSize.Length <= materialSizeInfo.Width) { return true; }
            return false;
        }
        private void SplitSize(CutSize cutSize)
        {
            if (cutSize.Length > cutSize.Width)
            {
                cutSize.Length = cutSize.Length / 2;
            }
            else
            {
                cutSize.Width = cutSize.Width / 2;
            }

            cutSize.Quantity = cutSize.Quantity * 2;
        }
        private List<RectPackerXml> GetInputXmls(List<CutSize> cutSizes, List<MaterialSizeInfo> materialSizeInfos)
        {
            var xmls = new List<RectPackerXml>();

            foreach (var (g, i) in cutSizes.GroupBy(x => new { x.MaterialInfoId, x.MaterialName }).WithIndex())
            {
                var xml = this.GetDefautInstance($"Sheet{i + 1}");

                // 原材リスト
                foreach (var m in materialSizeInfos.Where(x => x.MaterialInfoId == g.Key.MaterialInfoId))
                {
                    xml.SourceBoardList.Add(new BoardXml
                    {
                        Comment = m.Name,
                        Height = $"{m.Length}",
                        Width = $"{m.Width}",
                        Cost = $"{m.UnitPrice}",
                        Count = $"999",
                    });
                }

                // 部材リスト
                foreach (var c in g)
                {
                    xml.PartsBoardList.Add(new BoardXml
                    {
                        Comment = g.Key.MaterialName,
                        Height = $"{c.Length}",
                        Width = $"{c.Width}",
                        Count = $"{c.Quantity}",
                        CanRotate = "true", // TODO 条件追加
                    });
                }

                xmls.Add(xml);
            }

            return xmls;
        }
        private RectPackerXml GetDefautInstance(string materialName)
        {
            var xml = new RectPackerXml();
            xml.Option = new OptionXml
            {
                Material = materialName,
                SameSizePartsMerge = "true",
                Problem = "2D",
                LengthFormat = "ftDecimal",
                Precision = "0.1",
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
            xml.SourceBoardList = new List<BoardXml>();
            xml.PartsBoardList = new List<BoardXml>();
            return xml;
        }

        #endregion

        private List<RectPackerXml> GetResultXmls(string name, List<RectPackerXml> inputXmls)
        {
            var recxDirName = $"{DateTime.Now:yyyyMMddHHmmss}_{name}";
            var inputDirPath = Path.Combine(this.coreDirectoryPath, "input");

            Utility.DirectoryCreator.CreateSafely(inputDirPath);

            foreach (var inputXml in inputXmls)
            {
                var inputFilePath = Path.Combine(inputDirPath, $"{inputXml.Option.Material}.xml");
                Utility.XmlWriter.WriteXml(inputXml, inputFilePath, "UTF-8");
            }

            var inputRecxFilePath = Path.Combine(this.coreDirectoryPath, "input.recx");
            ZipFile.CreateFromDirectory(inputDirPath, inputRecxFilePath);

            var resultRecxFilePath = Path.Combine(this.coreDirectoryPath, $"{recxDirName}.recx");

            // 起動
            var pInfo = new ProcessStartInfo();
            pInfo.FileName = @"C:\Program Files (x86)\RectPacker\RectPacker.exe";
            pInfo.Arguments = $@"""{inputRecxFilePath}"" /Run /Save=""{resultRecxFilePath}""";

            Process p = Process.Start(pInfo);

            p.WaitForExit();

            // 結果xml取得
            var resultDirPath = Path.Combine(this.coreDirectoryPath, recxDirName);

            ZipFile.ExtractToDirectory(resultRecxFilePath, resultDirPath);

            var resultXmls = new List<RectPackerXml>();

            foreach (var filePath in Directory.GetFiles(resultDirPath))
            {
                resultXmls.Add(Utility.XmlReader.ReadXml<RectPackerXml>(filePath, "UTF-8"));
            }

            Directory.Delete(inputDirPath, true);
            File.Delete(inputRecxFilePath);
            Directory.Delete(resultDirPath, true);

            return resultXmls;
        }

        private List<BoardCost> Convert(List<RectPackerXml> xmls)
        {
            var ss = new List<BoardCost>();

            foreach (var xml in xmls)
            {
                foreach (var sb in xml.SourceBoardData)
                {
                    if (sb.UsedNumber == "0") { continue; }

                    var unitPrice = Utility.NumberFormatter.GetNullInt(sb.Cost);
                    var quantity = Utility.NumberFormatter.GetNullInt(sb.UsedNumber);

                    var s = new BoardCost
                    {
                        Name = sb.Comment,
                        Length = Utility.NumberFormatter.GetNullDouble(sb.Height) / 10,
                        Width = Utility.NumberFormatter.GetNullDouble(sb.Width) / 10,
                        UnitPrice = unitPrice,
                        Quantity = quantity,
                        TotalAmount = unitPrice * quantity
                    };
                    ss.Add(s);
                }
            }

            return ss;
        }

    }
}
