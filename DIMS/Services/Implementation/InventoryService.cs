using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DIMS.Services.Abstract;
using DIMS.ViewModels;
using Metron.Entities;
using Repository.Base;
using Repository.Core;

namespace DIMS.Services.Implementation
{
    public class ItemCategoryService : ServiceBase<ItemCategory>, IItemCategory, IService<ItemCategory>
    {
        private readonly IUnitOfWork _uow;

        public ItemCategoryService(IUnitOfWork uow)
            : base(uow)
        {
            _uow = uow;
        }

        public IEnumerable<ItemCategoryViewModel> GetItemCategories()
        {
            throw new NotImplementedException();
        }

        public int SaveItemCategory(ItemCategoryViewModel model)
        {
            var itemCategory = new ItemCategory();
            var entity =
                new MapperConfiguration((Action<IMapperConfiguration>) (cfg =>
                        cfg.CreateMap<ItemCategoryViewModel, ItemCategory>())).CreateMapper()
                    .Map<ItemCategoryViewModel, ItemCategory>(model);
            entity.CreatedDate = DateTime.Now;
            var num = _uow.Repository<ItemCategory>().Add(entity, false);
            return num;
        }

        public ItemCategoryViewModel GetCategory(int id)
        {
            return _uow.Repository<ItemCategoryViewModel>()
                .GetEntitiesBySql(string.Format("select * from ItemCategory where ItemCategoryId = '" + id + "'"))
                .First();
        }
    }

    public class StoreService : ServiceBase<ItemStore>, IItemStore, IService<ItemStore>
    {
        private readonly IUnitOfWork _uow;

        public StoreService(IUnitOfWork uow)
            : base(uow)
        {
            _uow = uow;
        }


        public int SaveStore(ItemStoreViewModel storeModel)
        {
            var model = new ItemStore();
            var entity =
                new MapperConfiguration(cfg =>
                        cfg.CreateMap<ItemStoreViewModel, ItemStore>()).CreateMapper()
                    .Map<ItemStoreViewModel, ItemStore>(storeModel);
            entity.CreatedDate = DateTime.Now;
            var num = _uow.Repository<ItemStore>().Add(entity, false);
            return num;
        }

        public ItemStoreViewModel GetSingleItem(int id)
        {
            var masDesignation = _uow.Repository<ItemStore>().Get(id);
            if (masDesignation == null) return null;
            return new ItemStoreViewModel
            {
                ItemStoreId = masDesignation.ItemStoreId,
                StoreName = masDesignation.StoreName,
                StockCode = masDesignation.StockCode,
                StoreDescription = masDesignation.StoreDescription
            };
        }
    }

    public class ItemSupplierService : ServiceBase<ItemSupplier>, IItemSupplier, IService<ItemSupplier>
    {

        private readonly IUnitOfWork _uow;

        public ItemSupplierService(IUnitOfWork uow)
            : base(uow)
        {
            _uow = uow;
        }

        public int Save(ItemSupplierViewModel param)
        {
            var model = new ItemSupplier();
            var entity =
                new MapperConfiguration(cfg =>
                        cfg.CreateMap<ItemSupplierViewModel, ItemSupplier>()).CreateMapper()
                    .Map<ItemSupplierViewModel, ItemSupplier>(param);
            entity.CreatedDate = DateTime.Now;
            var num = _uow.Repository<ItemSupplier>().Add(entity, false);
            return num;
        }

        public ItemSupplierViewModel GetSingleItem(int id)
        {
            var masDesignation = _uow.Repository<ItemSupplier>().Get(id);
            if (masDesignation == null) return null;
            return new ItemSupplierViewModel
            {
                ItemSupplierId = masDesignation.ItemSupplierId,
                ItemSupplierName = masDesignation.ItemSupplierName,
                ContactPersonEmail = masDesignation.ContactPersonEmail,
                ContactPersonName = masDesignation.ContactPersonName,
                Email = masDesignation.Email,
                Phone = masDesignation.Phone,
                Address = masDesignation.Address,
                IsActive = masDesignation.IsActive,
                CreatedDate = masDesignation.CreatedDate,
                Description = masDesignation.Description
            };
        }
    }

    public class ItemService : ServiceBase<Item>, IItem, IService<Item>
    {

        private readonly IUnitOfWork _uow;
        private readonly ItemCategoryService _categoryService;
        
        public ItemService(IUnitOfWork uow)
            : base(uow)
        {
            _uow = uow;
            _categoryService = new ItemCategoryService(_uow);
        }

        public int Save(ItemViewModel param)
        {
            var model = new Item();
            var entity =
                new MapperConfiguration(cfg =>
                        cfg.CreateMap<ItemViewModel, Item>()).CreateMapper()
                    .Map<ItemViewModel, Item>(param);
            entity.CreatedDate = DateTime.Now;
            var num = _uow.Repository<Item>().Add(entity, false);
            return num;
        }

        public IEnumerable<ItemViewModel> GetItemsList()
        {
            var orderedEnumerable = _uow.Repository<Item>().GetAll().OrderBy(A => A.Name);
            var designationViewModalList = new List<ItemViewModel>();
            foreach (var masDesignation in orderedEnumerable)
            {
                var category = _categoryService.GetCategory(masDesignation.ItemCategoryId);
               
                designationViewModalList.Add(new ItemViewModel
                {
                    ItemSupplierId = masDesignation.ItemSupplierId,
                    Name = masDesignation.Name,
                    Description = masDesignation.Description,
                    ItemCategoryId = masDesignation.ItemCategoryId,
                    ItemCategory = category,
                    Quantity = masDesignation.Quantity,
                    IsActive = masDesignation.IsActive,
                    CreatedDate = masDesignation.CreatedDate,
                    ItemId = masDesignation.ItemId
                });
            }

            return designationViewModalList.ToList();
        }

        public ItemViewModel GetSingleItem(int id)
        {
            var masDesignation = _uow.Repository<Item>().Get(id);
            if (masDesignation == null) return null;
            var category = _categoryService.GetCategory(masDesignation.ItemCategoryId);
            return new ItemViewModel
            {
                ItemId = masDesignation.ItemId,
                ItemSupplierId = masDesignation.ItemSupplierId,
                Name = masDesignation.Name,
                Description = masDesignation.Description,
                ItemCategoryId = masDesignation.ItemCategoryId,
                ItemCategory = category,
                Quantity = masDesignation.Quantity,
                IsActive = masDesignation.IsActive,
                CreatedDate = masDesignation.CreatedDate
            };
        }

        public IEnumerable<ItemViewModel> GetItemByCategory(int categoryId)
        {
            var orderedEnumerable = _uow.Repository<Item>().GetAll($"ItemCategoryId = {categoryId}");
            var designationViewModalList = new List<ItemViewModel>();
            foreach (var masDesignation in orderedEnumerable)
            {
                var category = _categoryService.GetCategory(masDesignation.ItemCategoryId);
                designationViewModalList.Add(new ItemViewModel
                {
                    ItemSupplierId = masDesignation.ItemSupplierId,
                    Name = masDesignation.Name,
                    Description = masDesignation.Description,
                    ItemCategoryId = masDesignation.ItemCategoryId,
                    ItemCategory = category,
                    Quantity = masDesignation.Quantity,
                    IsActive = masDesignation.IsActive,
                    CreatedDate = masDesignation.CreatedDate,
                    ItemId = masDesignation.ItemId
                });
            }

            return designationViewModalList.ToList();
        }

        public bool Edit(ItemViewModel param)
        {
            var masDesignation = _uow.Repository<Item>().Get(param.ItemId);
            if (masDesignation == null) return false;
            _uow.Repository<Item>().Update(new Item()
            {
                ItemId = param.ItemId,
                Name = param.Name,
                ItemSupplierId = param.ItemSupplierId,
                Description = param.Description,
                ItemCategoryId = param.ItemCategoryId,
                Quantity = param.Quantity,
                IsActive = param.IsActive,
                ModifiedDate = DateTime.Now
            });
            return true;
        }

    }

    public class ItemStockService : ServiceBase<ItemStock>, IItemStock, IService<ItemStock>
    {

        private readonly IUnitOfWork _uow;
        private readonly IItem _itemService;
        private readonly IItemStore _storeService;
        private readonly IItemSupplier _supplierService;
        public ItemStockService(IUnitOfWork uow)
            : base(uow)
        {
            _uow = uow;
            _itemService = new ItemService(_uow);
            _storeService= new StoreService(_uow);
            _supplierService = new ItemSupplierService(_uow);
        }

        public int Save(ItemStockViewModel param)
        {
            var model = new ItemStock();
            var entity =
                new MapperConfiguration(cfg =>
                        cfg.CreateMap<ItemStockViewModel, ItemStock>()).CreateMapper()
                    .Map<ItemStockViewModel, ItemStock>(param);
            entity.CreatedDate = DateTime.Now;
            var num = _uow.Repository<ItemStock>().Add(entity, false);
            return num;
        }

        public IEnumerable<ItemStockViewModel> GetItemsList()
        {
            var orderedEnumerable = _uow.Repository<ItemStock>().GetAll().OrderBy(a=>a.CreatedDate);
            var designationViewModalList = new List<ItemStockViewModel>();
            foreach (var masDesignation in orderedEnumerable)
            {
                var item = _itemService.GetSingleItem((int) masDesignation.ItemId);
                var store = new ItemStoreViewModel();
                var supplier = new ItemSupplierViewModel();
                if (masDesignation.StoreId != null)
                {
                    store = _storeService.GetSingleItem((int) masDesignation.StoreId);
                }

                if (masDesignation.SupplierId != null)
                {
                    supplier = _supplierService.GetSingleItem((int) masDesignation.SupplierId);
                }

                designationViewModalList.Add(new ItemStockViewModel
                {
                    ItemStockId = masDesignation.ItemStockId,
                    ItemId = masDesignation.ItemId,
                    SupplierId = masDesignation.SupplierId,
                    Symbol = masDesignation.Symbol,
                    StoreId = masDesignation.StoreId,
                    Date = masDesignation.Date,
                    Attachement = masDesignation.Attachement,
                    Description = masDesignation.Description,
                    Quantity = masDesignation.Quantity,
                    IsActive = masDesignation.IsActive,
                    CreatedDate = masDesignation.CreatedDate,
                    Item = item,
                    ItemCategory = item.ItemCategory,
                    ItemStore = store,
                    ItemSupplier = supplier
                });
            }

            return designationViewModalList.ToList();
        }

        public ItemStockViewModel GetSingleItem(int id)
        {
            var masDesignation = _uow.Repository<ItemStock>().Get(id);
            if (masDesignation == null) return null;
            var item = _itemService.GetSingleItem((int) masDesignation.ItemId);
            var store = new ItemStoreViewModel();
            var supplier = new ItemSupplierViewModel();
            if (masDesignation.StoreId != null)
            {
                store = _storeService.GetSingleItem((int) masDesignation.StoreId);
            }

            if (masDesignation.SupplierId != null)
            {
                supplier = _supplierService.GetSingleItem((int) masDesignation.SupplierId);
            }

            return new ItemStockViewModel
            {
                ItemStockId = masDesignation.ItemStockId,
                ItemId = masDesignation.ItemId,
                SupplierId = masDesignation.SupplierId,
                Symbol = masDesignation.Symbol,
                StoreId = masDesignation.StoreId,
                Date = masDesignation.Date,
                Attachement = masDesignation.Attachement,
                Description = masDesignation.Description,
                Quantity = masDesignation.Quantity,
                IsActive = masDesignation.IsActive,
                CreatedDate = masDesignation.CreatedDate,
                Item = item,
                ItemCategory = item.ItemCategory,
                ItemStore = store,
                ItemSupplier = supplier
            };

        }

        public bool Edit(ItemStockViewModel param)
        {
            var masDesignation = _uow.Repository<ItemStock>().Get(param.ItemStockId);
            if (masDesignation == null) return false;
            _uow.Repository<ItemStock>().Update(new ItemStock()
            {
                ItemStockId = param.ItemStockId,
                ItemId = param.ItemId,
                SupplierId = param.SupplierId,
                Symbol = param.Symbol,
                StoreId = param.StoreId,
                Date = param.Date,
                Attachement = param.Attachement,
                Description = param.Description,
                Quantity = param.Quantity,
                CreatedDate = param.CreatedDate,
                IsActive = param.IsActive,
                ModifiedDate = DateTime.Now
            });
            return true;
        }

        public DtoStock GetItemQuantity(int itemId)
        {
            var item = this._uow.Repository<DtoStock>().GetEntitiesBySql(string.Format(Queries.GetItemQuantity, (object)itemId)).First();
            item.AvlQuantity = item.AddedStock - item.Issued;
            return item;
        }
    }

    public class ItemIssueService : ServiceBase<ItemIssue>, IItemIssue, IService<ItemIssue>
    {
        private readonly IUnitOfWork _uow;
        private readonly IItem _itemService;
        public ItemIssueService(IUnitOfWork uow) : base(uow)
        {
            _uow = uow;
            _itemService=new ItemService(_uow);
        }

        public int Save(ItemIssueViewModel param)
        {
            var model = new ItemIssue();
            var entity =
                new MapperConfiguration(cfg =>
                        cfg.CreateMap<ItemIssueViewModel, ItemIssue>()).CreateMapper()
                    .Map<ItemIssueViewModel, ItemIssue>(param);
            entity.CreatedDate = DateTime.Now;
            var num = _uow.Repository<ItemIssue>().Add(entity, false);
            return num;
        }

        public IEnumerable<ItemIssueViewModel> GetItemsList()
        {
            var orderedEnumerable = _uow.Repository<ItemIssue>().GetAll().OrderBy(a => a.CreatedDate);
            var designationViewModalList = new List<ItemIssueViewModel>();
            foreach (var masDesignation in orderedEnumerable)
            {
                var item = _itemService.GetSingleItem((int)masDesignation.ItemId);

                designationViewModalList.Add(new ItemIssueViewModel
                {
                    ItemIssueId = masDesignation.ItemIssueId,
                    IssueType = masDesignation.IssueType,
                    IssueTo = masDesignation.IssueTo,
                    IssueBy = masDesignation.IssueBy,
                    IssueDate = masDesignation.IssueDate,
                    ReturnDate = masDesignation.IssueDate,
                    ItemCategoryId = masDesignation.ItemCategoryId,
                    ItemId = masDesignation.ItemId,
                    Quantity = masDesignation.Quantity,
                    Note = masDesignation.Note,
                    IsReturned = masDesignation.IsReturned,
                    IsActive = masDesignation.IsActive,
                    CreatedDate = masDesignation.CreatedDate,
                    ModifiedDate = masDesignation.ModifiedDate,
                    Item = item,
                    ItemCategory = item.ItemCategory
                });
            }

            return designationViewModalList.ToList();
        }

        public ItemIssueViewModel GetSingleItem(int id)
        {
            var masDesignation = _uow.Repository<ItemIssue>().Get(id);
            if (masDesignation == null) return null;

            var item = _itemService.GetSingleItem((int)masDesignation.ItemId);
            return new ItemIssueViewModel
            {
                ItemIssueId = masDesignation.ItemIssueId,
                IssueType = masDesignation.IssueType,
                IssueTo = masDesignation.IssueTo,
                IssueBy = masDesignation.IssueBy,
                IssueDate = masDesignation.IssueDate,
                ReturnDate = masDesignation.IssueDate,
                ItemCategoryId = masDesignation.ItemCategoryId,
                ItemId = masDesignation.ItemId,
                Quantity = masDesignation.Quantity,
                Note = masDesignation.Note,
                IsReturned = masDesignation.IsReturned,
                IsActive = masDesignation.IsActive,
                CreatedDate = masDesignation.CreatedDate,
                ModifiedDate = masDesignation.ModifiedDate,
                Item = item,
                ItemCategory = item.ItemCategory
            };
        }

        public bool Edit(ItemIssueViewModel masDesignation)
        {
            var param = _uow.Repository<ItemIssue>().Get(masDesignation.ItemIssueId);
            if (param == null) return false;
            _uow.Repository<ItemIssue>().Update(new ItemIssue()
            {
                ItemIssueId = masDesignation.ItemIssueId,
                IssueType = masDesignation.IssueType,
                IssueTo = masDesignation.IssueTo,
                IssueBy = masDesignation.IssueBy,
                IssueDate = masDesignation.IssueDate,
                ReturnDate = masDesignation.IssueDate,
                ItemCategoryId = masDesignation.ItemCategoryId,
                ItemId = masDesignation.ItemId,
                Quantity = masDesignation.Quantity,
                Note = masDesignation.Note,
                IsReturned = masDesignation.IsReturned,
                IsActive = masDesignation.IsActive,
                CreatedDate = param.CreatedDate,
                ModifiedDate = DateTime.Now,
            });
            return true;
        }
    }
}
