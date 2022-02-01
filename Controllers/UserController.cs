using PagedList;
using shoppingWebsite.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shoppingWebsite.Controllers
{
    public class UserController : Controller
    {
        shopping_dataBaseEntities db = new shopping_dataBaseEntities();
        // GET: User
        public ActionResult Index(int? page)
        {
            int pagesize = 9, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = db.table_category.Where(x => x.cat_statu == "1").OrderByDescending(x => x.cat_id).ToList();
            IPagedList<table_category> stu = list.ToPagedList(pageindex, pagesize);


            return View(stu);
        }
        public ActionResult login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult login(table_user uvm)
        {
            table_user u = db.table_user.Where(x => x.u_email == uvm.u_email && x.u_password == uvm.u_password).SingleOrDefault();
            if (u != null)
            {
                Session["u_id"] = u.u_id.ToString();
                return RedirectToAction("CreateAd");
            }
            else
            {
                ViewBag.error = "Invalid Email or password";
            }
            return View();
        }

        public ActionResult Create()
        {
            if (Session["ad_id"] == null)
            {
                return RedirectToAction("login");
            }
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(table_user uvm, HttpPostedFileBase imgfile)
        {
            string path = uploadimgfile(imgfile);
            if (path.Equals("-1"))
            {
                ViewBag.error = "Image could not be uploaded";
            }
            else
            {
                table_user u = new table_user();
                u.u_name = uvm.u_name;
                u.u_email = uvm.u_email;
                u.u_password = uvm.u_password;
                u.u_image = path;
                u.u_contact = uvm.u_contact;
                db.table_user.Add(u);
                db.SaveChanges();

                return RedirectToAction("login");


            }

            return View();
        }

        public string uploadimgfile(HttpPostedFileBase file)
        {
            Random r = new Random();
            string path = "-1";
            int random = r.Next();
            if (file != null && file.ContentLength > 0)
            {

                string extension = Path.GetExtension(file.FileName);
                if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png"))
                {
                    try
                    {

                        path = Path.Combine(Server.MapPath("~/Content/upload"), random + Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        path = "~/Content/upload/" + random + Path.GetFileName(file.FileName);

                        //    ViewBag.Message = "File uploaded successfully";
                    }
                    catch (Exception ex)
                    {
                        path = "-1";
                    }
                }
                else
                {
                    Response.Write("<script>alert('Only jpg ,jpeg or png formats are acceptable....'); </script>");
                }
            }

            else
            {
                Response.Write("<script>alert('Please select a file'); </script>");
                path = "-1";
            }



            return path;
        }

        [HttpGet]
        public ActionResult CreateAd()
        {
            List<table_category> li = db.table_category.ToList();
            ViewBag.categorylist = new SelectList(li, "cat_id", "cat_name");
            return View();
        }

        [HttpPost]
        public ActionResult CreateAd(table_product pvm, HttpPostedFileBase imgfile)
        {
            List<table_category> li = db.table_category.ToList();
            ViewBag.categorylist = new SelectList(li, "cat_id", "cat_name");


            string path = uploadimgfile(imgfile);
            if (path.Equals("-1"))
            {
                ViewBag.error = "Image could not be uploaded";
            }
            else
            {
                table_product p = new table_product();
                p.pro_name = pvm.pro_name;
                p.pro_price = pvm.pro_price;
                p.pro_image = path;
                p.pro_fk_cat = pvm.pro_fk_cat;
                p.pro_description = pvm.pro_description;
                p.pro_fk_user = Convert.ToInt32(Session["u_id"].ToString());
                db.table_product.Add(p);
                db.SaveChanges();
                Response.Redirect("index");

            }

            return View();
        }

        [HttpPost]
        public ActionResult Ads(int? id, int? page,string search)
        {
            int pagesize = 9, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = db.table_product.Where(x => x.pro_name.Contains(search)).OrderByDescending(x => x.pro_id).ToList();
            IPagedList<table_product> stu = list.ToPagedList(pageindex, pagesize);


            return View(stu);

        }

        
        public ActionResult Ads(int? id, int? page)
        {
            int pagesize = 9, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = db.table_product.Where(x => x.pro_fk_cat == id).OrderByDescending(x => x.pro_id).ToList();
            IPagedList<table_product> stu = list.ToPagedList(pageindex, pagesize);


            return View(stu);


        }
        public ActionResult ViewAd(int? id)
        {
            AdviewModel ad = new AdviewModel();
            table_product p = db.table_product.Where(x => x.pro_id == id).SingleOrDefault();
            ad.pro_id = p.pro_id;
            ad.pro_name = p.pro_name;
            ad.pro_image = p.pro_image;
            ad.pro_price = p.pro_price;
            ad.pro_description = p.pro_description;
            table_category cat = db.table_category.Where(x => x.cat_id == p.pro_fk_cat).SingleOrDefault();
            ad.cat_name = cat.cat_name;
            table_user u = db.table_user.Where(x => x.u_id == p.pro_fk_user).SingleOrDefault();
            ad.u_name = u.u_name;
            ad.u_image = u.u_image;
            ad.u_contact = u.u_contact;
            ad.pro_fk_user = u.u_id;




            return View(ad);
        }
        
        public ActionResult Signout()
        {
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteAd(int? id)
        {

            table_product p = db.table_product.Where(x => x.pro_id == id).SingleOrDefault();
            db.table_product.Remove(p);
            db.SaveChanges();
            return View("Index");
        }

    }
}