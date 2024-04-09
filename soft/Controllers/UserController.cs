using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using soft.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using NuGet.Protocol.Plugins;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;

namespace soft.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        Uri baseAdress = new Uri("http://localhost:5249/api");
        private readonly HttpClient _httpClient;

        public UserController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAdress;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<User> users = new List<User>();
            try
            {
                //requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "Your token");
                //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                HttpResponseMessage res = _httpClient.GetAsync(_httpClient.BaseAddress + "/User").Result;
                if (res.IsSuccessStatusCode)
                {
                    string data = res.Content.ReadAsStringAsync().Result;
                    users = JsonConvert.DeserializeObject<List<User>>(data);
                    ViewBag.DataSource = users;
                }

            }
            catch (Exception ex)
            {

            }
            return View(users);
        }
        [HttpGet]
        public IActionResult AddOrEdit(int id = 0)
        {
            User u = new User();
            if (id == 0)
            {
                return View(new User());
            }
            else
            {
                HttpResponseMessage res = _httpClient.GetAsync(_httpClient.BaseAddress + "/User/" + id).Result;
                if (res.IsSuccessStatusCode)
                {
                    string data = res.Content.ReadAsStringAsync().Result;
                    u = JsonConvert.DeserializeObject<User>(data);

                }
            }
            return View(u);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit([Bind("Id, UserName, UserEmail, Role, Password, saltPassword, Token,  KeepLoginIn")] User usr)
        {
            if (ModelState.IsValid)
            {
                string data = JsonConvert.SerializeObject(usr);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                if (usr.Id == 0)
                {
                    HttpResponseMessage res = _httpClient.PostAsync(_httpClient.BaseAddress + "/User/Add", content).Result;
                    if (res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    HttpResponseMessage res = _httpClient.PutAsync(_httpClient.BaseAddress + "/User", content).Result;
                    if (res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }

                }

            }
            return RedirectToAction("Index");

        }
        
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {

            if (id > 0)
            {
                HttpResponseMessage response = _httpClient.DeleteAsync(_httpClient.BaseAddress + "/User/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");

                }
            }
            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if(claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Dashboard");  
            return View();
        }
        [HttpPost]
        [Route("/Login")] 
        public async Task<IActionResult>  Login(string UserEmail, string Password)
        {
            var u = new User();
           
                if(UserEmail != null) {
                    HttpResponseMessage res = _httpClient.GetAsync(_httpClient.BaseAddress + "/User/mail/"+UserEmail).Result;
                    if (res.IsSuccessStatusCode)
                    {
                        string data = res.Content.ReadAsStringAsync().Result;
                        u = JsonConvert.DeserializeObject<User>(data);
                        bool verifpass = verifHashPassword(Password, u.Password, u.SaltPassword);
                        if(verifpass)
                        {
                            SignInUser(u);
                            return RedirectToAction("Index", "Dashboard");
                        }
                        else
                        {
                            BadRequest();
                        }
                    }
                    else
                    {
                        BadRequest();
                    }
               
            }
            return View();
        }
        private bool verifHashPassword(string enterPass, string storePass, string storeSalt)
        {
            var salBytes = Convert.FromBase64String(storeSalt);
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(enterPass, salBytes, 1000) ;
            return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == storePass;

        }
        private bool ValidateLogin(string UserEmail, string password)
        {
            if (UserEmail == "dahirou@gmail.com" && password == "1234")
                return true;
            else
                return false;
        }
        private async Task SignInUser(User u)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Email, u.UserEmail),
                new Claim(ClaimTypes.Role, u.Role)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authentificationProperties = new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = true,
            };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authentificationProperties);
        }
    }
}
