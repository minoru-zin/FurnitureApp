using FurnitureApp.Repository.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FurnitureApp.Repository.MaterialSizeInfos
{
    public class MaterialSizeInfoRepository
    {
        public List<MaterialSizeInfo> SelectAll()
        {
            List<MaterialSizeInfo> ms = null;

            RepositoryAction.Query(c =>
            {
                ms = new MaterialSizeInfoDao(c, null).SelectAll().ToList();
            });

            return ms;
        }

        public void Insert(MaterialSizeInfo m)
        {
            this.Insert(new List<MaterialSizeInfo> { m });
        }
        public void Insert(IEnumerable<MaterialSizeInfo> ms)
        {
            RepositoryAction.Transaction((c, t) =>
            {
                var dao = new MaterialSizeInfoDao(c, t);

                foreach (var m in ms)
                {
                    dao.Insert(m);
                }
            });
        }
        public void Update(MaterialSizeInfo m)
        {
            this.Update(new List<MaterialSizeInfo> { m });
        }
        public void Update(IEnumerable<MaterialSizeInfo> ms)
        {
            RepositoryAction.Transaction((c, t) =>
            {
                var dao = new MaterialSizeInfoDao(c, t);

                foreach (var m in ms)
                {
                    dao.Update(m);
                }
            });
        }
        public void Delete(MaterialSizeInfo m)
        {
            this.Delete(new List<MaterialSizeInfo> { m });
        }
        public void Delete(IEnumerable<MaterialSizeInfo> ms)
        {
            RepositoryAction.Transaction((c, t) =>
            {
                var dao = new MaterialSizeInfoDao(c, t);

                foreach (var m in ms)
                {
                    dao.DeleteById((int)m.Id);
                }
            });
        }
    }
}
