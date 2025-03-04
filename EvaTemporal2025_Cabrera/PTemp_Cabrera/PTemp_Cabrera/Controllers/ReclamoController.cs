using Microsoft.AspNetCore.Mvc;
using PTemp_Cabrera.Models;
using PTemp_Cabrera.Data;
using PTemp_Cabrera.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace PTemp_Cabrera.Controllers;

[Authorize]
public class ReclamoController : Controller
{
    private readonly DbtempCabreraContext dbtempCabreraContext;

    public ReclamoController(DbtempCabreraContext context)
    {
        dbtempCabreraContext = context;
    }

    [HttpGet]
    public IActionResult Ingresar()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Ingresar(ReclamoDTO reclamoDTO)
    {
        if(ModelState.IsValid)
        {
            var usuarioID = User.FindFirst("IdEmpleado")?.Value;
            if(usuarioID == null)
            {
                return Unauthorized();
            }
            var idEmpleado = int.Parse(usuarioID);

            //Verificar datos del consumidor y crear en caso no exista antes
            var consumidor = await dbtempCabreraContext.CConsumidors.FirstOrDefaultAsync(cr => cr.DuiConsumidor == reclamoDTO.DUIConsumidor);

            if(consumidor == null)
            {
                consumidor = new CConsumidor
                {
                    NombreConsumidor = reclamoDTO.NombreConsumidor,
                    ApellidoConsumidor = reclamoDTO.ApellidoConsumidor,
                    Direccion = reclamoDTO.DireccionConsumidor,
                    CorreoElectronico = reclamoDTO.CorreoElectronico,
                    DuiConsumidor = reclamoDTO.DUIConsumidor,
                    Activo = true
                };
                dbtempCabreraContext.CConsumidors.Add(consumidor);
                await dbtempCabreraContext.SaveChangesAsync();
            }

            //Ingreso de datos del reclamo
            var reclamo = new TReclamo
            {
                NombreProveedor = reclamoDTO.NombreProveedor,
                DireccionProveedor = reclamoDTO.DireccionProveedor,
                DetalleReclamo = reclamoDTO.DetalleReclamo,
                TelefonoProveedor = reclamoDTO.TelefonoProveedor,
                MontoReclamo = reclamoDTO.MontoReclamo,
                FechaIngreso = DateTime.Now,
                IdConsumidor = consumidor.IdConsumidor,
                IdEmpleado = idEmpleado, //Se debe asignar el id del usuario que ingreso al sistema
                IdEstado = 1, //Se establece Id Estado 1 por defecto = Pendiente de clasificar
                Activo = true
            };
            dbtempCabreraContext.TReclamos.Add(reclamo);
            await dbtempCabreraContext.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
        return View(reclamoDTO);
    }
}