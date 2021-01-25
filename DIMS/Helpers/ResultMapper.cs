using System.Collections.Generic;

namespace DIMS.Helpers
{
    //-----------------------------------------------------------------------
    /// <summary>
    /// Used as a return value for methods executing queries.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class PagedListResult<TEntity>
    {

        public PagedListResult()
        {
            HasNext = false;
            HasPrevious = false;
            Count = 0;
            Entities = new List<TEntity>();
            PageSize = 0;
            PageIndex = 0;
            Message = null;
            Success = false;
        }

        //-----------------------------------------------------------------------
        /// <summary>
        /// Does the returned result contains more rows to be retrieved?
        /// </summary>
        public bool HasNext { get; set; }

        //-----------------------------------------------------------------------
        /// <summary>
        /// Does the returned result is successfull?
        /// </summary>
        public bool Success { get; set; }

        //-----------------------------------------------------------------------
        /// <summary>
        /// Message for returned result or could be html as well.
        /// </summary>
        public string Message { get; set; }

        //-----------------------------------------------------------------------
        /// <summary>
        /// Does the returned result contains previous items ?
        /// </summary>
        public bool HasPrevious { get; set; }

        //-----------------------------------------------------------------------
        /// <summary>
        /// Total number of rows that could be possibly be retrieved.
        /// </summary>
        public int Count { get; set; }

        //-----------------------------------------------------------------------
        /// <summary>
        /// Result of the query.
        /// </summary>
        public IEnumerable<TEntity> Entities { get; set; }


        //-----------------------------------------------------------------------
        /// <summary>
        /// page size of the query.
        /// </summary>

        public int? PageSize { get; set; }

        //-----------------------------------------------------------------------
        /// <summary>
        /// page index of the query.
        /// </summary>

        public int PageIndex { get; set; }
    }
}