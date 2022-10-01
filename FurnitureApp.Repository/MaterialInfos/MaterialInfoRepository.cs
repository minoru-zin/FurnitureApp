using FurnitureApp.Repository.MaterialSizeInfos;
using FurnitureApp.Repository.Orders;
using FurnitureApp.Repository.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FurnitureApp.Repository.MaterialInfos
{
    public class MaterialInfoRepository
    {
        public List<MaterialInfo> SelectAll()
        {
            List<MaterialInfo> ms = null;

            RepositoryAction.Query(c =>
            {
                ms = new MaterialInfoDao(c, null).SelectAll().OrderBy(x => x.Sequence).ToList();
            });

            return ms;
        }

        public void Insert(MaterialInfo m)
        {
            this.Insert(new List<MaterialInfo> { m });
        }
        public void Insert(IEnumerable<MaterialInfo> ms)
        {
            RepositoryAction.Transaction((c, t) =>
            {
                var dao = new MaterialInfoDao(c, t);

                foreach (var m in ms)
                {
                    dao.Insert(m);
                }
            });
        }
        public void Update(MaterialInfo m)
        {
            this.Update(new List<MaterialInfo> { m });
        }
        public void Update(IEnumerable<MaterialInfo> ms)
        {
            RepositoryAction.Transaction((c, t) =>
            {
                var dao = new MaterialInfoDao(c, t);

                foreach (var m in ms)
                {
                    dao.Update(m);
                }
            });
        }
        public void Delete(MaterialInfo m)
        {
            this.Delete(new List<MaterialInfo> { m });
        }
        public void Delete(IEnumerable<MaterialInfo> ms)
        {
            RepositoryAction.Transaction((c, t) =>
            {
                var materialSizeInfoDao = new MaterialSizeInfoDao(c, t);
                var boardLayerDao = new BoardLayerDao(c, t);

                foreach (var m in ms)
                {
                    if (materialSizeInfoDao.ExistMaterialInfoId(m.Id))
                    {
                        throw new Exception($"素材規格マスタで使用されています : {m.Name}");
                    }
                    if (boardLayerDao.ExistMaterialInfoId(m.Id))
                    {
                        throw new Exception($"層情報で使用されています : {m.Name}");
                    }
                }

                var materialInfoDao = new MaterialInfoDao(c, t);

                foreach (var m in ms)
                {
                    materialInfoDao.DeleteById((int)m.Id);
                }
            });
        }
    }
}
