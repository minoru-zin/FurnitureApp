using FurnitureApp.Repository.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FurnitureApp.Repository.CostItemInfos
{
    public class CostItemInfoRepository
    {
        public List<CostItemInfo> SelectAll()
        {
            List<CostItemInfo> ms = null;

            RepositoryAction.Query(c =>
            {
                ms = new CostItemInfoDao(c, null).SelectAll().OrderBy(x => x.Sequence).ToList();
            });

            return ms;
        }

        public void Insert(CostItemInfo m)
        {
            this.Insert(new List<CostItemInfo> { m });
        }
        public void Insert(IEnumerable<CostItemInfo> ms)
        {
            RepositoryAction.Transaction((c, t) =>
            {
                var dao = new CostItemInfoDao(c, t);

                foreach (var m in ms)
                {
                    dao.Insert(m);
                }
            });
        }
        public void Update(CostItemInfo m)
        {
            this.Update(new List<CostItemInfo> { m });
        }
        public void Update(IEnumerable<CostItemInfo> ms)
        {
            RepositoryAction.Transaction((c, t) =>
            {
                var dao = new CostItemInfoDao(c, t);

                foreach (var m in ms)
                {
                    dao.Update(m);
                }
            });
        }
        public void Delete(CostItemInfo m)
        {
            this.Delete(new List<CostItemInfo> { m });
        }
        public void Delete(IEnumerable<CostItemInfo> ms)
        {
            RepositoryAction.Transaction((c, t) =>
            {
                var CostItemInfoDao = new CostItemInfoDao(c, t);

                foreach (var m in ms)
                {
                    CostItemInfoDao.DeleteById((int)m.Id);
                }
            });
        }
    }
}
