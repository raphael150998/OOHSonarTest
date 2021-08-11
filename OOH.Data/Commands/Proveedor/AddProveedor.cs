using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOH.Data.Commands.Proveedor
{
    /// <summary>
    /// Este comando es para crear un nuevo proveedor
    /// # el campo codigo, NRC y NIT no pueden repetirse en la base
    /// # 
    /// </summary>
    public class AddProveedor : IRequest<Models.Proveedor>
    {
        /// <summary>
        /// Es el codigo que se maneja a nivel de NetSuite
        /// </summary>
        public string Codigo { get; set; }

        /// <summary>
        /// Nombre de la empresa o persona
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Regsitro del contribuyente (debe ser unico)
        /// </summary>
        public string NRC { get; set; }

        /// <summary>
        /// Identificador tributario (debe ser unico)
        /// </summary>
        public string NIT { get; set; }

        /// <summary>
        /// Rubro en que se maneja la empresa en caso de ser empresa (en caos de ser persona naturla debe enviarse vacio)
        /// </summary>
        public string Giro { get; set; }

        /// <summary>
        /// correo
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// telefono
        /// </summary>
        public string Telefono { get; set; }

        /// <summary>
        /// Celular
        /// </summary>
        public string Celular { get; set; }

        /// <summary>
        /// Determina si es o no una empresa
        /// </summary>
        public bool PersonaJuridica { get; set; }

        /// <summary>
        /// Categoria del proveedor
        /// </summary>
        public int CategoriaId { get; set; }
    }
}
