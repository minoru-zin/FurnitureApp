using FurnitureApp.Repository.Orders;
using FurnitureApp.Repository.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FurnitureApp.Repository.PaintCostItemInfos
{
    public class PaintCostItemInfoRepository
    {
        public List<PaintCostItemInfo> SelectAll()
        {
            List<PaintCostItemInfo> ms = null;

            RepositoryAction.Query(c =>
            {
                ms = new PaintCostItemInfoDao(c, null).SelectAll().OrderBy(x => x.Sequence).ToList();
            });

            return ms;
        }

        public void Insert(PaintCostItemInfo m)
        {
            this.Insert(new List<PaintCostItemInfo> { m });
        }
        public void Insert(IEnumerable<PaintCostItemInfo> ms)
        {
            RepositoryAction.Transaction((c, t) =>
            {
                var dao = new PaintCostItemInfoDao(c, t);

                foreach (var m in ms)
                {
                    dao.Insert(m);
                }
            });
        }
        public void Update(PaintCostItemInfo m)
        {
            this.Update(new List<PaintCostItemInfo> { m });
        }
        public void Update(IEnumerable<PaintCostItemInfo> ms)
        {
            RepositoryAction.Transaction((c, t) =>
            {
                var dao = new PaintCostItemInfoDao(c, t);

                foreach (var m in ms)
                {
                    dao.Update(m);
                }
            });
        }
        public void Delete(PaintCostItemInfo m)
        {
            this.Delete(new List<PaintCostItemInfo> { m });
        }
        public void Delete(IEnumerable<PaintCostItemInfo> ms)
        {
            RepositoryAction.Transaction((c, t) =>
            {
                var boardDao = new BoardDao(c, t);

                foreach (var m in ms)
                {
                    if (boardDao.ExistPaintCostItemInfoCode(m.Code))
                    {
                        throw new Exception($"板情報で使用されています : {m.Name}");
                    }
                }

                var PaintCostItemInfoDao = new PaintCostItemInfoDao(c, t);

                foreach (var m in ms)
                {
                    PaintCostItemInfoDao.DeleteById((int)m.Id);
                }
            });
        }
    }
}
