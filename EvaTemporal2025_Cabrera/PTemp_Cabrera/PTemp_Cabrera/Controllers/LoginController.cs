

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
    public async Task<IActionResult> Login(Login login)
    {
        //Verificacion que los datos de ingreso sean validos 
        if(ModelState.IsValid)
        {
            //Encontra empleado con el Usuario y Clave ingresados, verificacin adicional que el estado sea Activo(1)
            var empleado = await dbtempCabreraContext.CEmpleados.FirstOrDefaultAsync(empleado => empleado.Usuario == login.Usuario && empleado.Clave == login.Clave && empleado.Activo);
            if(empleado != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else{ //Mensaje de error en caso los datos no coincidan 
                ModelState.AddModelError(string.Empty, "Usuario o Contraseña incorrectos, por favor verificar");
            }
        }
        return View(login);
    }

}