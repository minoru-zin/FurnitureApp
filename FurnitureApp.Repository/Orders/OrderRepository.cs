using FurnitureApp.Repository.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureApp.Repository.Orders
{
    public class OrderRepository
    {
        public string ProductFileDirName { get; } = "ProductFiles";

        public OrderRepository()
        {
            Utility.DirectoryCreator.CreateSafely(this.ProductFileDirName);
        }

        public List<Order> SelectTopOnlyFromCreatedDate(DateTime createdDateF, DateTime createdDateT)
        {
            var orders = new List<Order>();

            RepositoryAction.Query(c =>
            {
                orders = new OrderDao(c, null).SelectFromCreatedDate(createdDateF, createdDateT).ToList();
            });

            return orders;
        }
        private void SetOrderProperties(Order order,
            ProductDao productDao, BoardDao boardDao, BoardLayerDao boardLayerDao,
            BoardCostDao boardCostDao, KoguchiPasteCostDao koguchiPasteCostDao, FinishCutCostDao finishCutCostDao, MakeupBoardPasteCostDao makeupBoardPasteCostDao, PaintCostDao paintCostDao, CostDao costDao, ProductFileDao productFilePathDao)
        {
            order.Products = productDao.SelectByOrderId((int)order.Id).ToList();

            foreach (var product in order.Products)
            {
                product.Boards = boardDao.SelectByProductId((int)product.Id).ToList();

                foreach (var board in product.Boards)
                {
                    board.BoardLayers = boardLayerDao.SelectByBoardId((int)board.Id).ToList();
                }

                product.BoardCosts = boardCostDao.SelectByProductId((int)product.Id).ToList();
                product.KoguchiPasteCosts = koguchiPasteCostDao.SelectByProductId((int)product.Id).ToList();
                product.FinishCutCosts = finishCutCostDao.SelectByProductId((int)product.Id).ToList();
                product.MakeupBoardPasteCosts = makeupBoardPasteCostDao.SelectByProductId((int)product.Id).ToList();
                product.PaintCosts = paintCostDao.SelectByProductId((int)product.Id).ToList();
                product.Costs = costDao.SelectByProductId((int)product.Id).ToList();
                product.ProductFiles = productFilePathDao.SelectByProductId((int)product.Id).ToList();
            }
        }
        public Order SelectById(int id)
        {
            Order order = null;

            RepositoryAction.Query(c =>
            {
                order = new OrderDao(c, null).SelectById(id);
                var productDao = new ProductDao(c, null);
                var boardDao = new BoardDao(c, null);
                var boardLayerDao = new BoardLayerDao(c, null);

                var boardCostDao = new BoardCostDao(c, null);
                var koguchiPasteCostDao = new KoguchiPasteCostDao(c, null);
                var finishCutCostDao = new FinishCutCostDao(c, null);
                var makeupBoardPasteCostDao = new MakeupBoardPasteCostDao(c, null);
                var paintCostDao = new PaintCostDao(c, null);
                var costDao = new CostDao(c, null);
                var productFilePathDao = new ProductFileDao(c, null);

                this.SetOrderProperties(order,
                        productDao, boardDao, boardLayerDao,
                        boardCostDao, koguchiPasteCostDao, finishCutCostDao, makeupBoardPasteCostDao, paintCostDao, costDao, productFilePathDao);
            });

            return order;
        }
        public void Insert(Order order)
        {
            this.Insert(new List<Order> { order });
        }
        public void Insert(IEnumerable<Order> orders)
        {
            RepositoryAction.Transaction((c, t) =>
            {
                var orderDao = new OrderDao(c, t);
                var productDao = new ProductDao(c, t);
                var boardDao = new BoardDao(c, t);
                var boardLayerDao = new BoardLayerDao(c, t);

                var boardCostDao = new BoardCostDao(c, t);
                var koguchiPasteCostDao = new KoguchiPasteCostDao(c, t);
                var finishCutCostDao = new FinishCutCostDao(c, t);
                var makeupBoardPasteCostDao = new MakeupBoardPasteCostDao(c, t);
                var paintCostDao = new PaintCostDao(c, null);
                var costDao = new CostDao(c, t);
                var productFilePathDao = new ProductFileDao(c, t);

                foreach (var order in orders)
                {
                    orderDao.Insert(order);
                    order.Id = orderDao.SelectLast().Id;

                    this.InsertOrderProperties(order,
                        productDao, boardDao, boardLayerDao,
                        boardCostDao, koguchiPasteCostDao, finishCutCostDao, makeupBoardPasteCostDao, paintCostDao, costDao, productFilePathDao);
                }

                foreach (var order in orders)
                {
                    foreach (var product in order.Products)
                    {
                        foreach (var file in product.ProductFiles)
                        {
                            var filePath = this.GetProductFilePath(order.Id, file.FileName);
                            Utility.DirectoryCreator.CreateSafely(Path.GetDirectoryName(filePath));
                            File.Copy(file.SourceFilePath, filePath);
                        }
                    }
                }
            });
        }
        private void InsertOrderProperties(Order order,
            ProductDao productDao, BoardDao boardDao, BoardLayerDao boardLayerDao,
            BoardCostDao boardCostDao, KoguchiPasteCostDao koguchiPasteCostDao, FinishCutCostDao finishCutCostDao, MakeupBoardPasteCostDao makeupBoardPasteCostDao, PaintCostDao paintCostDao, CostDao costDao, ProductFileDao productFileDao)
        {
            order.Products.ForEach(x => x.OrderId = order.Id);

            foreach (var product in order.Products)
            {
                productDao.Insert(product);
                var productId = productDao.SelectLast().Id;

                foreach (var board in product.Boards)
                {
                    board.ProductId = productId;
                    boardDao.Insert(board);
                    var boardId = boardDao.SelectLast().Id;
                    board.BoardLayers.ForEach(x => x.BoardId = boardId);

                    foreach (var bl in board.BoardLayers)
                    {
                        boardLayerDao.Insert(bl);
                    }
                }

                foreach (var cost in product.BoardCosts)
                {
                    cost.ProductId = productId;
                    boardCostDao.Insert(cost);
                }
                foreach (var cost in product.KoguchiPasteCosts)
                {
                    cost.ProductId = productId;
                    koguchiPasteCostDao.Insert(cost);
                }
                foreach (var cost in product.FinishCutCosts)
                {
                    cost.ProductId = productId;
                    finishCutCostDao.Insert(cost);
                }
                foreach (var cost in product.MakeupBoardPasteCosts)
                {
                    cost.ProductId = productId;
                    makeupBoardPasteCostDao.Insert(cost);
                }
                foreach (var cost in product.PaintCosts)
                {
                    cost.ProductId = productId;
                    paintCostDao.Insert(cost);
                }
                foreach (var cost in product.Costs)
                {
                    cost.ProductId = productId;
                    costDao.Insert(cost);
                }
                foreach (var file in product.ProductFiles.Where(x => !x.IsDeleted))
                {
                    file.ProductId = productId;
                    productFileDao.Insert(file);
                }
            }
        }
        public void Update(Order order)
        {
            this.Update(new List<Order> { order });
        }
        public void Update(IEnumerable<Order> orders)
        {
            RepositoryAction.Transaction((c, t) =>
            {
                var orderDao = new OrderDao(c, t);
                var productDao = new ProductDao(c, t);
                var boardDao = new BoardDao(c, t);
                var boardLayerDao = new BoardLayerDao(c, t);

                var boardCostDao = new BoardCostDao(c, t);
                var koguchiPasteCostDao = new KoguchiPasteCostDao(c, t);
                var finishCutCostDao = new FinishCutCostDao(c, t);
                var makeupBoardPasteCostDao = new MakeupBoardPasteCostDao(c, t);
                var paintCostDao = new PaintCostDao(c, null);
                var costDao = new CostDao(c, t);
                var productFilePathDao = new ProductFileDao(c, t);

                foreach (var order in orders)
                {
                    // 削除
                    var oldOrder = this.SelectById((int)order.Id);

                    this.DeleteOrderProperties(oldOrder,
                        productDao, boardDao, boardLayerDao,
                        boardCostDao, koguchiPasteCostDao, finishCutCostDao, makeupBoardPasteCostDao, paintCostDao, costDao, productFilePathDao);
                    orderDao.Update(order);

                    this.InsertOrderProperties(order,
                        productDao, boardDao, boardLayerDao,
                        boardCostDao, koguchiPasteCostDao, finishCutCostDao, makeupBoardPasteCostDao, paintCostDao, costDao, productFilePathDao);
                }

                foreach (var order in orders)
                {
                    foreach (var product in order.Products)
                    {
                        foreach (var file in product.ProductFiles)
                        {
                            var filePath = this.GetProductFilePath(order.Id, file.FileName);

                            Utility.DirectoryCreator.CreateSafely(Path.GetDirectoryName(filePath));

                            if (file.IsDeleted && File.Exists(filePath))
                            {
                                File.Delete(filePath);
                                continue;
                            }
                            if (string.IsNullOrEmpty(file.SourceFilePath)) { continue; }

                            File.Copy(file.SourceFilePath, filePath);
                        }
                    }
                }
            });
        }

        public void Delete(Order order)
        {
            this.Delete(new List<Order> { order });
        }
        public void Delete(IEnumerable<Order> orders)
        {
            RepositoryAction.Transaction((c, t) =>
            {
                var orderDao = new OrderDao(c, t);
                var productDao = new ProductDao(c, t);
                var boardDao = new BoardDao(c, t);
                var boardLayerDao = new BoardLayerDao(c, t);

                var boardCostDao = new BoardCostDao(c, t);
                var koguchiPasteCostDao = new KoguchiPasteCostDao(c, t);
                var finishCutCostDao = new FinishCutCostDao(c, t);
                var makeupBoardPasteCostDao = new MakeupBoardPasteCostDao(c, t);
                var paintCostDao = new PaintCostDao(c, null);
                var costDao = new CostDao(c, t);
                var productFileDao = new ProductFileDao(c, t);

                foreach (var order in orders)
                {
                    orderDao.DeleteById((int)order.Id);
                    this.DeleteOrderProperties(order, productDao, boardDao, boardLayerDao,
                        boardCostDao, koguchiPasteCostDao, finishCutCostDao, makeupBoardPasteCostDao, paintCostDao, costDao, productFileDao);
                }

                foreach (var order in orders)
                {
                    var dirPath = this.GetProductFileDirPath(order.Id);

                    if (!Directory.Exists(dirPath)) { continue; }

                    Directory.Delete(dirPath, true);
                }
            });
        }
        private void DeleteOrderProperties(Order order, ProductDao productDao, BoardDao boardDao, BoardLayerDao boardLayerDao,
            BoardCostDao boardCostDao, KoguchiPasteCostDao koguchiPasteCostDao, FinishCutCostDao finishCutCostDao, MakeupBoardPasteCostDao makeupBoardPasteCostDao, PaintCostDao paintCostDao, CostDao costDao, ProductFileDao productFileDao)
        {
            productDao.DeleteByOrderId((int)order.Id);

            foreach (var product in order.Products)
            {
                boardDao.DeleteByProductId((int)product.Id);

                foreach (var board in product.Boards)
                {
                    boardLayerDao.DeleteByBoardId((int)board.Id);
                }

                boardCostDao.DeleteByProductId((int)product.Id);
                koguchiPasteCostDao.DeleteByProductId((int)product.Id);
                finishCutCostDao.DeleteByProductId((int)product.Id);
                makeupBoardPasteCostDao.DeleteByProductId((int)product.Id);
                paintCostDao.DeleteByProductId((int)product.Id);
                costDao.DeleteByProductId((int)product.Id);
                productFileDao.DeleteByProductId((int)product.Id);
            }
        }

        public IEnumerable<Product> SelectTopOnlyByProductCategoryInfoCode(int productCategoryInfoCode)
        {
            IEnumerable<Product> products = null;

            RepositoryAction.Query(c =>
            {
                products = new ProductDao(c, null).SelectByProductCategoryInfoCode(productCategoryInfoCode);
            });

            return products;
        }

        public string GetProductFilePath(int? orderId, string fileName)
        {
            return Path.Combine(this.GetProductFileDirPath(orderId), fileName);
        }
        private string GetProductFileDirPath(int? orderId)
        {
            return Path.Combine(this.ProductFileDirName, $"{orderId}");
        }
    }
}
