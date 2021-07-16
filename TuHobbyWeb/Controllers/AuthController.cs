using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using TuHobbyWeb.Helpers;
using TuHobbyWeb.Models.Entities;
using TuHobbyWeb.Models.ViewModels;

namespace TuHobbyWeb.Controllers
{
    public class AuthController : Controller
    {
        private AplicationDbContext _db = new AplicationDbContext();
        // GET: Auth
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Verify(string token)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.UserToken == token);
            if (user == null)
            {
                TempData["ErrorMessage"] = "Imposible activar la cuenta, el token no ha sido encontrado";
                return RedirectToAction("Index", "Home");
            }

            user.VerifiedAt = DateTime.Now;
            _db.Entry(user).Property(x => x.VerifiedAt).IsModified = true;
            await _db.SaveChangesAsync();
            TempData["SuccessMessage"] = "El usuario ha sido activado correctamente";
            return RedirectToAction("Login", "Auth");
        }

        // Registro de Usuario
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(UserRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Debo generar el Registro de Usuario
                using (var transaction = _db.Database.BeginTransaction())
                {
                    try
                    {
                        User user = await _db.Users.FirstOrDefaultAsync(x => x.EmailAddress == model.EmailAddress);
                        if (user != null)
                        {
                            TempData["ErrorMessage"] = "Este Email ya ha sido registrado";
                            return View(model);
                        }

                        var role = await _db.Roles.FirstOrDefaultAsync(x => x.RoleName == StringHelper.ROLE_USER);

                        PasswordHelper.CreatePassword(model.Password, out byte[] passwordHash, out byte[] passwordSalt);

                        user = new User
                        {
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            EmailAddress = model.EmailAddress,
                            RoleId = role.RoleId,
                            PasswordHash = passwordHash,
                            PasswordSalt = passwordSalt,
                            CreatedAt = DateTime.Now,
                            UserToken = Guid.NewGuid().ToString().Replace("-", string.Empty)
                        };

                        // Guarda el nuevo ususario en la DB
                        _db.Users.Add(user);
                        await _db.SaveChangesAsync();
                        // todo: Enviar Email para verificar email ingresado
                        string urlPath = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Authority + "/Auth/Verify?token=" + user.UserToken;
                        string body = $"Para activar su cuenta acceda a la siguiente url: <a href='{urlPath}'> Click aquí, para activar su cuenta</a>";
                        if (EmailHelper.Send(model.EmailAddress, "Verifique su Email", body, out string message))
                        {
                            TempData["SuccessMessage"] = "El Usuario ha sido creado correctamente. Por favor, validar la cuenta, verificando su Email.";
                            transaction.Commit();
                            return RedirectToAction("Index", "Home");
                        }
                        TempData["ErrorMessage"] = "Ocurrió un Problema enviando el Correo de Validación.";
                        transaction.Rollback();
                        return View(model);
                    } catch (Exception e)
                    {
                        transaction.Rollback();
                        TempData["ErrorMessage"] = e.Message;
                        return View(model);
                    }
                    
                }
                    
            }

            return View(model);
        }

        // Login del Usuario
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            // todo: Inicio de sesion
            // return View();

            if (ModelState.IsValid)
            {
                var user = await _db.Users.FirstOrDefaultAsync(x => x.EmailAddress == model.Email && x.VerifiedAt != null);

                if (user == null)
                {
                    TempData["ErrorMessage"] = "El Usuario no ha sido encontrado";
                    return RedirectToAction("Login");
                }

                if (!PasswordHelper.CheckPassword(model.Password, user.PasswordHash, user.PasswordSalt))
                {
                    TempData["ErrorMessage"] = "La contraseña es incorrecta";
                    return RedirectToAction("Login");
                }

                await InitOwin(user);
                TempData["SuccessMessage"] = "Conectado correctamente";
            }
            return RedirectToAction("Index", "Home");
        }

        private async Task InitOwin (User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.EmailAddress), 
                new Claim(ClaimTypes.Email, user.EmailAddress),
                new Claim("FullName", user.FullName)
            };

            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
            //var role = _db.Roles.FirstOrDefault(x => x.RoleId == user.RoleId);
            var role = await _db.Roles.FindAsync(user.RoleId);
            if (role != null)
                identity.AddClaim(new Claim(ClaimTypes.Role, role.RoleName));

            GetAuthentication().SignIn(identity);
        }

        // Cerrar Sesión
        [HttpGet]
        public ActionResult Logout()
        {
            CloseOwin();
            TempData["SuccessMessage"] = "Sesión se ha cerrado correctamente";
            return RedirectToAction("Index", "Home");
        }

        private void CloseOwin()
        {
            GetAuthentication().SignOut();
        }

        private IAuthenticationManager GetAuthentication()
        {
            var context = Request.GetOwinContext();
            return context.Authentication;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();

            base.Dispose(disposing);
        }
    }
}