using System;
using System.ComponentModel.DataAnnotations.Schema;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
    [Table("Books")]
    public class Books : EntityBase
    {
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
    public class BookIssues : EntityBase
    {
        [PrimaryKey]
        public int Id { get; set; }
        public int? BookId { get; set; }
        public int Quantity { get; set; }
        public DateTime? ReturnDate { get; set; }
        public DateTime? IssuedDate { get; set; }
        public bool? IsReturned { get; set; }
        public int? MemberId { get; set; }
        public bool IsActive { get; set; }
        public string IssueBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Note { get; set; }
    }


}
