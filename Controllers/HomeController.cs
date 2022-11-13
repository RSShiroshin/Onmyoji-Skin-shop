using Microsoft.AspNetCore.Mvc;
using PRN_Project2.Models;
using System;
using System.Data;
using System.Reflection;
using System.Security.Cryptography;
using System.Xml.Linq;


namespace PRN_Project2.Controllers
{
    public class HomeController : Controller
    {
        static UserHe160324 userLogin;
        static int numberOrder = 0;
        static bool search = false;
        static string searchTxt = "";
        static bool sort = true; // tang dan
        PRN_ProjectContext context = new PRN_ProjectContext();
        DataProvider data = new DataProvider();
        public IActionResult Index()
        {
            ViewBag.userLogin = userLogin;
            if ( userLogin == null )
                ViewBag.display = "none";
            if(userLogin != null )
            {
                if (userLogin.Roll <= 1)
                    ViewBag.display = "inline-block";
                if (userLogin.Roll > 1)
                    ViewBag.display = "none";
            }
                
                                                                                                                                           
            List<ProductHe160324> listProduct = context.ProductHe160324s.ToList();
            
            List<ProductHe160324> listBestProduct = new List<ProductHe160324>();

            if (userLogin != null)
            {
                List<OrderHe160324> userOrders = context.OrderHe160324s.Where(x => x.UserName == userLogin.UserName && x.Done==false && x.StatusDb==true).ToList();
                numberOrder = userOrders.Count();
            }
            
            try
            {               
                
                String strSelect = "select top 4 * from Product_HE160324 order by sold asc";
                DataTable dt = data.executeQuery(strSelect);
                //MessageBox.Show(dt.Rows.ToString());
                if (dt.Rows.Count > 0)
                {
                    for(int i = 0; i < dt.Rows.Count; i++)
                    {
                        string ProductId = dt.Rows[i][0].ToString();
                        string ProductName = dt.Rows[i][1].ToString();
                        string ShikiName = dt.Rows[i][2].ToString();
                        double Price = (double)dt.Rows[i][3];
                        double PhonePrice = (double)dt.Rows[i][4];
                        string Img = dt.Rows[i][5].ToString();
                        string Description = dt.Rows[i][6].ToString();
                        int sold =(int) dt.Rows[i][7];
                        int Sale = (int)dt.Rows[i][8];
                        ProductHe160324 pro = new ProductHe160324(ProductId, ProductName, 
                            ShikiName, Price, PhonePrice, Img, Description, sold, Sale);
                        listBestProduct.Add(pro);
                    }
                }
            }
            catch (Exception ex)
            {
                
            }

            ViewBag.listProduct = listProduct;
            ViewBag.listBestProduct = listBestProduct;
            ViewBag.numberOrder = numberOrder;
            return View(userLogin);
        }

        public IActionResult LogOut()
        {
            userLogin = null;
            return RedirectToAction("Index");
        }

        public IActionResult SearchProID(string searchID)
        {
            if (searchID == null || searchID == "")
            {
                search = false;
                searchTxt = "";
                
            } else
            {
                ProductHe160324 p = context.ProductHe160324s.FirstOrDefault(x => x.ProductId == searchID);
                if (p != null)
                {
                    search = true;
                    searchTxt = searchID;
                }
                else
                {
                    searchTxt = "ko";
                    return RedirectToAction("LoadProduct");
                }
            }
            
            return RedirectToAction("LoadProduct");
        }

        public IActionResult getUserLogin(int userID, String userName, String password, bool gender, String address
            , String ingameID, String ingameName, String phone, String email, String facebook, int roll, bool status)
        {
            userLogin = new UserHe160324(userID, userName, password, gender, address, ingameID, ingameName, phone, email, facebook, roll, status);
            return RedirectToAction("Index");
        }

        public IActionResult LoadUserOrder()
        {
            List<OrderHe160324> listOrder = context.OrderHe160324s.Where(o => o.UserName == userLogin.UserName && o.StatusDb == true).OrderBy(o => o.OrderDate).OrderBy(o => o.Done).ToList();

            ViewBag.listOrder = listOrder;
            if (userLogin == null)
                ViewBag.display = "none";
            if (userLogin != null)
            {
                if (userLogin.Roll <= 1)
                    ViewBag.display = "inline-block";
                if (userLogin.Roll > 1)
                    ViewBag.display = "none";
            }
            return View(userLogin);
        }

        //======================== PRODUCT========================        

        public IActionResult LoadProduct()
        {
            List<ProductHe160324> listProduct = new List<ProductHe160324>();
            if (sort == true)
                listProduct = context.ProductHe160324s.OrderBy(p => p.ProductName).ToList();
            else
                listProduct = context.ProductHe160324s.OrderByDescending(p => p.ProductName).ToList();

            if (searchTxt != "")
            {
                listProduct = listProduct.Where(x => x.ProductId.Contains(searchTxt.ToUpper())).ToList();    
            } 

            if(searchTxt == "ko")
            {
                listProduct = new List<ProductHe160324>();
            }
            ViewBag.searchTxt = searchTxt;

            ViewBag.listProduct = listProduct;
            return View(userLogin);
        }
        public IActionResult SortProductName()
        {
            if (sort == true)
                sort = false;
            else
                sort = true;
            return RedirectToAction("LoadProduct");
        }


        public IActionResult InUpProduct(String productID, string productName, string shikiName,
            double price, double phonePrice, string img, string description, int sale)
        {
            ProductHe160324 pro = context.ProductHe160324s.FirstOrDefault(x => x.ProductId == productID);

            try
            {
                if (pro == null)
                {
                    pro = new ProductHe160324(productID.ToUpper(), productName, shikiName, price, phonePrice, img, description, 0, sale);
                    context.ProductHe160324s.Add(pro);
                    context.SaveChanges();
                }
                else
                {
                    pro.ProductName = productName;
                    pro.ShikiName = shikiName;
                    pro.Price = price;
                    pro.PhonePrice = phonePrice;
                    pro.Img = img;
                    pro.Description = description;
                    pro.Sale = sale;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                ViewBag.errorMsg = e.Message;
            }
            

            return RedirectToAction("LoadProduct");
        }

        public IActionResult DelProduct(string delID)
        {
            try
            {
                ProductHe160324 pro = context.ProductHe160324s.FirstOrDefault(x => x.ProductId == delID);
                if (pro != null)
                {
                    context.ProductHe160324s.Remove(pro);
                    context.SaveChanges();
                }    
            }
            catch (Exception e )
            {
                ViewBag.errorMsg = e.Message;
            }
            return RedirectToAction("LoadProduct");
        }

        //=============================== USER ====================================
        public IActionResult LoadUser()
        {
            List<UserHe160324> listUser = context.UserHe160324s.ToList();

            ViewBag.listUser = listUser;
            return View(userLogin);
        }

        public IActionResult UpdateUserRole(int userID, int userRoll)
        {
            UserHe160324 user = context.UserHe160324s.FirstOrDefault(x => x.UserId == userID);

            try
            {
                if (user != null)
                {
                    if(userRoll > 1)
                    {
                        user.Roll = 1;
                        context.SaveChanges();
                    } else
                    {
                        user.Roll = 3;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                ViewBag.errorMsg = e.Message;
            }

            return RedirectToAction("LoadUser");
        }

        public IActionResult UpdateUserStatus(int userID, bool userStatus)
        {
            UserHe160324 user = context.UserHe160324s.FirstOrDefault(x => x.UserId == userID);

            try
            {
                if (user != null)
                {
                    if (userStatus == true)
                    {
                        user.Status = false;
                        context.SaveChanges();
                    }
                    else
                    {
                        user.Status = true;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                ViewBag.errorMsg = e.Message;
            }
            return RedirectToAction("LoadUser");
        }

        public IActionResult DelUser(int delID)
        {
            try
            {
                UserHe160324 user = context.UserHe160324s.FirstOrDefault(x => x.UserId == delID);
                if (user != null)
                {
                    context.UserHe160324s.Remove(user);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                ViewBag.errorMsg = e.Message;
            }
            return RedirectToAction("LoadUser");
        }

        //============================= ORDER =================================
        public IActionResult LoadOrder()
        {
            List<OrderHe160324> listOrder = context.OrderHe160324s.Where(o => o.StatusDb == true).OrderBy(o => o.OrderDate).OrderBy(o => o.Done).ToList();

            ViewBag.listOrder = listOrder;
            return View(userLogin);
        }

        public IActionResult UpdateOrderDone(int orderID)
        {
            OrderHe160324 order = context.OrderHe160324s.FirstOrDefault(o => o.OrderId == orderID);
            try
            {
                if(order != null)
                {
                    order.Done = true;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                ViewBag.errorMsg = e.Message;
            }

            return RedirectToAction("LoadOrder");
        }

        public IActionResult CancleOrder(int delID)
        {
            try
            {
                OrderHe160324 o = context.OrderHe160324s.FirstOrDefault(x => x.OrderId == delID);
                if (o != null)
                {
                    o.StatusDb = false;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                ViewBag.errorMsg = e.Message;
            }
            return RedirectToAction("LoadUserOrder");
        }

        public IActionResult ProductOrder(string productID)
        {
            
            try
            {
                ProductHe160324 p = context.ProductHe160324s.FirstOrDefault(x => x.ProductId == productID);
                
                if (p != null)
                {
                    List<CommentHe160324> productComment = context.CommentHe160324s.Where(c => c.ProductId == productID).OrderByDescending(c => c.CommentDate).ToList();
                    ViewBag.productComment = productComment;
                    ViewBag.productOrder = p;   
                    return View(userLogin);
                }
            }
            catch (Exception e) 
            {
                ViewBag.errorMsg = e.Message;
            }

            //return RedirectToAction("Index");
            return View(userLogin);
        }

        public IActionResult AddOrder(string userName, string receiver, string productID, bool paymentMethod)
        {
            try
            {
                DateTime orderDate = DateTime.Now;
                ProductHe160324 p = context.ProductHe160324s.FirstOrDefault(x => x.ProductId == productID);
                double price = 0;
                if (paymentMethod)
                {
                    price = (double)p.Price;
                } else
                {
                    price = (double)p.PhonePrice;
                }
                string strInsert = "insert into Order_HE160324 values('"+userName+"','"+receiver+"','"+productID+"','"+paymentMethod+"','"+orderDate+"',"+price+","+1+","+0+")";
                data.executeNonQuery(strInsert);
                p.Sold++;
                context.SaveChanges();
                //CommentHe160324 newCmt = new CommentHe160324(productID, userName, newComment, now);
                //context.CommentHe160324s.Add(newCmt);
                //context.SaveChanges();

                return RedirectToAction("LoadUserOrder");
            }
            catch (Exception e)
            {
                ViewBag.errorMsg = e.Message;
                return RedirectToAction("Index");
            }

        }


        //========================= COMMENT ================================
        [HttpGet]
        public IActionResult postComment(string userName, string productID, string newComment)
        {
            try
            {
                DateTime now = DateTime.Now;
                string strInsert = "insert into Comment_HE160324 values('" + productID+"','"+userName+"','"+newComment+"','"+now+"')";
                data.executeNonQuery(strInsert);
                //CommentHe160324 newCmt = new CommentHe160324(productID, userName, newComment, now);
                //context.CommentHe160324s.Add(newCmt);
                //context.SaveChanges();

                return RedirectToAction("ProductOrder", "Home", new {productID=""+productID+""});
            }
            catch (Exception e)
            {
                ViewBag.errorMsg = e.Message;
                return RedirectToAction("ProductOrder", "Home", new { productID = "" + productID + "" });
            }
            
        }
    }
}
