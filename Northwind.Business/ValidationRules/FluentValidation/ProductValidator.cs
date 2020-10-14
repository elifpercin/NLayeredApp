using FluentValidation;
using Northwind.Entites.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Business.ValidationRules.FluentValidation
{
    public class ProductValidator:AbstractValidator <Product>
    {
        // fluent valitaion // 
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).NotEmpty().WithMessage("ürün ismi boş olamaz");
            RuleFor(p => p.CategoryId).NotEmpty();
            RuleFor(p => p.UnitPrice).NotEmpty();
            RuleFor(p => p.QuantityPerUnit).NotEmpty();
            RuleFor(p => p.UnitsInStock).NotEmpty();

            RuleFor(p => p.UnitPrice).GreaterThan(0);//odan büyük olmalı
            RuleFor(p => p.UnitsInStock).GreaterThanOrEqualTo((short)0);//stockta hiç olmayabilir
            RuleFor(p => p.UnitPrice).GreaterThan(10).When (p=> p.CategoryId==2);//categoryid=2 olan ürünün fiyatı 10 dan büyük olmalı

            RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("Ürün adı A ile başlamalı");//kendi oluşturduğumuz rules 
        }

        private bool StartWithA(string arg)
        {
            return arg.StartsWith("A");
        }
    }
}
