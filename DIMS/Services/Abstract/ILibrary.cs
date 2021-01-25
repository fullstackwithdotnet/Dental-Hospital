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
    public interface IBooks : IService<Books>
    {
        int Save(BooksViewModel model);
        IEnumerable<BooksViewModel> GetItemsList();
        BooksViewModel GetSingleItem(int id);
        bool Edit(BooksViewModel masDesignation);
    }

    public interface IBookIssues : IService<BookIssues>
    {
        int Save(BookIssuesViewModel model);
        IEnumerable<BookIssuesViewModel> GetItemsList();
        BookIssuesViewModel GetSingleItem(int id);
        bool Edit(BookIssuesViewModel masDesignation);

        DtoStock GetItemQuantity(int bookId);
    }
}
