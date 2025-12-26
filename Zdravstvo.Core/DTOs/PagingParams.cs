using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zdravstvo.Core.DTOs
{
    public class PagingParams
    {
        private int _page = 1;
        private int _pageSize = 10;
        public int Page { get => _page; set => _page = (value < 1) ? 1 : value; }
        public int PageSize { get => _pageSize; set => _pageSize = (value < 1) ? 10 : (value > MaxPageSize ? MaxPageSize : value); }
        public int MaxPageSize { get; set; } = 100;
    }
}
