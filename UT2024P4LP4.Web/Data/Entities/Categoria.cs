﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UT2024P4LP4.Web.Data.Dtos;

namespace UT2024P4LP4.Web.Data.Entities;

[Table("Categorias")]
public class Categoria
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Nombre { get; set; } = null!;

    public static Categoria Create(string nombre) => new() { 
    Nombre = nombre
    };
    public bool Update(string nombre)
    {
        var S = false;
        if (Nombre != nombre) Nombre = nombre; S = true;
        return S;
    }
}
