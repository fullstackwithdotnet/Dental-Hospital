using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using DIMS.Helpers;
using DIMS.Infrastructure;
using DIMS.Services.Abstract;
using DIMS.ViewModels;
using Metron.Entities;
using Repository.Base;

namespace DIMS.Controllers
{
    public class ItemCategoryController : BaseController
    {

        private IUnitOfWork _uow;
        private readonly IItemCategory _service;
        public ItemCategoryController(IUnitOfWork uow, IItemCategory service, IUserService userservice)
            : base(uow, userservice)
        {
            _uow = uow;
            _service = service;
        }


        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Index()
        {
            GetPermissionforUser();
            if (!User.Departments.Contains(17))
                return View("../Error/AccessDenied");
            var orderedEnumerable = _uow.Repository<ItemCategory>().GetAll().OrderBy(A => A.CategoryName);
            var designationViewModalList = new List<ItemCategoryViewModel>();
            foreach (var masDesignation in orderedEnumerable)
                designationViewModalList.Add(new ItemCategoryViewModel
                {
                    ItemCategoryId = masDesignation.ItemCategoryId,
                    CategoryName = masDesignation.CategoryName,
                    CategoryDescription = masDesignation.CategoryDescription
                });
            return View(designationViewModalList);
        }

        [CustomAuthorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Create()
        {
            GetPermissionforUser();
            return User.Departments.Contains(17) ? View(new ItemCategoryViewModel()) : View("../Error/AccessDenied");
        }

        [HttpPost]
        public ActionResult Create(ItemCategoryViewModel model)
        {
            if (!User.Departments.Contains(17))
                return View("../Error/AccessDenied");
            if (ModelState.IsValid)
            {

                var num = _service.SaveItemCategory(model);
                if (num > 0)
                {
                    var result = new PagedListResult<ItemViewModel>()
                    {
                        Success = true,
                        Message = $"Category <em>{model.CategoryName}</em> is saved successfully.."
                    };
                    TempData["Result"] = result;

                }

                return RedirectToAction("Create");
            }
            return View(model);
        }

        [CustomAuthorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            GetPermissionforUser();
            if (!User.Departments.Contains(17))
                return View("../Error/AccessDenied");
            var masDesignation = _service.Get(id);
            return View(new ItemCategoryViewModel
            {
                ItemCategoryId = masDesignation.ItemCategoryId,
                CategoryName = masDesignation.CategoryName,
                CategoryDescription = masDesignation.CategoryDescription
            });
        }

        [CustomAuthorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(ItemCategoryViewModel vmItemCategory)
        {
            if (!User.Departments.Contains(17))
                return View("../Error/AccessDenied");

            if (!User.Departments.Contains(17))
                return View("../Error/AccessDenied");
            if (ModelState.IsValid)
            {
                _service.Update(new ItemCategory()
                {
                    CategoryName = vmItemCategory.CategoryName,
                    ItemCategoryId = vmItemCategory.ItemCategoryId,
                    CategoryDescription = vmItemCategory.CategoryDescription,
                    ModifiedDate = DateTime.Now
                });
                var result = new PagedListResult<ItemStoreViewModel>()
                {
                    Success = true,
                    Message = $"Category <em>{vmItemCategory.CategoryName}</em>  is updated successfully.."
                };
                TempData["Result"] = result;
                return RedirectToAction("Index");
            }
            return View(vmItemCategory);

        }


    }

    public class ItemStoreController : BaseController
    {

        private IUnitOfWork _uow;
        private readonly IItemStore _service;
        public ItemStoreController(IUnitOfWork uow, IUserService userservice, IItemStore storeService)
            : base(uow, userservice)
        {
            _uow = uow;
            _service = storeService;
        }


        [CustomAuthorize(Roles = "Admin")]
        [RestoreTempToModelState]
        public ActionResult Index()
        {
            ViewBag.Result = TempData["Result"];
            GetPermissionforUser();
            if (!User.Departments.Contains(17))
                return View("../Error/AccessDenied");
            var orderedEnumerable = _uow.Repository<ItemStore>().GetAll().OrderBy(A => A.StoreName);
            var designationViewModalList = new List<ItemStoreViewModel>();
            foreach (var masDesignation in orderedEnumerable)
                designationViewModalList.Add(new ItemStoreViewModel
                {
                    ItemStoreId = masDesignation.ItemStoreId,
                    StoreName = masDesignation.StoreName,
                    StockCode = masDesignation.StockCode,
                    StoreDescription = masDesignation.StoreDescription
                });
            return View(designationViewModalList);
        }

        [CustomAuthorize(Roles = "Admin")]
        [HttpGet]
        [RestoreTempToModelState]
        public ActionResult Create()
        {
            ViewBag.Result = TempData["Result"];
            ModelState.Clear();
            GetPermissionforUser();
            return User.Departments.Contains(17) ? View(new ItemStoreViewModel()) : View("../Error/AccessDenied");
        }

        [HttpPost]
        [StoreTempToModelState]
        public ActionResult Create(ItemStoreViewModel model)
        {
            if (!User.Departments.Contains(17))
                return View("../Error/AccessDenied");
            if (ModelState.IsValid)
            {
                var num = _service.SaveStore(model);
                if (num > 0)
                {
                    var result = new PagedListResult<ItemStoreViewModel>()
                    {
                        Success = true,
                        Message = $"<em>{model.StoreName}</em> is saved successfully.."
                    };
                    TempData["Result"] = result;
                    //return RedirectToAction("Create", new RouteValueDictionary(new
                    //{
                    //    controller = "ItemStore",
                    //    action = "Create",
                    //    id = num
                    //}));                
                }

                return RedirectToAction("Create");
            }

            //foreach (var modelState in ViewData.ModelState.Values)
            //{
            //    using (var enumerator = modelState.Errors.GetEnumerator())
            //    {
            //        if (enumerator.MoveNext())
            //        {
            //            var current = enumerator.Current;
            //            return RedirectToAction("ErrorWrite", new RouteValueDictionary(new
            //            {
            //                controller = "Error",
            //                action = "ErrorWrite",
            //                message = (current.ErrorMessage + "-" + (object)current.Exception)
            //            }));
            //        }
            //    }
            //}
            //return View("../Error/AccessDenied");
            return View(model);
        }

        [CustomAuthorize(Roles = "Admin")]
        [RestoreTempToModelState]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Result = TempData["Result"];
            GetPermissionforUser();
            if (!User.Departments.Contains(17))
                return View("../Error/AccessDenied");
            var masDesignation = _service.Get(id);
            return View(new ItemStoreViewModel()
            {
                ItemStoreId = masDesignation.ItemStoreId,
                StoreName = masDesignation.StoreName,
                StockCode = masDesignation.StockCode,
                StoreDescription = masDesignation.StoreDescription
            });
        }

        [CustomAuthorize(Roles = "Admin")]
        [HttpPost]
        [StoreTempToModelState]
        public ActionResult Edit(ItemStoreViewModel vmItemCategory)
        {
            if (!User.Departments.Contains(17))
                return View("../Error/AccessDenied");
            if (ModelState.IsValid)
            {
                _service.Update(new ItemStore()
                {
                    ItemStoreId = vmItemCategory.ItemStoreId,
                    StoreName = vmItemCategory.StoreName,
                    StockCode = vmItemCategory.StockCode,
                    StoreDescription = vmItemCategory.StoreDescription,
                    ModifiedDate = DateTime.Now
                });

                var result = new PagedListResult<ItemStoreViewModel>()
                {
                    Success = true,
                    Message = $"<em>{vmItemCategory.StoreName}</em> is updated successfully.."
                };
                TempData["Result"] = result;
                return RedirectToAction("Index");
            }
            //else
            //{
            //    foreach (ModelState modelState in ViewData.ModelState.Values)
            //    {
            //        using (IEnumerator<ModelError> enumerator = modelState.Errors.GetEnumerator())
            //        {
            //            if (enumerator.MoveNext())
            //            {
            //                var current = enumerator.Current;
            //                return RedirectToAction("ErrorWrite", new RouteValueDictionary(new
            //                {
            //                    controller = "Error",
            //                    action = "ErrorWrite",
            //                    message = (current.ErrorMessage + "-" + (object)current.Exception)
            //                }));
            //            }
            //        }
            //    }
            //}
            //return RedirectToAction("Index");
            return View(vmItemCategory);
        }
    }

    public class ItemSupplierController : BaseController
    {

        private readonly IUnitOfWork _uow;
        private readonly IItemSupplier _service;
        public ItemSupplierController(IUnitOfWork uow, IUserService userService, IItemSupplier supplierService)
            : base(uow, userService)
        {
            _uow = uow;
            _service = supplierService;
        }


        [CustomAuthorize(Roles = "Admin")]
        [RestoreTempToModelState]
        public ActionResult Index()
        {
            ViewBag.Result = TempData["Result"];
            GetPermissionforUser();
            if (!User.Departments.Contains(17))
                return View("../Error/AccessDenied");
            var orderedEnumerable = _uow.Repository<ItemSupplier>().GetAll().OrderBy(A => A.ItemSupplierName);
            var designationViewModalList = new List<ItemSupplierViewModel>();
            foreach (var masDesignation in orderedEnumerable)
                designationViewModalList.Add(new ItemSupplierViewModel
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
                });
            return View(designationViewModalList);
        }

        [CustomAuthorize(Roles = "Admin")]
        [HttpGet]
        [RestoreTempToModelState]
        public ActionResult Create()
        {
            ViewBag.Result = TempData["Result"];
            ModelState.Clear();
            GetPermissionforUser();
            return User.Departments.Contains(17) ? View(new ItemSupplierViewModel()) : View("../Error/AccessDenied");
        }

        [HttpPost]
        [StoreTempToModelState]
        public ActionResult Create(ItemSupplierViewModel model)
        {
            if (!User.Departments.Contains(17))
                return View("../Error/AccessDenied");
            if (ModelState.IsValid)
            {
                var num = _service.Save(model);
                if (num > 0)
                {
                    var result = new PagedListResult<ItemStoreViewModel>()
                    {
                        Success = true,
                        Message = $"<em>{model.ItemSupplierName}</em> supplier is saved successfully.."
                    };
                    TempData["Result"] = result;

                }

                return RedirectToAction("Create");
            }

            return View(model);
        }

        [CustomAuthorize(Roles = "Admin")]
        [RestoreTempToModelState]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Result = TempData["Result"];
            GetPermissionforUser();
            if (!User.Departments.Contains(17))
                return View("../Error/AccessDenied");
            var masDesignation = _service.Get(id);
            return View(new ItemSupplierViewModel()
            {
                ItemSupplierId = masDesignation.ItemSupplierId,
                ItemSupplierName = masDesignation.ItemSupplierName,
                ContactPersonEmail = masDesignation.ContactPersonEmail,
                ContactPersonName = masDesignation.ContactPersonName,
                ContactPersonPhone = masDesignation.ContactPersonPhone,
                Email = masDesignation.Email,
                Phone = masDesignation.Phone,
                Address = masDesignation.Address,
                IsActive = masDesignation.IsActive,
                CreatedDate = masDesignation.CreatedDate,
                Description = masDesignation.Description,
            });
        }

        [CustomAuthorize(Roles = "Admin")]
        [HttpPost]
        [StoreTempToModelState]
        public ActionResult Edit(ItemSupplierViewModel masDesignation)
        {
            if (!User.Departments.Contains(17))
                return View("../Error/AccessDenied");
            if (ModelState.IsValid)
            {
                _service.Update(new ItemSupplier()
                {
                    ItemSupplierId = masDesignation.ItemSupplierId,
                    ItemSupplierName = masDesignation.ItemSupplierName,
                    ContactPersonEmail = masDesignation.ContactPersonEmail,
                    ContactPersonName = masDesignation.ContactPersonName,
                    ContactPersonPhone = masDesignation.ContactPersonPhone,
                    Email = masDesignation.Email,
                    Phone = masDesignation.Phone,
                    Address = masDesignation.Address,
                    IsActive = masDesignation.IsActive,
                    CreatedDate = masDesignation.CreatedDate,
                    Description = masDesignation.Description,
                    ModifiedDate = DateTime.Now
                });

                var result = new PagedListResult<ItemStoreViewModel>()
                {
                    Success = true,
                    Message = $"<em>{masDesignation.ItemSupplierName}</em> supplier is updated successfully.."
                };
                TempData["Result"] = result;
                return RedirectToAction("Index");
            }

            return View(masDesignation);
        }
    }

    public class ItemController : BaseController
    {


        private readonly IItem _service;
        private readonly IItemCategory _categoryService;
        private readonly IItemStock _stockService;

        public ItemController(IUnitOfWork uow, IUserService userService, 
            IItemStock stockService,
            IItem itemService,
            IItemCategory categoryService)
            : base(uow, userService)
        {
            _service = itemService;
            _categoryService = categoryService;
            _stockService = stockService;
        }


        [CustomAuthorize(Roles = "Admin")]
        [RestoreTempToModelState]
        public ActionResult Index()
        {
            ViewBag.Result = TempData["Result"];
            GetPermissionforUser();
            if (!User.Departments.Contains(17))
                return View("../Error/AccessDenied");
            var list = _service.GetItemsList();
            foreach (var item in list)
            {
                var quantity = _stockService.GetItemQuantity(item.ItemId);
                item.Quantity = quantity.AvlQuantity;
            }
            return View(list);
        }

        [CustomAuthorize(Roles = "Admin")]
        [HttpGet]
        [RestoreTempToModelState]
        public ActionResult Create()
        {
            ViewBag.Result = TempData["Result"];
            ModelState.Clear();
            GetPermissionforUser();
            ViewBag.Categories = _categoryService.GetAll().OrderBy(x => x.CategoryName);
            return User.Departments.Contains(17) ? View(new ItemViewModel()) : View("../Error/AccessDenied");
        }

        [HttpPost]
        [StoreTempToModelState]
        public ActionResult Create(ItemViewModel model)
        {

            if (!User.Departments.Contains(17))
                return View("../Error/AccessDenied");
            if (ModelState.IsValid)
            {
                var num = _service.Save(model);
                if (num > 0)
                {
                    var result = new PagedListResult<ItemViewModel>()
                    {
                        Success = true,
                        Message = $"item <em>{model.Name}</em> is saved successfully.."
                    };
                    TempData["Result"] = result;

                }

                return RedirectToAction("Create");
            }
            ViewBag.Categories = _categoryService.GetAll().OrderBy(x => x.CategoryName);
            return View(model);
        }

        [CustomAuthorize(Roles = "Admin")]
        [RestoreTempToModelState]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Result = TempData["Result"];
            GetPermissionforUser();
            if (!User.Departments.Contains(17))
                return View("../Error/AccessDenied");

            ViewBag.Categories = _categoryService.GetAll().OrderBy(x => x.CategoryName);
            var masDesignation = _service.GetSingleItem(id);
            return View(masDesignation);
        }

        [CustomAuthorize(Roles = "Admin")]
        [HttpPost]
        [StoreTempToModelState]
        public ActionResult Edit(ItemViewModel masDesignation)
        {
            if (!User.Departments.Contains(17))
                return View("../Error/AccessDenied");
            if (ModelState.IsValid)
            {
                var isSaved = _service.Edit(masDesignation);
                if (!isSaved) return View(masDesignation);
                var result = new PagedListResult<ItemStoreViewModel>()
                {
                    Success = true,
                    Message = $"item <em>{masDesignation.Name}</em>  is updated successfully.."
                };
                TempData["Result"] = result;
                return RedirectToAction("Index");
            }
            ViewBag.Categories = _categoryService.GetAll().OrderBy(x => x.CategoryName);
            return View(masDesignation);
        }
    }

    public class ItemStockController : BaseController
    {

        private readonly IUnitOfWork _uow;
        private readonly IItemStock _service;
        private readonly IItemCategory _categoryService;
        private readonly IItem _itemService;
        private readonly IItemStore _itemStoreService;
        private readonly IItemSupplier _itemSupplierService;
        public ItemStockController(IUnitOfWork uow, IUserService userService,
            IItemStock itemStockService,
            IItemCategory categoryService,
            IItem itemService, IItemStore itemStoreService, IItemSupplier itemSupplierService)
            : base(uow, userService)
        {
            _uow = uow;
            _service = itemStockService;
            _categoryService = categoryService;
            _itemService = itemService;
            _itemStoreService = itemStoreService;
            _itemSupplierService = itemSupplierService;
        }


        [CustomAuthorize(Roles = "Admin")]
        [RestoreTempToModelState]
        public ActionResult Index()
        {
            ViewBag.Result = TempData["Result"];
            GetPermissionforUser();
            if (!User.Departments.Contains(17))
                return View("../Error/AccessDenied");
            var list = _service.GetItemsList();
            return View(list);
        }

        [CustomAuthorize(Roles = "Admin")]
        [HttpGet]
        [RestoreTempToModelState]
        public ActionResult Create()
        {
            ViewBag.Result = TempData["Result"];
            ModelState.Clear();
            GetPermissionforUser();
            ViewBag.Categories = _categoryService.GetAll().OrderBy(x => x.CategoryName);
            ViewBag.Stores = _itemStoreService.GetAll().OrderBy(A => A.StoreName);
            ViewBag.Suppliers = _itemSupplierService.GetAll().OrderBy(A => A.ItemSupplierName);
            return User.Departments.Contains(17) ? View(new ItemStockViewModel()) : View("../Error/AccessDenied");
        }

        [HttpPost]
        [StoreTempToModelState]
        public ActionResult Create(ItemStockViewModel model)
        {

            if (!User.Departments.Contains(17))
                return View("../Error/AccessDenied");
            if (ModelState.IsValid)
            {
                var num = _service.Save(model);
                if (num > 0)
                {
                    var result = new PagedListResult<ItemStockViewModel>()
                    {
                        Success = true,
                        Message = $"Record saved successfully.."
                    };
                    TempData["Result"] = result;

                }

                return RedirectToAction("Create");
            }
            ViewBag.Categories = _categoryService.GetAll().OrderBy(x => x.CategoryName);
            ViewBag.Stores = _itemStoreService.GetAll().OrderBy(A => A.StoreName);
            ViewBag.Suppliers = _itemSupplierService.GetAll().OrderBy(A => A.ItemSupplierName);

            return View(model);
        }

        [CustomAuthorize(Roles = "Admin")]
        [RestoreTempToModelState]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Result = TempData["Result"];
            GetPermissionforUser();
            if (!User.Departments.Contains(17))
                return View("../Error/AccessDenied");
            var masDesignation = _service.GetSingleItem(id);
            ViewBag.Categories = _categoryService.GetAll().OrderBy(x => x.CategoryName);
            ViewBag.Stores = _itemStoreService.GetAll().OrderBy(A => A.StoreName);
            ViewBag.Suppliers = _itemSupplierService.GetAll().OrderBy(A => A.ItemSupplierName);
            ViewBag.Items = _itemService.GetItemByCategory(masDesignation.Item.ItemCategoryId);

            return View(masDesignation);
        }

        [CustomAuthorize(Roles = "Admin")]
        [HttpPost]
        [StoreTempToModelState]
        public ActionResult Edit(ItemStockViewModel masDesignation)
        {
            if (!User.Departments.Contains(17))
                return View("../Error/AccessDenied");
            if (ModelState.IsValid)
            {
                var isSaved = _service.Edit(masDesignation);
                if (!isSaved) return View(masDesignation);
                var result = new PagedListResult<ItemStockViewModel>()
                {
                    Success = true,
                    Message = $"Record updated successfully.."
                };
                TempData["Result"] = result;
                return RedirectToAction("Index");
            }

            ViewBag.Categories = _categoryService.GetAll().OrderBy(x => x.CategoryName);
            ViewBag.Stores = _itemStoreService.GetAll().OrderBy(A => A.StoreName);
            ViewBag.Suppliers = _itemSupplierService.GetAll().OrderBy(A => A.ItemSupplierName);
            ViewBag.Items = _itemService.GetItemByCategory(masDesignation.Item.ItemCategoryId);
            return View(masDesignation);
        }

        [HttpPost]
        public JsonResult GetItems(int categoryId)
        {
            var items = _itemService.GetItemByCategory(categoryId);
            return Json(items, JsonRequestBehavior.AllowGet);
        }
    }

    public class ItemIssueController : BaseController
    {

        private readonly IUnitOfWork _uow;
        private readonly IItemIssue _service;
        private readonly IItemCategory _categoryService;
        private readonly IItem _itemService;
        private readonly IItemStock _stockService;
        private readonly IMASDepartmentService _departmentService;
        private readonly IMASDoctorService _doctorService;
        public ItemIssueController(IUnitOfWork uow,
            IUserService userService,
            IItemIssue itemIssueService,
            IItemCategory categoryService,
            IMASDepartmentService departmentService,
            IMASDoctorService doctorService,
            IItemStock stockService,
            IItem itemService)
            : base(uow, userService)
        {
            _uow = uow;
            _service = itemIssueService;
            _categoryService = categoryService;
            _itemService = itemService;
            _departmentService = departmentService;
            _doctorService = doctorService;
            _stockService = stockService;
        }


        [CustomAuthorize(Roles = "Admin")]
        [RestoreTempToModelState]
        public ActionResult Index()
        {
            ViewBag.Result = TempData["Result"];
            GetPermissionforUser();
            if (!User.Departments.Contains(17))
                return View("../Error/AccessDenied");
            var list = _service.GetItemsList();
            return View(list);
        }

        [CustomAuthorize(Roles = "Admin")]
        [HttpGet]
        [RestoreTempToModelState]
        public ActionResult Create()
        {
            ViewBag.Result = TempData["Result"];
            ModelState.Clear();
            GetPermissionforUser();
            ViewBag.Categories = _categoryService.GetAll().OrderBy(x => x.CategoryName);
            ViewBag.Departments = _departmentService.DepartmentList();
            return User.Departments.Contains(17) ? View(new ItemIssueViewModel()) : View("../Error/AccessDenied");
        }

        [HttpPost]
        [StoreTempToModelState]
        public ActionResult Create(ItemIssueViewModel model)
        {

            if (!User.Departments.Contains(17))
                return View("../Error/AccessDenied");
            if (ModelState.IsValid)
            {
                var num = _service.Save(model);
                if (num > 0)
                {
                    var result = new PagedListResult<ItemIssueViewModel>()
                    {
                        Success = true,
                        Message = $"Record saved successfully.."
                    };
                    TempData["Result"] = result;

                }

                return RedirectToAction("Create");
            }
            ViewBag.Categories = _categoryService.GetAll().OrderBy(x => x.CategoryName);
            ViewBag.Departments = _departmentService.DepartmentList();
            return View(model);
        }

        [CustomAuthorize(Roles = "Admin")]
        [RestoreTempToModelState]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Result = TempData["Result"];
            GetPermissionforUser();
            if (!User.Departments.Contains(17))
                return View("../Error/AccessDenied");
            var masDesignation = _service.GetSingleItem(id);
            ViewBag.Categories = _categoryService.GetAll().OrderBy(x => x.CategoryName);
            ViewBag.Items = _itemService.GetItemByCategory(masDesignation.Item.ItemCategoryId);
            ViewBag.Doctors = _doctorService.GetDoctorById(Int32.Parse(masDesignation.IssueType));
            return View(masDesignation);
        }

        [CustomAuthorize(Roles = "Admin")]
        [HttpPost]
        [StoreTempToModelState]
        public ActionResult Edit(ItemIssueViewModel masDesignation)
        {
            if (!User.Departments.Contains(17))
                return View("../Error/AccessDenied");
            if (ModelState.IsValid)
            {
                var isSaved = _service.Edit(masDesignation);
                if (!isSaved) return View(masDesignation);
                var result = new PagedListResult<ItemIssueViewModel>()
                {
                    Success = true,
                    Message = $"Record updated successfully.."
                };
                TempData["Result"] = result;
                return RedirectToAction("Index");
            }

            ViewBag.Categories = _categoryService.GetAll().OrderBy(x => x.CategoryName);
            ViewBag.Items = _itemService.GetItemByCategory(masDesignation.Item.ItemCategoryId);
            ViewBag.Doctors = _doctorService.GetDoctorById(Int32.Parse(masDesignation.IssueType));
            return View(masDesignation);
        }

        [HttpPost]
        public JsonResult GetDoctorsByDeparment(int id)
        {
            var items = _doctorService.GetDoctorById(id);
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ReturnItem(int itemIssueId)
        {
            var issueItem = _service.GetSingleItem(itemIssueId);
            if (issueItem != null)
            {
                issueItem.IsReturned = true;
                issueItem.ReturnDate = DateTime.Now;
                issueItem.ModifiedDate = DateTime.Now;

                _service.Edit(issueItem);
                return Json(new {Status = true}, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Status = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetQuantity(int itemId)
        {
            var items = _stockService.GetItemQuantity(itemId);
            return Json(items, JsonRequestBehavior.AllowGet);
        }
    }
}
