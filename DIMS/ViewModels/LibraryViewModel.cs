using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Base;
using Repository.Core;

namespace DIMS.ViewModels
{
    [Table("Books")]
    public class BooksViewModel : EntityBase
    {
        public BooksViewModel()
        {
            CreatedDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
            IsActive = true;
            Quantity = 0;
        }

        [PrimaryKey]
        public int Id { get; set; }
        public string Title { get; set; }
        public string BookNo { get; set; }
        public string IsbnNo { get; set; }
        public string Subject { get; set; }
        public string RankNo { get; set; }
        public string Publish { get; set; }
        public string Author { get; set; }
        public int? Quantity { get; set; }
        public double? UnitCost { get; set; }
        public string Description { get; set; }
        public string Availeble { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

    [Table("BookIssues")]
    public class BookIssuesViewModel : EntityBase
    {

        public BookIssuesViewModel()
        {
            CreatedDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
            IsActive = true;
            Quantity = 0;
        }

        [PrimaryKey] public int Id { get; set; }
        public int? BookId { get; set; }
        public DateTime? ReturnDate { get; set; }
        public DateTime? IssuedDate { get; set; }
        public bool? IsReturned { get; set; }
        public int? MemberId { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string IssueBy { get; set; }
        public string Note { get; set; }

        public int Quantity { get; set; }

        public BooksViewModel Book { get; set; }
        public DoctorViewModal Doctor { get; set; }
    }
}
