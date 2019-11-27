using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Common.ViewModels.CommentsViewModel
{
    public class CommentCreateViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public string ProductId { get; set; }
    }
}
