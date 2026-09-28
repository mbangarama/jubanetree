using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JubaneTree.DAL;
using JubaneTree.Models;
using System.Web.Script.Serialization;

namespace JubaneTree.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllMembers()
        {
            var _dataAccess = new DatabaseAccessLayer();

            var members = _dataAccess.GetAllMembers();

            //var members = GetMembers();
            var jsonSerieliser = Json(members);
            var json = new JavaScriptSerializer().Serialize(members);

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetMemberById(int Id)
        {
            var _dataAccess = new DatabaseAccessLayer();

            var member = _dataAccess.GetMemberById(Id);
           
            return PartialView("_MemberView", member);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveMember(Member member, HttpPostedFileBase upload)
        {

            if (!ModelState.IsValid)
            {
                return PartialView(
                 "~/Views/Home/_MemberView.cshtml",
                 member);
            }

            if (upload != null && upload.ContentLength > 0)
            {
                var imageUpload =
                    new ImageUpload
                    {
                        Width = 600
                    };

                var imageResult =
                    imageUpload.RenameUploadFile(upload);

                if (imageResult.Success)
                {
                    member.Image =
                        imageUpload.UploadPath +
                        imageResult.ImageName;
                }
                else
                {
                    ModelState.AddModelError(
                        "Image",
                        imageResult.ErrorMessage);
                }
            }

            var _dataAccess = new DatabaseAccessLayer();

            _dataAccess.SaveMember(member);

            return View("Index");
        }

        [HttpPost]
        public ActionResult CreateChild(Member member, HttpPostedFileBase upload)
        {
            var _dataAccess = new DatabaseAccessLayer();

            if (upload != null && upload.ContentLength > 0)
            {
                var imageUpload =
                    new ImageUpload
                    {
                        Width = 600
                    };

                var imageResult =
                    imageUpload.RenameUploadFile(upload);

                if (imageResult.Success)
                {
                    member.Image =
                        imageUpload.UploadPath +
                        imageResult.ImageName;
                }
                else
                {
                    ModelState.AddModelError(
                        "Image",
                        imageResult.ErrorMessage);
                }
            }

            _dataAccess.CreateChild(member);

            return View("Index");
        }

        [HttpGet]
        public ActionResult AddChild(int id, int code)
        {
            var _dataAccess = new DatabaseAccessLayer();

            var member = new Member();
            member.id = id;
            member.FamilyCode = code;

            return PartialView("AddChild", member);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public String GetData()
        {
            var returnJson = "";
            return returnJson;
        }

        public Members GetMembers()
        {
            Members members = new Members();

            members.Add(new Member()
            {
                id = 1,
                Title = "Mr",
                Name = "Khongo",
                Surname = "Jubane",

                //text = "Khongo Jubane",

                Telephone = "000",
                Mobile = "",
                Email = "",
                Address = "",
                Job = "",
                Company = "",
                Country = "",
                Image = "",
                IsAlive = false,

                nodes = new Members()
    {
        new Member()
        {
            id = 11,
            Title = "Mr",
            Name = "Amos",
            Surname = "Jubane",

            //text = "Amos Jubane",

            Telephone = "",
            Mobile = "",
            Email = "",
            Address = "",
            Job = "",
            Company = "",
            Country = "",
            Image = "",
            IsAlive = false,

            nodes = new Members()
            {
                new Member()
                {
                    id = 22,
                    Title = "Mrs",
                    Name = "Ennie",
                    Surname = "Jubane",

                    ///text = "Ennie Jubane",

                    Telephone = "",
                    Mobile = "",
                    Email = "",
                    Address = "",
                    Job = "",
                    Company = "",
                    Country = "",
                    Image = "",
                    IsAlive = false
                }
            }
        }
    }
            });

            return members;
        }
    }
}