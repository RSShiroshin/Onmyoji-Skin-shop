using System;
using System.Collections.Generic;

namespace PRN_Project2.Models
{
    public partial class CommentHe160324
    {
        public int CommentId { get; set; }
        public string? ProductId { get; set; }
        public string? UserName { get; set; }
        public string? Comment { get; set; }
        public DateTime? CommentDate { get; set; }

        public CommentHe160324()
        {
        }

        public CommentHe160324(int commentId, string? productId, string? userName, string? comment, DateTime? commentDate)
        {
            CommentId = commentId;
            ProductId = productId;
            UserName = userName;
            Comment = comment;
            CommentDate = commentDate;
        }

        public CommentHe160324(string? productId, string? userName, string? comment, DateTime? commentDate)
        {
            ProductId = productId;
            UserName = userName;
            Comment = comment;
            CommentDate = commentDate;
        }

        public virtual ProductHe160324? Product { get; set; }
        public virtual UserHe160324? UserNameNavigation { get; set; }
    }
}
