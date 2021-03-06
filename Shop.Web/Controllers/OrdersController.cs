﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Common.ViewModels.CartViewModel;
using Shop.Data.UnitOfWork;
using Shop.Domain.Entities;
using ZarinpalSandbox;

namespace Shop.Web.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly UnitOfWork _db;

        public OrdersController(UnitOfWork db)
        {
            _db = db;
        }

        public IActionResult AddToCart(string id)
        {
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Order order = _db.OrdersGenericRepository.where(o => o.UserId == currentUserId && !o.IsFinally).SingleOrDefault();
            if (order == null)
            {
                order = new Order()
                {
                    UserId = currentUserId,
                    CreatedTime = DateTime.Now,
                    IsFinally = false,
                    Sum = 0
                };
                _db.OrdersGenericRepository.Insert(order);
                _db.OrderDetailsGenericRepository.Insert(new OrderDetail
                {
                    Count = 1,
                    ProductId = id,
                    IsDeleted = false,
                    Price = _db.ProductsGenericRepository.GetById(id).Price,
                    OrderId = order.Id,
                });
                _db.Save();
            }
            else
            {
                var details = _db.OrderDetailsGenericRepository.where(o => o.OrderId == order.Id && o.ProductId == id)
                    .SingleOrDefault();
                if (details == null)
                {
                    _db.OrderDetailsGenericRepository.Insert(new OrderDetail
                    {
                        Count = 1,
                        ProductId = id,
                        IsDeleted = false,
                        Price = _db.ProductsGenericRepository.GetById(id).Price,
                        OrderId = order.Id,
                    });
                }
                else
                {
                    var product = _db.ProductsGenericRepository.GetById(id);
                    if (product.Quantity > details.Count)
                    {
                        details.Count += 1;
                        _db.OrderDetailsGenericRepository.Update(details);
                    }
                }
                _db.Save();
            }
            UpdateSumOrder(order.Id);
            return Redirect("/Home/Details?id=" + id);
        }

        public IActionResult ShowOrder()
        {
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Order order = _db.OrdersGenericRepository.where(o => o.UserId == currentUserId && !o.IsFinally).SingleOrDefault();
            var list = new List<ShowOrderViewModel>();
            if (order != null)
            {
                var details = _db.OrderDetailsGenericRepository.where(o => o.OrderId == order.Id);
                foreach (var item in details)
                {
                    var product = _db.ProductsGenericRepository.GetById(item.ProductId);
                    list.Add(new ShowOrderViewModel
                    {
                        ImageName = _db.ProductImagesGenericRepository.where(i => i.ProductId == item.ProductId).FirstOrDefault().ImagePath,
                        Title = product.Title,
                        Count = item.Count,
                        OrderDetailId = item.Id,
                        Price = item.Price,
                        Sum = item.Count * item.Price
                    });
                }
            }

            return View(list);

        }

        public IActionResult Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var orderDetails = _db.OrderDetailsGenericRepository.GetById(id);
                _db.OrderDetailsGenericRepository.Delete(orderDetails);
                _db.Save();
            }
            return RedirectToAction(nameof(ShowOrder));
        }

        public IActionResult Command(string id, string command)
        {
            if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(command))
            {
                var orderDetails = _db.OrderDetailsGenericRepository.GetById(id);
                switch (command)
                {
                    case "up":
                        {
                            if (_db.ProductsGenericRepository.GetById(orderDetails.ProductId).Quantity > orderDetails.Count)
                            {
                                orderDetails.Count += 1;

                                _db.OrderDetailsGenericRepository.Update(orderDetails);
                            }
                            break;
                        }
                    case "down":
                        {
                            orderDetails.Count -= 1;
                            if (orderDetails.Count == 0)
                                _db.OrderDetailsGenericRepository.Delete(orderDetails);
                            else
                                _db.OrderDetailsGenericRepository.Update(orderDetails);
                            break;
                        }
                }
                _db.Save();
            }
            return RedirectToAction(nameof(ShowOrder));
        }

        public void UpdateSumOrder(string orderId)
        {
            var order = _db.OrdersGenericRepository.GetById(orderId);
            order.Sum = _db.OrderDetailsGenericRepository.where(o => o.OrderId == order.Id)
                .Select(d => d.Count * d.Price).Sum();
            _db.OrdersGenericRepository.Update(order);
            _db.Save();
        }

        public IActionResult Payment()
        {
            var order = _db.OrdersGenericRepository.where(o => !o.IsFinally).SingleOrDefault();
            if (order == null)
            {
                return NotFound();
            }

            var payment = new Payment(decimal.ToInt32(order.Sum));
            var currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userPhone = _db.UsersGenericRepository.GetById(currentUser).PhoneNumber;
            var res = payment.PaymentRequest($"پرداخت فاکتور شماره {order.Id}",
                "https://localhost:44399/Orders/OnlinePayment/" + order.Id,null,userPhone);
            if (res.Result.Status == 100)
            {
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority);
            }
            else
            {
                return BadRequest("پرداخت امکان پذیر نیست بعدا تلاش فرمایید");
            }
            return null;
        }

        public IActionResult OnlinePayment(string id)
        {
            if (HttpContext.Request.Query["Status"] != ""
                && HttpContext.Request.Query["Status"].ToString().ToLower() == "ok"
                && HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"].ToString();
                var order = _db.OrdersGenericRepository.GetById(id);
                var payment = new Payment(decimal.ToInt32(order.Sum));
                var res = payment.Verification(authority).Result;
                if (res.Status == 100)
                {
                    order.IsFinally = true;
                    _db.OrdersGenericRepository.Update(order);
                    var details = _db.OrderDetailsGenericRepository.where( o => o.OrderId == order.Id).ToList();
                    foreach (var item in details)
                    {
                        var product = _db.ProductsGenericRepository.GetById(item.ProductId);
                        product.Quantity -= item.Count;
                    }
                    _db.Save();
                    ViewBag.Code = res.RefId;
                    return View();
                }
            }

            return NotFound();
        }
    }
}