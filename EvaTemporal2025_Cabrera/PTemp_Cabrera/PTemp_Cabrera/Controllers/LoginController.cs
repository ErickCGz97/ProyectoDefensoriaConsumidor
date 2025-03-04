

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PTemp_Cabrera.Data;
using PTemp_Cabrera.Models;

public class LoginController : Controller{

    //Acceso a la base de datos
    private readonly DbtempCabreraContext dbtempCabreraContext;

    public LoginController(DbtempCabreraContext dbtempC)
    {
        dbtempCabreraContext = dbtempC;
    }

    [HttpGet]
    public IActionResult Login()
    {
        //Mostra la vista de la pagina Login
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(Login loginModel)
    {
        //Verificacion que los datos de ingreso sean validos 
        if(ModelState.IsValid)
        {
            //Encuentra empleado con el Usuario y Clave ingresados, verificacin adicional que el estado sea Activo(1)
            var empleado = await dbtempCabreraContext.CEmpleados.FirstOrDefaultAsync(empleado => empleado.Usuario == loginModel.Usuario && empleado.Clave == loginModel.Clave && empleado.Activo);
            
            if(empleado != null)
            { //Autenticacion basada en Claims/Reclamaciones
                var claims = new List<Claim>
                {   //ClaimTypes.Name, empleado.Usuario : permite guardar el nombre del usuario
                    new Claim(ClaimTypes.Name, empleado.Usuario),
                    new Claim("IdEmpleado", empleado.IdEmpleado.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                 
                 return RedirectToAction ("Ingresar", "Reclamo");
                //return RedirectToAction("Index", "Home");
            }
            else{ //Mensaje de error en caso los datos no coincidan 
                ViewBag.ErrorMessage = "Usuario o contraseña incorrectos";
            }
        }
        return View(loginModel);
    }

    //Metodo para cerrar sesion y redirigir a la pagina de Login
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Login");
    }

}

//Reclamaciones - Claims
/*
Son declaraciones sobre un usuario que pueden incluir información como su identidad, roles y otros atributos. Estas declaraciones se utilizan para determinar lo que un usuario puede hacer y acceder dentro de una aplicación.
*/
