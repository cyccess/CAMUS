using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CAMUS.Models
{
    [Table("News")]
    public class News
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "标题")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "作者")]
        public string Author { get; set; }

        [Required]
        [Display(Name = "新闻内容")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "展示图片")]
        public string Image { get; set; }

        [Required]
        [Display(Name = "链接地址")]
        public string Link { get; set; }

        [Display(Name = "创建时间")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "是否删除")]
        public bool IsDelete { get; set; }
    }
}