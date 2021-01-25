using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIMS.ViewModels;
using Metron.Entities;
using Repository.Base;



namespace DIMS.Services.Abstract
{
    public interface IItemCategory : IService<ItemCategory>
    {

        IEnumerable<ItemCategoryViewModel> GetItemCategories();
        int SaveItemCategory(ItemCategoryViewModel model);
        ItemCategoryViewModel GetCategory(int id);
    }

    public interface IItemStore : IService<ItemStore>
    {
        int SaveStore(ItemStoreViewModel model);
        ItemStoreViewModel GetSingleItem(int id);
    }

    public interface IItemSupplier : IService<ItemSupplier>
    {
        int Save(ItemSupplierViewModel model);
        ItemSupplierViewModel GetSingleItem(int id);
    }

    public interface IItem : IService<Item>
    {
        int Save(ItemViewModel model);

        IEnumerable<ItemViewModel> GetItemsList();
        ItemViewModel GetSingleItem(int id);
        bool Edit(ItemViewModel masDesignation);
        IEnumerable<ItemViewModel>  GetItemByCategory(int categoryId);
    }

    public interface IItemStock : IService<ItemStock>
    {
        int Save(ItemStockViewModel model);

        IEnumerable<ItemStockViewModel> GetItemsList();
        ItemStockViewModel GetSingleItem(int id);
        bool Edit(ItemStockViewModel masDesignation);
        DtoStock GetItemQuantity(int itemId);
    }

    public interface IItemIssue : IService<ItemIssue>
    {
        int Save(ItemIssueViewModel model);

        IEnumerable<ItemIssueViewModel> GetItemsList();
        ItemIssueViewModel GetSingleItem(int id);
        bool Edit(ItemIssueViewModel masDesignation);
    }
}
