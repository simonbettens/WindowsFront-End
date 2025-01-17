﻿using System.Collections.Generic;
using System.Linq;
using WindowsFront_end.Models;

namespace WindowsFront_end.Models.DTO_s
{
    public static class CategoryDTO
    {
        public class Create
        {
            public string Name { get; set; }
        }

        public class Overview
        {
            public int CategoryId { get; set; }
            public string Name { get; set; }
            public List<int> Items { get; set; } = new List<int>();

        }

        public class Basic
        {
            public int CategoryId { get; set; }
            public string Name { get; set; }

            public Basic()
            {

            }

            public Basic(Category category)
            {
                CategoryId = category.CategoryId;
                Name = category.Name;
            }

        }




    }
}