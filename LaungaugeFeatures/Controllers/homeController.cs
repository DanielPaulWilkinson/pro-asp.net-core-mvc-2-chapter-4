﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaungaugeFeatures.Models;
using Microsoft.AspNetCore.Mvc;

namespace LaungaugeFeatures.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {

            //1) Adding values when values are missing.
            List<String> res = new List<string>();
            foreach (ProductVM p in ProductVM.getProduct())
            {
                string name = p?.name ?? "<No Name>";
                decimal? price = p?.price ?? 0;
                string Related = p?.Related?.name ?? "<None>";
                res.Add($"The product is a {name} with a price of {price}. related: {Related}");
                TempData["NULLABLE "] = res;  
            }
            //2) Indexing with syntax sugar
            Dictionary<string, ProductVM> products = new Dictionary<string, ProductVM>
            {
                ["kayak"] = new ProductVM { name = "kayak", price = 213 },
                ["bike"] = new ProductVM { name = "bike", price = 100 },
                ["car"] = new ProductVM { name = "car", price = 200 },
                ["boat"] = new ProductVM { name = "boat", price = 345 }
            };

            TempData["INDEX"] = products;

            //3) Pattern matching basic
            object[] data = new object[] { 125m,29.95m,"apple","orange",100,10 };
            decimal total = 0;
            for(int i = 0; i < data.Length; i++)
            {
                if(data[i] is decimal d)
                {
                    total += d;
                }
            }

            TempData["TOTAL"] = new string[] { $"Total: {total:C2}" };



            //4) Pattern matching in switch statments
            total = 0;
            for (int i = 0; i < data.Length; i++)
            {
                switch (data[i])
                {
                    case decimal DecValue:
                        total += DecValue;
                        break;
                    case int intValue when intValue > 50:
                        total += intValue;
                        break;
                }

            }

            TempData["TOTAL-SWITCH"] = new string[] { $"Total: {total:C2}" };


            //Page: 86
            //Desc: Added total prices to the solution
            //Who: DW 28/12/17
            ShoppingCartVM cart = new ShoppingCartVM {  Products = ProductVM.getProduct() };

            ProductVM[] ProductArray =
            {
                new ProductVM{name="Kayak",price=100},
                new ProductVM{name="Bike",price=20},
                new ProductVM{name="Car",price=789},
                new ProductVM{name="Boat",price=82178},
            }; 
            decimal carttotal = cart.TotalPrices();
            decimal arraytotal = ProductArray.TotalPrices();
            TempData["TotalPrices"] = new String[] { $" Cart Total: {carttotal:C2}", $"Array Total: {arraytotal:C2}" };




            return View();
        }
    }
}