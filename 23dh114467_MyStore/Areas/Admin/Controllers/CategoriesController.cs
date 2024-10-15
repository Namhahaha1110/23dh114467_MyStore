﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _23dh114467_MyStore.Models;

namespace _23dh114467_MyStore.Areas.Admin.Controllers
{
    public class CategoriesController : Controller
    {
        private MyStoreEntities1 db = new MyStoreEntities1();

        // GET: Admin/Categories
        //GET: lấy dữ liệu từ bảng Category trong DB để hiển thị lên
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        // GET: Admin/Categories/Details/5
        //Details: lấy chi tiết 1 bản ghi có CategoryID = id
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); //mã lỗi 400: thiếu giá trị truyền vào 
            }
            Category category = db.Categories.Find(id);
            if (category == null) //không tìm thấy bản ghi nào
            {
                return HttpNotFound(); //mã lỗi 404
            }
            return View(category);
        }

        // GET: Admin/Categories/Create
        //Load form Create
        //[HttpGet] : phương thức mặc định nên không cần khai báo
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Categories/Create
        //POST: lưu dữ liệu nhập vào từ form Create vào DB 
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryID,CategoryName")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Admin/Categories/Edit/5
        //GET: lấy dữ liệu của một danh mục đã có sao cho CategoryID = id
        public ActionResult Edit(int? id) => Details(id);
        //if (id == null)
        //{
        //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //}
        //Category category = db.Categories.Find(id);
        //if (category == null)
        //{
        //    return HttpNotFound();
        //}
        //return View(category);


        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryID,CategoryName")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Admin/Categories/Delete/5
        public ActionResult Delete(int? id)=>Details(id);
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Category category = db.Categories.Find(id);
        //    if (category == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(category);
        //}

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
