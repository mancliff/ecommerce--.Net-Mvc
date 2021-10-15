using Ecommerce.DAL;
using Ecommerce.Models;
using Ecommerce.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();

        public List<SelectListItem> GetCategory()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = _unitOfWork.GetRepositoryInstance<Tbl_Catergory>().GetAllRecords();
            foreach (var item in cat)
            {
                list.Add(new SelectListItem { Value = item.CategoryId.ToString(), Text = item.Categoryname });
            }
            return list;
        }
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Catergories()
        {
            List<Tbl_Catergory> allcategories = _unitOfWork.GetRepositoryInstance<Tbl_Catergory>().GetAllRecordsIQueryable().Where(i => i.isDelete == false).ToList();
            return View(allcategories);
        }

        public ActionResult AddCatergory()
        {
            return UpdateCatergory(0);
        }
        public ActionResult UpdateCatergory(int categoryId)
        {
            Catergory cd;
            if (categoryId != null)
            {
                cd = JsonConvert.DeserializeObject<Catergory>(JsonConvert.SerializeObject(_unitOfWork.GetRepositoryInstance<Tbl_Catergory>().GetFirstorDefault(categoryId)));
            }
            else
            {
                cd = new Catergory();
            }
            return View("UpdateCatergory", cd);
        }

        public ActionResult Product()
        {
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Product>().GetProduct());
        }

        public ActionResult ProductEdit(int productId)
        {
            ViewBag.CategoryList = GetCategory();
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Product>().GetFirstorDefault(productId));
        }

        [HttpPost]
        public ActionResult ProductEdit(Tbl_Product tbl, HttpPostedFileBase file)
        {
            string pic = null;
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/Productimg/"), pic);
                file.SaveAs(path);
            }
            tbl.ProductImage = file != null ? pic : tbl.ProductImage;
            tbl.ModifiedDate = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<Tbl_Product>().Update(tbl);
            return RedirectToAction("Product");
        }

        public ActionResult ProductAdd()
        {
            ViewBag.CategoryList = GetCategory();
            return View();
        }

        [HttpPost]
        public ActionResult ProductAdd(Tbl_Product tbl, HttpPostedFileBase file)
        {
            string pic = null;
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/Productimg/"), pic);
                file.SaveAs(path);
            }
            tbl.ProductImage = pic;
            tbl.CreatedDate = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<Tbl_Product>().Add(tbl);
            return RedirectToAction("Product");
        }
    }
}