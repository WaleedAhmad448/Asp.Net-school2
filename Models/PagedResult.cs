using System;
using System.Collections.Generic;

namespace AspNet_school2.Models
{
    public class PagedResult<T>
    {
        public List<T> Data { get; set; } = new List<T>(); // قائمة البيانات من النوع T
        public int Page { get; set; } // رقم الصفحة الحالية
        public int PageSize { get; set; } // عدد العناصر في الصفحة الواحدة
        public int TotalRecords { get; set; } // العدد الإجمالي للعناصر في جميع الصفحات
        public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize); // العدد الإجمالي للصفحات
    }
}