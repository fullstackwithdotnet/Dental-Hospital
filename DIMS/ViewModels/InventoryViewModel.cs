using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Base;
using Repository.Core;

namespace DIMS.ViewModels
{
    [Table("ItemCategory")]
    public class ItemCategoryViewModel : EntityBase
    {

        public ItemCategoryViewModel()
        {
            CreatedDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
            IsActive = true;
        }

        [PrimaryKey] public int ItemCategoryId { get; set; }
        [Display(Name = "Item Category")] public string CategoryName { get; set; }
        [Display(Name = "Description")] public string CategoryDescription { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

    [Table("ItemStoreViewModel")]
    public class ItemStoreViewModel : EntityBase
    {

        public ItemStoreViewModel()
        {
            CreatedDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
            IsActive = true;
        }

        [PrimaryKey] public int ItemStoreId { get; set; }
        [Display(Name = "Item Store Name")] public string StoreName { get; set; }
        [Display(Name = "Item Stock Code")] public string StockCode { get; set; }
        [Display(Name = "Description")] public string StoreDescription { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

    [Table("ItemSupplierViewModel")]
    public class ItemSupplierViewModel : EntityBase
    {
        public ItemSupplierViewModel()
        {
            CreatedDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
            IsActive = true;
        }

        [PrimaryKey] public int ItemSupplierId { get; set; }
        [Display(Name = "Name")] public string ItemSupplierName { get; set; }
        [Display(Name = "Phone")] public string Phone { get; set; }
        [Display(Name = "Email")] public string Email { get; set; }
        [Display(Name = "Address")] public string Address { get; set; }

        [Display(Name = "Contact Person Name")]
        public string ContactPersonName { get; set; }

        [Display(Name = "Contact Person Phone")]
        public string ContactPersonPhone { get; set; }

        [Display(Name = "Contact Person Email")]
        public string ContactPersonEmail { get; set; }

        [Display(Name = "Description")] public string Description { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

    [Table("Item")]
    public class ItemViewModel : EntityBase
    {

        public ItemViewModel()
        {
            CreatedDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
            IsActive = true;
            Quantity = 0;
        }

        [PrimaryKey] public int ItemId { get; set; }
        [Display(Name = "Item Category")] public int ItemCategoryId { get; set; }
        [Display(Name = "Item Name")] public string Name { get; set; }
        public string ItemPhoto { get; set; }

        [Display(Name = "Item Description")] public string Description { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ItemStoreId { get; set; }
        public int? ItemSupplierId { get; set; }
        public int? Quantity { get; set; }

        public virtual ItemCategoryViewModel ItemCategory { get; set; }
    }

    [Table("ItemStock")]
    public class ItemStockViewModel : EntityBase
    {

        public ItemStockViewModel()
        {
            CreatedDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
            IsActive = true;
            Quantity = 0;
            Symbol = "+";
            Date = (DateTime) CreatedDate;
        }

        [PrimaryKey] public int ItemStockId { get; set; }
        [Display(Name = "Item")] public int? ItemId { get; set; }
        [Display(Name = "Supplier")] public int? SupplierId { get; set; }
        [Display(Name = "Symbol")] public string Symbol { get; set; }
        [Display(Name = "Store")] public int? StoreId { get; set; }
        [Display(Name = "Quantity")] public int? Quantity { get; set; }
        public DateTime Date { get; set; }

        [Display(Name = "Attach Document")] public string Attachement { get; set; }

        [Display(Name = "Description")] public string Description { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public ItemViewModel Item { get; set; }
        public ItemSupplierViewModel ItemSupplier { get; set; }
        public ItemStoreViewModel ItemStore { get; set; }
        public ItemCategoryViewModel ItemCategory { get; set; }
    }

    [Table("ItemIssue")]
    public class ItemIssueViewModel : EntityBase
    {
        public ItemIssueViewModel()
        {
            CreatedDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
            IsActive = true;
            Quantity = 0;
        }

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

        public ItemCategoryViewModel ItemCategory { get; set; }
        public ItemViewModel Item { get; set; }
    }

    public class DtoStock : EntityBase
    {
        public int ItemId { get; set; }
        public int ItemCategoryId { get; set; }
        public int AddedStock { get; set; }
        public int Returned { get; set; }
        public int Issued { get; set; }
        public int Quantity { get; set; }
        public int AvlQuantity { get; set; }
    }
}
