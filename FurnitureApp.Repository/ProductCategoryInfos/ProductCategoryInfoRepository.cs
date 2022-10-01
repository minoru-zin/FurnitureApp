using FurnitureApp.Repository.Orders;
using FurnitureApp.Repository.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FurnitureApp.Repository.ProductCategoryInfos
{
    public class ProductCategoryInfoRepository
    {
        public List<ProductCategoryInfo> SelectAll()
        {
            List<ProductCategoryInfo> ms = null;

            RepositoryAction.Query(c =>
            {
                ms = new ProductCategoryInfoDao(c, null).SelectAll().OrderBy(x => x.Sequence).ToList();
            });

            return ms;
        }

        public void Insert(ProductCategoryInfo m)
        {
            this.Insert(new List<ProductCategoryInfo> { m });
        }
        public void Insert(IEnumerable<ProductCategoryInfo> ms)
        {
            RepositoryAction.Transaction((c, t) =>
            {
                var dao = new ProductCategoryInfoDao(c, t);

                foreach (var m in ms)
                {
                    dao.Insert(m);
                }
            });
        }
        public void Update(ProductCategoryInfo m)
        {
            this.Update(new List<ProductCategoryInfo> { m });
        }
        public void Update(IEnumerable<ProductCategoryInfo> ms)
        {
            RepositoryAction.Transaction((c, t) =>
            {
                var dao = new ProductCategoryInfoDao(c, t);

                foreach (var m in ms)
                {
                    dao.Update(m);
                }
            });
        }
        public void Delete(ProductCategoryInfo m)
        {
            this.Delete(new List<ProductCategoryInfo> { m });
        }
        public void Delete(IEnumerable<ProductCategoryInfo> ms)
        {
            RepositoryAction.Transaction((c, t) =>
            {
                var productDao = new ProductDao(c, t);
                
                foreach (var m in ms)
                {
                    if (productDao.ExistProductCategoryInfoId(m.Id))
                    {
                        throw new Exception($"製品情報で使用されています : {m.Name}");
                    }
                }

                var productCategoryInfoDao = new ProductCategoryInfoDao(c, t);

                foreach (var m in ms)
                {
                    productCategoryInfoDao.DeleteById((int)m.Id);
                }
            });
        }
    }
}
