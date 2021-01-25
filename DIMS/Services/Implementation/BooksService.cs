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
    public class BooksService : ServiceBase<Books>, IBooks, IService<Books>
    {
        private readonly IUnitOfWork _uow;
        public BooksService(IUnitOfWork uow) : base(uow)
        {
            _uow = uow;
        }

        public int Save(BooksViewModel param)
        {
            var model = new Item();
            var entity =
                new MapperConfiguration(cfg =>
                        cfg.CreateMap<BooksViewModel, Books>()).CreateMapper()
                    .Map<BooksViewModel, Books>(param);
            entity.CreatedDate = DateTime.Now;
            var num = _uow.Repository<Books>().Add(entity, false);
            return num;
        }

        public IEnumerable<BooksViewModel> GetItemsList()
        {
            var orderedEnumerable = _uow.Repository<Books>().GetAll().OrderBy(x=>x.Title);
            var designationViewModalList = new List<BooksViewModel>();
            foreach (var masDesignation in orderedEnumerable)
            {
                designationViewModalList.Add(new BooksViewModel
                {
                    Id = masDesignation.Id,
                    Title = masDesignation.Title,
                    BookNo = masDesignation.BookNo,
                    IsbnNo = masDesignation.IsbnNo,
                    Subject = masDesignation.Subject,
                    RankNo = masDesignation.RankNo,
                    Publish = masDesignation.Publish,
                    Author = masDesignation.Author,
                    Quantity = masDesignation.Quantity,
                    UnitCost = masDesignation.UnitCost,
                    Description = masDesignation.Description,
                    Availeble = masDesignation.Availeble,
                    IsActive = masDesignation.IsActive,
                    CreatedDate = masDesignation.CreatedDate,
                    ModifiedDate = masDesignation.ModifiedDate
                });
            }

            return designationViewModalList.ToList();
        }

        public BooksViewModel GetSingleItem(int id)
        {
            var masDesignation = _uow.Repository<Books>().Get(id);
            if (masDesignation == null) return null;
            return new BooksViewModel
            {
                Id = masDesignation.Id,
                Title = masDesignation.Title,
                BookNo = masDesignation.BookNo,
                IsbnNo = masDesignation.IsbnNo,
                Subject = masDesignation.Subject,
                RankNo = masDesignation.RankNo,
                Publish = masDesignation.Publish,
                Author = masDesignation.Author,
                Quantity = masDesignation.Quantity,
                UnitCost = masDesignation.UnitCost,
                Description = masDesignation.Description,
                Availeble = masDesignation.Availeble,
                IsActive = masDesignation.IsActive,
                CreatedDate = masDesignation.CreatedDate,
                ModifiedDate = masDesignation.ModifiedDate
            };
        }

        public bool Edit(BooksViewModel masDesignation)
        {
            var param = _uow.Repository<Books>().Get(masDesignation.Id);
            if (param == null) return false;
            _uow.Repository<Books>().Update(new Books()
            {
                Id = masDesignation.Id,
                Title = masDesignation.Title,
                BookNo = masDesignation.BookNo,
                IsbnNo = masDesignation.IsbnNo,
                Subject = masDesignation.Subject,
                RankNo = masDesignation.RankNo,
                Publish = masDesignation.Publish,
                Author = masDesignation.Author,
                Quantity = masDesignation.Quantity,
                UnitCost = masDesignation.UnitCost,
                Description = masDesignation.Description,
                Availeble = masDesignation.Availeble,
                IsActive = masDesignation.IsActive,
                CreatedDate = masDesignation.CreatedDate,
                ModifiedDate = DateTime.Now
            });
            return true;
        }
    }

    public class BooksIssueService : ServiceBase<BookIssues>, IBookIssues, IService<BookIssues>
    {
        private readonly IUnitOfWork _uow;
        private readonly IBooks _bookService;
        private readonly IMASDoctorService _doctorService;
        public BooksIssueService(IUnitOfWork uow) : base(uow)
        {
            _uow = uow;
            _bookService = new BooksService(uow);
            _doctorService = new MasDoctorService(uow);
        }

        public int Save(BookIssuesViewModel param)
        {
            var entity =
                new MapperConfiguration(cfg =>
                        cfg.CreateMap<BookIssuesViewModel, BookIssues>()).CreateMapper()
                    .Map<BookIssuesViewModel, BookIssues>(param);
            entity.CreatedDate = DateTime.Now;
            var num = _uow.Repository<BookIssues>().Add(entity, false);
            return num;
        }

        public IEnumerable<BookIssuesViewModel> GetItemsList()
        {
            var orderedEnumerable = _uow.Repository<BookIssues>().GetAll().OrderByDescending(x => x.CreatedDate);
            var designationViewModalList = new List<BookIssuesViewModel>();
            foreach (var masDesignation in orderedEnumerable)
            {
                var book = _bookService.GetSingleItem((int)masDesignation.BookId);
                var doctor = _doctorService.GetSingleDoctor((int)masDesignation.MemberId);
                designationViewModalList.Add(new BookIssuesViewModel
                {
                    Id = masDesignation.Id,
                    BookId = masDesignation.BookId,
                    ReturnDate = masDesignation.ReturnDate,
                    IssuedDate = masDesignation.IssuedDate,
                    IsReturned = masDesignation.IsReturned,
                    MemberId = masDesignation.MemberId,
                    IsActive = masDesignation.IsActive,
                    CreatedDate = masDesignation.CreatedDate,
                    IssueBy = masDesignation.IssueBy,
                    ModifiedDate = masDesignation.ModifiedDate,
                    Note = masDesignation.Note,
                    Quantity = masDesignation.Quantity,
                    Book = book,
                    Doctor = doctor
                });
            }

            return designationViewModalList.ToList();
        }

        public BookIssuesViewModel GetSingleItem(int id)
        {
            var masDesignation = _uow.Repository<BookIssues>().Get(id);
            if (masDesignation == null) return null;
            return new BookIssuesViewModel
            {
                Id = masDesignation.Id,
                BookId = masDesignation.BookId,
                ReturnDate = masDesignation.ReturnDate,
                IssuedDate = masDesignation.IssuedDate,
                Quantity = masDesignation.Quantity,
                IsReturned = masDesignation.IsReturned,
                MemberId = masDesignation.MemberId,
                Note = masDesignation.Note,
                IsActive = masDesignation.IsActive,
                CreatedDate = masDesignation.CreatedDate,
                ModifiedDate = masDesignation.ModifiedDate
            };
        }

        public bool Edit(BookIssuesViewModel masDesignation)
        {
            var param = _uow.Repository<BookIssues>().Get(masDesignation.Id);
            if (param == null) return false;
            _uow.Repository<BookIssues>().Update(new BookIssues()
            {
                Id = masDesignation.Id,
                BookId = masDesignation.BookId,
                ReturnDate = masDesignation.ReturnDate,
                IsReturned = masDesignation.IsReturned,
                MemberId = masDesignation.MemberId,
                IsActive = masDesignation.IsActive,
                Quantity = masDesignation.Quantity,
                Note = masDesignation.Note,
                CreatedDate = masDesignation.CreatedDate,
                ModifiedDate = masDesignation.ModifiedDate
            });
            return true;
        }

        public DtoStock GetItemQuantity(int bookId)
        {
            throw new NotImplementedException();
        }
    }
}
