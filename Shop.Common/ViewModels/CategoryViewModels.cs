using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Common.ViewModels
{
    public class CategoryListViewModels
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Parent { get; set; }
        public int ParentId { get; set; }
    }
}
