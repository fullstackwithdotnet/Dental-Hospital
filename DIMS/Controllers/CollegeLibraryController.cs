using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DIMS.Helpers;
using DIMS.Infrastructure;
using DIMS.Services.Abstract;
using DIMS.ViewModels;
using Repository.Base;

namespace DIMS.Controllers
{
    public class BooksController : BaseController
    {
        private IUnitOfWork _uow;
        private readonly IBooks _bookService;
        public BooksController(IUnitOfWork uow, IBooks service, IUserService userservice)
            : base(uow, userservice)
        {
            _uow = uow;
            _bookService = service;
        }

        [CustomAuthorize(Roles = "Admin")]
        [RestoreTempToModelState]
        public ActionResult Index()
        {
            GetPermissionforUser();
            if (!User.Departments.Contains(17))
                return View("../Error/AccessDenied");
            var orderedEnumerable = _bookService.GetItemsList();
            return View(orderedEnumerable);
        }

        [CustomAuthorize(Roles = "Admin")]
        [HttpGet]
        [RestoreTempToModelState]
        public ActionResult Create()
        {
            ViewBag.Result = TempData["Result"];
            ModelState.Clear();
            GetPermissionforUser();
            return User.Departments.Contains(17) ? View(new BooksViewModel()) : View("../Error/AccessDenied");
        }

        [HttpPost]
        [StoreTempToModelState]
        public ActionResult Create(BooksViewModel model)
        {

            if (!User.Departments.Contains(17))
                return View("../Error/AccessDenied");
            if (ModelState.IsValid)
            {
                var num = _bookService.Save(model);
                if (num > 0)
                {
                    var result = new PagedListResult<BooksViewModel>()
                    {
                        Success = true,
                        Message = $"Book <em>{model.Title}</em> is saved successfully.."
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
            var masDesignation = _bookService.GetSingleItem(id);
            return View(masDesignation);
        }

        [CustomAuthorize(Roles = "Admin")]
        [HttpPost]
        [StoreTempToModelState]
        public ActionResult Edit(BooksViewModel masDesignation)
        {
            if (!User.Departments.Contains(17))
                return View("../Error/AccessDenied");
            if (ModelState.IsValid)
            {
                var isSaved = _bookService.Edit(masDesignation);
                if (!isSaved) return View(masDesignation);
                var result = new PagedListResult<BooksViewModel>()
                {
                    Success = true,
                    Message = $"Book <em>{masDesignation.Title}</em>  is updated successfully.."
                };
                TempData["Result"] = result;
                return RedirectToAction("Index");
            }

            return View(masDesignation);
        }

    }

    public class BookIssueController : BaseController
    {
        private readonly IBookIssues _bookIssueService;
        private readonly IMASDepartmentService _departmentService;
        private readonly IMASDoctorService _doctorService;
        private readonly IBooks _bookService;
        public BookIssueController(IUnitOfWork uow, IBookIssues service,
            IMASDepartmentService departmentService,
            IMASDoctorService doctorService,
            IBooks bookService,
            IUserService userservice)
            : base(uow, userservice)
        {
            _bookIssueService = service;
            _departmentService = departmentService;
            _doctorService = doctorService;
            _bookService = bookService;
        }

        [CustomAuthorize(Roles = "Admin")]
        [RestoreTempToModelState]
        public ActionResult Index()
        {
            GetPermissionforUser();
            if (!User.Departments.Contains(17))
                return View("../Error/AccessDenied");
            var orderedEnumerable = _bookIssueService.GetItemsList();
            return View(orderedEnumerable);
        }

        [CustomAuthorize(Roles = "Admin")]
        [HttpGet]
        [RestoreTempToModelState]
        public ActionResult Create()
        {
            ViewBag.Result = TempData["Result"];
            ModelState.Clear();
            GetPermissionforUser();
            ViewBag.Departments = _departmentService.DepartmentList();
            ViewBag.Books = _bookService.GetItemsList();
            return User.Departments.Contains(17) ? View(new BookIssuesViewModel()) : View("../Error/AccessDenied");
        }

        [HttpPost]
        [StoreTempToModelState]
        public ActionResult Create(BookIssuesViewModel model)
        {

            if (!User.Departments.Contains(17))
                return View("../Error/AccessDenied");
            if (ModelState.IsValid)
            {
                var num = _bookIssueService.Save(model);
                if (num > 0)
                {
                    var result = new PagedListResult<BookIssuesViewModel>()
                    {
                        Success = true,
                        Message = $"Book Is Issued successfully.."
                    };
                    TempData["Result"] = result;

                }

                return RedirectToAction("Create");
            }
            ViewBag.Departments = _departmentService.DepartmentList();
            return View(model);
        }
        

    }
}
