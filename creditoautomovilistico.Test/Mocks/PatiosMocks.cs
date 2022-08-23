using creditoautomovilistico.API.Models;
using creditoautomovilistico.Infrastructure.Models;
using System.Collections.Generic;

namespace creditoautomovilistico.Test.Mocks
{
    public static class PatiosMocks
    {
        public static PatioResponseModel GetMockedPatio(string nombre)
        {
            return new PatioResponseModel()
            {
                Id = 1,
                Direccion = "Address",
                Telefono = "098575595",
                Nombre = nombre,
                NumeroPuntoVenta = 1
            };
        }

        public static PatioPayloadModel GetMockedPatioPayload(string nombre)
        {
            return new PatioPayloadModel()
            {
                Direccion = "Address",
                Telefono = "098575595",
                Nombre = nombre,
                NumeroPuntoVenta = 1
            };
        }

        public static Patio GetMockedPatioDbo(string nombre, string ejecutivo = "")
        {
            return new Patio()
            {
                Id = 1,
                Direccion = "Address",
                Telefono = "098575595",
                Nombre = nombre,
                NumeroPuntoVenta = 1,
                Ejecutivos = new List<Ejecutivo>() { new Ejecutivo() { Id = 1, Identificacion = ejecutivo} }
            };
        }
    }
}
