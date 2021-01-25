using System;
using System.ComponentModel.DataAnnotations.Schema;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
    [Table("ItemCategory")]
    public class ItemCategory : EntityBase
    {
        [PrimaryKey] public int ItemCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

    [Table("ItemStore")]
    public class ItemStore : EntityBase
    {
        [PrimaryKey] public int ItemStoreId { get; set; }
        public string StoreName { get; set; }
        public string StockCode { get; set; }
        public string StoreDescription { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

    [Table("ItemSupplier")]
    public class ItemSupplier : EntityBase
    {
        [PrimaryKey]
        public int ItemSupplierId { get; set; }
        public string ItemSupplierName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string ContactPersonName { get; set; }
        public string ContactPersonPhone { get; set; }
        public string ContactPersonEmail { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

    [Table("Item")]
    public class Item : EntityBase
    {
        [PrimaryKey]
        public int ItemId { get; set; }
        public int ItemCategoryId { get; set; }
        public string Name { get; set; }
        public string ItemPhoto { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ItemStoreId { get; set; }
        public int? ItemSupplierId { get; set; }
        public int? Quantity { get; set; }
    }


    [Table("ItemStock")]
    public class ItemStock : EntityBase
    {
        [PrimaryKey]
        public int ItemStockId { get; set; }
        public int? ItemId { get; set; }
        public int? SupplierId { get; set; }
        public string Symbol { get; set; }
        public int? StoreId { get; set; }
        public int? Quantity { get; set; }
        public DateTime Date { get; set; }
        public string Attachement { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        //public Item Item { get; set; }
        //public ItemSupplier ItemSupplier { get; set; }
        //public ItemStore ItemStore { get; set; }
    }

    [Table("ItemIssue")]
    public class ItemIssue : EntityBase
    {
        [PrimaryKey]
        public int ItemIssueId { get; set; }
        public string IssueType { get; set; }
        public string IssueTo { get; set; }
        public string IssueBy { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int? ItemCategoryId { get; set; }
        public int? ItemId { get; set; }
        public int Quantity { get; set; }
        public string Note { get; set; }
        public bool IsReturned { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        //public ItemCategory ItemCategory { get; set; }
        //public Item Item { get; set; }
    }




}
