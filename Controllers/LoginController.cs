using Microsoft.AspNetCore.Mvc;
using PRN_Project2;
using PRN_Project2.Models;

namespace PRN_Project.Controllers
{
    public class Login : Controller
    {

        static String errMess = "";
        DataProvider data = new DataProvider();
        public IActionResult LoginPage()
        {
            ViewBag.ErrMess = errMess;
            return View();
        }

        PRN_ProjectContext context = new PRN_ProjectContext();
        public IActionResult CheckLogin(string userName, string password)
        {
            var builder = WebApplication.CreateBuilder();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession();

            var user = context.UserHe160324s.FirstOrDefault(x => x.UserName == userName);
            if (user != null)
            {
                if (user.Password == password)
                {
                    ViewBag.userLogin = user;
                    return View();
                }
                else
                {
                    errMess = "Sai MK r";
                    return RedirectToAction("LoginPage");
                }
            }
            else
            {
                errMess = "Ngu, TK ko ton tai";
                return RedirectToAction("LoginPage");
            }
            return View();
        }

        public IActionResult Register()
        {
            ViewBag.ErrMess = errMess;
            return View();
        }

        public IActionResult CreateAccount(string userName, string password, string rePassword, bool gender, string address, string ingameID,
            string ingameName, string phone, string email, string facebook)
        {
            UserHe160324 user = context.UserHe160324s.FirstOrDefault(x => x.UserName == userName);
            if(user != null)
            {
                errMess = "UserName da ton tai";
                return RedirectToAction("Register");
            } else
            {
                if(password != rePassword)
                {
                    errMess = "RePassword khong trung`";
                    return RedirectToAction("Register");
                }

                string strInsert = "insert into User_HE160324 values('"+userName+ "','"+ password + "','"+gender+"'," +
                    "'"+address+"','"+ingameID+"','"+ingameName+"','"+phone+"','"+email+"','"+facebook+"',"+3+","+1+")";
                data.executeNonQuery(strInsert);
                errMess = "";
            }
            return RedirectToAction("LoginPage");
        }
    }
}
