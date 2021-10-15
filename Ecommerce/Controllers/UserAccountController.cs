using Ecommerce.DAL;
using Ecommerce.Models;
using Ecommerce.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class UserAccountController : Controller
    {
        dbEcommerceEntities objDBEntity = new dbEcommerceEntities();
        // GET: UserAccount
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            User objM = new User();
            return View(objM);
        }

        [HttpPost]
        public ActionResult Register(User objU, HttpPostedFileBase file)
        {
            bool status = false;
            string message = "";
            string pic = null;
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/Profile Images/"), pic);
                file.SaveAs(path);
            }
            objU.UserImage = pic;
            if (ModelState.IsValid)
            {
                if (!objDBEntity.Tbl_Users.Any(m => m.EmailId == objU.Email))
                {
                    Tbl_Users objTbl = new DAL.Tbl_Users();


                    //objTbl.UserImage = objM.UserImage;
                    objTbl.CreatedOn = DateTime.Now;
                    objTbl.RoleId = 2;
                    objTbl.EmailId = objU.Email;
                    objTbl.FirstName = objU.FirstName;
                    objTbl.LastName = objU.LastName;
                    objTbl.Password = Crypto.Hash(objU.Password);


                    _unitOfWork.GetRepositoryInstance<Tbl_Users>().Add(objTbl);
                    message = "Registration successfully done.";
                    status = true;
                    //objM.SuccessMessage = "User is Succesfully Added";
                    //return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Error", "Email is Already exists!");
                    //return View();
                }
            }
            ViewBag.Message = message;
            ViewBag.Status = status;
            return View();
        }

        public ActionResult Login()
        {
            LoginModel objLogin = new LoginModel();
            return View(objLogin);
        }

        [HttpPost]
        public ActionResult Login(LoginModel objLogin)
        {

            if (ModelState.IsValid)
            {
                string encryPassword = Crypto.Hash(objLogin.Password);
                Tbl_Users obj = new DAL.Tbl_Users();
                if (objDBEntity.Tbl_Users.Where(m => m.EmailId == objLogin.Email && m.Password == encryPassword).FirstOrDefault() == null)
                {
                    ModelState.AddModelError("Error", "Invalid Email and Password");
                    return View();
                }
                else
                {
                    Session["Email"] = objLogin.Email;
                    Session["FirstName"] = objDBEntity.Tbl_Users.Single(m => m.EmailId == objLogin.Email && m.Password == encryPassword).FirstName;
                    Session["UserId"] = objDBEntity.Tbl_Users.Single(m => m.EmailId == objLogin.Email && m.Password == encryPassword).UserId;
                    Session["LastName"] = objDBEntity.Tbl_Users.Single(m => m.EmailId == objLogin.Email && m.Password == encryPassword).LastName;

                    return RedirectToAction("Index", "Home");

                }


            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}