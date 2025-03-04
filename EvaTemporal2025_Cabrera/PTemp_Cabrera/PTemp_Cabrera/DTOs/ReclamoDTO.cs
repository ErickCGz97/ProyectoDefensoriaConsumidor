namespace PTemp_Cabrera.DTOs;

using System.ComponentModel.DataAnnotations;
using PTemp_Cabrera.Models;

public class ReclamoDTO{

    //Datos del Reclamo
    [Display(Name = "Nombre del Proveedor:")]
    [Required(ErrorMessage = "El nombre del proveedor es obligatorio")]
    public string NombreProveedor { get; set; }

    [Display(Name = "Direccion del Proveedor:")]
    [Required(ErrorMessage = "La direccion del proveedor es obligatorio")]
    public string DireccionProveedor {get; set; }
    
    [Display(Name = "Detalle del Reclamo:")]
    [Required(ErrorMessage = "El detalle del reclamo es obligatorio")]
    public string DetalleReclamo {get; set; }

    [Display(Name = "Telefono del Proveedor:")]
    public string TelefonoProveedor {get;set; }
       
    [Display(Name = "Monto del Reclamo:")]
    public decimal MontoReclamo {get; set; }

    //Datos del consumidor que interpone el reclamo
    [Display(Name = "Nombre del Consumidor:")]
    [Required(ErrorMessage = "El nombre del consumidor es obligatorio")]
    public string NombreConsumidor {get; set; }

    [Display(Name = "Apellido del Consumidor:")]
    [Required(ErrorMessage = "El apellido del consumidor es obligatorio")]
    public string ApellidoConsumidor {get; set;}

    [Display(Name = "Dirección del Consumidor")]
    [Required(ErrorMessage = "La dirección del consumidor es obligatoria.")]
    public string DireccionConsumidor { get; set; }

    [Display(Name = "Correo electronico del Consumidor:")]
    public string CorreoElectronico {get; set;}

    [Display(Name = "DUI del Consumidor:")]
    [Required(ErrorMessage = "El DUI del consumidor es obligatorio")]
    [RegularExpression(@"\d{9}", ErrorMessage = "El DUI debe tener 9 digitos")]
    public string DUIConsumidor { get; set; }
}